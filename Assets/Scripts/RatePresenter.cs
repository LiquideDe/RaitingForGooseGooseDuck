using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public abstract class RatePresenter 
{
    public event Action Exit;
    protected DatabaseController _database;
    protected AudioManager _audioManager;
    protected PlayersHolder _playersHolder;
    private int _amountLvls;
    protected int _currentLvl = 0;
    private View _viewForDestroy;

    [Inject]
    private void Construct(AudioManager audioManager, PlayersHolder playersHolder)
    {
        _audioManager = audioManager;
        _playersHolder = playersHolder;
    }
    public virtual void Initialize(View view, int amountLvls)
    {
        _database = new DatabaseController();
        _viewForDestroy = view;
        _amountLvls = amountLvls;
        Subscribe();
        ShowRate();
    }

    public abstract void ShowRate();
    private void Subscribe()
    {
        _viewForDestroy.Next += NextPlayer;
        _viewForDestroy.Prev += PrevPlayer;
        _viewForDestroy.Exit += ExitDown;
    }

    private void Unscribe()
    {
        _viewForDestroy.Next -= NextPlayer;
        _viewForDestroy.Prev -= PrevPlayer;
        _viewForDestroy.Exit -= ExitDown;
    }

    private void ExitDown()
    {
        _audioManager.PlayCancel();
        Unscribe();
        _viewForDestroy.DestroyView();
        Exit?.Invoke();
    }

    private void PrevPlayer()
    {
        _audioManager.PlayClick();
        if (_currentLvl == 0)
            _currentLvl = _amountLvls - 1;
        else
            _currentLvl--;

        ShowRate();
    }

    private void NextPlayer()
    {
        _audioManager.PlayClick();
        if (_currentLvl + 1 < _amountLvls)
            _currentLvl++;
        else
            _currentLvl = 0;

        ShowRate();
    }
}
