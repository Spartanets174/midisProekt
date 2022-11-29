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
    //Ссылки на др скрипты
    //public List<QuestionsAnsAnswers> QnA;
    public AnswerScript answerScript;
    public Timer Timer;
    //Ссылки на объекты в юнити
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
    //Переменные для различных целей
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
        //Отключение окна интерфейса с результатами при старте
        Results.SetActive(false);
        //Массив из текстовх вопросов 
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
    //Функция для проверки вопроса на правильность и подсчёта очков
    public void TrueQAndScoresCombo()
    {
        //Для правильного расчёта правильных ответов (используется  в tutorial)
        next.GetComponent<AnswerScript>().isCorrect = false;
        //Считает кол-во нажатых кнопок
        count = 0;
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i].transform.GetChild(1).GetComponent<Toggle>().isOn == true)
            {
                count++;
            }
        }
        //Проверяет есть ли ещё вопросы в тесте
        if (IsAnyQ())
        {
            //Проверка сколько правильных вариантов ответа в тесте (один)
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
    //Скрипт для кнопки назад
    public void Back()
    {
        if (NumQuestion > 1)
        {
            NumQuestion--;
            QuestionTypeCurrent = questionsArray[NumQuestion - 1].questionType;
            currentQuestion = NumQuestion - 1;
            //Для условия вопроса
            QuestionTxt.text = questionsArray[NumQuestion - 1].Question;
            count = 0;
            //Для номера вопроса
            QuetionNum.GetComponent<Text>().text = NumQuestion.ToString();
            //Установление всех checkbox в отключенное состояние
            for (int i = 0; i < options.Length; i++)
            {
                options[i].transform.GetChild(1).GetComponent<Toggle>().isOn = false;                
            }            
            next.GetComponent<AnswerScript>().isCorrect = false;
            SetAnswers();
        }
    }
        //Скрипт когда вопросов не осталось
        void GameOver()
    {
        Questions.SetActive(false);
        Results.SetActive(true);
        ResultsTxt.text = rightQuestions + "/" + totalQuestions;
        ResultsTime.text = $"Время прохождения: {Timer.timerText.text}";
    }
    //Скрипт подсчёта правильных ответов и генерации нового вопроса
    public void correct()
    {
        rightQuestions++;
        generateQuestion();
    }
    //Скрипт для неправильного ответа и генерации нового вопроса
    public void wrong()
    {
        generateQuestion();
    }
    //Скрипт для установки определённых текста/ картинок для каждого типа вопроса
    void SetAnswers()
    {
        //Если в вопросе варианты ответа и сам вопрос текстовые   
        if (QuestionTypeCurrent == QuestionType.TextQuestionAndOption)
        {
            //Строки идущие до цикла устанавливают видимость определённых частей интерфейса в зависимости от его типа, а также меняют картинку, текст и цвет текста
            TextOfQuestion.SetActive(false);
            QuestionImg.SetActive(false);
            //В цикле устанавливают видимость для элементов интерфейса, отвечающих за варианты ответа и задают им картинки/текст/цвет и т.д. в зависимости от типа вопроса
            for (int i = 0; i < options.Length; i++)
            {
                answersImg[i].SetActive(true);
                answers[i].SetActive(true);
                answersImg[i].GetComponent<Image>().color = new Color(0, 0, 0, 0.75f);
                answersImg[i].GetComponent<Image>().sprite = questionsArray[NumQuestion - 1].ImgOfQuestion;
                options[i].transform.GetChild(0).GetComponent<Text>().text = $"Вариант {i + 1}";
                next.GetComponent<AnswerScript>().isCorrect = false;
                answers[i].GetComponent<Text>().text = $"{i + 1}. " + questionsArray[NumQuestion - 1].Answers[i];
            }
        }
        //Если в вопросе нужно дополнить текст недостающими словами
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
        //Если в вопросе сам вопрос текстовый а варианты ответа картинки
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
                options[i].transform.GetChild(0).GetComponent<Text>().text = $"Вариант {i + 1}";
                next.GetComponent<AnswerScript>().isCorrect = false;
            }
        }
        //Если в вопросе сам вопрос картинка а варианты текстовые
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

    //Скрипт для генерации нового вопроса
    void generateQuestion()
    {
        if (totalQuestions > NumQuestion)
        {
            NumQuestion++;
            QuestionTypeCurrent = questionsArray[NumQuestion - 1].questionType;
            currentQuestion = NumQuestion - 1;
            //Для условия вопроса
            QuestionTxt.text = questionsArray[NumQuestion - 1].Question;
            count = 0;
            //Для номера вопроса
            QuetionNum.GetComponent<Text>().text = NumQuestion.ToString();
            //Установление всех checkbox в отключенное состояние
            for (int i = 0; i < options.Length; i++)
            {
                options[i].transform.GetChild(1).GetComponent<Toggle>().isOn = false;
            }
            SetAnswers();
        }
        //Если пользователь ответил на все вопросы
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


    //проверка листов на что-то
    private bool IsAnyQ()
    {
        int numQ = 0;       
        foreach (var pair in dict)
        {
            numQ += pair.Value.list.Count;
        }

        return numQ > 0;
    }
    ////Массив из текстовх вопросов ()
    //public void StartTextQuest()
    //{      
    //    for (int i = 0; i < 10; i++)
    //    {
    //        int NumberTypeQ = Random.Range(0, 2);
    //        questionsArray[i] = dict[(QuestionType)NumberTypeQ].list[Random.Range(0, dict[(QuestionType)NumberTypeQ].list.Count)];
    //    }
    //}
    ////Массив из вопросов с картинками ()
    //public void StartImgQuest()
    //{
    //    for (int i = 0; i < 10; i++)
    //    {
    //        int NumberTypeQ = Random.Range(3, 4);
    //        questionsArray[i] = dict[(QuestionType)NumberTypeQ].list[Random.Range(0, dict[(QuestionType)NumberTypeQ].list.Count)];
    //    }
    //}
    ////Массив из всех вопросов
    //public void StartAllQuest()
    //{       
    //    for (int i = 0; i < 10; i++)
    //    {
    //        int NumberTypeQ = Random.Range(0, 4);
    //        questionsArray[i] = dict[(QuestionType)NumberTypeQ].list[Random.Range(0, dict[(QuestionType)NumberTypeQ].list.Count)];
    //    }
    //}
}
