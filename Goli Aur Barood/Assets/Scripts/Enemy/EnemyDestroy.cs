using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    private Spawn spawn;

    private void Awake()
    {
        spawn = FindObjectOfType<Spawn>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == Properties.ENEMY_BOUNDARY)
        {
            Destroy(transform.parent.gameObject);

            if (spawn != null)
                spawn.count = spawn.count - 1;
        }
    }
}