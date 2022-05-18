using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    private const float DECREASING_FACTOR = 3f;   // Multiplication factor for decreasing the momentum while in air

    private GameObject player;
    private Enemy enemy;
    private Spawn spawn;
    private Rigidbody2D rigidbody2DEnemy;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find(Properties.PLAYER_GAME_OBJECT_NAME);
        enemy = gameObject.GetComponent<Enemy>();
        spawn = FindObjectOfType<Spawn>();
        rigidbody2DEnemy = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!enemy.isAlive)
            EnemyDieMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemy.isAlive && (collision.tag == Properties.PLAYER_BULLET_TAG || collision.tag == Properties.PLAYER_INVINCIBILITY_BARRIER_TAG))
        {
            if (collision.tag == Properties.PLAYER_BULLET_TAG)
                collision.GetComponent<PlayerBulletDestroy>().DestroyPlayerBullet();

            enemy.isAlive = false;
            MoveToLayer(transform, Properties.DEAD_LAYER);

            if(spawn != null)
                spawn.count = spawn.count - 1;

            Vector2 force = new Vector2(0, 5);
            force.x = (transform.position.x > player.transform.position.x) ? 3 : -3;
            rigidbody2DEnemy.velocity = Vector2.zero;
            rigidbody2DEnemy.AddForce(force, ForceMode2D.Impulse);
        }
    }

    private void EnemyDieMovement()
    {
        if (rigidbody2DEnemy.velocity.x > 0)
            rigidbody2DEnemy.velocity = new Vector2(rigidbody2DEnemy.velocity.x - Time.deltaTime * DECREASING_FACTOR, rigidbody2DEnemy.velocity.y);

        else if (rigidbody2DEnemy.velocity.x < 0)
            rigidbody2DEnemy.velocity = new Vector2(rigidbody2DEnemy.velocity.x + Time.deltaTime * DECREASING_FACTOR, rigidbody2DEnemy.velocity.y);
    }

    private void MoveToLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            MoveToLayer(child, layer);
    }
}