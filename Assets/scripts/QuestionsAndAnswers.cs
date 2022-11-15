using UnityEngine;
using UnityEngine.UI;
[System.Serializable]



//����� ��� �����
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


//����� ��� ������������� �� ������� ���
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

//����� ��� ������������� �� ������������� ��������
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
    //����� ������ ���������, � �������� � ���� ��������
    QuestionTextOptionImg,
    //����� ������ ���������, � �������� � ���� ������
    OptionTextQuestionImg
}