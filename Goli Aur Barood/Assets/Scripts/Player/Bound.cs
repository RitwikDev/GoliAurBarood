using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = cam.WorldToScreenPoint(transform.position);
        float screenWidth = Screen.width;
        float leftBound = screenWidth * 0.07f;
        float rightBound = screenWidth * 0.93f;

        if (playerPosition.x < leftBound)
            transform.position = cam.ScreenToWorldPoint(new Vector3(leftBound, playerPosition.y, playerPosition.z));

        else if (playerPosition.x > rightBound)
            transform.position = cam.ScreenToWorldPoint(new Vector3(rightBound, transform.position.y, transform.position.z));
    }
}