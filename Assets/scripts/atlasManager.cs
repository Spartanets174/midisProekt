using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[System.Serializable]
public class atlasManager : MonoBehaviour
{
    public List<InstrumentInfo> instrumentInfo; // ������ ���� ������������
    public GameObject atlasUI;
    public GameObject instrumentUI;
    public GameObject ItemAtlas;
    public GameObject description;
    public Text search;
    private GameObject instrument; // ������������ ��������
    private Transform instrumentPos;

    public void instrumentChoose()
    {
        string nameButton = EventSystem.current.currentSelectedGameObject.name; // �������� ��� ������������ �����������
        for (int i = 0; i < instrumentInfo.Count; i++) // ���� ����������� � �����
        {
            Debug.Log(nameButton);
            if (instrumentInfo[i].instrumentName == nameButton)
            {
                // ������������ ����������
                atlasUI.SetActive(false);
                instrumentUI.SetActive(true);
                // �������� �������� �����������
                instrument = Instantiate(instrumentInfo[i].instrumentModel) as GameObject;
                Debug.Log("Okay");
                instrument.transform.position = new Vector3(0, 0, 0);
                instrument.transform.rotation = new Quaternion(0, 0, 0, 0);
                instrumentPos = ItemAtlas.transform;
                instrument.transform.SetParent(instrumentPos, false);
                Debug.Log(instrument.transform.position);
                Debug.Log(instrument.transform.position);
                // �������� �������� �����������
                description.GetComponentInChildren<Text>().text = instrumentInfo[i].instrumentDescription;
            }
        }
    }
    public void backToMenu()
    {
        // ������������ ����������
        atlasUI.SetActive(true);
        instrumentUI.SetActive(false);
        // ����������� ��������
        Destroy(instrument);
    }

   
}

[System.Serializable]
public class InstrumentInfo
{
    public string instrumentName;
    public GameObject instrumentModel;
    public string instrumentDescription;
}