using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CharacterChanger : MonoBehaviour
{
    public List<Sprite> SpriteList;
    public GameObject Character;
    public int character = 0;
    private System.Random rng;

    private void Start()
    {
        rng = new();
    }

    private void Update()
    {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(1))
        {

            this.Character.GetComponent<SpriteRenderer>().sprite = this.SpriteList[rng.Next(0, SpriteList.Count)];
            character++;
          
             if (character >= SpriteList.Count)
            {
                character = 0;
            }
        }

    }
}
