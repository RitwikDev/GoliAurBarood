using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;      // For the new InputSystem

public class PlayerMovement : MonoBehaviour
{
    private const float JUMP_FACTOR = 12f;       // Multiplication factor for jump action
    private const float MOVEMENT_SPEED = 5f;    // Multiplication factor for movement
    private const float DECREASING_FACTOR = 1.3f;   // Multiplication factor for decreasing the momentum while in air

    private PlayerInputActions playerInputActions;  // Reference to the Input System
    private InputAction inputActionMovement;
    private Rigidbody2D rigidbody2DPlayer;  // Player's RigidBody
    private GroundCheck groundCheck;
    private Player player;

    public bool isDucking { get; private set; }     // To keep track if the player is ducking or not
    public bool isRunningStraight { get; private set; }
    public bool isFalling;
    public bool isJumping { get; set; }
    public bool noMovement { get; private set; }
    public Vector2 movementDirection { get; private set; }
    
    private void Awake()
    {
        groundCheck = transform.Find(Properties.GROUND_CHECK_NAME).gameObject.GetComponent<GroundCheck>();

        rigidbody2DPlayer = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputActions();
        player = gameObject.GetComponent<Player>();
    }

    private void Start()
    {
        isJumping = true;
    }

    private void FixedUpdate()
    {
        if (groundCheck.isGrounded)
            isJumping = isFalling = false;

        if (!groundCheck.isGrounded && !isJumping)
            isFalling = true;

        if(player.isAlive)
            PlayerMove();
        UpdateGravity();
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

    // This function updates the value of gravity based on the elevation of the player
    private void UpdateGravity()
    {
        // Increasing the gravity when the player starts to descend
        if (rigidbody2DPlayer.velocity.y < 0.1)
            rigidbody2DPlayer.gravityScale = 3f;
        else
            rigidbody2DPlayer.gravityScale = 2f;    // Else, restoring the gravity to its default value of 1
    }

    // This function moves the player. It is also responsible for providing momentum to the player.
    private void PlayerMove()
    {
        movementDirection = inputActionMovement.ReadValue<Vector2>();
        float moveX = movementDirection.x;
        float moveY = movementDirection.y;

        // Player if touching the ground
        if(groundCheck.isGrounded)
        {
            isRunningStraight = (moveX != 0 && moveY == 0);
            isDucking = (moveX == 0 && moveY < 0);
            noMovement = (moveX == 0 && moveY == 0);

            // Player is not moving in the x-direction
            if(moveX == 0)
                rigidbody2DPlayer.velocity = new Vector2(MOVEMENT_SPEED * moveX, rigidbody2DPlayer.velocity.y); // For jumping

            else
                rigidbody2DPlayer.velocity = new Vector2(MOVEMENT_SPEED * moveX, rigidbody2DPlayer.velocity.y);
        }

        else
        {
            if (moveX == 0)
            {
                // The new velocity of the player. Speed is the same as before in y-direction
                // The below if-else block is used to provide momentum to the player while in air

                if (rigidbody2DPlayer.velocity.x > 0)
                    rigidbody2DPlayer.velocity = new Vector2(rigidbody2DPlayer.velocity.x - Time.deltaTime * MOVEMENT_SPEED * DECREASING_FACTOR, rigidbody2DPlayer.velocity.y);

                else if (rigidbody2DPlayer.velocity.x < 0)
                    rigidbody2DPlayer.velocity = new Vector2(rigidbody2DPlayer.velocity.x + Time.deltaTime * MOVEMENT_SPEED * DECREASING_FACTOR, rigidbody2DPlayer.velocity.y);
            }

            else
                rigidbody2DPlayer.velocity = new Vector2(MOVEMENT_SPEED * moveX, rigidbody2DPlayer.velocity.y);
        }
    }

    // This function is used for making the player jump
    public void PlayerJump(InputAction.CallbackContext context)
    {
        if (!player.isAlive)
            return;

        // If the jump button is pressed and the player is on the ground, then jump
        if(context.performed && groundCheck.isGrounded)
        {
            // If the player is ducking
            if (isDucking)
            {
                // Make the player fall from the platform
                if (groundCheck.currentGround != null && groundCheck.currentGround.tag == Properties.GROUND_TAG_CAN_FALL_NAME)
                {
                    groundCheck.isGrounded = false;
                    isDucking = false;
                    isFalling = true;

                }
            }

            else if(Mathf.Abs(rigidbody2DPlayer.velocity.y) < 0.01)
            {
                rigidbody2DPlayer.AddForce(Vector2.up * JUMP_FACTOR, ForceMode2D.Impulse);   // Adding upward force to jump
                groundCheck.isGrounded = false;
                isJumping = true;
            }
        }
    }
}