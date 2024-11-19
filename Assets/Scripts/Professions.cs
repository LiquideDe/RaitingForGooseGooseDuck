using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Professions
{
    private static List<string> _peacefulProfessions = new List<string>() {"Авантюрист", "Астрал" , "Взломщик" , "Детектив", "Знаменитость", "Инженер", "Канадский Гусь", "Линчеватель",
        "Любовник", "Медиум", "Мимик", "Могильщик", "Мститель", "Следопыт", "Смотритель", "Сталкер", "Телохранитель", "Толстосум", "Шериф", "Спаситель", "Лоббист",
        "Политик", "Прохвост", "Охотник на демонов", "Портной", "Инквизитор", "Додо" };

    private static List<string> _gooseProfessions = new List<string>() {"Авантюрист", "Астрал" , "Взломщик" , "Детектив", "Знаменитость", "Инженер", "Канадский Гусь", "Линчеватель",
        "Любовник", "Медиум", "Мимик", "Могильщик", "Мститель", "Следопыт", "Смотритель", "Сталкер", "Телохранитель", "Толстосум", "Шериф", "Спаситель", "Лоббист",
        "Политик", "Прохвост", "Охотник на демонов", "Портной", "Инквизитор", "Ученый", "Святой" };

    private static List<string> _duckProfessions = new List<string>() {"Хитман", "Каннибал", "Морф", "Усмиритель", "Профессионал",
        "Шпион", "Ассасин", "Весельчак", "Подрывник", "Ниндзя", "Гробовщик", "Невидимка", "Серийный убийца", "Колдун", "Эспер", "Проповедник", "Стукач", "Сектант",
    "Жрец", "Вор Личностей", "Рабочий"};

    public static List<string> PeacefulProfessions => _peacefulProfessions;

    public static List<string> GooseProfessions => _gooseProfessions;
    public static List<string> DuckProfessions => _duckProfessions; 
}
