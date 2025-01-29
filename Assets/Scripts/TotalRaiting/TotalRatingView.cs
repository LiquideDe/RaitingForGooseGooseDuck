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

    public void ShowRate(string nameRate, List<Player> players, string ed)
    {
        _textNameNomination.text = nameRate;
        CreatePanels(players, ed);
    }

    private void CreatePanels(List<Player> players, string ed)
    {
        if (_playerPanels.Count > 0)
        {
            foreach (PlayerPanel player in _playerPanels)
                player.DestroyPanel();
            _playerPanels.Clear();
        }
        string moded = ed;
        foreach(Player player in players)
        {
            PlayerPanel panel = Instantiate(_playerPanelprefab, _content);
            if (string.Compare(" ножей", ed) == 0)
            {
                if (player.WinRate == 1)
                    moded = " нож";
                else if (player.WinRate > 1 && player.WinRate < 5)
                    moded = " ножa";
                else if (player.WinRate == 0 || player.WinRate > 4)
                    moded = " ножей";
            }

            if(string.Compare(" кубков", ed) == 0)
            {
                if (player.WinRate == 1)
                    moded = " кубок";
                else if (player.WinRate > 1 && player.WinRate < 5)
                    moded = " кубка";
                else if (player.WinRate == 0 || player.WinRate > 4)
                    moded = " кубков";
            }

            panel.Initialize(player, moded);
            _playerPanels.Add(panel);
        }
    }

    
}
