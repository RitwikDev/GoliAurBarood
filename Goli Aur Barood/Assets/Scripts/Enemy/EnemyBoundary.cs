using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoundary : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Vector3 scale = cam.ScreenToWorldPoint(new Vector3(1.5f * Screen.width, 1.12f * Screen.height, 1));
        transform.localScale = scale;
    }
}