using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    float maxSpeed = 10f;
    private void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 vel = rb.velocity;
        if (vel. magnitude > maxSpeed)
        {
            rb.velocity = vel.normalized * maxSpeed;
        }
        
    }
    float minSpeed = 1f;

    private void fixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 vel = rb.velocity;
        if (vel. magnitude < minSpeed)
        {
            rb.velocity = vel.normalized * minSpeed;
        }
    }
}
