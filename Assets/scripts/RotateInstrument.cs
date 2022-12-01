using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInstrument : MonoBehaviour
{
    public float rotateSpeed; // �������� �������� ������
    public float idleRotateSpeed; // �������� �������� � ���
    public Transform transformObject;
    public float idle_lim; // ����� �� ����� � ���
    private Quaternion originalPos; // ����������� ��������� �������
    float last_ui = 0.0f;
    bool idle = true; // ������� ������ idle

    void Start()
    {
        originalPos = transform.rotation; // ����������� ������������ ��������� �������
    }

    void Update()
    {
        RotateObject(); // �������� ������� �����
        FixedUpdate(); // ����/����� �� ���
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
                // ����� �� ���
                transform.Rotate(0, 0, 0);
            }
            last_ui = Time.time;
        }
        if ((Time.time - last_ui) > idle_lim || idle)
        {
            // ����������� ������� � ����������� ���������
            if (!idle)
            {
                transform.rotation = originalPos;
            }
            idle = true;
            // ���� � ���
            transform.Rotate(0, idleRotateSpeed, 0);
        }
    }
}
