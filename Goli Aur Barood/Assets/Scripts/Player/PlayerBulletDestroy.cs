using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletDestroy : MonoBehaviour
{
    public float bulletTTL = 5f;
    private float timer;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        Vector3 position = cam.WorldToScreenPoint(transform.position);

        if (timer >= bulletTTL || position.x <= 0 || position.x >= Screen.width || position.y <= 0 || position.y >= Screen.height)
        {
            PlayerFire.bulletCount = PlayerFire.bulletCount - 1;
            Destroy(gameObject);
        }
    }

    public void DestroyPlayerBullet()
    {
        PlayerFire.bulletCount = PlayerFire.bulletCount - 1;
        Destroy(gameObject);
    }
}