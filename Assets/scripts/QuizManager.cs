using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    //Ссылки на др скрипты
    public List<QuestionsAnsAnswers> QnA;
    public AnswerScript answerScript;

    public Menu Menu;
    //Ссылки на объекты в юнити
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
   //Переменные для различных целей
    public int totalQuestions = 0;
    public int rightQuestions;
    public int NumQuestion=0;
    public int count = 0;

    private void Start()
    {
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

    }
    //Проверяет правильные ли кнопки выбрал пользователь при каждом клике на кнопку и возваращет true или false для функции Answer в AnswerScript
    void onClick()
    {
        //Проверяет есть ли ещё вопросы в тесте
        if (QnA.Count > 0)
        {
            //Проверка сколько правильных вариантов ответа в тесте (один)
            if (QnA[currentQuestion].CorrectAnswer.Length == 1)
            {              
                //Цикл перебора всех кнопок с вариантами ответов
                for (int i = 0; i < options.Length; i++)
                {
                    //Проверка включен ли checkbox
                    if (options[i].transform.GetChild(1).GetComponent<Toggle>().isOn == true)
                    {
                        count++;
                        //Проверка включен ли checkbox и является ли он правильным ответов
                        if (((options[i].transform.GetChild(1).GetComponent<Toggle>().isOn == true) && (i + 1 == QnA[currentQuestion].CorrectAnswer[0]))&&count==1)
                        {
                            //Передача true для isCorrect в скрипте AnswerScript
                            next.GetComponent<AnswerScript>().isCorrect = true;
                        }
                    }
                    //Debug.Log("Правильный ли первый: " + options[i].transform.GetChild(1).GetComponent<Toggle>().isOn + " номер первого: " + i + " правильный ответ первого: " + QnA[currentQuestion].CorrectAnswer[0]);                   
                }
            }
            //Проверка сколько правильных вариантов ответа в тесте (два)
            else
            {
                //Цикл перебора всех кнопок с вариантами ответов
                for (int i = 0; i < options.Length; i++)
                {
                    count = 0;
                    if (options[i].transform.GetChild(1).GetComponent<Toggle>().isOn == true)
                    {                        
                        if (options[i].transform.GetChild(1).GetComponent<Toggle>().isOn == true && (i + 1 == QnA[currentQuestion].CorrectAnswer[0]))
                        {
                            count++;
                            //Цикл перебора всех кнопок с вариантами ответов для проверки второй кнопки на правильность
                            for (int j = i + 1; j < options.Length; j++)
                            {                              
                                //Debug.Log("Правильный ли первый: " + options[i].transform.GetChild(1).GetComponent<Toggle>().isOn + " номер первого: " + i + " правильный ответ первого: " + QnA[currentQuestion].CorrectAnswer[0] + " Правильный ли второй " + options[j].transform.GetChild(1).GetComponent<Toggle>().isOn + " номер второго: " + j + " правильный ответ второго: " + QnA[currentQuestion].CorrectAnswer[1]);
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
    //Скрипт для вызова onClick
    public void Checking() 
    {
        onClick();
    }

    //Скрипт когда вопросов не осталось
     void GameOver()
    {
        Questions.SetActive(false); 
        Results.SetActive(true);
        ResultsTxt.text = rightQuestions + "/" + totalQuestions;
    }
    //Скрипт подсчёта правильных ответов и генерации нового вопроса
    public void correct()
    {
        rightQuestions++;
        //Удаляет текущий вопрос, чтобы он не повторялся в будущем
        //QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }
    //Скрипт для неправильного ответа и генерации нового вопроса
    public void wrong()
    {
        //QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }
    //Скрипт для установки определённых текста/ картинок для каждого типа вопроса
    void SetAnswers()
    {       
        //Если в вопросе варианты ответа и сам вопрос текстовые
        if (QnA[currentQuestion].questionType.ToString() == "TextQuestionAndOption")
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
                answersImg[i].GetComponent<Image>().sprite = QnA[currentQuestion].ImgOfQuestion;
                options[i].transform.GetChild(0).GetComponent<Text>().text = $"Вариант {i + 1}";
                next.GetComponent<AnswerScript>().isCorrect = false;
                answers[i].GetComponent<Text>().text = $"{i+1}. "+QnA[currentQuestion].Answers[i];
            }
        }
        //Если в вопросе нужно дополнить текст недостающими словами
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
        //Если в вопросе сам вопрос текстовый а варианты ответа картинки
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
                options[i].transform.GetChild(0).GetComponent<Text>().text = $"Вариант {i + 1}";
                next.GetComponent<AnswerScript>().isCorrect = false;                
            }
        }
        //Если в вопросе сам вопрос картинка а варианты текстовые
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
    //Скрипт для генерации нового вопроса
    void generateQuestion()
    {
        //Если ещё есть вопросы в массиве
        if (totalQuestions > NumQuestion)
        {
            //Рандомизация вопроса
            currentQuestion = Random.Range(0, QnA.Count);
            //Для условия вопроса
            QuestionTxt.text = QnA[currentQuestion].Question;
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
}
