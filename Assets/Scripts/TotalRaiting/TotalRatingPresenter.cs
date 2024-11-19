using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class TotalRatingPresenter : RatePresenter, IPresenter
{
    private TotalRatingView _view;

    private List<string> _nameRatings = new List<string>() {"Чаще всего был Додо", "Лучший Додо","Чаще всего был Голубем", "Лучший голубь"
        ,"Чаще всего был Пеликаном", "Лучший Пеликан", "Чаще всего был Канадским гусем","Кого чаще всего убивали", "Чаще всего топили", "Чаще топили на мирной роли", 
        "Чаще всего был мирным", "Лучший мирный гусь", "Чаще всего был Уткой", "Самая эффективная Уточка", "Лучший игрок"};

    public void Initialize(TotalRatingView view)
    {
        _view = view;
        base.Initialize(view, _nameRatings.Count);
    }

    public override void ShowRate()
    {
        _database.StartConnection();
        List<Player> players = new List<Player>();
        for(int i = 0; i < _playersHolder.Players.Count; i++)
            if (_playersHolder.Players[i].IsPlayerShowing)
            {
            players.Add(new Player());
            players[^1].Name = _playersHolder.Players[i].Name;
            players[^1].ShowingName = _playersHolder.Players[i].ShowingName;
            players[^1].Sprite = _playersHolder.Players[i].Sprite;
            StateMachineRate(players[^1]);
            }
        List<Player> sortedList = players.OrderByDescending(o => o.WinRate).ToList();
        for (int i = 0; i < sortedList.Count; i++)
            sortedList[i].Place = i + 1;
        _view.ShowRate(_nameRatings[_currentLvl], sortedList);
        _database.CloseConnection();
    }

    private void StateMachineRate(Player player)
    {

        if (_nameRatings[_currentLvl] == "Чаще всего был Додо")
        {
            MoreOftenProffession(player, "Додо");
        }
        else if(_nameRatings[_currentLvl] == "Лучший Додо")
        {
            BestInProffession(player, "Додо");
        }
        else if(_nameRatings[_currentLvl] == "Чаще всего был Голубем")
        {
            MoreOftenProffession(player, "Голубь");
        }
        else if (_nameRatings[_currentLvl] == "Лучший голубь")
        {
            BestInProffession(player, "Голубь");
        }
        else if (_nameRatings[_currentLvl] == "Чаще всего был Пеликаном")
        {
            MoreOftenProffession(player, "Пеликан");
        }
        else if (_nameRatings[_currentLvl] == "Лучший Пеликан")
        {
            BestInProffession(player, "Пеликан");
        }
        else if (_nameRatings[_currentLvl] == "Чаще всего был Канадским гусем")
        {
            MoreOftenProffession(player, "Канадский Гусь");
        }
        else if (_nameRatings[_currentLvl] == "Кого чаще всего убивали")
        {
            MoreOftenDies(player);
        }
        else if (_nameRatings[_currentLvl] == "Чаще всего топили")
        {
            Kicked(player);
        }
        else if (_nameRatings[_currentLvl] == "Чаще топили на мирной роли")
        {
            KickedAsPeaceful(player);
        }
        else if (_nameRatings[_currentLvl] == "Чаще всего был мирным")
        {
            CalculateGooseGames(player, Professions.GooseProfessions);
        }
        else if (_nameRatings[_currentLvl] == "Лучший мирный гусь")
        {
            BestGoose(player, Professions.GooseProfessions);
        }
        else if (_nameRatings[_currentLvl] == "Чаще всего был Уткой")
        {
            CalculateGooseGames(player, Professions.DuckProfessions);
        }
        else if (_nameRatings[_currentLvl] == "Самая эффективная Уточка")
        {
            BestGoose(player, Professions.DuckProfessions);
        }
        else if (_nameRatings[_currentLvl] == "Лучший игрок")
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
        player.WinGames = _database.ExecuteOrder($"SELECT Count(IsWining) FROM {player.Name} WHERE FinalState = 'Кикнули'");
        player.WinRate = (float)Math.Round((float)player.WinGames / (float)player.Games * 100, 2);
    }

    private void KickedAsPeaceful(Player player)
    {   
        int kickedAtPeacfull = 0;
        foreach (string proffession in Professions.PeacefulProfessions)
            kickedAtPeacfull += _database.ExecuteOrder($"SELECT Count(FinalState) FROM {player.Name} WHERE FinalState = 'Кикнули' AND Profession = '{proffession}'");

        Kicked(player);
        player.Games = player.WinGames;
        player.WinGames = kickedAtPeacfull;
        player.WinRate = (float)Math.Round((float)player.WinGames / (float)player.Games * 100, 2);
    }

    private void MoreOftenDies(Player player)
    {
        player.Games = _database.CountAllGames(player.Name);
        player.WinGames = _database.ExecuteOrder($"SELECT Count(FinalState) FROM {player.Name} WHERE FinalState = 'Убили'");
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
