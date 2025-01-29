using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        
        _database.StartConnection();
        PlayerOptions player = _playersHolder.Players[_currentLvl];

        int totalGames = _database.CountAllGames(player.Name);
        int totalDeaths = _database.ExecuteOrder($"SELECT Count(FinalState) FROM {player.Name} WHERE FinalState = 'Убили'");
        int totalKicks = _database.ExecuteOrder($"SELECT Count(IsWining) FROM {player.Name} WHERE FinalState = 'Кикнули'");
        int kickedAtPeacfull = 0;
        foreach (string proffession in Professions.PeacefulProfessions)
            kickedAtPeacfull += _database.ExecuteOrder($"SELECT Count(FinalState) FROM {player.Name} WHERE FinalState = 'Кикнули' AND Profession = '{proffession}'");

        string nameCharacter = $"Рейтинг игрока {player.ShowingName}";
        string games = $"Всего игр - {totalGames}";
        string deaths = $"Всего умер - {totalDeaths}. ({Math.Round((float)totalDeaths / (float)totalGames * 100, 2)}%)";
        string kicked = $"Всего утопили - {totalKicks}. ({Math.Round((float)totalKicks / (float)totalGames * 100, 2)}%)";
        string kickedAsPeacefullText = $"На мирной роли - {kickedAtPeacfull}. ({Math.Round((float)kickedAtPeacfull / (float)totalKicks * 100, 2)}%)";
        _view.ShowCharacter(player.Sprite, nameCharacter, games, deaths, kicked, kickedAsPeacefullText);

        for (int i = 0; i < Professions.GooseProfessions.Count; i++)
        {
            int amountGames = _database.CountProffessionsGames(player.Name, Professions.GooseProfessions[i]);
            if (amountGames > 0)
                CreateProfessionpanel(Professions.GooseProfessions[i], amountGames, player.Name, totalGames);
        }

        for (int i = 0; i < Professions.DuckProfessions.Count; i++)
        {
            int amountGames = _database.CountProffessionsGames(player.Name, Professions.DuckProfessions[i]);
            if (amountGames > 0)
                CreateProfessionpanel(Professions.DuckProfessions[i], amountGames, player.Name, totalGames);
        }

        for (int i = 0; i < Professions.NeutralProfessions.Count; i++)
        {
            int amountGames = _database.CountProffessionsGames(player.Name, Professions.NeutralProfessions[i]);
            if (amountGames > 0)
                CreateProfessionpanel(Professions.NeutralProfessions[i], amountGames, player.Name, totalGames);
        }

        _database.CloseConnection();
    }

    private void CreateProfessionpanel(string nameProfession, int amountGames, string nameCharacter, int totalGames)
    {
        int amountWinningGames = _database.CountWiningProffessionsGames(nameCharacter, nameProfession);
        int amoundDeath = _database.ExecuteOrder($"SELECT Count(IsWining) FROM {nameCharacter} WHERE FinalState = 'Убили' AND Profession = '{nameProfession}'");
        int amountKicked = _database.ExecuteOrder($"SELECT Count(IsWining) FROM {nameCharacter} WHERE FinalState = 'Кикнули' AND Profession = '{nameProfession}'");
        string finalText = $"Всего игр - {amountGames}\n";
        finalText += $"Выиграл - {amountWinningGames}\n";
        finalText += $"Убили - {amoundDeath}\n";
        finalText += $"Кикнули - {amountKicked}";
        string winrate = $"{Math.Round((float)amountGames / (float)totalGames * 100, 2)}%";
        _view.ShowProfession(nameProfession, finalText, winrate);
    }
}
