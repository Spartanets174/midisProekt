using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionsAnsAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject Questions;
    public GameObject Results;

    public Text QuestionTxt;
    public Text ResultsTxt;

    public int totalQuestions = 0;
    public int rightQuestions;

    private void Start()
    {
        totalQuestions = QnA.Count;
        Results.SetActive(false);
        generateQuestion();
    }
    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
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
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];
            if (QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }
    void generateQuestion()
    {
        if (QnA.Count>0)
        {
            currentQuestion = Random.Range(0, QnA.Count);

            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();
        }
        else
        {
            Debug.Log("Vse");
            GameOver();
        }

         
    }

}
