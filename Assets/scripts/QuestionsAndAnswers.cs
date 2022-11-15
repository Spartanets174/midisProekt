using UnityEngine;
using UnityEngine.UI;
[System.Serializable]



//класс для ВСЕГО
public class QuestionsAnsAnswers 
{
    public QuestionType questionType;
    public string Question;
    public string Text;
    public Sprite ImgOfQuestion;
    public string[] Answers;
    public Sprite[] ImgAnswers;
    public int [] CorrectAnswer;
}


//класс для использования во ВНЕШНИЙ ВИД
public class QuestionsAnsAnswersVnesh
{
    public QuestionType questionType;
    public string Question;
    public string Text;
    public Sprite ImgOfQuestion;
    public string[] Answers;
    public Sprite[] ImgAnswers;
    public int[] CorrectAnswer;
}

//класс для использования во ИСПОЛЬЗОВАНИЕ ПРЕДМЕТА
public class QuestionsAnsAnswersUse
{
    public QuestionType questionType;
    public string Question;
    public string Text;
    public Sprite ImgOfQuestion;
    public string[] Answers;
    public Sprite[] ImgAnswers;
    public int[] CorrectAnswer;
}
public enum QuestionType
{
    TextQuestionAndOption,
    WriteWords,
    //Когда вопрос текстовый, а варианты в виде картинок
    QuestionTextOptionImg,
    //Когда вопрос картинкой, а варианты в виде текста
    OptionTextQuestionImg
}