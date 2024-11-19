using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class MainMenuPresenter : IPresenter
{
    public event Action GoToGooseRate, GoToTotalRate, GoToProperty;

    private MainMenuView _view;
    private AudioManager _audioManager;

    [Inject]
    private void Construct(AudioManager audioManager) => _audioManager = audioManager;

    public void Initialize(MainMenuView view)
    {
        _view = view;
        Subscribe();
        ShowRating();
    }

    private void Subscribe()
    {
        _view.Exit += Exit;
        _view.GoToGooseRate += GoToGooseRateDown;
        _view.GoToTotalRate += GoToTotalRateDown;
        _view.GoToProperty += GoToPropertyDown;
    }

    private void Unscribe()
    {
        _view.Exit -= Exit;
        _view.GoToGooseRate -= GoToGooseRateDown;
        _view.GoToTotalRate -= GoToTotalRateDown;
        _view.GoToProperty -= GoToPropertyDown;
    }

    private void GoToTotalRateDown()
    {
        _audioManager.PlayClick();
        Unscribe();
        _view.DestroyView();
        GoToTotalRate?.Invoke();        
    }

    private void GoToGooseRateDown()
    {
        _audioManager?.PlayClick();
        Unscribe();
        _view.DestroyView();
        GoToGooseRate?.Invoke();
    }

    private void GoToPropertyDown()
    {
        _audioManager?.PlayClick();
        Unscribe();
        _view.DestroyView();
        GoToProperty?.Invoke();
    }

    private void ShowRating()
    {
        DatabaseController databaseController = new DatabaseController();
        databaseController.StartConnection();
        int totalGames = databaseController.ExecuteOrder("SELECT Count(Number) FROM Games");
        int gooseWin = databaseController.ExecuteOrder("SELECT Count(Number) FROM Games WHERE Win = 'Гуси'");
        int duckWin = databaseController.ExecuteOrder("SELECT Count(Number) FROM Games WHERE Win = 'Нейтрал'");
        int neutralWin = databaseController.ExecuteOrder("SELECT Count(Number) FROM Games WHERE Win = 'Утки'");

        float gooseWinRate = (float)gooseWin / totalGames;
        float neutralWinRate = (float)neutralWin / totalGames;
        float duckWinRate = (float)duckWin / totalGames;

        int halfSize = (int)_view.GooseRate.GetComponent<RectTransform>().rect.height / 2;

        _view.GooseRate.position = new Vector3(_view.GooseRate.position.x, halfSize * gooseWinRate, 0);
        _view.NeutralRate.position = new Vector3(_view.NeutralRate.position.x, halfSize * neutralWinRate, 0);
        _view.DuckRate.position = new Vector3(_view.DuckRate.position.x, halfSize * duckWinRate, 0);

        _view.GooseRate.localScale = new Vector3(1, gooseWinRate, 1);
        _view.NeutralRate.localScale = new Vector3(1, neutralWinRate, 1);
        _view.DuckRate.localScale = new Vector3(1, duckWinRate, 1);        

        _view.TextGoose.text = $"{(int)(gooseWinRate * 100)}%";
        _view.TextNeutral.text = $"{(int)(neutralWinRate * 100)}%";
        _view.TextDuck.text = $"{(int)(duckWinRate * 100)}%";

        _view.TextGoose.transform.position = new Vector3(_view.TextGoose.transform.position.x, 400 * gooseWinRate);
        _view.TextNeutral.transform.position = new Vector3(_view.TextNeutral.transform.position.x, 400 * neutralWinRate);
        _view.TextDuck.transform.position = new Vector3(_view.TextDuck.transform.position.x, 400 * duckWinRate);
        databaseController.CloseConnection();
    }

    private void Exit() => Application.Quit();
    
}
