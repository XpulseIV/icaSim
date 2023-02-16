using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public sealed class GameManagerV2 : MonoBehaviour
{
    public TimeKeeper timeKeeper;
    public int dayCount;
    public static System.Random Rng = new();
    public GameObject Person;
    public GameObject Item;
    public GameObject kassamedkassa;

    public List<Sprite> peopleSprites;
    public List<Sprite> itemSprites;
    public List<int> itemPrices;

    // Start is called before the first frame update
    private void Start()
    {
        this.timeKeeper.nextDay ??= new UnityEvent();
        this.timeKeeper.nextDay.AddListener(this.Day);

        this.SpawnPerson();
    }

    private void SpawnPerson()
    {
        int pIdx = GameManagerV2.Rng.Next(0, 50);
        this.Person.GetComponent<SpriteRenderer>().sprite = this.peopleSprites[pIdx];

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

            for (int i = 0; i < GameManagerV2.Rng.Next(1, 4); i++)
            {
                this.SpawnObject((i + 1) / 10f, i);
            }
        }, () => { }, false, "Hi");
    }

    private void SpawnObject(float difference, int height)
    {
        int oIdx = GameManagerV2.Rng.Next(0, 26);
        GameObject ob = Object.Instantiate(this.Item, new Vector3(13 - difference, height, 0),
            Quaternion.Euler(0, 0, GameManagerV2.Rng.Next(0, 366)));
        ob.transform.localScale /= 2;
        ob.GetComponent<SpriteRenderer>().sprite = this.itemSprites[oIdx];
        ob.GetComponent<ItemP>().price = this.itemPrices[oIdx];
        Object.Destroy(ob.GetComponent<PolygonCollider2D>());
        ob.AddComponent<PolygonCollider2D>();
    }


    private void Day()
    {
        Debug.Log("The next day");
        this.dayCount++;
        this.timeKeeper._timeRemaining = this.timeKeeper.maxTime;
    }

// Update is called once per frame
    private void Update() { }
}