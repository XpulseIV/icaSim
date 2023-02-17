using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class IDkort : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI AgeText;

    public List<Sprite> SpriteList;
    public GameObject Character;

    public void characters_change()
    {
        string[] names =
        {
            "Bertil", "Bob", "Jessica", "Anna", "Jack", "Coby", "Leo", "Anton", "Maria", "Noah", "Thor",
            "Klara", "Malte", "Isac", "Jacob", "Michael", "Sam", "Theo", "Max", "Hannes", "Erik"
        };
        string randomName = names[Random.Range(0, names.Length)];
        this.NameText.text = randomName;

        this.AgeText.text = Random.Range(12, 76).ToString();

        this.Character.GetComponent<SpriteRenderer>().sprite = this.SpriteList[Random.Range(0, SpriteList.Count)];
    }
}   
    