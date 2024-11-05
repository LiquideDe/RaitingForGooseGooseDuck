using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class MainMenuPresenter : IPresenter
{
    public event Action GoToGooseRate, GoToTotalRate;

    private MainMenuView _view;
    private AudioManager _audioManager;

    [Inject]
    private void Construct(AudioManager audioManager) => _audioManager = audioManager;

    public void Initialize(MainMenuView view)
    {
        _view = view;
        Subscribe();
    }

    private void Subscribe()
    {
        _view.Exit += Exit;
        _view.GoToGooseRate += GoToGooseRateDown;
        _view.GoToTotalRate += GoToTotalRateDown;
    }

    private void GoToTotalRateDown()
    {
        _audioManager.PlayClick();
        _view.DestroyView();
        GoToTotalRate?.Invoke();        
    }

    private void GoToGooseRateDown()
    {
        _audioManager?.PlayClick();
        _view.DestroyView();
        GoToGooseRate?.Invoke();
    }

    private void Exit() => Application.Quit();
    
}
