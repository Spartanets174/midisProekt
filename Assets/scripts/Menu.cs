using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour


{
<<<<<<< HEAD
=======

    public bool CompetitiveQue;
    public bool ControlAndStudy;
>>>>>>> parent of f4f9e31 (РўР°Р№РјРµСЂ)
    // Start is called before the first frame update
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Atlas()
    {
        SceneManager.LoadScene("AtlasMap");
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
