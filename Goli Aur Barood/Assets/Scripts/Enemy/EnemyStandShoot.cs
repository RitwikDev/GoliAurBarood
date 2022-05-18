using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStandShoot : MonoBehaviour
{
    private Enemy enemy;
    private EnemyFire enemyFire;
    private float initialFireWaitTime;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.GetComponent<Enemy>();
        enemyFire = transform.GetComponent<EnemyFire>();
        initialFireWaitTime = Random.Range(1, 3);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemy.runStraight || !enemy.isAlive)
            return;

        LookAt(enemy.angle);

        if (timer >= initialFireWaitTime)
            enemyFire.Fire();

        else
            timer = timer + Time.fixedDeltaTime;
    }

    private void LookAt(float angle)
    {
        // Enemy should face left
        if(angle <= 90 && angle >= -90)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);

            // Look straight
            if (angle <= 22.5 && angle >= -22.5)
            {
                enemy.shootStraight = true;

                enemy.shootUp = false;
                enemy.shootDiagonalUp = false;
                enemy.shootDiagonalDown = false;
            }

            else if(angle > 22.5 && angle <= 67.5)
            {
                enemy.shootDiagonalUp = true;

                enemy.shootUp = false;
                enemy.shootStraight = false;
                enemy.shootDiagonalDown = false;
            }

            else if(angle > 67.5)
            {
                enemy.shootUp = true;

                enemy.shootStraight = false;
                enemy.shootDiagonalUp = false;
                enemy.shootDiagonalDown = false;
            }

            else if(angle < -22.5 && angle >= -67.5)
            {
                enemy.shootDiagonalDown = true;

                enemy.shootUp = false;
                enemy.shootDiagonalUp = false;
                enemy.shootStraight = false;
            }

            else if(angle < -67.5)
            {
                // Shoot down
            }
        }

        // Enemy should face right
        else
        {
            transform.eulerAngles = Vector3.zero;

            // Look straight
            if (angle <= -157.5 || angle >= 157.5)
            {
                enemy.shootStraight = true;

                enemy.shootUp = false;
                enemy.shootDiagonalUp = false;
                enemy.shootDiagonalDown = false;
            }

            else if (angle > 112.5 && angle <= 157.5)
            {
                enemy.shootDiagonalUp = true;

                enemy.shootUp = false;
                enemy.shootStraight = false;
                enemy.shootDiagonalDown = false;
            }

            else if (angle > 90 && angle <= 112.5)
            {
                enemy.shootUp = true;

                enemy.shootStraight = false;
                enemy.shootDiagonalUp = false;
                enemy.shootDiagonalDown = false;
            }

            else if (angle < -112.5 && angle >= -157.5)
            {
                enemy.shootDiagonalDown = true;

                enemy.shootUp = false;
                enemy.shootDiagonalUp = false;
                enemy.shootStraight = false;
            }

            else if (angle > -112.5)
            {
                // Shoot down
            }
        }
    }
}