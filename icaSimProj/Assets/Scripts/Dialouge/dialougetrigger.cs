using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialougetrigger : MonoBehaviour
{
    public Dialouge Dialouge;

    public void TriggerDialogue()
    {
        FindObjectOfType<dialougemanager>().StartDialogue(Dialouge);
    }
}
