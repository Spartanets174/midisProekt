
using System.Collections.Generic;
using static LeaderBoard;

public static class  SetQMass 
{
    public static string uName = "";
    public static string uSecondName = "";
    public static List<Player> leaderList = new List<Player>();
    public static bool setText = false;
    public static bool setImage = false;
        public static void setQuestionText()
        {
            setText = true;
            setImage = false;
        }
        public static void setQuestionImg()
        {
            setText = false;
            setImage = true;
        }
        public static void setQuestionAll()
        {
            setText = true;
            setImage = true;
        }  

}
public class ResultQuestion
{
    public string textQuestion { get; set; }
    public bool isRight { get; set; }
    public string userAns { get; set; }
    public string rightAns{ get; set; }
}