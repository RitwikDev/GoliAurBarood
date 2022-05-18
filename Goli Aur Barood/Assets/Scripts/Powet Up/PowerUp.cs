using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        MachineGun, Super, Invincibility
    }

    public Sprite spriteMachineGunItem;
    public Sprite spritSuperGunItem;
    public Sprite spriteBarrierItem;

    public PowerUpType powerUpType;

    private GameObject gameObjectItem;

    private void Awake()
    {
        gameObjectItem = transform.Find(Properties.POWER_UP_ITEM_GAME_OBJECT).gameObject;
        gameObjectItem.SetActive(false);
    }

    public void MakeItemAppear()
    {
        Rigidbody2D rigidbody2DItem = gameObjectItem.GetComponent<Rigidbody2D>();
        SpriteRenderer spriteRenderer = gameObjectItem.GetComponent<SpriteRenderer>();

        if (powerUpType == PowerUpType.MachineGun)
            spriteRenderer.sprite = spriteMachineGunItem;

        else if (powerUpType == PowerUpType.Super)
            spriteRenderer.sprite = spritSuperGunItem;

        else if (powerUpType == PowerUpType.Invincibility)
            spriteRenderer.sprite = spriteBarrierItem;

        gameObjectItem.SetActive(true);
        rigidbody2DItem.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    public void GrantPowerUp()
    {
        Player player = GameObject.Find(Properties.PLAYER_GAME_OBJECT_NAME).GetComponent<Player>();

        if (powerUpType == PowerUpType.Invincibility)
            player.isInvincible = true;

        else if (powerUpType == PowerUpType.MachineGun)
            player.currentGun = Player.GunType.Machine;

        else if (powerUpType == PowerUpType.Super)
            player.currentGun = Player.GunType.Super;

        Destroy(gameObject);
    }
}