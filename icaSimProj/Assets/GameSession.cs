using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public sealed class GameSession : MonoBehaviour
{
    public int money;
    private void Awake()
    {
        int numSessions = Object.FindObjectsOfType<GameSession>().Length;

        if (numSessions > 1)
        {
            Object.Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
