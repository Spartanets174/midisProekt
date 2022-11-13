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
    //Скрипт для проверки правильно ли пользователь отвтил на вопрос или нет при нажатии на стрелку далее
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
    //Скрипт для включения или отклоючения checkbox'a в нажатой кнопки с помощбю метода onClick на кнопках с вариантами ответов
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
    //Скрипт для вызова OnClick
    public void SetTrue()
    {
        OnClick();
    }
}
    