using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public sealed class stigmanager : MonoBehaviour
{
    public bool moveStig;
    public bool stretchStig;

    public Vector3 moveStartPos = new(11.8f, -50, 0);
    public Vector3 moveEndPos = new(11.8f, 1, 0);
    public Vector3 stretchStartPos = new(1, 10, 1);
    public Vector3 stretchEndPos = new(1, 1, 1);

    // Movement speed in units per second.
    public float moveSpeed = 5.0F;
    public float stretchSpeed = 15.0F;

    // Time when the movement started.
    private float _startTime;

    // Total distance between the markers.
    private float _journeyLength;

    public GameObject stig;
    public GameObject hand;
    public GameObject kassamedkassa;
    public GameObject dialougeSystemYes;

    public Dialouge d;

    // Start is called before the first frame update
    private void Start()
    {
        this._startTime = Time.time;

        // Calculate the journey length.
        this._journeyLength = Vector3.Distance(this.moveStartPos, this.moveEndPos);

        this.moveStig = true;
    }

    private static float T(float t)
    {
        float v1 = t * t;
        float v2 = 1.0f - (1.0f - t) * (1.0f - t);
        return Mathf.Lerp(v1, v2, t);
    }

    private void Stretch()
    {
        this.stretchStig = true;
        this._startTime = Time.time;

        // Calculate the journey length.
        this._journeyLength = Vector3.Distance(this.stretchStartPos, this.stretchEndPos);
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
            this.stig.transform.position = Vector3.Lerp(this.moveStartPos, this.moveEndPos, stigmanager.T(fractionOfJourney));

            if (this.stig.transform.position.y >= 1.0f)
            {
                this.moveStig = false;
                this.Stretch();
            }
        }

        if (this.stretchStig)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - this._startTime) * this.stretchSpeed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / this._journeyLength;

            this.stig.transform.localScale = Vector3.Lerp(this.stretchStartPos, this.stretchEndPos, stigmanager.T(fractionOfJourney));

            if (this.stig.transform.localScale.y <= 1.5f)
            {
                this.stretchStig = false;
                this.hand.SetActive(false);
                this.kassamedkassa.SetActive(false);
                this.dialougeSystemYes.SetActive(true);

                string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "/Assets/Assets/Text/icastigdialog.txt", Encoding.UTF8).Where(static (line) => line != "�").ToArray()[1..10];
                this.d = new Dialouge("Ica stig", lines);

                Camera.main!.GetComponent<dialougetrigger>().SetD(this.d);
                Camera.main!.GetComponent<dialougetrigger>().DialogStart();
            }
        }
    }
}
