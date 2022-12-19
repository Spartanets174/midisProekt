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
    public GameObject search;
    private GameObject instrument; // ������������ ��������
    private Transform instrumentPos;
    public Animator anim;
    private bool animationCheck = false;

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
                Animator instrumentAnimator = instrument.AddComponent<Animator>() as Animator;
                instrumentAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(nameButton);
                // �������� �������� �����������
                description.GetComponentInChildren<Text>().text = instrumentInfo[i].instrumentDescription;
            }
        }
    }

    public void PlayAnimation()
    {
        anim = instrument.GetComponent<Animator>();
        if (!animationCheck)
        {
            anim.SetTrigger("PlayAnim");
            animationCheck = true;
        }
        else
        {
            anim.ResetTrigger("PlayAnim");
            anim.SetTrigger("PlayAnim");
            animationCheck = true;
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
        for (int i = 0; i < instrumentInfo.Count; i++) // ���� ����������� � �����
        {
            if (instrumentInfo[i].instrumentName == arg0)
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
                Animator instrumentAnimator = instrument.AddComponent<Animator>() as Animator;
                instrumentAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(arg0);
                // �������� �������� �����������
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