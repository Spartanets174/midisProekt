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
    public GameObject questionForResults;
    public Transform Content;
    public Text ComboResultsTxt;
    public Text ScoresResultsTxt;
    public Text Grade;

    public Text QuestionTxt;
    public Text ResultsTxt;
    public Text ResultsTime;
    questionHolder questionHolder;
    //���������� ��� ��������� �����
     int NumberTypeQ;
     int NumberQuestion;
     int totalQuestions = 0;
     int rightQuestions;
     int NumQuestion = 0;
     int count = 0;
    //��� ������
    List<ResultQuestion> resultsQuestions = new List<ResultQuestion>();
    bool isRightQuestion;
    string userAnswer = "";

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
        userAnswer = "";
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i].transform.GetChild(1).GetComponent<Toggle>().isOn == true)
            {
                count++;
                userAnswer += $"{i + 1} ";
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
                    isRightQuestion = true;
                }
                else
                {
                    next.GetComponent<AnswerScript>().isCorrect = false;
                    isRightQuestion = false;
                }
            }
            else
            {
                if (options[questionsArray[NumQuestion - 1].CorrectAnswer[0] - 1].transform.GetChild(1).GetComponent<Toggle>().isOn == true && options[questionsArray[NumQuestion - 1].CorrectAnswer[1] - 1].transform.GetChild(1).GetComponent<Toggle>().isOn == true && count == 2)
                {
                    next.GetComponent<AnswerScript>().isCorrect = true;
                    isRightQuestion = true;
                }
                else
                {
                    next.GetComponent<AnswerScript>().isCorrect = false;
                    isRightQuestion = false;
                }
            }
        }
    }
    //������ ��� ������ �����
    public void Back()
    {
        if (NumQuestion > 1)
        {
            Debug.Log($"{resultsQuestions.Count}, {NumQuestion}");
            resultsQuestions.Remove(resultsQuestions[NumQuestion - 2]);
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
    public void forResults()
    {
        string right = "";
        for (int i = 0; i < questionsArray[NumQuestion - 1].CorrectAnswer.Length; i++)
        {
            right += $"{questionsArray[NumQuestion - 1].CorrectAnswer[i].ToString()} ";
        }
        resultsQuestions.Add(new ResultQuestion
        {
            textQuestion = questionsArray[NumQuestion - 1].Question,
            isRight = isRightQuestion,
            userAns = userAnswer,
            rightAns = right,
        });
    }
    //������ ����� �������� �� ��������
    public void GameOver()
    {
        Questions.SetActive(false);
        Results.SetActive(true);
        for (int i = 0; i < resultsQuestions.Count; i++)
        {
            if (resultsQuestions[i].isRight)
            {
                rightQuestions++;
            }
        }
        ResultsTxt.text = $"�� �������� ��������� �� {rightQuestions} �������� �� {NumQuestion}";

        ResultsTime.text = $"����� �� �����������: {Timer.timerText.text}";
        Grade.text = Mathf.Round(rightQuestions / 2).ToString();
        ComboResultsTxt.text = $"{SetQMass.uName}";
        ScoresResultsTxt.text = $"{SetQMass.uSecondName}";
        for (int i = 0; i < resultsQuestions.Count; i++)
        {
            questionForResults.GetComponent<questionHolder>().Question.text = resultsQuestions[i].textQuestion;
            if (resultsQuestions[i].isRight)
            {
                questionForResults.GetComponent<questionHolder>().isRight.image.sprite = questionForResults.GetComponent<questionHolder>().right;
                questionForResults.GetComponent<questionHolder>().isRight.interactable = false;
            }
            else
            {
                questionForResults.GetComponent<questionHolder>().isRight.image.sprite = questionForResults.GetComponent<questionHolder>().wrong;
                questionForResults.GetComponent<questionHolder>().isRight.interactable = true;
            }
            Debug.Log($"{resultsQuestions[i].textQuestion}, {resultsQuestions[i].isRight}, {resultsQuestions[i].userAns}, {resultsQuestions[i].rightAns}");
            if (resultsQuestions[i].userAns == "")
            {
                questionForResults.GetComponent<questionHolder>().userAns.text = $"�������: ������";
            }
            else
            {
                questionForResults.GetComponent<questionHolder>().userAns.text = $"�������: {resultsQuestions[i].userAns}";
            }
            Debug.Log($"{resultsQuestions[i].textQuestion}, {resultsQuestions[i].isRight}, {resultsQuestions[i].userAns}, {resultsQuestions[i].rightAns}");
            questionForResults.GetComponent<questionHolder>().rightAnsText.text = $"���������� �����: {resultsQuestions[i].rightAns}";
            Debug.Log($"{resultsQuestions[i].textQuestion}, {resultsQuestions[i].isRight}, {resultsQuestions[i].userAns}, {resultsQuestions[i].rightAns}");
            Instantiate(questionForResults, Vector3.zero, Quaternion.identity, Content);
        }
    }
    //������ �������� ���������� ������� � ��������� ������ �������
    public void correct()
    {
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
}
