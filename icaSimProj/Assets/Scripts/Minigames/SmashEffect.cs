using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashEffect : MonoBehaviour
{
    public GameObject Smasheffect;

    void OnMouseDown()
    {
        Destroy(gameObject);
        Instantiate(Smasheffect, transform.position, Quaternion.identity);
    }
   
}
