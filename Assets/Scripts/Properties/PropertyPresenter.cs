using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PropertyPresenter : RatePresenter, IPresenter
{
    private PropertyView _view;
    public void Initialize(PropertyView view)
    {
        _view = view;
        base.Initialize(view, 0);
        _view.PlayerToggleChange += PlayerToggleChange;
    }    

    public override void ShowRate()
    {
        _view.Initialize(_playersHolder.Players);
    }

    private void PlayerToggleChange(string name, bool isShowing)
    {
        _audioManager.PlayClick();
        for(int i = 0; i < _playersHolder.Players.Count; i++)        
            if(string.Compare(name, _playersHolder.Players[i].ShowingName, true) == 0)
            {
                _playersHolder.Players[i].IsPlayerShowing = isShowing;
            }              
    }
}
