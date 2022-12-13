using UnityEngine;
using UnityEngine.UI;
using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;



//public class QuestionsAnsAnswers
//{
//    public QuestionType questionType;
//    public string Question;
//    public string Text;
//    public Sprite ImgOfQuestion;
//    public string[] Answers;
//    public Sprite[] ImgAnswers;
//    public int[] CorrectAnswer;
//}

// Новый скрипт вопросов

[System.Serializable]
public class QuestionsAnsAnswers
{
    public QuestionType questionType;
    public string Question;
    public string Text;
    public Sprite ImgOfQuestion;
    public GameObject ModelInst;
    public string[] Answers;
    public Sprite[] ImgAnswers;
    public GameObject[] ModelInsts;
    public int[] CorrectAnswer;
}

//Создание обертки для листа
[System.Serializable]
public class QuestionS
{
    //public QuestionType questionType;
    public List<QuestionsAnsAnswers> list;
}

//Создание словаря, где будут хранится массивы с вопросами и типами
[System.Serializable]
public class MyDictioanary : SerializableDictionaryBase<QuestionType, QuestionS> { }

public enum QuestionType
{
    TextQuestionAndOption,
    WriteWords,
    QuestionTextOptionImg,
    OptionTextQuestionImg
}