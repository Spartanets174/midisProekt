using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableHolder : MonoBehaviour
{
    public Text number;
    public Text user;
    public Text scores;
    public Text combo;
    public Text date;

    public void SetData(int _numder, UserInfo _userInfo )
    {
        number.text = _numder.ToString();
        user.text = _userInfo.firstName + " " + _userInfo.lastName;
        scores.text = _userInfo.score;
        combo.text = _userInfo.combo;
        date.text = _userInfo.date;
    }
}
