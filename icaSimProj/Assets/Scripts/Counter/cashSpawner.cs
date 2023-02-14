using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cashSpawner : MonoBehaviour
{
   public int number;


    void Update()
    {
        int tensCount = number / 10;
        int twentiesCount = number / 20;
        int fiftiesCount = number / 50;
    }
}
