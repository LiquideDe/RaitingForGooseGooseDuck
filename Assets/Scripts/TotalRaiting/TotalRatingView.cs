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
            if (string.Compare(" �����", ed) == 0)
            {
                if (player.WinRate == 1)
                    moded = " ���";
                else if (player.WinRate > 1 && player.WinRate < 5)
                    moded = " ���a";
                else if (player.WinRate == 0 || player.WinRate > 4)
                    moded = " �����";
            }

            if(string.Compare(" ������", ed) == 0)
            {
                if (player.WinRate == 1)
                    moded = " �����";
                else if (player.WinRate > 1 && player.WinRate < 5)
                    moded = " �����";
                else if (player.WinRate == 0 || player.WinRate > 4)
                    moded = " ������";
            }

            panel.Initialize(player, moded);
            _playerPanels.Add(panel);
        }
    }

    
}
