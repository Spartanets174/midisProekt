using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public QuizManager quizManager;
    public bool isCorrect=true;
    public int count = 0;
    public void Answer()
    {      
        if (isCorrect)
        {
            Debug.Log("True");
            quizManager.correct();
        }
        else
        {
            Debug.Log("False");
            quizManager.wrong();
        }
    }
    void OnClick()
    {
        string nameButton = EventSystem.current.currentSelectedGameObject.name;
        for (int i = 0; i < quizManager.options.Length; i++)
        {
            if (quizManager.options[i].name == nameButton)
            {
                quizManager.options[i].transform.GetChild(1).GetComponent<Toggle>().isOn = !quizManager.options[i].transform.GetChild(1).GetComponent<Toggle>().isOn;
            }
        }; 
        
    }
    public void SetTrue()
    {
        OnClick();
    }
}
    