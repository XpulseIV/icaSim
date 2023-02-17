using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public sealed class eaMoney : MonoBehaviour
{
    public GameSession Session;
    public BoxCollider2D collider;
    public List<Collider2D> Collision2Ds;

    public GameObject m50;
    public GameObject m20;
    public GameObject m10;

    public UnityEvent s;

    private void Start()
    {
        this.s ??= new UnityEvent();
    }

    private void Update()
    {
        this.collider.OverlapCollider(new ContactFilter2D(), this.Collision2Ds);
        foreach (Collider2D s in this.Collision2Ds.Where(static asd => asd.CompareTag("Money")))
        {
            if (s.gameObject.GetComponent<SpriteRenderer>().sprite == this.m50.GetComponent<SpriteRenderer>().sprite)
            {
                this.Session.money += 50;
                Object.Destroy(s.gameObject);
            }
            else if (s.gameObject.GetComponent<SpriteRenderer>().sprite == this.m20.GetComponent<SpriteRenderer>().sprite)
            {
                this.Session.money += 20;
                Object.Destroy(s.gameObject);
            }
            else if (s.gameObject.GetComponent<SpriteRenderer>().sprite == this.m10.GetComponent<SpriteRenderer>().sprite)
            {
                this.Session.money += 10;
                Object.Destroy(s.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && GameObject.FindGameObjectsWithTag("Money").Length == 0)
        {
            this.s.Invoke();
        }
    }
}
