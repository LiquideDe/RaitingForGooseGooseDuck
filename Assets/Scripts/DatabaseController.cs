using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.IO;
using System.Linq;

public class DatabaseController
{
    SqliteConnection _connection;

    public void StartConnection()
    {
        List<string> paths = Directory.GetFiles($"{Application.dataPath}/StreamingAssets","*.db").ToList();
        if(paths.Count > 0)
        {
            _connection = new SqliteConnection($"Data Source={paths[0]}");
            _connection.Open();
        }
        else
        {
            throw new Exception("Не нашли базу данных");
        }
        
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
            throw new System.Exception($"Не получилось выполнить команду. ({command})");
    }

    public List<string> GetProfessions(string name)
    {
        if (_connection.State == ConnectionState.Open)
        {
            SqliteCommand sqliteCommand = new SqliteCommand();
            sqliteCommand.Connection = _connection;
            sqliteCommand.CommandText = $"SELECT NameProf FROM Professions WHERE SideConflict = '{name}'";
            SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader();
            List<string> names = new List<string>();
            while (sqliteDataReader.Read())
            {
                names.Add(sqliteDataReader.GetString(0));
            }
            return names;
        }
        else
        {
            Debug.LogError($"Не пришли профессии");
            return null;           
        }
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
