using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashEffect : MonoBehaviour
{

    public GameObject Smasheffect;
    public AudioSource aas;
    public GameObject

    void OnMouseDown()
    {
        aas.Play();

        Destroy(this.gameObject);
        Object.Instantiate(this.Smasheffect, transform.position, Quaternion.identity);
    }
}
