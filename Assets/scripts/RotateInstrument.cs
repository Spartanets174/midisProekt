using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInstrument : MonoBehaviour
{
    public float rotateSpeed; // скорость вращения мышкой
    public float idleRotateSpeed; // скорость вращение в АФК
    public Transform transformObject;
    public float idle_lim; // время до входа в АФК
    private Quaternion originalPos; // изначальное положение объекта
    float last_ui = 0.0f;
    bool idle = true; // текущий статус idle

    void Start()
    {
        originalPos = transform.rotation; // запоминание изначального положения объекта
    }

    void Update()
    {
        RotateObject(); // вращение объекта мышью
        FixedUpdate(); // вход/выход из АФК
    }
    
    private void RotateObject()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {
            transformObject.Rotate(
                new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotateSpeed, Space.World
            );
        }
    }

    private void FixedUpdate()
    {
        if (Input.anyKeyDown) {
            if (idle)
            {
                idle = false;
                // выход из АФК
                transform.Rotate(0, 0, 0);
            }
            last_ui = Time.time;
        }
        if ((Time.time - last_ui) > idle_lim || idle)
        {
            // возвращение объекта в изначальное положение
            if (!idle)
            {
                transform.rotation = originalPos;
            }
            idle = true;
            // вход в АФК
            transform.Rotate(0, idleRotateSpeed, 0);
        }
    }
}
