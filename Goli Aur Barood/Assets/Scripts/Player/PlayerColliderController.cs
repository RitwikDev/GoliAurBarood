using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    public Collider2D baseCollider;
    public Collider2D standingTrigger;
    public Collider2D duckingTrigger;
    public Collider2D jumpingTrigger;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if (playerMovement.isJumping)
            Jump();

        else if (playerMovement.isDucking)
            Duck();

        else if (playerMovement.isFalling)
            Fall();

        else
            Stand();
    }

    private void Stand()
    {
        baseCollider.enabled = standingTrigger.enabled = true;
        duckingTrigger.enabled = jumpingTrigger.enabled = false;
    }

    private void Duck()
    {
        baseCollider.enabled = duckingTrigger.enabled = true;
        standingTrigger.enabled = jumpingTrigger.enabled = false;
    }

    private void Jump()
    {
        jumpingTrigger.enabled = true;
        baseCollider.enabled = standingTrigger.enabled = duckingTrigger.enabled = false;
    }

    private void Fall()
    {
        standingTrigger.enabled = true;
        baseCollider.enabled = duckingTrigger.enabled = jumpingTrigger.enabled = false;
    }
}