using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletInstance;
    private GameObject currentBullet;
    private Enemy enemy;

    public float bulletSpeed = 4f;
    public bool isPressingFireButton { get; private set; }
    public bool canFire { get; private set; }
    public bool recoil { get; private set; }
    public bool fired { get; private set; }

    private const float INTER_FIRE_TIME = 0.3f;
    private const float FIRE_LONG_PAUSE_TIME = 3f;
    private float timer;
    private float pauseTimer;
    private int fireCount;
    private int bulletLimit = 3;
    public static int bulletCount;

    private void Start()
    {
        timer = 0;
        canFire = true;
        currentBullet = bulletInstance;
        enemy = gameObject.GetComponent<Enemy>();
    }

    private void FixedUpdate()
    {
        if (!enemy.isAlive)
            return;

        timer = timer + Time.fixedDeltaTime;

        if(fired && timer >= INTER_FIRE_TIME)
        {
            recoil = false;
            fired = false;
        }

        if (fireCount >= bulletLimit)
        {
            canFire = false;
            pauseTimer = pauseTimer + Time.fixedDeltaTime;
            if(pauseTimer >= FIRE_LONG_PAUSE_TIME)
            {
                pauseTimer = 0;
                fireCount = 0;
                canFire = true;
            }
        }
    }

    public void Fire()
    {
        if (canFire && timer >= INTER_FIRE_TIME)
        {
            if (bulletCount >= bulletLimit)
                return;

            fireCount = fireCount + 1;
            bulletCount = bulletCount + 1;
            timer = 0;
            isPressingFireButton = true;
            recoil = true;
            fired = true;

            bulletInstance = Instantiate(currentBullet, firePoint.transform.position, transform.rotation);
            Vector2 bulletDirection = Vector2.zero;

            // If the enemy is facing right
            if(transform.eulerAngles.y == 0)
            {
                if (enemy.shootStraight)
                    bulletDirection = new Vector2(bulletSpeed, 0);

                else if (enemy.shootDiagonalUp)
                    bulletDirection = new Vector2(bulletSpeed, bulletSpeed);

                else if(enemy.shootDiagonalDown)
                    bulletDirection = new Vector2(bulletSpeed, -bulletSpeed);
            }

            else
            {
                if (enemy.shootStraight)
                    bulletDirection = new Vector2(-bulletSpeed, 0);

                else if (enemy.shootDiagonalUp)
                    bulletDirection = new Vector2(-bulletSpeed, bulletSpeed);

                else if(enemy.shootDiagonalDown)
                    bulletDirection = new Vector2(-bulletSpeed, -bulletSpeed);
            }

            if (enemy.shootUp)
                bulletDirection = new Vector2(0, bulletSpeed);

            bulletInstance.tag = Properties.ENEMY_TAG;
            bulletInstance.layer = Properties.ENEMY_LAYER;
            bulletInstance.GetComponent<SpriteRenderer>().sortingOrder = Properties.PLAYER_BULLET_SORTING_ORDER;
            bulletInstance.GetComponent<Rigidbody2D>().AddForce(bulletDirection, ForceMode2D.Impulse);
            bulletInstance.AddComponent<EnemyBulletDestroy>();
        }
    }
}