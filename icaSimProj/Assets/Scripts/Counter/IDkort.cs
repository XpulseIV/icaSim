using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IDkort : MonoBehaviour
{
    private List<int> random_number_list = new List<int>();
    public TextMeshProUGUI random_numbers_holder;
    public int total_numbers_in_list = 1;

    public void random_number_generator()
    {
        random_number_list.Clear();
        random_numbers_holder.text = "";

        for (int i = 0; i < total_numbers_in_list; i++)
        {
            int rand = Random.Range(1, 99);
            random_number_list.Add(rand);
        }

        foreach (var num in random_number_list)
        {
            random_numbers_holder.text += num.ToString() + " ";
        }
    }

    public TextMeshProUGUI TextBox;
    public string Thenames;
    public void PickRandomFromList()
    {
        string[] names = new string[] { "Bertil", "Bob", "Jessica","Anna","Jack","Coby","Leo","Anton","Maria","Noah","Thor","Klara","Malte","Isac","Jacob", "Michael","Sam","Theo","Max","Hannes","Erik"};
        string randomName = names[Random.Range(0, names.Length)];
        TextBox.GetComponent<TextMeshProUGUI>().text = "" + randomName;
    }

    public List<Sprite> SpriteList;
    public GameObject Character;
    public int character = 0;
    private System.Random rng;

    private void Start()
    {
        rng = new();
    }

    public void characters_change()
    {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

     
        

            this.Character.GetComponent<SpriteRenderer>().sprite = this.SpriteList[rng.Next(0, SpriteList.Count)];
            character++;

            if (character >= SpriteList.Count)
            {
                character = 0;
            }
        

    }
}   
    