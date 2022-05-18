using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject gameObjectEnemy;

    public int count;
    private int maxEnemyCount;
    private float timer;

    public bool canSpawn { get; set; }

    private void Awake()
    {
        maxEnemyCount = Random.Range(1, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn && count < maxEnemyCount)
        {
            if (timer >= 2)
            {
                GameObject enemyGameObject = Instantiate(gameObjectEnemy);
                enemyGameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0) ;
                Enemy enemy = enemyGameObject.GetComponent<Enemy>();

                enemy.canRun = true;
                enemyGameObject.GetComponent<EnemyMovement>().enabled = true;
                enemy.moveX = (gameObject.name == Properties.ENEMY_SPAWN_LEFT_NAME) ? 1 : -1;

                if (Random.Range(1, 3) > 1.5)
                    enemy.canShoot = true;

                count = count + 1;
                timer = 0;
            }

            else
                timer = timer + Time.deltaTime;
        }

        else
            maxEnemyCount = Random.Range(1, 4);
    }
}