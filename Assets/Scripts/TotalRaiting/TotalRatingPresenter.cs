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
        List<string> peaceProfessions = new List<string>() { "Авантюрист", "Астрал" , "Взломщик" , "Детектив", "Знаменитость", "Инженер", "Канадский Гусь", "Линчеватель",
        "Любовник", "Медиум", "Мимик", "Могильщик", "Мститель", "Следопыт", "Смотритель", "Сталкер", "Телохранитель", "Толстосум", "Шериф", "Спаситель", "Лоббист",
        "Политик", "Прохвост"};

        List<string> duckProffessions = new List<string>() {"Хитман", "Каннибал", "Морф", "Усмиритель", "Профессионал",
        "Шпион", "Ассасин", "Весельчак", "Подрывник", "Ниндзя", "Гробовщик", "Невидимка", "Серийный убийца", "Колдун", "Эспер", "Проповедник", "Стукач"};

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
        else if (_nameRatings[_currentLvl] == "Чаще всего убивали")
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
            CalculateGooseGames(player, peaceProfessions);
        }
        else if (_nameRatings[_currentLvl] == "Лучший мирный гусь")
        {
            BestGoose(player, peaceProfessions);
        }
        else if (_nameRatings[_currentLvl] == "Чаще всего был Уткой")
        {
            CalculateGooseGames(player, duckProffessions);
        }
        else if (_nameRatings[_currentLvl] == "Самая эффективная Уточка")
        {
            BestGoose(player, duckProffessions);
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
        List<string> peaceProfessions = new List<string>() { "Авантюрист", "Астрал" , "Взломщик" , "Детектив", "Знаменитость", "Инженер", "Канадский Гусь", "Линчеватель",
        "Любовник", "Медиум", "Мимик", "Могильщик", "Мститель", "Следопыт", "Смотритель", "Сталкер", "Телохранитель", "Толстосум", "Шериф", "Спаситель", "Лоббист",
        "Политик", "Прохвост", "Додо"};

        int kickedAtPeacfull = 0;
        foreach (string proffession in peaceProfessions)
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
