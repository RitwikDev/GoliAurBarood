using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLife : MonoBehaviour
{
    private GameObject[] lifeImages = new GameObject[5];

    private void Awake()
    {
        for (int i = 0; i < lifeImages.Length; i = i + 1)
            lifeImages[i] = GameObject.Find(Properties.UI_LIFE_IMAGE + i);
    }

    public void UpdateLifeCount(int life)
    {
        for (int i = 0; i < lifeImages.Length; i = i + 1)
            lifeImages[i].SetActive(false);

        for (int i=0; i<life; i=i+1)
        {
            lifeImages[i].SetActive(true);
            if (i >= 4)
                break;
        }
    }
}