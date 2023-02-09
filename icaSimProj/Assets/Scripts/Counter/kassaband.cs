using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kassaband : MonoBehaviour
{
    public List<Collider2D> results;
    public int size = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<CapsuleCollider2D>()
            .OverlapCollider(new ContactFilter2D().NoFilter(), results);

        this.size = this.results.Count;

        if (results.Count > 1)
        {
            foreach (Collider2D icaObject in this.results)
            {
                if (icaObject.CompareTag("Object")) icaObject.gameObject.GetComponent<Rigidbody2D>().MovePosition(Vector2.left * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        
    }
}
