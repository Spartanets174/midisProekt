using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour 
{
    public InputField uNameBox—ontrol;
    public InputField uSecondNameBox—ontrol;
    public InputField uNameBoxCompetitive;
    public InputField uSecondNameBoxCompetitive;
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ToAtlas()
    {
        SceneManager.LoadScene("AtlasMap");
    }
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void Control()
    {
        if (uNameBox—ontrol.textComponent.text ==""|| uSecondNameBox—ontrol.textComponent.text == "")
        {
            uNameBox—ontrol.placeholder.color = new Color(0.83f, 0.12f, 0.12f);
            uSecondNameBox—ontrol.placeholder.color = new Color(0.83f, 0.12f, 0.12f);
        }
        else
        {
            SetQMass.uName = uNameBox—ontrol.textComponent.text;
            SetQMass.uSecondName = uSecondNameBox—ontrol.textComponent.text;
            SceneManager.LoadScene("Control");
        }      
    }
    public void Competitive()
    {       
        if (uNameBoxCompetitive.textComponent.text == "" || uSecondNameBoxCompetitive.textComponent.text == "")
        {
            uNameBoxCompetitive.placeholder.color = new Color(0.83f, 0.12f, 0.12f);
            uSecondNameBoxCompetitive.placeholder.color = new Color(0.83f, 0.12f, 0.12f);
        }
        else
        {
            SetQMass.uName = uNameBoxCompetitive.textComponent.text;
            SetQMass.uSecondName = uSecondNameBoxCompetitive.textComponent.text;
            SceneManager.LoadScene("Competitive");
        }        
    }
    public void Table()
    {
        SceneManager.LoadScene("Table");
    }
    public void Exit()
    {
        Debug.Log("“˚ ‚˚¯ÂÎ");
        Application.Quit();
    }
    public void setQuestionText()
    {
        SetQMass.setText = true;
        SetQMass.setImage = false;
    }
    public  void setQuestionImg()
    {
        SetQMass.setText = false;
        SetQMass.setImage = true;
    }
    public void setQuestionAll()
    {
        SetQMass.setText = true;
        SetQMass.setImage = true;
    }
    public void colorChange()
    {
        uNameBox—ontrol.placeholder.color = new Color(0.4716981f, 0.4716981f, 0.4716981f);
        uSecondNameBox—ontrol.placeholder.color = new Color(0.4716981f, 0.4716981f, 0.4716981f);
        uNameBoxCompetitive.placeholder.color = new Color(0.4716981f, 0.4716981f, 0.4716981f);
        uSecondNameBoxCompetitive.placeholder.color = new Color(0.4716981f, 0.4716981f, 0.4716981f);
    }
}

