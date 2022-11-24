using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    //public Dictionary<QuestionType, QuestionS> dict = new Dictionary<QuestionType, QuestionS>();
    public MyDictioanary dict;
    //������ �� �� �������
    //public List<QuestionsAnsAnswers> QnA;
    public AnswerScript answerScript;
    public Timer Timer;
    public Menu Menu;
    //������ �� ������� � �����
    public GameObject next;
    public GameObject[] options;
    public GameObject[] answers;
    public GameObject[] Toggle;
    public GameObject TextOfQuestion;
    public GameObject QuestionImg;
    public Text ComboText;
    public Text ScoresText;
    public GameObject[] answersImg;
    public int currentQuestion;
    public QuestionType QuestionTypeCurrent;

    public GameObject Questions;
    public GameObject Results;
    public GameObject QuetionNum;

    public Text QuestionTxt;
    public Text ResultsTxt;
    public Text ResultsTime;
    public Text ComboResultsTxt;
    public Text ScoresResultsTxt;
    //���������� ��� ��������� �����
    public int totalQuestions = 0;
    public int rightQuestions;
    public int NumQuestion = 0;
    public int count = 0;
    public int Scores = 0;
    public int Combo = 0;
    public int MaxCombo = 0;


    private void Start()
    {
        //��������� ������������� ���-�� �������� � ����������� �� ������
        string sName = SceneManager.GetActiveScene().name;
        if (sName == "Tutorial" || sName == "Control")
        {
            totalQuestions = 10;
        }
        //���������� ���� ���������� � ������������ ��� ������
        Results.SetActive(false);
        generateQuestion();
    }
    private void Update()
    {
        string sName = SceneManager.GetActiveScene().name;
        if (sName == "Competitive")
        {
            if (Timer._timeLeft <= 0)
            {
                GameOver();
            }
        }

    }
    //������� ��� �������� ������� �� ������������ � �������� �����
    public void TrueQAndScoresCombo()
    {
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
            if (dict[QuestionTypeCurrent].list[currentQuestion].CorrectAnswer.Length == 1)
            {
                if (options[dict[QuestionTypeCurrent].list[currentQuestion].CorrectAnswer[0]-1].transform.GetChild(1).GetComponent<Toggle>().isOn == true&&count==1)
                {
                    Combo++;
                    Scores = Scores + 300 * Combo;
                    next.GetComponent<AnswerScript>().isCorrect = true;
                }
                else
                {
                    if (count==0)
                    {
                        Combo = 0;
                    }
                    Combo = 0;
                    Scores = Scores + 0 * Combo;
                    next.GetComponent<AnswerScript>().isCorrect = false;
                }
            }
            else
            {
                if (options[dict[QuestionTypeCurrent].list[currentQuestion].CorrectAnswer[0] - 1].transform.GetChild(1).GetComponent<Toggle>().isOn == true && options[dict[QuestionTypeCurrent].list[currentQuestion].CorrectAnswer[1] - 1].transform.GetChild(1).GetComponent<Toggle>().isOn == true && count == 2)
                {
                    Combo++;
                    Scores = Scores + 300 * Combo;
                    next.GetComponent<AnswerScript>().isCorrect = true;
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
                    }               
                }
            }
            if (MaxCombo<=Combo)
            {
                MaxCombo = Combo;
            }
        }
    }
    //������ ����� �������� �� ��������

    //������ ����� �������� �� ��������
    void GameOver()
    {
        Questions.SetActive(false);
        Results.SetActive(true);
        string sName = SceneManager.GetActiveScene().name;
        if (sName == "Tutorial" || sName == "Control")
        {
            ResultsTxt.text = rightQuestions + "/" + totalQuestions;
            ResultsTime.text = $"����� �����������: {Timer.timerText.text}";
        }
        else
        {
            ResultsTxt.text = $"{rightQuestions} / {NumQuestion - 1}";
            ComboResultsTxt.text = $"������������ �����: {MaxCombo}";
            ScoresResultsTxt.text = $"������� �����: {Scores}";
        }
    }
    //������ �������� ���������� ������� � ��������� ������ �������
    public void correct()
    {
        rightQuestions++;
        string sName = SceneManager.GetActiveScene().name;
        //������� ������� ������, ����� �� �� ���������� � �������
        if (sName == "Tutorial" || sName == "Control")
        {
            dict[QuestionTypeCurrent].list.RemoveAt(currentQuestion);
            //QnA.RemoveAt(currentQuestion);


        }
        generateQuestion();
    }
    //������ ��� ������������� ������ � ��������� ������ �������
    public void wrong()
    {
        string sName = SceneManager.GetActiveScene().name;
        if (sName == "Tutorial" || sName == "Control")
        {
            dict[QuestionTypeCurrent].list.RemoveAt(currentQuestion);
            //QnA.RemoveAt(currentQuestion);

        }
        generateQuestion();
    }
    //������ ��� ��������� ����������� ������/ �������� ��� ������� ���� �������
    void SetAnswers()
    {
        //���� � ������� �������� ������ � ��� ������ ���������
        //QnA[currentQuestion].questionType.ToString() == "TextQuestionAndOption"


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

    private bool IsAnyQInType(QuestionType type)
    {
        return dict[type].list.Count > 0;
    }

    //������ ��� ��������� ������ �������
    void generateQuestion()
    {
        string sName = SceneManager.GetActiveScene().name;
        //���� ��� ���� ������� � �������
        if (sName == "Tutorial" || sName == "Control")
        {
            if (totalQuestions > NumQuestion)
            {
                //������������ �������


                QuestionTypeCurrent = (QuestionType)Random.Range(0, dict.Count);

                //!!! - - - �������� - - - !!!
                if (!IsAnyQInType(QuestionTypeCurrent))
                {
                    generateQuestion();
                }

                currentQuestion = Random.Range(0, dict[QuestionTypeCurrent].list.Count);
                //��� ������� �������
                QuestionTxt.text = dict[QuestionTypeCurrent].list[currentQuestion].Question;
                NumQuestion++;
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
            else
            {
                Debug.Log("Vse");
                GameOver();
            }
        }
        else
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
            else
            {
                Debug.Log("Vse");
                GameOver();
            }
        }
        //���� ������������ ������� �� ��� �������

    }
    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    //��������� ���������� �� ������ ������ ������������ ��� ������ ����� �� ������ � ���������� true ��� false ��� ������� Answer � AnswerScript


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