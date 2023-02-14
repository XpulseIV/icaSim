using System;
using UnityEngine;
using UnityEngine.Events;

public sealed class TimeKeeper : MonoBehaviour
{
    float currentTime;
    public float _timeRemaining;
    public float maxTime = 5.0f;

    public UnityEvent nextDay;

    private void Start()
    {
        this._timeRemaining = this.maxTime;
    }

    // Update is called once per frame
    private void Update()
    {
        if (this._timeRemaining > 0)
        {
            this._timeRemaining -= Time.deltaTime;
        }
        else
        {
            this.nextDay.Invoke();
        }
    }

}