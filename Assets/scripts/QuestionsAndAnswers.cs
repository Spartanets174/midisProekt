using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

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
public enum QuestionType
{
    TextQuestionAndOption,
    WriteWords,
    // огда вопрос текстовый, а варианты в виде картинок
    QuestionTextOptionImg,
    // огда вопрос картинкой, а варианты в виде текста
    OptionTextQuestionImg
}