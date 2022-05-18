using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    private SpriteRenderer boundarySpriteRenderer;
    //public Vector2 boundaryX { get; private set; }
    //public Vector2 boundaryY { get; private set; }

    private void Start()
    {
        boundarySpriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //boundaryX = new Vector2(transform.position.x - boundarySpriteRenderer.bounds.size.x / 2, transform.position.x + boundarySpriteRenderer.bounds.size.x / 2);
        //boundaryY = new Vector2(transform.position.y - boundarySpriteRenderer.bounds.size.y / 2, transform.position.y + boundarySpriteRenderer.bounds.size.y / 2);
    }
}