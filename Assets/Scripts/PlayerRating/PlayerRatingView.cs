using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerRatingView : View
{
    [SerializeField] private TextMeshProUGUI _textName, _textTotalGames, _textDeath, _textKicked, _textKickedAsPeacefull;
    [SerializeField] private List<TextMeshProUGUI> _textProgessions = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> _textPercent = new List<TextMeshProUGUI>();
    [SerializeField] private Image _imagePortrait;
    [SerializeField] private Scrollbar _scrollbar;
    
    public TextMeshProUGUI TextName => _textName;
    public TextMeshProUGUI TextTotalGames => _textTotalGames; 
    public TextMeshProUGUI TextDeath => _textDeath;
    public TextMeshProUGUI TextKicked => _textKicked;
    public TextMeshProUGUI TextKickedAsPeacefull => _textKickedAsPeacefull;
    public List<TextMeshProUGUI> TextProgessions => _textProgessions; 
    public List<TextMeshProUGUI> TextPercent => _textPercent;
    public Image ImagePortrait => _imagePortrait;

    public Scrollbar Scrollbar => _scrollbar;
}
