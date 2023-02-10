using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePull : MonoBehaviour
{
    public Camera Camera;
    void Start()
    {
        Cursor.visible = false;
    }


    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        transform.position = mousePosition;

    }
}
