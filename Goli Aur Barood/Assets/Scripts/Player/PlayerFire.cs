using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    private const int DEFAULT_BULLET_LIMIT = 5;
    private const int MACHINE_GUN_BULLET_LIMIT = 8;
    private const int SUPER_BULLET_LIMIT = 10;
    private const float DEFAULT_INTER_FIRE_TIME = 0.1f;
    private const float MACHINE_GUN_INTER_FIRE_TIME = 0.2f;
    private const float RECOIL_TIME = 0.1f;

    public Transform firePoint;
    public GameObject bullet;

    private PlayerInputActions playerInputActions;
    private InputAction inputActionMovement;
    private Player player;

    public bool isPressingFireButton { get; private set; }
    public bool canFire { get; private set; }
    public bool recoil { get; private set; }
    public bool fired { get; private set; }
    public static int bulletCount;

    private float bulletSpeed = 15f;
    private float fireTimer;
    private float recoilTimer;
    private float interFireTime;
    private int bulletLimit;
    private Player.GunType currentGun;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        player = gameObject.GetComponent<Player>();
    }

    private void Start()
    {
        fireTimer = 0;
        canFire = true;
        interFireTime = DEFAULT_INTER_FIRE_TIME;
        bulletLimit = DEFAULT_BULLET_LIMIT;
        currentGun = Player.GunType.Default;
    }

    private void OnEnable()
    {
        inputActionMovement = playerInputActions.Player.MovementDirections;
        inputActionMovement.Enable();
    }

    private void OnDisable()
    {
        inputActionMovement.Disable();
    }

    private void Update()
    {
        fireTimer = fireTimer + Time.deltaTime;

        if(currentGun != player.currentGun)
        {
            currentGun = player.currentGun;
            bulletCount = 0;
        }

        bulletLimit = (player.currentGun == Player.GunType.Default) ? DEFAULT_BULLET_LIMIT : ((player.currentGun == Player.GunType.Machine) ? MACHINE_GUN_BULLET_LIMIT : SUPER_BULLET_LIMIT);
        interFireTime = (player.currentGun == Player.GunType.Machine) ? MACHINE_GUN_INTER_FIRE_TIME : DEFAULT_INTER_FIRE_TIME;

        canFire = (bulletCount < bulletLimit);

        if(recoil)
        {
            recoilTimer = recoilTimer + Time.deltaTime;
            if(recoilTimer >= RECOIL_TIME)
            {
                recoilTimer = 0;
                recoil = false;
            }
        }

        if (isPressingFireButton && player.currentGun == Player.GunType.Machine && fireTimer >= interFireTime)
            FireNormal();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!player.isAlive)
            return;

        if(context.started && fireTimer >= interFireTime)
        {
            if (player.currentGun != Player.GunType.Super)
                FireNormal();

            else
                FireSuper();
        }

        if (context.canceled)
            isPressingFireButton = false;
    }

    private void FireNormal()
    {
        if (!player.isAlive)
            return;

        if (bulletCount >= bulletLimit)
            return;

        bulletCount = bulletCount + 1;
        fireTimer = 0;
        isPressingFireButton = true;
        recoil = true;
        fired = true;

        Vector2 playerDirection = inputActionMovement.ReadValue<Vector2>();
        GameObject bulletInstance = Instantiate(bullet, firePoint.transform.position, transform.rotation);
        Vector2 bulletDirection;

        // If the player is not pressing any button
        if (playerDirection == Vector2.zero)
        {
            // If the player is facing right
            if (transform.eulerAngles.y == 0)
                bulletDirection = new Vector2(bulletSpeed, 0f);

            else
                bulletDirection = new Vector2(-bulletSpeed, 0f);
        }

        else
        {
            // If the player is pressing just the down button
            if (playerDirection.x == 0 && playerDirection.y != 0)
            {
                // If the player is pressing the up button
                if (playerDirection.y == 1)
                    bulletDirection = new Vector2(0f, bulletSpeed);

                // If the player is jumping and pressing the down button, then the bullet should travel vertically down
                else if (System.Math.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) > 0.05)
                    bulletDirection = new Vector2(0f, -bulletSpeed);

                else
                {
                    // If the player is facing right
                    if (transform.eulerAngles.y == 0)
                        bulletDirection = new Vector2(bulletSpeed, 0f);

                    else
                        bulletDirection = new Vector2(-bulletSpeed, 0f);
                }
            }

            else
                bulletDirection = new Vector2(playerDirection.x * bulletSpeed, playerDirection.y * bulletSpeed);
        }

        bulletInstance.layer = Properties.PLAYER_LAYER;
        bulletInstance.tag = Properties.PLAYER_BULLET_TAG;
        bulletInstance.GetComponent<SpriteRenderer>().sortingOrder = Properties.PLAYER_BULLET_SORTING_ORDER;
        bulletInstance.GetComponent<Rigidbody2D>().AddForce(bulletDirection, ForceMode2D.Impulse);
        bulletInstance.AddComponent<PlayerBulletDestroy>();
    }

    private void FireSuper()
    {
        if (!player.isAlive)
            return;

        if (bulletCount >= bulletLimit)
            return;

        fireTimer = 0;
        isPressingFireButton = true;
        recoil = true;
        fired = true;

        Vector2 playerDirection = inputActionMovement.ReadValue<Vector2>();

        int bulletInstanceCount = bulletLimit - bulletCount;
        bulletInstanceCount = (bulletInstanceCount > 5) ? bulletInstanceCount - 5 : bulletInstanceCount;    // max(bulletInstanceCount) = 5
        bulletCount = bulletCount + bulletInstanceCount;

        GameObject[] bulletInstance = new GameObject[bulletInstanceCount];
        for(int i=0; i<bulletInstance.Length; i=i+1)
            bulletInstance[i] = Instantiate(bullet, firePoint.transform.position, transform.rotation);

        Vector2[] bulletDirection = new Vector2[bulletInstanceCount];

        // If the player is not pressing any button
        if (playerDirection == Vector2.zero)
        {
            // If the player is facing right
            if (transform.eulerAngles.y == 0)
            {
                for (int i = 0; i < bulletInstance.Length; i = i + 1)
                {
                    if (i == 0)
                        bulletDirection[0] = new Vector2(bulletSpeed, 0);
                    else if (i == 1)
                        bulletDirection[1] = new Vector2(bulletSpeed, bulletSpeed / 2);
                    else if (i == 2)
                        bulletDirection[2] = new Vector2(bulletSpeed, bulletSpeed);
                    else if (i == 3)
                        bulletDirection[3] = new Vector2(bulletSpeed, -bulletSpeed / 2);
                    else if (i == 4)
                        bulletDirection[4] = new Vector2(bulletSpeed, -bulletSpeed);
                }
            }

            else
            {
                for (int i = 0; i < bulletInstance.Length; i = i + 1)
                {
                    if (i == 0)
                        bulletDirection[0] = new Vector2(-bulletSpeed, 0);
                    else if (i == 1)
                        bulletDirection[1] = new Vector2(-bulletSpeed, bulletSpeed / 2);
                    else if (i == 2)
                        bulletDirection[2] = new Vector2(-bulletSpeed, bulletSpeed);
                    else if (i == 3)
                        bulletDirection[3] = new Vector2(-bulletSpeed, -bulletSpeed / 2);
                    else if (i == 4)
                        bulletDirection[4] = new Vector2(-bulletSpeed, -bulletSpeed);
                }
            }
        }

        else
        {
            // If the player is pressing just the down button
            if (playerDirection.x == 0 && playerDirection.y != 0)
            {
                // If the player is pressing the up button
                if (playerDirection.y == 1)
                {
                    for (int i = 0; i < bulletInstance.Length; i = i + 1)
                    {
                        if(i==0)
                            bulletDirection[0] = new Vector2(0, bulletSpeed);
                        else if(i==1)
                            bulletDirection[1] = new Vector2(bulletSpeed / 2, bulletSpeed);
                        else if(i==2)
                            bulletDirection[2] = new Vector2(bulletSpeed, bulletSpeed);
                        else if(i==3)
                            bulletDirection[3] = new Vector2(-bulletSpeed / 2, bulletSpeed);
                        else if(i==4)
                            bulletDirection[4] = new Vector2(-bulletSpeed, bulletSpeed);
                    }
                }

                // If the player is jumping and pressing the down button, then the bullet should travel vertically down
                else if (System.Math.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) > 0.05)
                {
                    for (int i = 0; i < bulletInstance.Length; i = i + 1)
                    {
                        if(i==0)
                            bulletDirection[0] = new Vector2(0, -bulletSpeed);
                        else if(i==1)
                            bulletDirection[1] = new Vector2(bulletSpeed / 2, -bulletSpeed);
                        else if(i==2)
                            bulletDirection[2] = new Vector2(bulletSpeed, -bulletSpeed);
                        else if(i==3)
                            bulletDirection[3] = new Vector2(-bulletSpeed / 2, -bulletSpeed);
                        else if(i==4)
                            bulletDirection[4] = new Vector2(-bulletSpeed, -bulletSpeed);
                    }
                }

                else
                {
                    // If the player is facing right
                    if (transform.eulerAngles.y == 0)
                    {
                        for (int i = 0; i < bulletInstance.Length; i = i + 1)
                        {
                            if(i==0)
                                bulletDirection[0] = new Vector2(bulletSpeed, 0);
                            else if(i==1)
                                bulletDirection[1] = new Vector2(bulletSpeed, bulletSpeed / 2);
                            else if(i==2)
                                bulletDirection[2] = new Vector2(bulletSpeed, bulletSpeed);
                            else if(i==3)
                                bulletDirection[3] = new Vector2(bulletSpeed, -bulletSpeed / 2);
                            else if(i==4)
                                bulletDirection[4] = new Vector2(bulletSpeed, -bulletSpeed);
                        }
                    }

                    else
                    {
                        for (int i = 0; i < bulletInstance.Length; i = i + 1)
                        {
                            if (i == 0)
                                bulletDirection[0] = new Vector2(-bulletSpeed, 0);
                            else if (i == 1)
                                bulletDirection[1] = new Vector2(-bulletSpeed, bulletSpeed / 2);
                            else if (i == 2)
                                bulletDirection[2] = new Vector2(-bulletSpeed, bulletSpeed);
                            else if (i == 3)
                                bulletDirection[3] = new Vector2(-bulletSpeed, -bulletSpeed / 2);
                            else if (i == 4)
                                bulletDirection[4] = new Vector2(-bulletSpeed, -bulletSpeed);
                        }
                    }
                }
            }

            else if(playerDirection.x !=0 && playerDirection.y == 0)
            {
                for (int i = 0; i < bulletInstance.Length; i = i + 1)
                {
                    if (i == 0)
                        bulletDirection[0] = new Vector2(playerDirection.x * bulletSpeed, 0);
                    else if (i == 1)
                        bulletDirection[1] = new Vector2(playerDirection.x * bulletSpeed, bulletSpeed / 2);
                    else if (i == 2)
                        bulletDirection[2] = new Vector2(playerDirection.x * bulletSpeed, bulletSpeed);
                    else if (i == 3)
                        bulletDirection[3] = new Vector2(playerDirection.x * bulletSpeed, -bulletSpeed / 2);
                    else if (i == 4)
                        bulletDirection[4] = new Vector2(playerDirection.x * bulletSpeed, -bulletSpeed);
                }
            }

            else
            {
                for (int i = 0; i < bulletInstance.Length; i = i + 1)
                {
                    if (i == 0)
                        bulletDirection[0] = new Vector2(playerDirection.x * bulletSpeed, playerDirection.y * bulletSpeed);
                    else if (i == 1)
                        bulletDirection[1] = new Vector2(playerDirection.x * bulletSpeed / 1.3f, playerDirection.y * 3 * bulletSpeed / 2 / 1.3f);
                    else if (i == 2)
                        bulletDirection[2] = new Vector2(playerDirection.x * bulletSpeed / 1.7f, playerDirection.y * 2 * bulletSpeed / 1.7f);
                    else if (i == 3)
                        bulletDirection[3] = new Vector2(playerDirection.x * 3 * bulletSpeed / 2 / 1.3f, playerDirection.y * bulletSpeed / 1.3f);
                    else if (i == 4)
                        bulletDirection[4] = new Vector2(playerDirection.x * 2 * bulletSpeed / 1.7f, playerDirection.y * bulletSpeed / 1.7f);
                }
            }
        }

        for (int i = 0; i < bulletInstance.Length; i = i + 1)
        {
            bulletInstance[i].layer = Properties.PLAYER_LAYER;
            bulletInstance[i].tag = Properties.PLAYER_BULLET_TAG;
            bulletInstance[i].GetComponent<SpriteRenderer>().sortingOrder = Properties.PLAYER_BULLET_SORTING_ORDER;
            bulletInstance[i].GetComponent<Rigidbody2D>().AddForce(bulletDirection[i], ForceMode2D.Impulse);
            bulletInstance[i].AddComponent<PlayerBulletDestroy>();
        }
    }
}