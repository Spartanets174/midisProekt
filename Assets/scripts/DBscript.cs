using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;
using UnityEngine.UI;
using System;
using UnityEngine.SocialPlatforms.Impl;

public static class ConnectionInfo
{
    public static string ip = "127.0.0.1";
    public static string uid = "root";
    public static string pwd = "123456";
    public static string database = "mydb1";
}
public class UserInfo
{
    public string firstName;
    public string lastName;
    public string score;
    public string combo;
    public string date;
    public string userId;
} 
public class DBscript : MonoBehaviour
{
   
    
    static string conectionString = $"server={ConnectionInfo.ip}; " +
        $"uid={ConnectionInfo.uid}; " +
        $"pwd={ConnectionInfo.pwd}; " +
        $"Database = {ConnectionInfo.database}; " +
        $"SSLMode=none";
    static MySqlConnection con;

    public static UserInfo userInfo = new UserInfo();

    private void Awake()
    {
        con = new MySqlConnection(conectionString);
        try
        {
            con.Open();
        }
        catch (Exception ex)
        {
            Debug.Log("Произошла ошибка!" + ex);
            return;
        }
        
    }
    private void OnApplicationQuit()
    {
        con.Close();
    }



    public static void InsertData()
    {
        string query = $"Insert into {ConnectionInfo.database}.users (firstName, lastname, score, combo, date) values ('{userInfo.firstName}','{userInfo.lastName}','{userInfo.score}','{userInfo.combo}','{userInfo.date}')";
        try
        {
            var command = new MySqlCommand(query, con);
            command.ExecuteNonQuery();
            command.Dispose();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public static List<UserInfo> SelectData()
    {
        string query = $"Select * from {ConnectionInfo.database}.users ORDER BY score desc";
        try
        {
            var command = new MySqlCommand(query, con);
            List<UserInfo> userList = new List<UserInfo>();
            var result = command.ExecuteReader();
            while (result.Read())
            {
                userList.Add(new UserInfo()
                {
                    firstName = result.GetString("firstName"),
                    lastName = result.GetString("lastname"),
                    score = result.GetString("score"),
                    combo = result.GetString("combo"),
                    date = result.GetString("date"),
                });
            }
            return userList;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            return new List<UserInfo>();
        }
    }
}