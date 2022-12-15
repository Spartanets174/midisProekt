using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class atlasSearch : MonoBehaviour
{
    void Start()
    {
        var input = gameObject.GetComponent<InputField>();
        var se = new InputField.SubmitEvent();
        se.AddListener(SubmitName);
        input.onEndEdit.AddListener(SubmitName);
    }

    private void SubmitName(string arg0)
    {
        Debug.Log(arg0);
    }
}
