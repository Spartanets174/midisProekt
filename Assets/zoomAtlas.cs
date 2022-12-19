using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomAtlas : MonoBehaviour
{
    public float zoomChange;
    public float smoothChange;
    public float minSize, maxSize;
    private Camera cam;
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    private void Update()
    {
        if (Input.mouseScrollDelta.y>0 && cam.fieldOfView > 20)
        {
            cam.fieldOfView-= zoomChange * Time.deltaTime * smoothChange;
        }
        if (Input.mouseScrollDelta.y < 0 && cam.fieldOfView <60)
        {
            cam.fieldOfView += zoomChange * Time.deltaTime * smoothChange;
        }
    }
}
