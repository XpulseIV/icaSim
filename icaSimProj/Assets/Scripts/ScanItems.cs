using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public sealed class ScanItems : MonoBehaviour
{
    public PolygonCollider2D Collider;

    public TextMeshProUGUI Price;
    public TextMeshProUGUI Items;
    public List<Collider2D> collisions;
    public List<Collider2D> itemList;
    public GameObject kassamedkassacanvas;
    public List<Sprite> forbiddenItems;
    public List<int> hmmm;

    public GameObject money50;
    public GameObject money20;
    public GameObject money10;

    // Start is called before the first frame update

    private void SpawnMoney(int value)
    {
        int[] denominations = { 50, 20, 10 };
        GameObject[] prefabs = { this.money50, this.money20, this.money10 };
        Vector3 spawnPosition = new(Random.Range(-5f, 5f), 0.5f, 0);

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

        if (actualItems != this.itemList.Count)
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
                }, static () => { }, false, "Ok");
        }
        else
        {
            using RNGCryptoServiceProvider rng = new();
            byte[] bytes = new byte[1];
            rng.GetBytes(bytes);
            int randomNumber = (bytes[0] % 100) + 1;
            bool characterAffordsItems = randomNumber <= 65;

            bool demandId = this.itemList.Any(i => i.gameObject.GetComponent<SpriteRenderer>().sprite == this.forbiddenItems.Any());

            hmmm = Enumerable.Range(10, Convert.ToInt32(this.Price.text) - 10).Where(x => (x % 10) == 0).ToList();
            this.SpawnMoney(Convert.ToInt32(characterAffordsItems ? this.Price.text : new System.Random().Next(0, hmmm.Count)));
            /*if (demandId)
            {
                
            }

            switch (characterAffordsItems)
            {
                case true:
                    
                    break;
                case false:
                    QuestionDialogUI.Instance.ShowQuestion("-you don't have enough money", () =>
                    {
                        this.kassamedkassacanvas.SetActive(false);
                        switch (new System.Random().Next(0, 2))
                        {
                            case 0:
                                QuestionDialogUI.Instance.ShowQuestion("-Please let me buy this", () =>
                                {
                                    this.kassamedkassacanvas.SetActive(true);

                                    GameObject[] someExtrasHuh = GameObject.FindGameObjectsWithTag("Object");
                                    for (int i = actualItems; i < someExtrasHuh.Length; i++)
                                    {
                                        Object.Destroy(someExtrasHuh[i]);
                                    }
                                }, () => { }, true, "No");
                                break;
                            case 1:
                                QuestionDialogUI.Instance.ShowQuestion("-Please this is all i got, I need to feed my family",
                                    () =>
                                    {
                                        this.kassamedkassacanvas.SetActive(true);

                                        GameObject[] someExtrasHuh = GameObject.FindGameObjectsWithTag("Object");
                                        for (int i = actualItems; i < someExtrasHuh.Length; i++)
                                        {
                                            Object.Destroy(someExtrasHuh[i]);
                                        }
                                    }, () => { }, true, "No");
                                break;
                        }
                    }, () => { });
                    break;
            }*/
        }
    }

    // Update is called once per frame
    private void Update()
    {
        this.Collider.OverlapCollider(new ContactFilter2D().NoFilter(), this.collisions).ToString();

        this.itemList = this.collisions.Where(static ob => ob.gameObject.CompareTag("Object")).ToList();

        int collectivePrice = this.itemList.Sum(static collision => collision.gameObject.GetComponent<ItemP>().price);
        int itemNumber = this.itemList.Count;
        this.Price.text = collectivePrice.ToString();
        this.Items.text = itemNumber.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.SpawnMoney(2023);
        }
    }
}