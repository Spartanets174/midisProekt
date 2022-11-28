using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnswerScript : MonoBehaviour
{
    public QuizManager quizManager;
    public QManagerCompetitive QMCompetitive;
    public bool isCorrect = true;
    public int count = 0;
    public void Answer()
    {
        string sName = SceneManager.GetActiveScene().name;
        if (isCorrect)
        {
            Debug.Log("True");
            if (sName == "Competitive")
            {
                QMCompetitive.correct();
            }
            else
            {
                quizManager.correct();
            }
            
        }
        else
        {
            Debug.Log("False");
            if (sName == "Competitive")
            {
                QMCompetitive.wrong();
            }
            else
            {
                quizManager.wrong();
            }
        }
    }
    void OnClick()
    {
        string sName = SceneManager.GetActiveScene().name;
        string nameButton = EventSystem.current.currentSelectedGameObject.name;
        if (sName == "Competitive")
        {
            for (int i = 0; i < QMCompetitive.options.Length; i++)
            {
                if (QMCompetitive.options[i].name == nameButton)
                {
                    QMCompetitive.options[i].transform.GetChild(1).GetComponent<Toggle>().isOn = !QMCompetitive.options[i].transform.GetChild(1).GetComponent<Toggle>().isOn;
                }
            };
        }
        else
        {
            for (int i = 0; i < quizManager.options.Length; i++)
            {
                if (quizManager.options[i].name == nameButton)
                {
                    quizManager.options[i].transform.GetChild(1).GetComponent<Toggle>().isOn = !quizManager.options[i].transform.GetChild(1).GetComponent<Toggle>().isOn;
                }
            };
        }      
    }
    public void SetTrue()
    {
        OnClick();
    }
}