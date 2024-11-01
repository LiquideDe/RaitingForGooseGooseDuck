using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DatabaseController : MonoBehaviour
{
    SqliteConnection _connection;

    private void Start()
    {
        _connection = new SqliteConnection($"Data Source={Application.dataPath}/StreamingAssets/goose.db");
        _connection.Open();
    }

    public int ExecuteOrder(string command)
    {
        if (_connection.State == ConnectionState.Open)
        {
            SqliteCommand sqliteCommand = new SqliteCommand();
            sqliteCommand.Connection = _connection;
            sqliteCommand.CommandText = command;
            SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader();

            return Convert.ToInt32(sqliteDataReader.GetValue(0));
        }
        else
            throw new System.Exception($"Что то пошло не так");
    }
}
