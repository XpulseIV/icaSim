using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class clickpersondialouge : MonoBehaviour
{
    public bool Clickable;
    private Color startcolor;
    public UnityEvent onClickPerson;

    // Start is called before the first frame update
    void Start()
    {
        this.onClickPerson ??= new UnityEvent();
    }

    // Update is called once per frame
    void Update() { }

    void OnMouseEnter()
    {
        if (this.Clickable)
        {
            startcolor = GetComponent<Renderer>().material.color;
            GetComponent<Renderer>().material.color
                = Color.red;

            if (Input.GetMouseButton(0))
            {
                this.onClickPerson.Invoke();
            }
        }
    }

    void OnMouseExit()
    {
        if (this.Clickable) {
            GetComponent<Renderer>().material.color
                = startcolor;
        }
    }
}