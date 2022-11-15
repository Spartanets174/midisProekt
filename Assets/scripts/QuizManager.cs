using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionsAnsAnswers> QnA;
    public AnswerScript answerScript;

<<<<<<< HEAD
=======
    public Menu Menu;
    //Ссылки на объекты в юнити
>>>>>>> parent of f4f9e31 (РўР°Р№РјРµСЂ)
    public GameObject next;
    public GameObject[] options;
    public GameObject[] answers;
    public GameObject[] Toggle;
    public int currentQuestion;

    public GameObject Questions;
    public GameObject Results;
    public GameObject QuetionNum;

    public Text QuestionTxt;
    public Text ResultsTxt;

    public int totalQuestions = 0;
    public int rightQuestions;
    public int NumQuestion=0;
    public bool[] isRightOption = new bool[4];

    private void Start()
    {
<<<<<<< HEAD
        totalQuestions = QnA.Count;
        Results.SetActive(false);
        generateQuestion();
    }
    private void Update()
    {
       
    }
    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
=======
        if(Menu.CompetitiveQue == true)
        {
            totalQuestions = QnA.Count;
        }

        if(Menu.ControlAndStudy == true)
        {
            totalQuestions = 10;
        }
        
        //Отключение окна интерфейса с результатами при старте
        Results.SetActive(false);
        generateQuestion();
        Debug.Log(Menu.CompetitiveQue);
        Debug.Log(Menu.ControlAndStudy);

>>>>>>> parent of f4f9e31 (РўР°Р№РјРµСЂ)
    }

    void onClick()
    {
        if (QnA.Count > 0)
        {
            if (QnA[currentQuestion].CorrectAnswer.Length == 1)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    Debug.Log("Правильный ли первый: " + options[i].transform.GetChild(1).GetComponent<Toggle>().isOn + " номер первого: " + i + " правильный ответ первого: " + QnA[currentQuestion].CorrectAnswer[0]);
                    if ((options[i].transform.GetChild(1).GetComponent<Toggle>().isOn == true) && (i + 1 == QnA[currentQuestion].CorrectAnswer[0]))
                    {
                        next.GetComponent<AnswerScript>().isCorrect = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < options.Length; i++)
                {
                    if (options[i].transform.GetChild(1).GetComponent<Toggle>().isOn == true && (i + 1 == QnA[currentQuestion].CorrectAnswer[0]))
                    {
                        for (int j = i + 1; j < options.Length; j++)
                        {
                            Debug.Log("Правильный ли первый: " + options[i].transform.GetChild(1).GetComponent<Toggle>().isOn + " номер первого: " + i + " правильный ответ первого: " + QnA[currentQuestion].CorrectAnswer[0] + " Правильный ли второй " + options[j].transform.GetChild(1).GetComponent<Toggle>().isOn + " номер второго: " + j + " правильный ответ второго: " + QnA[currentQuestion].CorrectAnswer[1]);
                            if ((options[j].transform.GetChild(1).GetComponent<Toggle>().isOn == true) && (j + 1 == QnA[currentQuestion].CorrectAnswer[1]))
                            {
                                next.GetComponent<AnswerScript>().isCorrect = true;
                            }
                        }
                    }
                }
            }
        }      
    }
    public void Checking() 
    {
        onClick();
    }

     void GameOver()
    {
        Questions.SetActive(false); 
        Results.SetActive(true);
        ResultsTxt.text = rightQuestions + "/" + totalQuestions;
    }

    public void correct()
    {
        rightQuestions++;
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }
    
    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            next.GetComponent<AnswerScript>().isCorrect = false;
            answers[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];  
        }
    }
    void generateQuestion()
    {
        if (QnA.Count>0)
        {
            currentQuestion = Random.Range(0, QnA.Count);
            QuestionTxt.text = QnA[currentQuestion].Question;
            NumQuestion++;
            QuetionNum.GetComponent<Text>().text = NumQuestion.ToString();
            for (int i = 0; i < isRightOption.Length; i++)
            {
                isRightOption[i] = false;
            }
            for (int i = 0; i < options.Length; i++)
            {
               options[i].transform.GetChild(1).GetComponent<Toggle>().isOn = false;
            }
            SetAnswers();
        }
        else
        {
            Debug.Log("Vse");
            GameOver();
        }


    }

    public enum QuestionType 
    { 
        TextAnswerAndOption,
        TextAnswerOptionImg,
        ImgAnswerOptionText
    }

}
