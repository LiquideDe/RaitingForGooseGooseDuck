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
        _view.TextName.text = $"Рейтинг игрока {player.ShowingName}";
        int totalGames = _database.CountAllGames(player.Name);
        _view.TextTotalGames.text = $"Всего игр - {totalGames}";
        int totalDeaths = _database.ExecuteOrder($"SELECT Count(FinalState) FROM {player.Name} WHERE FinalState = 'Убили'");
        _view.TextDeath.text = $"Всего умер - {totalDeaths}. ({Math.Round((float)totalDeaths/(float)totalGames*100,2)}%)";
        int totalKicks = _database.ExecuteOrder($"SELECT Count(IsWining) FROM {player.Name} WHERE FinalState = 'Кикнули'");
        _view.TextKicked.text = $"Всего утопили - {totalKicks}. ({Math.Round((float)totalKicks / (float)totalGames * 100, 2)}%)";

        int kickedAtPeacfull = 0;
        foreach(string proffession in Professions.PeacefulProfessions)        
            kickedAtPeacfull += _database.ExecuteOrder($"SELECT Count(FinalState) FROM {player.Name} WHERE FinalState = 'Кикнули' AND Profession = '{proffession}'");
        
        _view.TextKickedAsPeacefull.text = $"На мирной роли - {kickedAtPeacfull}. ({Math.Round((float)kickedAtPeacfull / (float)totalKicks * 100, 2)}%)";

        List<string> allProffessions = new List<string>() {"Додо", "Стервятник", "Голубь", "Сокол", "Пеликан", "Толстосум", "Медиум", "Линчеватель", 
        "Шериф", "Канадский Гусь", "Мимик", "Детектив", "Смотритель", "Политик", "Взломщик", "Могильщик", "Знаменитость", "Мститель", "Астрал", "Инженер",
        "Сталкер", "Спаситель", "Лоббист", "Любовник", "Телохранитель", "Прохвост", "Следопыт", "Хитман", "Каннибал", "Морф", "Усмиритель", "Профессионал",
        "Шпион", "Ассасин", "Весельчак", "Подрывник", "Ниндзя", "Гробовщик", "Невидимка", "Серийный убийца", "Колдун", "Эспер", "Проповедник", "Стукач"};

        List<string> allProffessionsSklonenie = new List<string>() {"Додо", "Стервятником", "Голубем", "Соколом", "Пеликаном", "Толстосумом", "Медиумом", "Линчевателем",
        "Шерифом", "Канадским Гусем", "Мимиком", "Детективом", "Смотрителем", "Политиком", "Взломщиком", "Могильщиком", "Знаменитостью", 
            "Мстителем", "Астралом", "Инженером",
        "Сталкером", "Спасителем", "Лоббистом", "Любовником", "Телохранителем", "Прохвостом", "Следопытом", "Хитманом", "Каннибалом", "Морфом", "Усмирителем", 
            "Профессионалом", "Шпионом", "Ассасином", "Весельчаком", "Подрывником", "Ниндзя", "Гробовщиком", "Невидимкой", "Серийным убийцей", 
            "Колдуном", "Эспером", "Проповедником", "Стукачом"};

        for (int i = 0; i < _view.TextProgessions.Count; i++)
        {
            int amountGames = _database.CountProffessionsGames(player.Name, allProffessions[i]);
            int amountWinningGames = _database.CountWiningProffessionsGames(player.Name, allProffessions[i]);
            if(amountGames > 0)
            {
                _view.TextProgessions[i].text = $"Был {allProffessionsSklonenie[i]} {amountGames} раз. \n Выиграл {amountWinningGames} раз. ({Math.Round((float)amountWinningGames / (float)amountGames * 100, 2)}%)";
                _view.TextPercent[i].text = $"{Math.Round((float)amountGames / (float)totalGames * 100, 2)}%";
            }
            else
            {
                _view.TextProgessions[i].text = $"Был {allProffessionsSklonenie[i]} 0 раз.";
                _view.TextPercent[i].text = $"0%";
            }
                
            
        }

        _database.CloseConnection();
    }
}
