using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class TotalRatingPresenter : RatePresenter, IPresenter
{
    private TotalRatingView _view;

    private List<string> _nameRatings = new List<string>() {"���� ����� ��� ����", "������ ����","���� ����� ��� �������", "������ ������"
        ,"���� ����� ��� ���������", "������ �������", "���� ����� ��� ��������� �����","���� ���� ����� �������", "���� ����� ������", "���� ������ �� ������ ����", 
        "���� ����� ��� ������", "������ ������ ����", "���� ����� ��� �����", "����� ����������� ������", "������ �����"};

    public void Initialize(TotalRatingView view)
    {
        _view = view;
        base.Initialize(view);
    }

    public override void ShowRate()
    {
        _database.StartConnection();
        List<Player> players = new List<Player>();
        for(int i = 0; i < _playersHolder.Players.Count; i++)
        {
            players.Add(new Player());
            players[i].Name = _playersHolder.Players[i].Name;
            players[i].ShowingName = _playersHolder.Players[i].ShowingName;
            players[i].Sprite = _playersHolder.Players[i].Sprite;
            StateMachineRate(players[i]);
        }
        List<Player> sortedList = players.OrderByDescending(o => o.WinRate).ToList();
        for (int i = 0; i < sortedList.Count; i++)
            sortedList[i].Place = i + 1;
        _view.ShowRate(_nameRatings[_currentLvl], sortedList);
        _database.CloseConnection();
    }

    private void StateMachineRate(Player player)
    {
        List<string> peaceProfessions = new List<string>() { "����������", "������" , "��������" , "��������", "������������", "�������", "��������� ����", "�����������",
        "��������", "������", "�����", "���������", "��������", "��������", "����������", "�������", "�������������", "���������", "�����", "���������", "�������",
        "�������", "��������"};

        List<string> duckProffessions = new List<string>() {"������", "��������", "����", "����������", "������������",
        "�����", "�������", "���������", "���������", "������", "���������", "���������", "�������� ������", "������", "�����", "�����������", "������"};

        if (_nameRatings[_currentLvl] == "���� ����� ��� ����")
        {
            MoreOftenProffession(player, "����");
        }
        else if(_nameRatings[_currentLvl] == "������ ����")
        {
            BestInProffession(player, "����");
        }
        else if(_nameRatings[_currentLvl] == "���� ����� ��� �������")
        {
            MoreOftenProffession(player, "������");
        }
        else if (_nameRatings[_currentLvl] == "������ ������")
        {
            BestInProffession(player, "������");
        }
        else if (_nameRatings[_currentLvl] == "���� ����� ��� ���������")
        {
            MoreOftenProffession(player, "�������");
        }
        else if (_nameRatings[_currentLvl] == "������ �������")
        {
            BestInProffession(player, "�������");
        }
        else if (_nameRatings[_currentLvl] == "���� ����� ��� ��������� �����")
        {
            MoreOftenProffession(player, "��������� ����");
        }
        else if (_nameRatings[_currentLvl] == "���� ����� �������")
        {
            MoreOftenDies(player);
        }
        else if (_nameRatings[_currentLvl] == "���� ����� ������")
        {
            Kicked(player);
        }
        else if (_nameRatings[_currentLvl] == "���� ������ �� ������ ����")
        {
            KickedAsPeaceful(player);
        }
        else if (_nameRatings[_currentLvl] == "���� ����� ��� ������")
        {
            CalculateGooseGames(player, peaceProfessions);
        }
        else if (_nameRatings[_currentLvl] == "������ ������ ����")
        {
            BestGoose(player, peaceProfessions);
        }
        else if (_nameRatings[_currentLvl] == "���� ����� ��� �����")
        {
            CalculateGooseGames(player, duckProffessions);
        }
        else if (_nameRatings[_currentLvl] == "����� ����������� ������")
        {
            BestGoose(player, duckProffessions);
        }
        else if (_nameRatings[_currentLvl] == "������ �����")
        {
            BestOfTheBest(player);
        }
        
    }

    private void MoreOftenProffession(Player player, string proffession)
    {
        player.Games = _database.CountAllGames(player.Name);
        player.WinGames = _database.CountProffessionsGames(player.Name, proffession);
        player.WinRate = (float)Math.Round((float)player.WinGames / (float)player.Games * 100, 2);
    }

    private void BestInProffession(Player player, string proffession)
    {
        player.Games = _database.CountProffessionsGames(player.Name, proffession);
        player.WinGames = _database.CountWiningProffessionsGames(player.Name, proffession);
        player.WinRate = (float)Math.Round((float)player.WinGames / (float)player.Games * 100, 2);
    }

    private void Kicked(Player player)
    {
        player.Games = _database.CountAllGames(player.Name);
        player.WinGames = _database.ExecuteOrder($"SELECT Count(IsWining) FROM {player.Name} WHERE FinalState = '�������'");
        player.WinRate = (float)Math.Round((float)player.WinGames / (float)player.Games * 100, 2);
    }

    private void KickedAsPeaceful(Player player)
    {
        List<string> peaceProfessions = new List<string>() { "����������", "������" , "��������" , "��������", "������������", "�������", "��������� ����", "�����������",
        "��������", "������", "�����", "���������", "��������", "��������", "����������", "�������", "�������������", "���������", "�����", "���������", "�������",
        "�������", "��������", "����"};

        int kickedAtPeacfull = 0;
        foreach (string proffession in peaceProfessions)
            kickedAtPeacfull += _database.ExecuteOrder($"SELECT Count(FinalState) FROM {player.Name} WHERE FinalState = '�������' AND Profession = '{proffession}'");

        Kicked(player);
        player.Games = player.WinGames;
        player.WinGames = kickedAtPeacfull;
        player.WinRate = (float)Math.Round((float)player.WinGames / (float)player.Games * 100, 2);
    }

    private void MoreOftenDies(Player player)
    {
        player.Games = _database.CountAllGames(player.Name);
        player.WinGames = _database.ExecuteOrder($"SELECT Count(FinalState) FROM {player.Name} WHERE FinalState = '�����'");
        player.WinRate = (float)Math.Round((float)player.WinGames / (float)player.Games * 100, 2);
    }

    private void CalculateGooseGames(Player player, List<string> professions)
    {      

        int games = 0;
        foreach (string proffession in professions)
            games += _database.ExecuteOrder($"SELECT Count(FinalState) FROM {player.Name} WHERE Profession = '{proffession}'");

        player.Games = _database.CountAllGames(player.Name);
        player.WinGames = games;
        player.WinRate = (float)Math.Round((float)player.WinGames / (float)player.Games * 100, 2);
    }

    private void BestGoose(Player player, List<string> professions)
    {
        CalculateGooseGames(player, professions);
        player.Games = player.WinGames;

        int peacefulGames = 0;
        foreach (string proffession in professions)
            peacefulGames += _database.CountWiningProffessionsGames(player.Name, proffession);

        player.WinGames = peacefulGames;
        player.WinRate = (float)Math.Round((float)player.WinGames / (float)player.Games * 100, 2);

    }

    private void BestOfTheBest(Player player)
    {
        player.Games = _database.CountAllGames(player.Name);
        player.WinGames = _database.ExecuteOrder($"SELECT Count(IsWining) FROM {player.Name} WHERE IsWining = 1");
        player.WinRate = (float)Math.Round((float)player.WinGames / (float)player.Games * 100, 2);
    }


}