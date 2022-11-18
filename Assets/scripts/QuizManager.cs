using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    //������ �� �� �������
    public List<QuestionsAnsAnswers> QnA;
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
    public int NumQuestion=0;
    public int count = 0;
    public int Scores = 0;
    public int Combo = 0;
    public int MaxCombo = 0;
    public int checkScoresForOne = 0;
    public int checkScoresForTwo = 0;
    public bool BScoresForOne = false;
    public bool BScoresForTwo = false;

    private void Start()
    {
        //��������� ������������� ���-�� �������� � ����������� �� ������
        string sName = SceneManager.GetActiveScene().name;
        if(sName == "Tutorial" || sName == "Control")
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
            ResultsTxt.text = $"{rightQuestions} / {NumQuestion-1}";
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
            QnA.RemoveAt(currentQuestion);
        }
        generateQuestion();
    }
    //������ ��� ������������� ������ � ��������� ������ �������
    public void wrong()
    {
        string sName = SceneManager.GetActiveScene().name;
        if (sName == "Tutorial" || sName == "Control")
        {
            QnA.RemoveAt(currentQuestion);
        }
        generateQuestion();
    }
    //������ ��� ��������� ����������� ������/ �������� ��� ������� ���� �������
    void SetAnswers()
    {       
        //���� � ������� �������� ������ � ��� ������ ���������
        if (QnA[currentQuestion].questionType.ToString() == "TextQuestionAndOption")
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
                answersImg[i].GetComponent<Image>().sprite = QnA[currentQuestion].ImgOfQuestion;
                options[i].transform.GetChild(0).GetComponent<Text>().text = $"������� {i + 1}";
                next.GetComponent<AnswerScript>().isCorrect = false;
                answers[i].GetComponent<Text>().text = $"{i+1}. "+QnA[currentQuestion].Answers[i];
            }
        }
        //���� � ������� ����� ��������� ����� ������������ �������
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
        //���� � ������� ��� ������ ��������� � �������� ������ ��������
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
                options[i].transform.GetChild(0).GetComponent<Text>().text = $"������� {i + 1}";
                next.GetComponent<AnswerScript>().isCorrect = false;                
            }
        }
        //���� � ������� ��� ������ �������� � �������� ���������
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
                currentQuestion = Random.Range(0, QnA.Count);
                //��� ������� �������
                QuestionTxt.text = QnA[currentQuestion].Question;
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
            if (Timer._timeLeft >0)
            {
                //������������ �������
                currentQuestion = Random.Range(0, QnA.Count);
                //��� ������� �������
                QuestionTxt.text = QnA[currentQuestion].Question;                
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
    void onClick()
    {
        //��������� ���� �� ��� ������� � �����
        if (QnA.Count > 0)
        {

            //�������� ������� ���������� ��������� ������ � ����� (����)
            if (QnA[currentQuestion].CorrectAnswer.Length == 1)
            {
                //���� �������� ���� ������ � ���������� �������
                for (int i = 0; i < options.Length; i++)
                {
                    //�������� ������� �� checkbox
                    if (options[i].transform.GetChild(1).GetComponent<Toggle>().isOn == true) 
                    { 
                        count++;
                        //�������� ������� �� checkbox � �������� �� �� ���������� �������
                        if (((options[i].transform.GetChild(1).GetComponent<Toggle>().isOn == true) && (i + 1 == QnA[currentQuestion].CorrectAnswer[0])) && count == 1)
                        {
                            //���������� �������� ������ ��� �������� ��� �������� ����� � ��������
                            checkScoresForOne = i;
                            //������ �� ������ ������ ��� ����������� �����
                            BScoresForOne = true; 
                            //�������� true ��� isCorrect � ������� AnswerScript
                            next.GetComponent<AnswerScript>().isCorrect = true;
                        }
                        else
                        {
                            next.GetComponent<AnswerScript>().isCorrect = false;
                            BScoresForOne = false;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                    
                    //Debug.Log("���������� �� ������: " + options[i].transform.GetChild(1).GetComponent<Toggle>().isOn + " ����� �������: " + i + " ���������� ����� �������: " + QnA[currentQuestion].CorrectAnswer[0]);                   
                }
            }
            //�������� ������� ���������� ��������� ������ � ����� (���)
            else
            {
                //���� �������� ���� ������ � ���������� �������
                for (int i = 0; i < options.Length; i++)
                {
                    count = 0;
                    if (options[i].transform.GetChild(1).GetComponent<Toggle>().isOn == true)
                    {
                        next.GetComponent<AnswerScript>().isCorrect = false;
                        BScoresForOne = false;
                        BScoresForTwo = false;
                        if (options[i].transform.GetChild(1).GetComponent<Toggle>().isOn == true && (i + 1 == QnA[currentQuestion].CorrectAnswer[0] || i + 1 == QnA[currentQuestion].CorrectAnswer[1]))
                        {    
                            checkScoresForOne = i;                          
                            BScoresForOne = true;
                            count++;
                            //���� �������� ���� ������ � ���������� ������� ��� �������� ������ ������ �� ������������
                            for (int j = i + 1; j < options.Length; j++)
                            {
                                //Debug.Log("���������� �� ������: " + options[i].transform.GetChild(1).GetComponent<Toggle>().isOn + " ����� �������: " + i + " ���������� ����� �������: " + QnA[currentQuestion].CorrectAnswer[0] + " ���������� �� ������ " + options[j].transform.GetChild(1).GetComponent<Toggle>().isOn + " ����� �������: " + j + " ���������� ����� �������: " + QnA[currentQuestion].CorrectAnswer[1]);
                                if (options[j].transform.GetChild(1).GetComponent<Toggle>().isOn == true)
                                {
                                    count++;
                                    if (((options[j].transform.GetChild(1).GetComponent<Toggle>().isOn == true) && (j + 1 == QnA[currentQuestion].CorrectAnswer[1])) && count == 2)
                                    {
                                        //���������� �������� ������ ��� �������� ��� �������� ����� � ��������
                                        checkScoresForTwo = j;
                                        BScoresForTwo = true;
                                        next.GetComponent<AnswerScript>().isCorrect = true;
                                    }
                                    else
                                    {
                                        next.GetComponent<AnswerScript>().isCorrect = false;
                                        BScoresForTwo = false;
                                    }
                                }
                            }
                        }                       
                    }
                }

            }
        }
    }
    //������ ��� ������ onClick
    public void Checking()
    {
        onClick();
    }
    //������� ��� �������� �����
    public void ScoresAndCombo()
    {
        //������� � ������� �������� ��� ������� � 2 ����������� ��������
        if (QnA[currentQuestion].CorrectAnswer.Length == 2)
        {
            //�������� ����� ������ ������ � �������� �� ��� ������ ����������
            if (options[checkScoresForOne].transform.GetChild(1).GetComponent<Toggle>().isOn == true && options[checkScoresForTwo].transform.GetChild(1).GetComponent<Toggle>().isOn == true&&BScoresForOne&&BScoresForTwo)
            {            
                Combo++;
                //������� ������� �����
                Scores = Scores + 300 * Combo;
            }
            else
            {
                if (options[checkScoresForOne].transform.GetChild(1).GetComponent<Toggle>().isOn == true && BScoresForOne)
                {
                    Combo++;
                    Scores = Scores + 150 * Combo;
                }
                else
                {
                    Combo = 0;
                    Scores = Scores + 0 * Combo;
                }
            }
        }
        //������� � ������� �������� ��� ������� � 1 ����������
        else
        { 
            if (options[checkScoresForOne].transform.GetChild(1).GetComponent<Toggle>().isOn == true && BScoresForOne)
            {
                Combo++;
                Scores = Scores + 300 * Combo;
            }
            else
            {
                Combo = 0;
                Scores = Scores + 0 * Combo;
            }
        }
        BScoresForTwo = false;
        BScoresForOne = false;
        if (MaxCombo <= Combo)
        {
            MaxCombo = Combo;
        }
        //����:{options[checkScoresForOne].transform.GetChild(1).GetComponent<Toggle>().isOn}, ���:{options[checkScoresForTwo].transform.GetChild(1).GetComponent<Toggle>().isOn}
    }
}
