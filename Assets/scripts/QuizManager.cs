using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    //public Dictionary<QuestionType, QuestionS> dict = new Dictionary<QuestionType, QuestionS>();
    public MyDictioanary dict;
    public QuestionsAnsAnswers[] questionsArray = new QuestionsAnsAnswers[10];
    //������ �� �� �������
    //public List<QuestionsAnsAnswers> QnA;
    public AnswerScript answerScript;
    public Timer Timer;
    //������ �� ������� � �����
    public GameObject next;
    public GameObject back;
    public GameObject[] options;
    public GameObject[] answers;
    public GameObject[] Toggle;
    public GameObject TextOfQuestion;
    public GameObject QuestionImg;
    public GameObject[] answersImg;
    public int currentQuestion;
    public QuestionType QuestionTypeCurrent;

    public GameObject Questions;
    public GameObject Results;
    public GameObject QuetionNum;

    public Text QuestionTxt;
    public Text ResultsTxt;
    public Text ResultsTime;
    //���������� ��� ��������� �����
    public int NumberTypeQ;
    public int NumberQuestion;
    public int totalQuestions = 0;
    public int rightQuestions;
    public int NumQuestion = 0;
    public int count = 0;


    private void Start()
    {
        totalQuestions = 10;
        Debug.Log($"{SetQMass.setImage}, {SetQMass.setText}, {dict.Count}");
        //���������� ���� ���������� � ������������ ��� ������
        Results.SetActive(false);
        //������ �� �������� �������� 
        for (int i = 0; i < 10; i++)
        {
            generateQuestionAndType();           
            questionsArray[i] = dict[(QuestionType)NumberTypeQ].list[NumberQuestion];
            dict[questionsArray[i].questionType].list.RemoveAt(NumberQuestion);
        }
        generateQuestion();
    }
    public void generateQuestionAndType()
    {
        if (SetQMass.setText == true && SetQMass.setImage == false)
        {
            NumberTypeQ = Random.Range(0, 2);
            NumberQuestion = Random.Range(0, dict[(QuestionType)NumberTypeQ].list.Count);
        }
        if (SetQMass.setText == false && SetQMass.setImage == true)
        {
            NumberTypeQ = Random.Range(2, 4);
            NumberQuestion = Random.Range(0, dict[(QuestionType)NumberTypeQ].list.Count);
        }
        if (SetQMass.setText == true && SetQMass.setImage == true)
        {
            NumberTypeQ = Random.Range(0, 4);
            NumberQuestion = Random.Range(0, dict[(QuestionType)NumberTypeQ].list.Count);
        }
        if (dict[(QuestionType)NumberTypeQ].list.Count == 0)
        {
            generateQuestionAndType();
        }
    }
    //������� ��� �������� ������� �� ������������ � �������� �����
    public void TrueQAndScoresCombo()
    {
        //��� ����������� ������� ���������� ������� (������������  � tutorial)
        next.GetComponent<AnswerScript>().isCorrect = false;
        //������� ���-�� ������� ������
        count = 0;
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i].transform.GetChild(1).GetComponent<Toggle>().isOn == true)
            {
                count++;
            }
        }
        //��������� ���� �� ��� ������� � �����
        if (IsAnyQ())
        {
            //�������� ������� ���������� ��������� ������ � ����� (����)
            if (questionsArray[NumQuestion - 1].CorrectAnswer.Length == 1)
            {
                if (options[questionsArray[NumQuestion - 1].CorrectAnswer[0]-1].transform.GetChild(1).GetComponent<Toggle>().isOn == true&&count==1)
                {                    
                    next.GetComponent<AnswerScript>().isCorrect = true;
                }
                else
                {
                    next.GetComponent<AnswerScript>().isCorrect = false;
                }
            }
            else
            {
                if (options[questionsArray[NumQuestion - 1].CorrectAnswer[0] - 1].transform.GetChild(1).GetComponent<Toggle>().isOn == true && options[questionsArray[NumQuestion - 1].CorrectAnswer[1] - 1].transform.GetChild(1).GetComponent<Toggle>().isOn == true && count == 2)
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
    //������ ��� ������ �����
    public void Back()
    {
        if (NumQuestion > 1)
        {
            NumQuestion--;
            QuestionTypeCurrent = questionsArray[NumQuestion - 1].questionType;
            currentQuestion = NumQuestion - 1;
            //��� ������� �������
            QuestionTxt.text = questionsArray[NumQuestion - 1].Question;
            count = 0;
            //��� ������ �������
            QuetionNum.GetComponent<Text>().text = NumQuestion.ToString();
            //������������ ���� checkbox � ����������� ���������
            for (int i = 0; i < options.Length; i++)
            {
                options[i].transform.GetChild(1).GetComponent<Toggle>().isOn = false;                
            }            
            next.GetComponent<AnswerScript>().isCorrect = false;
            SetAnswers();
        }
    }
        //������ ����� �������� �� ��������
        void GameOver()
    {
        Questions.SetActive(false);
        Results.SetActive(true);
        ResultsTxt.text = rightQuestions + "/" + totalQuestions;
        ResultsTime.text = $"����� �����������: {Timer.timerText.text}";
    }
    //������ �������� ���������� ������� � ��������� ������ �������
    public void correct()
    {
        rightQuestions++;
        generateQuestion();
    }
    //������ ��� ������������� ������ � ��������� ������ �������
    public void wrong()
    {
        generateQuestion();
    }
    //������ ��� ��������� ����������� ������/ �������� ��� ������� ���� �������
    void SetAnswers()
    {
        //���� � ������� �������� ������ � ��� ������ ���������   
        if (QuestionTypeCurrent == QuestionType.TextQuestionAndOption)
        {
            //������ ������ �� ����� ������������� ��������� ����������� ������ ���������� � ����������� �� ��� ����, � ����� ������ ��������, ����� � ���� ������
            TextOfQuestion.SetActive(false);
            QuestionImg.SetActive(false);
            //� ����� ������������� ��������� ��� ��������� ����������, ���������� �� �������� ������ � ������ �� ��������/�����/���� � �.�. � ����������� �� ���� �������
            for (int i = 0; i < options.Length; i++)
            {
                answersImg[i].SetActive(true);
                answers[i].SetActive(true);
                answersImg[i].GetComponent<Image>().color = new Color(0, 0, 0, 0.75f);
                answersImg[i].GetComponent<Image>().sprite = questionsArray[NumQuestion - 1].ImgOfQuestion;
                options[i].transform.GetChild(0).GetComponent<Text>().text = $"������� {i + 1}";
                next.GetComponent<AnswerScript>().isCorrect = false;
                answers[i].GetComponent<Text>().text = $"{i + 1}. " + questionsArray[NumQuestion - 1].Answers[i];
            }
        }
        //���� � ������� ����� ��������� ����� ������������ �������
        if (QuestionTypeCurrent == QuestionType.WriteWords)
        {
            TextOfQuestion.SetActive(true);
            QuestionImg.SetActive(true);
            QuestionImg.GetComponent<Image>().sprite = questionsArray[NumQuestion - 1].ImgOfQuestion;
            QuestionImg.GetComponent<Image>().color = new Color(0, 0, 0, 0.75f);
            TextOfQuestion.GetComponent<Text>().text = questionsArray[NumQuestion - 1].Text;
            for (int i = 0; i < options.Length; i++)
            {
                answersImg[i].SetActive(false);
                answers[i].SetActive(false);
                options[i].transform.GetChild(0).GetComponent<Text>().text = $"{i + 1}. " + $"{questionsArray[NumQuestion - 1].Answers[i]}";
                next.GetComponent<AnswerScript>().isCorrect = false;
            }
        }
        //���� � ������� ��� ������ ��������� � �������� ������ ��������
        if (QuestionTypeCurrent == QuestionType.QuestionTextOptionImg)
        {
            TextOfQuestion.SetActive(false);
            QuestionImg.SetActive(false);
            for (int i = 0; i < options.Length; i++)
            {
                answers[i].SetActive(false);
                answersImg[i].SetActive(true);
                answersImg[i].GetComponent<Image>().color = new Color(255, 255, 255, 1);
                answersImg[i].GetComponent<Image>().sprite = questionsArray[NumQuestion - 1].ImgAnswers[i];
                options[i].transform.GetChild(0).GetComponent<Text>().text = $"������� {i + 1}";
                next.GetComponent<AnswerScript>().isCorrect = false;
            }
        }
        //���� � ������� ��� ������ �������� � �������� ���������
        if (QuestionTypeCurrent == QuestionType.OptionTextQuestionImg)
        {
            TextOfQuestion.SetActive(false);
            QuestionImg.SetActive(true);
            QuestionImg.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            QuestionImg.GetComponent<Image>().sprite = questionsArray[NumQuestion - 1].ImgOfQuestion;
            for (int i = 0; i < options.Length; i++)
            {
                answers[i].SetActive(false);
                answersImg[i].SetActive(false);
                options[i].transform.GetChild(0).GetComponent<Text>().text = $"{i + 1}. " + questionsArray[NumQuestion - 1].Answers[i];
                next.GetComponent<AnswerScript>().isCorrect = false;
            }
        }
    }

    //������ ��� ��������� ������ �������
    void generateQuestion()
    {
        if (totalQuestions > NumQuestion)
        {
            NumQuestion++;
            QuestionTypeCurrent = questionsArray[NumQuestion - 1].questionType;
            currentQuestion = NumQuestion - 1;
            //��� ������� �������
            QuestionTxt.text = questionsArray[NumQuestion - 1].Question;
            count = 0;
            //��� ������ �������
            QuetionNum.GetComponent<Text>().text = NumQuestion.ToString();
            //������������ ���� checkbox � ����������� ���������
            for (int i = 0; i < options.Length; i++)
            {
                options[i].transform.GetChild(1).GetComponent<Toggle>().isOn = false;
            }
            SetAnswers();
        }
        //���� ������������ ������� �� ��� �������
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


    //�������� ������ �� ���-��
    private bool IsAnyQ()
    {
        int numQ = 0;       
        foreach (var pair in dict)
        {
            numQ += pair.Value.list.Count;
        }

        return numQ > 0;
    }
    ////������ �� �������� �������� ()
    //public void StartTextQuest()
    //{      
    //    for (int i = 0; i < 10; i++)
    //    {
    //        int NumberTypeQ = Random.Range(0, 2);
    //        questionsArray[i] = dict[(QuestionType)NumberTypeQ].list[Random.Range(0, dict[(QuestionType)NumberTypeQ].list.Count)];
    //    }
    //}
    ////������ �� �������� � ���������� ()
    //public void StartImgQuest()
    //{
    //    for (int i = 0; i < 10; i++)
    //    {
    //        int NumberTypeQ = Random.Range(3, 4);
    //        questionsArray[i] = dict[(QuestionType)NumberTypeQ].list[Random.Range(0, dict[(QuestionType)NumberTypeQ].list.Count)];
    //    }
    //}
    ////������ �� ���� ��������
    //public void StartAllQuest()
    //{       
    //    for (int i = 0; i < 10; i++)
    //    {
    //        int NumberTypeQ = Random.Range(0, 4);
    //        questionsArray[i] = dict[(QuestionType)NumberTypeQ].list[Random.Range(0, dict[(QuestionType)NumberTypeQ].list.Count)];
    //    }
    //}
}
