using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class PlayerRatingPresenter : RatePresenter, IPresenter
{
    
    private PlayerRatingView _view;
    public void Initialize(PlayerRatingView view)
    {
        _view = view;
        base.Initialize(view, _playersHolder.Players.Count);
        
    }
    public override void ShowRate()
    {
        _view.Scrollbar.value = 0;
        _database.StartConnection();
        PlayerOptions player = _playersHolder.Players[_currentLvl];
        _view.ImagePortrait.sprite = player.Sprite;
        _view.TextName.text = $"������� ������ {player.ShowingName}";
        int totalGames = _database.CountAllGames(player.Name);
        _view.TextTotalGames.text = $"����� ��� - {totalGames}";
        int totalDeaths = _database.ExecuteOrder($"SELECT Count(FinalState) FROM {player.Name} WHERE FinalState = '�����'");
        _view.TextDeath.text = $"����� ���� - {totalDeaths}. ({Math.Round((float)totalDeaths/(float)totalGames*100,2)}%)";
        int totalKicks = _database.ExecuteOrder($"SELECT Count(IsWining) FROM {player.Name} WHERE FinalState = '�������'");
        _view.TextKicked.text = $"����� ������� - {totalKicks}. ({Math.Round((float)totalKicks / (float)totalGames * 100, 2)}%)";

        int kickedAtPeacfull = 0;
        foreach(string proffession in Professions.PeacefulProfessions)        
            kickedAtPeacfull += _database.ExecuteOrder($"SELECT Count(FinalState) FROM {player.Name} WHERE FinalState = '�������' AND Profession = '{proffession}'");
        
        _view.TextKickedAsPeacefull.text = $"�� ������ ���� - {kickedAtPeacfull}. ({Math.Round((float)kickedAtPeacfull / (float)totalKicks * 100, 2)}%)";

        List<string> allProffessions = new List<string>() {"����", "����������", "������", "�����", "�������", "���������", "������", "�����������", 
        "�����", "��������� ����", "�����", "��������", "����������", "�������", "��������", "���������", "������������", "��������", "������", "�������",
        "�������", "���������", "�������", "��������", "�������������", "��������", "��������", "������", "��������", "����", "����������", "������������",
        "�����", "�������", "���������", "���������", "������", "���������", "���������", "�������� ������", "������", "�����", "�����������", "������"};

        List<string> allProffessionsSklonenie = new List<string>() {"����", "������������", "�������", "�������", "���������", "�����������", "��������", "������������",
        "�������", "��������� �����", "�������", "����������", "�����������", "���������", "����������", "�����������", "�������������", 
            "���������", "��������", "���������",
        "���������", "����������", "���������", "����������", "��������������", "����������", "����������", "��������", "����������", "������", "�����������", 
            "��������������", "�������", "���������", "�����������", "�����������", "������", "�����������", "����������", "�������� �������", 
            "��������", "�������", "�������������", "��������"};

        for (int i = 0; i < _view.TextProgessions.Count; i++)
        {
            int amountGames = _database.CountProffessionsGames(player.Name, allProffessions[i]);
            int amountWinningGames = _database.CountWiningProffessionsGames(player.Name, allProffessions[i]);
            if(amountGames > 0)
            {
                _view.TextProgessions[i].text = $"��� {allProffessionsSklonenie[i]} {amountGames} ���. \n ������� {amountWinningGames} ���. ({Math.Round((float)amountWinningGames / (float)amountGames * 100, 2)}%)";
                _view.TextPercent[i].text = $"{Math.Round((float)amountGames / (float)totalGames * 100, 2)}%";
            }
            else
            {
                _view.TextProgessions[i].text = $"��� {allProffessionsSklonenie[i]} 0 ���.";
                _view.TextPercent[i].text = $"0%";
            }
                
            
        }

        _database.CloseConnection();
    }
}
