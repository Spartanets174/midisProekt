using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] public float time;
    [SerializeField] public Text timerText;
    public float _timeLeft = 0f;
    int count;

    public IEnumerator StartTimer()
    {
        string sName = SceneManager.GetActiveScene().name;
        //Для остальных
        if (sName == "Tutorial" || sName == "Control")
        {
            while (true)
            {
                _timeLeft += Time.deltaTime;
                UpdateTimeText();
                yield return null;
            }
        }
        //Для соревновательного
        else
        {
            while (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                UpdateTimeText();
                yield return null;
            }
        }        
    }

    public void Start()
    {
        _timeLeft = time;
        StartCoroutine(StartTimer());
    }

    public void UpdateTimeText()
    {
        if (_timeLeft < 0)
            _timeLeft = 0;

        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
    

}
