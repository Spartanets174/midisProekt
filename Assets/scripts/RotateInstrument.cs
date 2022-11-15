using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInstrument : MonoBehaviour
{
    public float rotateSpeed;
    private Transform transformObject;
    // Start is called before the first frame update
    void Start()
    {
        transformObject = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0);
    }
}
