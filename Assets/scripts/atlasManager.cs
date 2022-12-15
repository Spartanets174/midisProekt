using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[System.Serializable]
public class atlasManager : MonoBehaviour
{
    public List<InstrumentInfo> instrumentInfo; // список всех инструментов
    public GameObject atlasUI;
    public GameObject instrumentUI;
    public GameObject ItemAtlas;
    public GameObject description;
    public GameObject search;
    private GameObject instrument; // появляющаяся моделька
    private Transform instrumentPos;
    private Animator anim;

    public void instrumentChoose()
    {
        string nameButton = EventSystem.current.currentSelectedGameObject.name; // получаем имя повляющегося инструмента
        for (int i = 0; i < instrumentInfo.Count; i++) // ищем инструментв в листе
        {
            Debug.Log(nameButton);
            if (instrumentInfo[i].instrumentName == nameButton)
            {
                // переключение интерфейса
                atlasUI.SetActive(false);
                instrumentUI.SetActive(true);
                // создание модельки инструмента
                instrument = Instantiate(instrumentInfo[i].instrumentModel) as GameObject;
                Debug.Log("Okay");
                instrument.transform.position = new Vector3(0, 0, 0);
                instrument.transform.rotation = new Quaternion(0, 0, 0, 0);
                instrumentPos = ItemAtlas.transform;
                instrument.transform.SetParent(instrumentPos, false);
                Debug.Log(instrument.transform.position);
                Debug.Log(instrument.transform.position);
                // создание описания инструмента
                description.GetComponentInChildren<Text>().text = instrumentInfo[i].instrumentDescription;
            }
        }
    }

    void Start()
    {
        var input = search.GetComponent<InputField>();
        var se = new InputField.SubmitEvent();
        se.AddListener(SubmitName);
        input.onEndEdit.AddListener(SubmitName);
    }

    private void SubmitName(string arg0)
    {
        for (int i = 0; i < instrumentInfo.Count; i++) // ищем инструментв в листе
        {
            if (instrumentInfo[i].instrumentName == arg0)
            {
                // переключение интерфейса
                atlasUI.SetActive(false);
                instrumentUI.SetActive(true);
                // создание модельки инструмента
                instrument = Instantiate(instrumentInfo[i].instrumentModel) as GameObject;
                Debug.Log("Okay");
                instrument.transform.position = new Vector3(0, 0, 0);
                instrument.transform.rotation = new Quaternion(0, 0, 0, 0);
                instrumentPos = ItemAtlas.transform;
                instrument.transform.SetParent(instrumentPos, false);
                Debug.Log(instrument.transform.position);
                Debug.Log(instrument.transform.position);
                // создание описания инструмента
                description.GetComponentInChildren<Text>().text = instrumentInfo[i].instrumentDescription;
            }
        }
        arg0 = "";
    }

    public void instrumentAnim()
    {

    }

    public void backToMenu()
    {
        // переключение интерфейса
        atlasUI.SetActive(true);
        instrumentUI.SetActive(false);
        // уничтожение модельки
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