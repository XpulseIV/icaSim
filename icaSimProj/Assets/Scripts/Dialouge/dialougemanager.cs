using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialougemanager : MonoBehaviour
{
    public Queue<string> sentences;
    
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }


}