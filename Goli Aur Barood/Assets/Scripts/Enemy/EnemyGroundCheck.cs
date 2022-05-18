using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour
{
    public bool isGrounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == Properties.GROUND_LAYER)
        {
            // If the enemy is above the ground
            isGrounded = ((collision.GetComponent<Collider2D>().bounds.center.y - collision.GetComponent<Collider2D>().bounds.size.y / 2) < transform.position.y);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == Properties.GROUND_LAYER)
            isGrounded = false;
    }
}