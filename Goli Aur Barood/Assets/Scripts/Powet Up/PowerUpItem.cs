using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    private PowerUp powerUp;

    private void Awake()
    {
        powerUp = gameObject.GetComponentInParent<PowerUp>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Properties.PLAYER_TAG)
            powerUp.GrantPowerUp();
    }
}