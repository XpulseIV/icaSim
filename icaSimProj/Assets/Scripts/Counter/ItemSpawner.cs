using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<Sprite> SpriteList;
    public GameObject Item;
    public int item = 0;
    private System.Random rng;

    void Start()
    {
        rng = new();
    }

    void Update()
    {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButtonDown(1))
        {
            this.Item.GetComponent<SpriteRenderer>().sprite = this.SpriteList[rng.Next(0, SpriteList.Count)];
            item++;

            if (item >= SpriteList.Count)
            {
                item = 0;
            }
        }

    }
}
