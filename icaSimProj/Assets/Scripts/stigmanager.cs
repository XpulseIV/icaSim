using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void StartDialouge()
    {
        bool no = false;
        QuestionDialogUI.Instance.ShowQuestion(
            "-Hey! You must be the new employee I was told about. Welcome to the job kid!",
            () =>
            {
                QuestionDialogUI.Instance.ShowQuestion("-My name is Stig but you might know me as ICA Stig?",
                    () =>
                    {
                        QuestionDialogUI.Instance.ShowQuestion(
                            "-Let's get started with your training right away?",
                            () =>
                            {
                                QuestionDialogUI.Instance.ShowQuestion(
                                    "-your job is to sit here in the register and greet customers. When charging them there are a few things to keep track of!",
                                    () =>
                                    {
                                        QuestionDialogUI.Instance.ShowQuestion(
                                            "-when scanning the customer's item make sure to not scan 'em too fast or too slow! we don't wanna stress our customers now do we?",
                                            () =>
                                            {
                                                QuestionDialogUI.Instance.ShowQuestion(
                                                    "- also there will be some underage rascals tryna buy some \"restricted\" items. So make sure to keep track of the customer's ID if they look suspicious?",
                                                    () =>
                                                    {
                                                        QuestionDialogUI.Instance.ShowQuestion(
                                                            "-if they aint got a valid ID or enough money just click on them to ask them to either leave or cough up the cash?",
                                                            () =>
                                                            {
                                                                QuestionDialogUI.Instance.ShowQuestion(
                                                                    "-you got all that?", () =>
                                                                    {
                                                                        QuestionDialogUI.Instance.ShowQuestion(
                                                                            "-great kid welcome to the family. Now ill let you get to work ill check up on you at the end of your shift?", () =>
                                                                            {
                                                                                QuestionDialogUI.Instance
                                                                                    .ShowQuestion(
                                                                                        "-good luck kid! see you around?", () => { kassamedkassa.SetActive(true); },
                                                                                        static () => { });
                                                                            }, static () => { });
                                                                    }, () => { SceneManager.LoadScene(SceneManager.GetActiveScene().name); });
                                                            }, static () => { });
                                                    }, static () => { });
                                            }, static () => { });
                                    }, static () => { });
                            }, static () => { });
                    }, static () => { });
            }, static () => { });
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
            this.stig.transform.position =
                Vector3.Lerp(this.moveStartPos, this.moveEndPos, stigmanager.T(fractionOfJourney));

            if (this.stig.transform.position.y >= 0.9)
            {
                this.stig.transform.position = new Vector3(11.8f, 1, 0);
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

            this.stig.transform.localScale =
                Vector3.Lerp(this.stretchStartPos, this.stretchEndPos, stigmanager.T(fractionOfJourney));

            if (!(this.stig.transform.localScale.y <= 1.5f)) return;

            this.stretchStig = false;
            this.kassamedkassa.SetActive(false);
            this.hand.SetActive(true);


            this.StartDialouge();
        }
    }
}