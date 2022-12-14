using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsersLoader : MonoBehaviour
{
    public GameObject leaderTablePrefab;
    public Transform contentParent;
    void Start()
    {
        List<UserInfo> usersList = DBscript.SelectData();
       // List<UserInfo> sortedList;
        for (int i = 0; i < usersList.Count; i++)
        {
            GameObject inst = Instantiate(leaderTablePrefab, contentParent);
            TableHolder tableHolder = inst.GetComponent<TableHolder>();
            tableHolder.SetData(i + 1, usersList[i]);
        }

    }

}
