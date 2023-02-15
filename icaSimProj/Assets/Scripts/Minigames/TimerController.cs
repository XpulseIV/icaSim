using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public sealed class TimerController : MonoBehaviour
{
    private IAstarAI _playerAi;
    public GameObject timesUpText;
    public Image foreGroundImage;
    private float _timeRemaining;
    public float maxTime = 5.0f;
    private TimeSpan _timeSpan;


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
            this.foreGroundImage.fillAmount = this._timeRemaining / this.maxTime;
        }
        else
        {
            this.timesUpText.SetActive(true);
            Huh huhh = Camera.main!.GetComponent<Huh>();
            huhh.AI.destination = huhh.goal.position;
            huhh.AI.SearchPath();
            huhh.AI.canMove = true;
            huhh.moveP = false;
        }
    }
}
