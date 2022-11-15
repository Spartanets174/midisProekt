using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
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
        SceneManager.LoadScene("Tutorial");
    }
    public void Control()
    {
        SceneManager.LoadScene("Control");
    }
    public void Competitive()
    {
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
