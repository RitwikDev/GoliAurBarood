using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBox : MonoBehaviour
{
    private const float COLOUR_CHANGE_TIME = 0.1f;
    private int health;
    private float timer;
    private bool changeColour;

    private PowerUp powerUp;
    private SpriteRenderer spriteRenderer;
    private Color defaultColour;

    private void Awake()
    {
        health = 2;
        powerUp = gameObject.GetComponentInParent<PowerUp>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        defaultColour = spriteRenderer.color;
    }

    private void Update()
    {
        if(changeColour)
        {
            timer = timer + Time.deltaTime;
            if(timer >= COLOUR_CHANGE_TIME)
            {
                timer = 0;
                changeColour = false;
                spriteRenderer.color = defaultColour;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Properties.PLAYER_BULLET_TAG)
        {
            if (health <= 0)
            {
                gameObject.SetActive(false);
                powerUp.MakeItemAppear();
            }

            else
            {
                health = health - 1;
                Destroy(collision.gameObject);
                spriteRenderer.color = new Color(0.7f, 0.7f, 0.7f);
                changeColour = true;
            }
        }
    }
}