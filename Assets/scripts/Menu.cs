using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour


{

    public bool CompetitiveQue;
    public bool ControlAndStudy;
    // Start is called before the first frame update
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Atlas()
    {
        SceneManager.LoadScene("Atlas");
    }
    public void Tutorial()
    {
        ControlAndStudy = true;
        CompetitiveQue = false;
        SceneManager.LoadScene("Tutorial");
        Debug.Log(CompetitiveQue);
        Debug.Log(ControlAndStudy);

    }
    public void Control()
    {
        ControlAndStudy = true;
        CompetitiveQue = false;
        SceneManager.LoadScene("Control");


    }
    public void Competitive()
    {
        ControlAndStudy = false;
        CompetitiveQue = true;
        SceneManager.LoadScene("Competitive");

    }
    public void Table()
    {
        SceneManager.LoadScene("Table");
    }
    public void Exit()
    {
        Debug.Log("Ты вышел");
        Application.Quit();
    }

    
}
