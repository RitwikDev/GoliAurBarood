using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    private Enemy enemy;
    private GameObject enemyGameObject;
    private EnemyMovement enemyMovement;

    private void Awake()
    {
        enemyGameObject = transform.parent.gameObject;
        enemyMovement = enemyGameObject.GetComponent<EnemyMovement>();
        enemy = enemyGameObject.GetComponent<Enemy>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!enemy.isAlive)
            return;

        // Jump randomly
        if (Random.Range(1, 3) < 1.5)
            return;

        if (collision.gameObject.layer == Properties.GROUND_LAYER)
            enemyMovement.EnemyJump();
    }
}