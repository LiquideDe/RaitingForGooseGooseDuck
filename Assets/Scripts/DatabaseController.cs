using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DatabaseController
{
    SqliteConnection _connection;

    public void StartConnection()
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
            throw new System.Exception($"Что то пошло не так. ({command})");
    }

    public void CloseConnection() => _connection.Close();

    public int CountAllGames(string namePlayer)
    {
        string command = $"SELECT Count(IsWining) FROM {namePlayer}";
        return ExecuteOrder(command);
    }

    public int CountProffessionsGames(string namePlayer, string nameProffession)
    {
        string command = $"SELECT Count(IsWining) FROM {namePlayer} WHERE Profession = '{nameProffession}'";
        return ExecuteOrder(command);
    }

    public int CountWiningProffessionsGames(string namePlayer, string nameProffession)
    {
        string command = $"SELECT Count(IsWining) FROM {namePlayer} WHERE Profession = '{nameProffession}' AND IsWining = 1";
        return ExecuteOrder(command);
    }
}
