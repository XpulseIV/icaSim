using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPos : MonoBehaviour
{
    public Vector2 clampX;
    public Vector2 clampY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float clampedX = Mathf.Clamp(transform.localPosition.x, clampX.x, clampX.y);
        float clampedY = Mathf.Clamp(transform.localPosition.y, clampY.x, clampY.y);
        transform.localPosition = new Vector3(clampedX, clampedY, transform.localPosition.z);
    }
}
