using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PropertyView : View
{
    [SerializeField] private PlayerProperty _playerPropertyPrefab;
    [SerializeField] private Transform _content;
    private List<PlayerProperty> playerProperties = new List<PlayerProperty>();
    public event Action<string, bool> PlayerToggleChange;

    public void Initialize(List<PlayerOptions> playerOptions)
    {
        if(playerProperties.Count > 0)
        {
            for(int i = 0; i < playerProperties.Count; i++)
            {
                playerProperties[i].ToggleChange -= PlayerToggleChanged;
                playerProperties[i].DestroyPanel();
            }
                
            playerProperties.Clear();
        }

        foreach(PlayerOptions player in playerOptions)
        {
            PlayerProperty playerProperty = Instantiate(_playerPropertyPrefab, _content);
            playerProperty.Initialize(player);
            playerProperties.Add(playerProperty);
            playerProperty.ToggleChange += PlayerToggleChanged;
        }
    }

    private void PlayerToggleChanged(string arg1, bool arg2) => PlayerToggleChange(arg1, arg2);
}
