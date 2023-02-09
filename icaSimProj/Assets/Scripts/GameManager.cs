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
        this.icaStig.SetActive(false);
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

    private static float F(float t)
    {
        float v1 = t * t;
        float v2 = 1.0f - (1.0f - t) * (1.0f - t);
        return Mathf.Lerp(v1, v2, t);
    }

    // Update is called once per frame
    private void Update()
    {
        if (this.moveStig)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - this._startTime) * this.moveSpeed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / this._journeyLength;

            // Set our position as a fraction of the distance between the markers.
            this.icaStig.transform.position = Vector3.Lerp(this.moveStartPos, this.moveEndPos, fractionOfJourney);

            if (Math.Abs(this.icaStig.transform.position.y - 1) < 0.05f)
            {
                this.calculateNewValues = true;
                this.moveStig = false;
            }
        }

        if (this.calculateNewValues)
        {
            this._startTime = Time.time;

            // Calculate the journey length.
            this._journeyLength = Vector3.Distance(this.moveStartPos, this.moveEndPos);
            this.calculateNewValues = false;
            this.strechStig = true;
        }

        if (this.strechStig)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - this._startTime) * this.stretchSpeed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / this._journeyLength;

            // Set our position as a fraction of the distance between the markers.
            this.icaStig.transform.localScale = Vector3.Lerp(this.stretchStartPos, this.stretchMoveEndPos,
                GameManager.F(fractionOfJourney));

            if (!(Math.Abs(this.icaStig.transform.localScale.y - 1) < 0.05f)) return;

            this.strechStig = false;
        }
    }
}
