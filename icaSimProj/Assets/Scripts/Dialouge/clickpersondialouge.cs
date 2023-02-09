using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickpersondialouge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private Color startcolor;
    void OnMouseEnter()
    {
        startcolor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color
= Color.red;
    }
    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color
= startcolor;
    }
    
}
