using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    
    public List<QuestionsAnsAnswers> QnA;
    public AnswerScript answerScript;

    public GameObject next;
    public GameObject[] options;
    public GameObject[] answers;
    public GameObject[] Toggle;
    public GameObject TextOfQuestion;
    public GameObject QuestionImg;
    public GameObject[] answersImg;
    public int currentQuestion;

    public GameObject Questions;
    public GameObject Results;
    public GameObject QuetionNum;

    public Text QuestionTxt;
    public Text ResultsTxt;

    public int totalQuestions = 0;
    public int rightQuestions;
    public int NumQuestion=0;
    public int count = 0;

    private void Start()
    {
        totalQuestions = QnA.Count;
        Results.SetActive(false);
        generateQuestion();
    }
    void onClick()
    {
        if (QnA.Count > 0)
        {
            if (QnA[currentQuestion].CorrectAnswer.Length == 1)
            {              
                for (int i = 0; i < options.Length; i++)
                {
                    if (options[i].transform.GetChild(1).GetComponent<Toggle>().isOn == true)
                    {
                        count++;
                        if (((options[i].transform.GetChild(1).GetComponent<Toggle>().isOn == true) && (i + 1 == QnA[currentQuestion].CorrectAnswer[0]))&&count==1)
                        {
                            next.GetComponent<AnswerScript>().isCorrect = true;
                        }
                    }
                    //Debug.Log("ѕравильный ли первый: " + options[i].transform.GetChild(1).GetComponent<Toggle>().isOn + " номер первого: " + i + " правильный ответ первого: " + QnA[currentQuestion].CorrectAnswer[0]);                   
                }
            }
            else
            {
               
                for (int i = 0; i < options.Length; i++)
                {
                    count = 0;
                    if (options[i].transform.GetChild(1).GetComponent<Toggle>().isOn == true)
                    {                        
                        if (options[i].transform.GetChild(1).GetComponent<Toggle>().isOn == true && (i + 1 == QnA[currentQuestion].CorrectAnswer[0]))
                        {
                            count++;
                            for (int j = i + 1; j < options.Length; j++)
                            {                              
                                //Debug.Log("ѕравильный ли первый: " + options[i].transform.GetChild(1).GetComponent<Toggle>().isOn + " номер первого: " + i + " правильный ответ первого: " + QnA[currentQuestion].CorrectAnswer[0] + " ѕравильный ли второй " + options[j].transform.GetChild(1).GetComponent<Toggle>().isOn + " номер второго: " + j + " правильный ответ второго: " + QnA[currentQuestion].CorrectAnswer[1]);
                                if (options[j].transform.GetChild(1).GetComponent<Toggle>().isOn == true)
                                {
                                    count++;
                                    if (((options[j].transform.GetChild(1).GetComponent<Toggle>().isOn == true) && (j + 1 == QnA[currentQuestion].CorrectAnswer[1]))&&count==2)
                                    {
                                        next.GetComponent<AnswerScript>().isCorrect = true;
                                    }
                                    else
                                    {
                                        next.GetComponent<AnswerScript>().isCorrect = false;
                                    }
                                }                               
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
        if (QnA[currentQuestion].questionType.ToString() == "TextQuestionAndOption")
        {
            TextOfQuestion.SetActive(false);
            QuestionImg.SetActive(false);   
            for (int i = 0; i < options.Length; i++)
            {
                answersImg[i].SetActive(true);
                answers[i].SetActive(true);
                answersImg[i].GetComponent<Image>().color = new Color(0, 0, 0, 0.75f);
                answersImg[i].GetComponent<Image>().sprite = QnA[currentQuestion].ImgOfQuestion;
                options[i].transform.GetChild(0).GetComponent<Text>().text = $"¬ариант {i + 1}";
                next.GetComponent<AnswerScript>().isCorrect = false;
                answers[i].GetComponent<Text>().text = $"{i+1}. "+QnA[currentQuestion].Answers[i];
            }
        }
        if (QnA[currentQuestion].questionType.ToString() == "WriteWords")
        {
            TextOfQuestion.SetActive(true);
            QuestionImg.SetActive(true);
            QuestionImg.GetComponent<Image>().sprite = QnA[currentQuestion].ImgOfQuestion;
            QuestionImg.GetComponent<Image>().color = new Color(0, 0, 0, 0.75f);
            TextOfQuestion.GetComponent<Text>().text = QnA[currentQuestion].Text;
            for (int i = 0; i < options.Length; i++)
            {
                answersImg[i].SetActive(false);
                answers[i].SetActive(false);
                options[i].transform.GetChild(0).GetComponent<Text>().text = $"{i + 1}. " + $"{QnA[currentQuestion].Answers[i]}";
                next.GetComponent<AnswerScript>().isCorrect = false;
            }
        }
        if (QnA[currentQuestion].questionType.ToString() == "QuestionTextOptionImg")
        {
            TextOfQuestion.SetActive(false);
            QuestionImg.SetActive(false);
            for (int i = 0; i < options.Length; i++)
            {
                answers[i].SetActive(false);
                answersImg[i].SetActive(true);
                answersImg[i].GetComponent<Image>().color = new Color(255, 255, 255, 1);
                answersImg[i].GetComponent<Image>().sprite = QnA[currentQuestion].ImgAnswers[i];
                options[i].transform.GetChild(0).GetComponent<Text>().text = $"¬ариант {i + 1}";
                next.GetComponent<AnswerScript>().isCorrect = false;                
            }
        }
        if (QnA[currentQuestion].questionType.ToString() == "OptionTextQuestionImg")
        {
            TextOfQuestion.SetActive(false);          
            QuestionImg.SetActive(true);
            QuestionImg.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            QuestionImg.GetComponent<Image>().sprite = QnA[currentQuestion].ImgOfQuestion;
            for (int i = 0; i < options.Length; i++)
            {
                answers[i].SetActive(false);
                answersImg[i].SetActive(false);
                options[i].transform.GetChild(0).GetComponent<Text>().text = $"{i + 1}. " + QnA[currentQuestion].Answers[i];
                next.GetComponent<AnswerScript>().isCorrect = false;
            }
        }
    }
    void generateQuestion()
    {
        if (QnA.Count>0)
        {
            currentQuestion = Random.Range(0, QnA.Count);
            QuestionTxt.text = QnA[currentQuestion].Question;
            NumQuestion++;
            count = 0;
            QuetionNum.GetComponent<Text>().text = NumQuestion.ToString();           
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
    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
