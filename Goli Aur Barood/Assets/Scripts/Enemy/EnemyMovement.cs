using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private const float JUMP_FACTOR = 12f;       // Multiplication factor for jump action
    private const float MOVEMENT_SPEED = 5f;    // Multiplication factor for movement

    private Rigidbody2D rigidbody2DEnemy;  // Enemy's RigidBody
    private EnemyGroundCheck enemyGroundCheck;
    private Enemy enemy;

    public bool stop;
    
    private void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
        enemyGroundCheck = transform.Find(Properties.GROUND_CHECK_NAME).gameObject.GetComponent<EnemyGroundCheck>();
        rigidbody2DEnemy = GetComponent<Rigidbody2D>();

        stop = false;
    }

    private void FixedUpdate()
    {
        if (enemy.isAlive)
        {
            if (enemyGroundCheck.isGrounded)
            {
                enemy.isJumping = enemy.isFalling = false;
                if (!stop)
                    EnemyMoveStraight();
                else
                    EnemyStop();
            }

            else if (!enemy.isJumping)
                enemy.isFalling = true;
        }

        UpdateGravity();
    }

    // This function updates the value of gravity based on the elevation of the enemy
    private void UpdateGravity()
    {
        // Increasing the gravity when the enemy starts to descend
        if (rigidbody2DEnemy.velocity.y < 0.1)
            rigidbody2DEnemy.gravityScale = 3f;
        else
            rigidbody2DEnemy.gravityScale = 2f;    // Else, restoring the gravity to its default value of 1
    }

    // This function moves the enemy. It is also responsible for providing momentum to the enemy.
    private void EnemyMoveStraight()
    {
        enemy.runStraight = true;
        rigidbody2DEnemy.velocity = new Vector2(MOVEMENT_SPEED * enemy.moveX, rigidbody2DEnemy.velocity.y);
        transform.eulerAngles = (enemy.moveX == -1) ? new Vector2(0, 180) : Vector2.zero;
    }

    private void EnemyStop()
    {
        enemy.runStraight = false;
        rigidbody2DEnemy.velocity = new Vector2(0, rigidbody2DEnemy.velocity.y);
    }

    public void EnemyJump()
    {
        // If the jump button is pressed and the player is on the ground, then jump
        if (enemyGroundCheck.isGrounded)
        {
            // If the enemy is not ducking
            rigidbody2DEnemy.AddForce(Vector2.up * JUMP_FACTOR, ForceMode2D.Impulse);   // Adding upward force to jump
            enemyGroundCheck.isGrounded = false;
            enemy.isJumping = true;
            enemy.runStraight = false;
        }
    }
}