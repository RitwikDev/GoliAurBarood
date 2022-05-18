using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFallingGroundCheck : MonoBehaviour
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = FindObjectOfType<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Properties.GROUND_LAYER)
            enemy.isFalling = false;
    }
}