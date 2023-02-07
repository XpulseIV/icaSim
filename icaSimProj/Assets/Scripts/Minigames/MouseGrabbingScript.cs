using System;
using UnityEngine;

public sealed class MouseGrabbingScript : MonoBehaviour
{
    public Rigidbody2D selectedObject;
    private Vector3 _offset;
    private Vector3 _mousePosition;
    public float maxSpeed = 2;
    private Vector2 _mouseForce;
    private Vector3 _lastPosition;

    private void Update()
    {
        this._mousePosition = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        this._mousePosition.z = 0;
        
        if (this.selectedObject)
        {
            this._mouseForce = (this._mousePosition - this._lastPosition) / Time.deltaTime;
            this._mouseForce = Vector2.ClampMagnitude(this._mouseForce, this.maxSpeed);
            this._lastPosition = this._mousePosition;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(this._mousePosition);
            if (targetObject && targetObject.CompareTag("Object"))
            {
                Debug.Log("Hit");

                this.selectedObject = targetObject.transform.gameObject.GetComponent<Rigidbody2D>();

                this._offset = this.selectedObject.transform.position - this._mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0) && this.selectedObject)
        {
            this.selectedObject.velocity = Vector2.zero;
            this.selectedObject.AddForce(this._mouseForce, ForceMode2D.Impulse);
            this.selectedObject = null;
        }
    }

    /*private void FixedUpdate()
    {
        if (this.selectedObject)
        {
            this.selectedObject.MovePosition(this._mousePosition + this._offset);
        }
    }*/
}