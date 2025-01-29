using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Professions
{
    private static List<string> _peacefulProfessions = new List<string>();

    private static List<string> _gooseProfessions = new List<string>();

    private static List<string> _duckProfessions = new List<string>();

    private static List<string> _neutralProfessions = new List<string>();

    public static List<string> PeacefulProfessions => _peacefulProfessions;

    public static List<string> GooseProfessions => _gooseProfessions;
    public static List<string> DuckProfessions => _duckProfessions;

    public static List<string> NeutralProfessions => _neutralProfessions;

    public static void FillLists()
    {
        DatabaseController databaseController = new DatabaseController();
        databaseController.StartConnection();
        _gooseProfessions.AddRange(databaseController.GetProfessions("Гуси"));
        _duckProfessions.AddRange(databaseController.GetProfessions("Утки"));
        _neutralProfessions.AddRange(databaseController.GetProfessions("Нейтрал"));
        _peacefulProfessions.AddRange(_gooseProfessions);
        _peacefulProfessions.Add("Додо");
    }
}
