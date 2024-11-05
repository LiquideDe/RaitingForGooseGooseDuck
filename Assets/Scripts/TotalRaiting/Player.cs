using System;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    public string Name { get; set; }
    public string ShowingName { get; set; }
    public int Games { get; set; }
    public int WinGames { get; set; }
    public float WinRate { get; set; }

    public int Place { get; set; }

    public Sprite Sprite { get; set; }
}
