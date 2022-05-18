using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private EnemyMovement enemyMovement;
    private EnemyStandShoot enemyStandShoot;

    public bool canShoot;
    public bool canRun;

    public bool isAlive { get; set; }
    public bool runStraight { get; set; }
    public bool shootStraight { get; set; }
    public bool shootUp { get; set; }
    public bool shootDiagonalUp { get; set; }
    public bool shootDiagonalDown { get; set; }
    public bool isJumping { get; set; }
    public bool isDucking { get; set; }
    public bool isFalling { get; set; }

    public float angle { get; set; }
    public int moveX { get; set; }

    private bool shotPrev;
    private float dieTimer;
    private float shootTime;
    private float shootTimer;
    private float distanceFromPlayer;

    private void Start()
    {
        player = GameObject.Find(Properties.PLAYER_GAME_OBJECT_NAME);
        enemyMovement = gameObject.GetComponent<EnemyMovement>();
        enemyStandShoot = gameObject.GetComponent<EnemyStandShoot>();

        isAlive = true;

        Vector3 direction = transform.position - player.transform.position;
        Vector3 xAxis = Vector3.right;
        angle = Vector3.SignedAngle(direction, xAxis, Vector3.forward);
        transform.eulerAngles = (angle >= -90 && angle <= 90) ? new Vector2(0, 180) : Vector2.zero;

        if (canRun && canShoot)
        {
            shootTime = Random.Range(4f, 7f);
            distanceFromPlayer = Random.Range(8f, 12f);
        }

        else if (canShoot)
            enemyStandShoot.enabled = true;

        gameObject.GetComponent<EnemyMovement>().enabled = canRun;
        moveX = (angle >= -90 && angle <= 90) ? -1 : 1;
    }

    private void FixedUpdate()
    {
        if (!isAlive)
        {
            dieTimer = dieTimer + Time.deltaTime;
            if (dieTimer >= 2)
                Destroy(gameObject);
        }

        else
        {
            Vector3 direction = transform.position - player.transform.position;
            Vector3 xAxis = Vector3.right;
            angle = Vector3.SignedAngle(direction, xAxis, Vector3.forward);

            if (canShoot && canRun)
            {
                if (!shotPrev)
                {
                    if (Mathf.Abs(transform.position.x - player.transform.position.x) <= distanceFromPlayer)
                    {
                        enemyMovement.stop = true;
                        enemyStandShoot.enabled = true;

                        // Resume running after some time
                        shootTimer = shootTimer + Time.fixedDeltaTime;
                        if (shootTimer > shootTime)
                        {
                            shootTimer = 0;

                            shootStraight = false;
                            shootUp = false;
                            shootDiagonalUp = false;
                            shootDiagonalDown = false;

                            enemyMovement.stop = false;
                            enemyStandShoot.enabled = false;
                            shotPrev = true;
                        }
                    }
                }

                else
                {
                    shootTimer = shootTimer + Time.fixedDeltaTime;
                    if(shootTimer > shootTime)
                    {
                        shootTimer = 0;
                        shotPrev = false;
                    }
                }
            }
        }
    }
}