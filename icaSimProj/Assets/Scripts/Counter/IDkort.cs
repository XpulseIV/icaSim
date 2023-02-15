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

    public Image randomImage;
    public Sprite s0;
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite[] images;

    public void random_image_generator()
    {
        images = new Sprite[4];
        images[0] = s0;
        images[1] = s1;
        images[2] = s2;
        images[3] = s3;
    }

    void changeImage()
    {
        int num = UnityEngine.Random.Range(0, images.Length);
        randomImage.sprite = images[num];
    }

    public TextMeshProUGUI TextBox;
    public string Thenames;
    public void PickRandomFromList()
    {
        string[] names = new string[] { "Bertil", "Bob", "Jessica","anna","jack"};
        string randomName = names[Random.Range(0, names.Length)];
        TextBox.GetComponent<TextMeshProUGUI>().text = "" + randomName;
    }
}   
