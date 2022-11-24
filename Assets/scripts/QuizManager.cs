using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    //public Dictionary<QuestionType, QuestionS> dict = new Dictionary<QuestionType, QuestionS>();
    public MyDictioanary dict;
    //Ссылки на др скрипты
    //public List<QuestionsAnsAnswers> QnA;
    public AnswerScript answerScript;
    public Timer Timer;
    public Menu Menu;
    //Ссылки на объекты в юнити
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
    //Переменные для различных целей
    public int totalQuestions = 0;
    public int rightQuestions;
    public int NumQuestion = 0;
    public int count = 0;
    public int Scores = 0;
    public int Combo = 0;
    public int MaxCombo = 0;


    private void Start()
    {
        //Установка максимального кол-ва вопросов в зависимости от режима
        string sName = SceneManager.GetActiveScene().name;
        if (sName == "Tutorial" || sName == "Control")
        {
            totalQuestions = 10;
        }
        //Отключение окна интерфейса с результатами при старте
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
    //Функция для проверки вопроса на правильность и подсчёта очков
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
        //Проверяет есть ли ещё вопросы в тесте
        if (IsAnyQ())
        {
            //Проверка сколько правильных вариантов ответа в тесте (один)
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
    //Скрипт когда вопросов не осталось

    //Скрипт когда вопросов не осталось
    void GameOver()
    {
        Questions.SetActive(false);
        Results.SetActive(true);
        string sName = SceneManager.GetActiveScene().name;
        if (sName == "Tutorial" || sName == "Control")
        {
            ResultsTxt.text = rightQuestions + "/" + totalQuestions;
            ResultsTime.text = $"Время прохождения: {Timer.timerText.text}";
        }
        else
        {
            ResultsTxt.text = $"{rightQuestions} / {NumQuestion - 1}";
            ComboResultsTxt.text = $"Максимальное комбо: {MaxCombo}";
            ScoresResultsTxt.text = $"Набрано очков: {Scores}";
        }
    }
    //Скрипт подсчёта правильных ответов и генерации нового вопроса
    public void correct()
    {
        rightQuestions++;
        string sName = SceneManager.GetActiveScene().name;
        //Удаляет текущий вопрос, чтобы он не повторялся в будущем
        if (sName == "Tutorial" || sName == "Control")
        {
            dict[QuestionTypeCurrent].list.RemoveAt(currentQuestion);
            //QnA.RemoveAt(currentQuestion);


        }
        generateQuestion();
    }
    //Скрипт для неправильного ответа и генерации нового вопроса
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
    //Скрипт для установки определённых текста/ картинок для каждого типа вопроса
    void SetAnswers()
    {
        //Если в вопросе варианты ответа и сам вопрос текстовые
        //QnA[currentQuestion].questionType.ToString() == "TextQuestionAndOption"


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
                answersImg[i].GetComponent<Image>().sprite = dict[QuestionTypeCurrent].list[currentQuestion].ImgOfQuestion;
                options[i].transform.GetChild(0).GetComponent<Text>().text = $"Вариант {i + 1}";
                next.GetComponent<AnswerScript>().isCorrect = false;
                answers[i].GetComponent<Text>().text = $"{i + 1}. " + dict[QuestionTypeCurrent].list[currentQuestion].Answers[i];
            }
        }
        //Если в вопросе нужно дополнить текст недостающими словами
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
                answersImg[i].GetComponent<Image>().sprite = dict[QuestionTypeCurrent].list[currentQuestion].ImgAnswers[i];
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

    //Скрипт для генерации нового вопроса
    void generateQuestion()
    {
        string sName = SceneManager.GetActiveScene().name;
        //Если ещё есть вопросы в массиве
        if (sName == "Tutorial" || sName == "Control")
        {
            if (totalQuestions > NumQuestion)
            {
                //Рандомизация вопроса


                QuestionTypeCurrent = (QuestionType)Random.Range(0, dict.Count);

                //!!! - - - ВНИМАНИЕ - - - !!!
                if (!IsAnyQInType(QuestionTypeCurrent))
                {
                    generateQuestion();
                }

                currentQuestion = Random.Range(0, dict[QuestionTypeCurrent].list.Count);
                //Для условия вопроса
                QuestionTxt.text = dict[QuestionTypeCurrent].list[currentQuestion].Question;
                NumQuestion++;
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
                //Рандомизация вопроса
                QuestionTypeCurrent = (QuestionType)Random.Range(0, dict.Count);
                currentQuestion = Random.Range(0, dict[QuestionTypeCurrent].list.Count);
                //Для условия вопроса
                QuestionTxt.text = dict[QuestionTypeCurrent].list[currentQuestion].Question;
                NumQuestion++;
                count = 0;
                //Для номера вопроса
                QuetionNum.GetComponent<Text>().text = NumQuestion.ToString();
                //Обновление значений для очков и комбо
                ScoresText.text = Scores.ToString();
                ComboText.text = Combo.ToString() + "x";
                //Установление всех checkbox в отключенное состояние
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
        //Если пользователь ответил на все вопросы

    }
    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    //Проверяет правильные ли кнопки выбрал пользователь при каждом клике на кнопку и возваращет true или false для функции Answer в AnswerScript


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

}