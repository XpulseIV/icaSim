using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public sealed class GameManagerV2 : MonoBehaviour
{
    public static readonly System.Random Rng = new();
    public GameObject Person;
    public GameObject Item;
    public GameObject kassamedkassa;

    public eaMoney moneyEater;
    public List<Sprite> peopleSprites;
    public List<Sprite> itemSprites;
    public List<int> itemPrices;
    public GameObject p;
    public GameObject kassamedkassacanvas2;

    // Start is called before the first frame update
    private void Start()
    {
        this.moneyEater.s.AddListener(this.nextPerson);

        this.SpawnPerson();
    }

    void nextPerson()
    {
        string[] byes =
        {
            "-Bye",
            "-Have a nice day",
            "-Thank you"
        };

        this.kassamedkassa.SetActive(false);
        QuestionDialogUI.Instance.ShowQuestion(byes[new System.Random().Next(0, 3)], () =>
        {
            this.kassamedkassa.SetActive(true);

            GameObject[] items = GameObject.FindGameObjectsWithTag("Object");
            foreach (GameObject s in items)
            {
                Object.Destroy(s);
            }

            kassamedkassacanvas2.SetActive(true);
            this.SpawnPerson();
        }, () => { }, false, "Bye");
    }

    private void SpawnPerson()
    {
        Object.Destroy(this.Person);
        this.Person = Object.Instantiate(p, new Vector3(9, 0), Quaternion.identity);
        int pIdx = GameManagerV2.Rng.Next(0, 50);
        this.Person.GetComponent<SpriteRenderer>().sprite = this.peopleSprites[pIdx];
        this.Person.AddComponent<PolygonCollider2D>();
        this.Person.GetComponent<PolygonCollider2D>().isTrigger = true;

        string[] his =
        {
            "-Good day",
            "-Morning",
            "-Hi there sexy",
            "-Hello young fella"
        };

        this.kassamedkassa.SetActive(false);
        QuestionDialogUI.Instance.ShowQuestion(his[new System.Random().Next(0, 4)], () =>
        {
            this.kassamedkassa.SetActive(true);
            this.kassamedkassacanvas2.SetActive(true);

            for (int i = 0; i < GameManagerV2.Rng.Next(1, 4); i++)
            {
                this.SpawnObject(i);
            }
        }, () => { }, false, "Hi");
    }

    private void SpawnObject(int height)
    {
        int oIdx = GameManagerV2.Rng.Next(0, 26);
        GameObject ob = Object.Instantiate(this.Item, new Vector3(13, height + (GameManagerV2.Rng.Next(1, 5)), 0),
            Quaternion.Euler(0, 0, GameManagerV2.Rng.Next(0, 366)));
        ob.transform.localScale /= 2;
        ob.GetComponent<SpriteRenderer>().sprite = this.itemSprites[oIdx];
        ob.GetComponent<ItemP>().price = this.itemPrices[oIdx];
        Object.Destroy(ob.GetComponent<PolygonCollider2D>());
        ob.AddComponent<PolygonCollider2D>();
    }

// Update is called once per frame
    private void Update() { }
}