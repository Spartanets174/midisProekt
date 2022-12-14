using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Крч библиотека для создания файлов и не только
using System.IO;

public class LeaderBoard : MonoBehaviour
{
    public string filename = "";
    public List<Player> leaderList = new List<Player>();


    [System.Serializable]
    public class Player {

        public int idLeader { get; set; }
        public string name { get; set; }
        public int score { get; set; }
        public int comboScore { get; set; }
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }

    [System.Serializable]
    public class PlayerList {
        public List<Player> leaderList = new List<Player>();
        //public Player[] player;
    }

    


    // Start is called before the first frame update
    void Start()
    {
        //filename = Application.dataPath + "/test.csv";
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        WriteCSV();
    //}

    public void WriteCSV()
    {
        if (leaderList.Count > 0)
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("Id, Name, Score, ComboScore, Day, Month, Year");
            tw.Close();

            tw = new StreamWriter(filename, true);
            for (int i = 0; i < leaderList.Count; i++)
            {
                tw.WriteLine(leaderList[i].idLeader + "," + leaderList[i].name + "," + leaderList[i].score + "," + leaderList[i].comboScore + "," + leaderList[i].day + "," + leaderList[i].month + "," + leaderList[i].year);
            }
            tw.Close();
        }


        //Скрипт с конца функции GameOver()
        //leaderList.Add(new Player
        //{
        //    idLeader = leaderList.Count,
        //    name = SetQMass.uName + " " + SetQMass.uSecondName,
        //    score = Scores,
        //    comboScore = MaxCombo,
        //    day = System.DateTime.Now.Day,
        //    month = System.DateTime.Now.Month,
        //    year = System.DateTime.Now.Year
        //});


        //WriteCSV();

    }




}
