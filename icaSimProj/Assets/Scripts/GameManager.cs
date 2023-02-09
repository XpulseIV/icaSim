using System;
using Unity.VisualScripting;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public bool moveStig = true;
    public bool strechStig;
    public bool calculateNewValues;

    public Camera camera;
    public GameObject icaStig;
    public GameObject hand;
    public GameObject kassamedkassakassautankassa;

    public Vector3 moveStartPos = new(11.8f, -50, 0);
    public Vector3 moveEndPos = new(11.8f, 1, 0);
    public Vector3 stretchStartPos = new(1, 10, 1);
    public Vector3 stretchMoveEndPos = new(1, 1, 1);

    // Movement speed in units per second.
    public float moveSpeed = 5.0F;
    public float stretchSpeed = 15.0F;

    // Time when the movement started.
    private float _startTime;

    // Total distance between the markers.
    private float _journeyLength;

    // Start is called before the first frame update
    private void Start()
    {
        this.hand.SetActive(true);
        this.kassamedkassakassautankassa.SetActive(true);

        this.camera.Render();

        this.GreetPlayer();
    }

    private void GreetPlayer()
    {
        this.icaStig.transform.position -= Vector3.up * 50;
        this.icaStig.transform.localScale += Vector3.up * 9;

        this.icaStig.SetActive(true);
        this.icaStig.GetComponent<CapsuleCollider2D>().enabled = false;

        this._startTime = Time.time;

        // Calculate the journey length.
        this._journeyLength = Vector3.Distance(this.moveStartPos, this.moveEndPos);
    }

}
