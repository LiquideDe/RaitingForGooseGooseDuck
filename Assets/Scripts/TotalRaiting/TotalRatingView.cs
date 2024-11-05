using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TotalRatingView : View
{    
    [SerializeField] private TextMeshProUGUI _textNameNomination;
    [SerializeField] private Transform _content;
    [SerializeField] private PlayerPanel _playerPanelprefab;
    private List<PlayerPanel> _playerPanels = new List<PlayerPanel>();      

    public void ShowRate(string nameRate, List<Player> players)
    {
        _textNameNomination.text = nameRate;
        CreatePanels(players);
    }

    private void CreatePanels(List<Player> players)
    {
        if (_playerPanels.Count > 0)
        {
            foreach (PlayerPanel player in _playerPanels)
                player.DestroyPanel();
            _playerPanels.Clear();
        }

        foreach(Player player in players)
        {
            PlayerPanel panel = Instantiate(_playerPanelprefab, _content);
            panel.Initialize(player);
            _playerPanels.Add(panel);
        }
    }

    
}
