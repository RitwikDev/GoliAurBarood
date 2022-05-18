using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float RESPAWN_WAIT_TIME = 2;
    private const float RESPAWN_INVINCIBILITY_TIME = 4;
    private const float NORMAL_INVINCIBILITY_TIME = 10;

    public enum GunType
    {
        Default, Machine, Super
    }

    private GameObject playerRespawn;
    private GameObject invincibilityBarrier;
    private PlayerMovement playerMovement;
    private ShowLife showLife;

    public bool isAlive { get; set; }
    public bool isInvincible { get; set; }
    public bool isRespawnInvincible { get; private set; }
    public GunType currentGun { get; set; }

    private int lifeRemaining;
    private bool invincibilitySet;
    private float respawnTimer;
    private float invincibilityTimer;
    private float respawnInvincibilityTimer;

    private void Awake()
    {
        playerRespawn = GameObject.Find(Properties.PLAYER_RESPAWN_GAME_OBJECT);
        invincibilityBarrier = transform.Find(Properties.PLAYER_INVINCIBILITY_BARRIER).gameObject;
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        showLife = GameObject.Find(Properties.UI_LIFE_GAME_OBJECT).GetComponent<ShowLife>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        isInvincible = false;
        lifeRemaining = 2;
        showLife.UpdateLifeCount(lifeRemaining);
    }

    private void Update()
    {
        if(!isAlive)
        {
            respawnTimer = respawnTimer + Time.deltaTime;

            if (respawnTimer >= RESPAWN_WAIT_TIME)
            {
                respawnTimer = 0;
                if (lifeRemaining > 0)
                {
                    lifeRemaining = lifeRemaining - 1;
                    PlayerRespawn();
                }
            }
        }

        else
        {
            if (isInvincible)
            {
                if (!invincibilitySet)
                    Invincible();

                invincibilityTimer = invincibilityTimer + Time.deltaTime;
                if (invincibilityTimer >= NORMAL_INVINCIBILITY_TIME)
                {
                    invincibilityTimer = 0;
                    NotInvincible();
                }
            }

            else
                NotInvincible();

            if (isRespawnInvincible)
            {
                respawnInvincibilityTimer = respawnInvincibilityTimer + Time.deltaTime;
                if (respawnInvincibilityTimer >= RESPAWN_INVINCIBILITY_TIME)
                {
                    respawnInvincibilityTimer = 0;
                    NotRespawnInvincible();
                }
            }

            else
                NotRespawnInvincible();
        }
    }

    private void PlayerRespawn()
    {
        gameObject.SetActive(false);

        transform.position = playerRespawn.transform.position;
        isAlive = true;
        playerMovement.isJumping = true;
        isRespawnInvincible = true;
        respawnInvincibilityTimer = 0;
        RemovePowerUps();
        showLife.UpdateLifeCount(lifeRemaining);

        gameObject.SetActive(true);
    }

    private void Invincible()
    {
        transform.Find(Properties.PLAYER_COLLIDERS_GAME_OBJECT).gameObject.layer = Properties.DEAD_LAYER;
        invincibilityBarrier.GetComponent<Collider2D>().enabled = true;
        invincibilitySet = true;
    }

    private void NotInvincible()
    {
        if(!isRespawnInvincible)
            transform.Find(Properties.PLAYER_COLLIDERS_GAME_OBJECT).gameObject.layer = Properties.PLAYER_LAYER;
        invincibilityBarrier.GetComponent<Collider2D>().enabled = false;
        invincibilitySet = false;
        isInvincible = false;
    }

    private void NotRespawnInvincible()
    {
        if(!isInvincible)
            transform.Find(Properties.PLAYER_COLLIDERS_GAME_OBJECT).gameObject.layer = Properties.PLAYER_LAYER;
        isRespawnInvincible = false;
    }

    private void RemovePowerUps()
    {
        isInvincible = false;
        currentGun = GunType.Default;
    }
}