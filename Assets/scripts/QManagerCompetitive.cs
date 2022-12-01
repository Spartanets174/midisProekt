using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QManagerCompetitive : MonoBehaviour
{
    //������ �� ������� � ���������
    public MyDictioanary dict; 
    //������ �� �� �������
    public AnswerScript answerScript;
    public Timer Timer;
    //������ �� ������� � �����
    public GameObject next;
    public GameObject[] options;
    public GameObject[] answers;
    public GameObject[] Toggle;
    public GameObject TextOfQuestion;
    public GameObject QuestionImg;
    public Text ComboText;
    public Text ScoresText;
    public Text QuestionTxt;
    public GameObject[] answersImg;
    public int currentQuestion;   
    public QuestionType QuestionTypeCurrent;

    public GameObject Questions;
    public GameObject Results;
    public GameObject QuetionNum;
 
    public Text ResultsTxt;
    public Text ComboResultsTxt;
    public Text ScoresResultsTxt;
    public Text Grade;
    public Transform Content;
    public GameObject questionForResults;
    public bool over=false;
    //���������� ��� ��������� �����
    int rightQuestions;
    int NumQuestion = 0;
    int count = 0;
    int Scores = 0;
    int Combo = 0;
    int MaxCombo = 0;
    //��� �����������
    List<ResultQuestion> resultsQuestions = new List<ResultQuestion>();
    questionHolder questionHolder;
    bool isRightQuestion;
    string userAnswer="";
    

    private void Start()
    {
        Results.SetActive(false);
        resultsQuestions.Clear();
        generateQuestion();
    }
    private void Update()
    {
       if (Timer._timeLeft <= 0 && over==false)
         {
            Debug.Log(over);
            over = true;
            GameOver();          
         }
    }
    //������� ��� �������� ������� �� ������������ � �������� �����
    public void TrueQAndScoresCombo()
    {
        userAnswer = "";
        count = 0;
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i].transform.GetChild(1).GetComponent<Toggle>().isOn == true)
            {
                count++;
                userAnswer +=$"{i+1} ";
            }
        }
        //��������� ���� �� ��� ������� � �����
        if (IsAnyQ())
        {
            //�������� ������� ���������� ��������� ������ � ����� (����)
            if (dict[QuestionTypeCurrent].list[currentQuestion].CorrectAnswer.Length == 1)
            {
                if (options[dict[QuestionTypeCurrent].list[currentQuestion].CorrectAnswer[0] - 1].transform.GetChild(1).GetComponent<Toggle>().isOn == true && count == 1)
                {
                    Combo++;
                    Scores = Scores + 300 * Combo;
                    next.GetComponent<AnswerScript>().isCorrect = true;
                    isRightQuestion = true;
                }
                else
                {
                    if (count == 0)
                    {
                        Combo = 0;
                    }
                    Combo = 0;
                    Scores = Scores + 0 * Combo;
                    next.GetComponent<AnswerScript>().isCorrect = false;
                    isRightQuestion = false;
                }
            }
            else
            {
                if (options[dict[QuestionTypeCurrent].list[currentQuestion].CorrectAnswer[0] - 1].transform.GetChild(1).GetComponent<Toggle>().isOn == true && options[dict[QuestionTypeCurrent].list[currentQuestion].CorrectAnswer[1] - 1].transform.GetChild(1).GetComponent<Toggle>().isOn == true && count == 2)
                {
                    Combo++;
                    Scores = Scores + 300 * Combo;
                    next.GetComponent<AnswerScript>().isCorrect = true;
                    isRightQuestion = true;
                }
                else
                {
                    if ((options[dict[QuestionTypeCurrent].list[currentQuestion].CorrectAnswer[0] - 1].transform.GetChild(1).GetComponent<Toggle>().isOn == true || options[dict[QuestionTypeCurrent].list[currentQuestion].CorrectAnswer[1] - 1].transform.GetChild(1).GetComponent<Toggle>().isOn == true) && count == 1)
                    {
                        Combo++;
                        Scores = Scores + 150 * Combo;
                    }
                    else
                    {
                        if (count == 0)
                        {
                            Combo = 0;
                        }
                        Combo = 0;
                        Scores = Scores + 0 * Combo;
                        next.GetComponent<AnswerScript>().isCorrect = false;
                        isRightQuestion = false;
                    }
                }
            }
            if (MaxCombo <= Combo)
            {
                MaxCombo = Combo;
            }
        }
    }
    //������ ��� ������
    public void forResults()
    {
        string right="";
        for (int i = 0; i < dict[QuestionTypeCurrent].list[currentQuestion].CorrectAnswer.Length; i++)
        {
            right += $"{dict[QuestionTypeCurrent].list[currentQuestion].CorrectAnswer[i].ToString()} ";
        }       
        resultsQuestions.Add(new ResultQuestion
        {
            textQuestion = dict[QuestionTypeCurrent].list[currentQuestion].Question,            
            isRight = isRightQuestion,
            userAns = userAnswer,
            rightAns = right,
        });
        Debug.Log($"{resultsQuestions[resultsQuestions.Count-1].textQuestion}, {resultsQuestions[resultsQuestions.Count-1].isRight}, {resultsQuestions[resultsQuestions.Count-1].userAns}, {resultsQuestions[resultsQuestions.Count-1].rightAns}, {resultsQuestions.Count}");
    }

    //������ ����� �������� �� ��������
    public void GameOver()
    {
        Questions.SetActive(false);
        Results.SetActive(true);
        ResultsTxt.text = $"�� �������� ��������� �� {rightQuestions} �������� �� {NumQuestion - 1}";
        ComboResultsTxt.text = $"�����: {MaxCombo}";
        ScoresResultsTxt.text = $"������� �����: {Scores}";
        
        
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
                answersImg[i].GetComponent<Image>().sprite = dict[QuestionTypeCurrent].list[currentQuestion].ImgOfQuestion;
                options[i].transform.GetChild(0).GetComponent<Text>().text = $"������� {i + 1}";
                next.GetComponent<AnswerScript>().isCorrect = false;
                answers[i].GetComponent<Text>().text = $"{i + 1}. " + dict[QuestionTypeCurrent].list[currentQuestion].Answers[i];
            }
        }
        //���� � ������� ����� ��������� ����� ������������ �������
        if (QuestionTypeCurrent == QuestionType.WriteWords)
        {
            TextOfQuestion.SetActive(true);
            QuestionImg.SetActive(true);
            QuestionImg.GetComponent<Image>().sprite = dict[QuestionTypeCurrent].list[currentQuestion].ImgOfQuestion;
            QuestionImg.GetComponent<Image>().color = new Color(0, 0, 0, 0.75f);
            TextOfQuestion.GetComponent<Text>().text = dict[QuestionTypeCurrent].list[currentQuestion].Text;
            for (int i = 0; i < options.Length; i++)
            {
                answersImg[i].SetActive(false);
                answers[i].SetActive(false);
                options[i].transform.GetChild(0).GetComponent<Text>().text = $"{i + 1}. " + $"{dict[QuestionTypeCurrent].list[currentQuestion].Answers[i]}";
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
                answersImg[i].GetComponent<Image>().sprite = dict[QuestionTypeCurrent].list[currentQuestion].ImgAnswers[i];
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
            QuestionImg.GetComponent<Image>().sprite = dict[QuestionTypeCurrent].list[currentQuestion].ImgOfQuestion;
            for (int i = 0; i < options.Length; i++)
            {
                answers[i].SetActive(false);
                answersImg[i].SetActive(false);
                options[i].transform.GetChild(0).GetComponent<Text>().text = $"{i + 1}. " + dict[QuestionTypeCurrent].list[currentQuestion].Answers[i];
                next.GetComponent<AnswerScript>().isCorrect = false;
            }
        }
    }
    void generateQuestion()
    {
        string sName = SceneManager.GetActiveScene().name;
        if (sName == "Competitive")
        {
            if (Timer._timeLeft > 0)
            {
                //������������ �������
                QuestionTypeCurrent = (QuestionType)Random.Range(0, dict.Count);
                currentQuestion = Random.Range(0, dict[QuestionTypeCurrent].list.Count);
                //��� ������� �������
                QuestionTxt.text = dict[QuestionTypeCurrent].list[currentQuestion].Question;
                NumQuestion++;
                count = 0;
                //��� ������ �������
                QuetionNum.GetComponent<Text>().text = NumQuestion.ToString();
                //���������� �������� ��� ����� � �����
                ScoresText.text = Scores.ToString();
                ComboText.text = Combo.ToString() + "x";
                //������������ ���� checkbox � ����������� ���������
                for (int i = 0; i < options.Length; i++)
                {
                    options[i].transform.GetChild(1).GetComponent<Toggle>().isOn = false;
                }
                SetAnswers();
            }
            //���� ������������ ������� �� ��� ������� ��� ��������� �����
            else
            {
                Debug.Log("Vse");
                GameOver();
            }
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
