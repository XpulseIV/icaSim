using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public sealed class MouseGrabbingScript : MonoBehaviour
{
    public Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnMouseOver()
    {
        if (!Input.GetMouseButton(0)) return;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = this._camera.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;

        this.transform.position = mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
