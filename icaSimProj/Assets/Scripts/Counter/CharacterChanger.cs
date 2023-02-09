using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChanger : MonoBehaviour
{
    public List<Sprite> SpriteList;
    public GameObject Character;
    public int character = 0;

    private void Update()
    {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(1))
        {
            this.Character.GetComponent<SpriteRenderer>().sprite = this.SpriteList[character];
            character++;
          
             if (character >= SpriteList.Count)
            {
                character = 0;
            }
        }

    }
}
