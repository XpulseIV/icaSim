using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{

    public GameObject Times_up_text;
    public Image ForeGroundImage;
    float time_remaining;
    public float max_time = 5.0f;
    private TimeSpan _timeSpan;
    





    void Start()
    {
        time_remaining = max_time;
    }

    // Update is called once per frame
    void Update()
    {
        if (time_remaining > 0)
        {
            time_remaining -= Time.deltaTime;
            this.ForeGroundImage.fillAmount = time_remaining / max_time;
        }
        else
        {
            Times_up_text.SetActive(true);
        }

    }
}
