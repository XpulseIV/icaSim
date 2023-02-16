using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class ScanItems : MonoBehaviour
{
    public PolygonCollider2D Collider;
    public TextMeshProUGUI Price;
    public TextMeshProUGUI Items;
    public List<Collider2D> collisions;
    public GameObject kassamedkassacanvas;

    public GameObject money50;
    public GameObject money20;
    public GameObject money10;

    // Start is called before the first frame update
    void Start() { }

    void spawnMoney(int value)
    {
        int[] denominations = { 50, 20, 10 };
        GameObject[] prefabs = { this.money50, this.money20, this.money10 };
        Vector3 spawnPosition = new(Random.Range(-5f, 5f), 0.5f, Random.Range(-5f, 5f));

        for (int i = 0; i < denominations.Length; i++)
        {
            int numNotes = value / denominations[i];
            value %= denominations[i];

            for (int j = 0; j < numNotes; j++)
            {
                Object.Instantiate(prefabs[i], spawnPosition + new Vector3(0, 0, j * 0.1f), Quaternion.identity);
            }
        }
    }


    public void OnScan()
    {
        int actualItems = GameObject.FindGameObjectsWithTag("Object").Length;
        int itemsInScanningArea = this.collisions.Count;

        if (actualItems != itemsInScanningArea)
        {
            this.kassamedkassacanvas.SetActive(false);
            QuestionDialogUI.Instance.ShowQuestion(
                "Items are missing from the scanning area, make sure they are all there before scanning",
                () =>
                {
                    this.kassamedkassacanvas.SetActive(true);

                    GameObject[] someExtrasHuh = GameObject.FindGameObjectsWithTag("Object");
                    for (int i = actualItems; i < someExtrasHuh.Length; i++)
                    {
                        Object.Destroy(someExtrasHuh[i]);
                    }
                },
                () => { }, false, "Ok");
        }
        else
        {
            using RNGCryptoServiceProvider rng = new();
            byte[] bytes = new byte[1];
            rng.GetBytes(bytes);
            int randomNumber = (bytes[0] % 100) + 1;
            bool characterAffordsItems = randomNumber <= 65;

            switch (characterAffordsItems)
            {
                case true:
                    this.spawnMoney(Convert.ToInt32(this.Price.text));
                    break;
                case false:
                    break;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        this.Price.text = this.collisions.Sum(collision => collision.gameObject.GetComponent<ItemP>().price).ToString();
        this.Items.text = this.Collider.OverlapCollider(new ContactFilter2D().NoFilter(), this.collisions).ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.spawnMoney(new System.Random().Next(10, 171));
        }
    }
}