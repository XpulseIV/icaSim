using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class dialougetrigger : MonoBehaviour
{
    public Dialouge Dialouge;
    public dialougemanager Dialougemanager;
    public bool triggeredDialouge = false;

    public void SetD(Dialouge dialouge)
    {
        this.Dialouge = dialouge;
    }

    public void DialogStart()
    {
        this.triggeredDialouge = true;
        this.Dialougemanager.StartDialogue(this.Dialouge);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && this.triggeredDialouge)
        {
            this.Dialougemanager.DisplayNextSentence();
        }
    }
}
