using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDirection : MonoBehaviour
{
    // gameObject is the player
    // transform is the player's transform

    private PlayerInputActions playerInputActions;  // Reference to the Input System
    private InputAction inputActionMovementAllDirections;
    private Player player;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        player = gameObject.GetComponent<Player>();
    }

    private void OnEnable()
    {
        inputActionMovementAllDirections = playerInputActions.Player.MovementDirections;
        inputActionMovementAllDirections.Enable();
    }

    private void OnDisable()
    {
        inputActionMovementAllDirections.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isAlive)
            return;

        Vector2 direction = inputActionMovementAllDirections.ReadValue<Vector2>();
        if(direction != Vector2.zero)
        {
            float rotation = (direction.x < 0)? 180 : ((direction.x > 0)? 0 : gameObject.transform.eulerAngles.y);
            gameObject.transform.eulerAngles = new Vector3(0.0f, rotation, 0.0f);
        }
    }
}