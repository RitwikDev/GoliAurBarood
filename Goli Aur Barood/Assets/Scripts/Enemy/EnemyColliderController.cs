using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderController : MonoBehaviour
{
    public Collider2D baseCollider;
    public Collider2D standingTrigger;
    public Collider2D duckingTrigger;
    public Collider2D jumpingTrigger;

    private Enemy enemy;
    private EnemyMovement enemyMovement;

    private void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
        enemyMovement = transform.GetComponent<EnemyMovement>();
    }

    private void FixedUpdate()
    {
        if (enemy.isJumping && enemy.isAlive)
            Jump();

        else if (enemy.isDucking || !enemy.isAlive)
            Duck();

        else if (enemy.isFalling)
            Fall();

        else
            Stand();
    }

    private void Stand()
    {
        baseCollider.enabled = standingTrigger.enabled = true;
        duckingTrigger.enabled = jumpingTrigger.enabled = false;
    }

    private void Duck()
    {
        baseCollider.enabled = duckingTrigger.enabled = true;
        standingTrigger.enabled = jumpingTrigger.enabled = false;
    }

    private void Jump()
    {
        jumpingTrigger.enabled = true;
        baseCollider.enabled = standingTrigger.enabled = duckingTrigger.enabled = false;
    }

    private void Fall()
    {
        standingTrigger.enabled = true;
        baseCollider.enabled = duckingTrigger.enabled = jumpingTrigger.enabled = false;
    }
}