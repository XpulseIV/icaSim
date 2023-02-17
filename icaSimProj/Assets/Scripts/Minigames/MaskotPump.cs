using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

    


public class MaskotPump : MonoBehaviour
{

    [Header("Custom Event")]
    public UnityEvent myEvents;
    public int maxPumps = 6;
    int numPumps = 0;
    public GameObject canvas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(myEvents == null)
        {
            print("MaskotPump was triggered but myEvents was null");
        }
        else
        {
            print("MaskotPump Activated. Triggering" + myEvents);
            myEvents.Invoke();
            numPumps++;
            if ( numPumps >= maxPumps)
            {
                canvas.SetActive(true);
            }
        }
    }
   

}
