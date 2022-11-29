
public static class  SetQMass 
{
    public static string uName = "";
    public static string uSecondName = "";

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
