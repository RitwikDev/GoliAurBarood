using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGroundCheck : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Properties.GROUND_LAYER)
            playerMovement.isFalling = false;
    }
}
