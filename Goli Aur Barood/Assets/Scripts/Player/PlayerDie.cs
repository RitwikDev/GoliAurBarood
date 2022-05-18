using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    private const float DECREASING_FACTOR = 3f;   // Multiplication factor for decreasing the momentum while in air
    private Rigidbody2D rigidbody2DPlayer;
    private Player player;

    // Start is called before the first frame update
    void Awake()
    {
        player = gameObject.GetComponentInParent<Player>();
        rigidbody2DPlayer = gameObject.GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!player.isAlive)
            PlayerDieMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.isAlive && collision.tag == Properties.ENEMY_TAG && collision.tag != Properties.ENEMY_FALL_GROUND_CHECK)
        {
            player.isAlive = false;
            gameObject.layer = Properties.DEAD_LAYER;

            Vector2 force = new Vector2(0, 5);
            force.x = (transform.position.x > collision.transform.position.x) ? 3 : -3;
            rigidbody2DPlayer.velocity = Vector2.zero;
            rigidbody2DPlayer.AddForce(force, ForceMode2D.Impulse);
        }
    }

    private void PlayerDieMovement()
    {
        if (rigidbody2DPlayer.velocity.x > 0)
            rigidbody2DPlayer.velocity = new Vector2(rigidbody2DPlayer.velocity.x - Time.deltaTime * DECREASING_FACTOR, rigidbody2DPlayer.velocity.y);

        else if (rigidbody2DPlayer.velocity.x < 0)
            rigidbody2DPlayer.velocity = new Vector2(rigidbody2DPlayer.velocity.x + Time.deltaTime * DECREASING_FACTOR, rigidbody2DPlayer.velocity.y);
    }
}