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
        int pIdx = GameManagerV2.Rng.Next(0, 49);
        this.Person.GetComponent<SpriteRenderer>().sprite = this.peopleSprites[pIdx];
    }

    private void SpawnObject()
    {
        int oIdx = GameManagerV2.Rng.Next(0, 33);
        GameObject ob = Object.Instantiate(this.Item, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        ob.transform.localScale /= 2;
        ob.GetComponent<SpriteRenderer>().sprite = this.itemSprites[oIdx];
        Object.Destroy(ob.GetComponent<Light2D>());
        Object.Destroy(ob.GetComponent<PolygonCollider2D>());
        ob.AddComponent<PolygonCollider2D>();
    }

    private void removeItems()
    {
        GameObject[] stuff = GameObject.FindGameObjectsWithTag("Object");
        foreach (GameObject i in stuff)
        {
            Object.Destroy(i);
        }
    }

    private void Day()
    {
        Debug.Log("The next day");
        this.dayCount++;
        this.timeKeeper._timeRemaining = this.timeKeeper.maxTime;

        this.removeItems();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.SpawnPerson();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            this.SpawnObject();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            this.removeItems();
        }
    }
}
