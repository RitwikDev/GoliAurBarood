using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnLocation : MonoBehaviour
{
    private Spawn spawnLeft;
    private Spawn spawnRight;

    private void Awake()
    {
        spawnLeft = GameObject.Find(Properties.ENEMY_SPAWN_LEFT_NAME).GetComponent<Spawn>();
        spawnRight = GameObject.Find(Properties.ENEMY_SPAWN_RIGHT_NAME).GetComponent<Spawn>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent != null && collision.transform.parent.name == Properties.PLAYER_GAME_OBJECT_NAME)
        {
            if (Random.Range(1, 3) == 1)
                spawnLeft.canSpawn = true;

            if (Random.Range(1, 3) == 1)
                spawnRight.canSpawn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent != null && collision.transform.parent.name == Properties.PLAYER_GAME_OBJECT_NAME)
            spawnLeft.canSpawn = spawnRight.canSpawn = false;
    }
}