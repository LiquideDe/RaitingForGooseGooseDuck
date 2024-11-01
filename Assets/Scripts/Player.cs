using System;
using UnityEngine;

public class Player
{
    private string _name;
    private int _amountGamesTotal, _amountGamesDuck, _amountGamesGoose, _amountGamesPigeon, _amountGamesPelican, _amountGamesDodo;
    private int _amountVictoriesDuck, _amountVictoriesGoose, _amountVictoriesPigeon, _amountVictoriesPelican, _amountVictoriesDodo;
    private int _kickedGoose, _kickedDuck;
    private float _rate;
    private PlayerOptions _playerOptions;

    public Player(PlayerOptions playerOptions)
    {
        _name = playerOptions.Name;
        _playerOptions = playerOptions;
    }

    public float Rate => _rate;

    public int AmountGamesTotal => _amountGamesTotal;
    public int AmountGamesDuck  => _amountGamesDuck;
    public int AmountGamesGoose => _amountGamesGoose;
    public int AmountGamesPigeon => _amountGamesPigeon; 
    public int AmountGamesPelican => _amountGamesPelican;
    public int AmountGamesDodo => _amountGamesDodo; 
    public int AmountVictoriesDuck => _amountVictoriesDuck;
    public int AmountVictoriesGoose  => _amountVictoriesGoose; 
    public int AmountVictoriesPigeon => _amountVictoriesPigeon; 
    public int AmountVictoriesPelican => _amountVictoriesPelican;
    public int AmountVictoriesDodo => _amountVictoriesDodo;
    public string Name => _name;

    public PlayerOptions PlayerOptions => _playerOptions; 
    public void SetTotalGames(int games) => _amountGamesTotal = games;

    public void AddGooseGames(int games, int victories)
    {
        _amountGamesGoose += games;
        _amountVictoriesGoose += victories;
        CalculateRate(games);
    }

    public void AddDuckGames(int games, int victories)
    {
        _amountGamesDuck += games;
        _amountVictoriesDuck += victories;
        CalculateRate(games);
    }

    public void AddPigeonGames(int games, int victories)
    {
        _amountGamesPigeon += games;
        _amountVictoriesPigeon += victories;
        CalculateRate(games);
    }

    public void AddPelicanGames(int games, int victories)
    {
        _amountGamesPelican += games;
        _amountVictoriesPelican += victories;
        CalculateRate(games);
    }

    public void AddDodoGames(int games, int victories)
    {
        _amountGamesDodo += games;
        _amountVictoriesDodo += victories;
        CalculateRate(games);
    }

    public void CalculateDuckGames() => CalculateRate(_amountGamesDuck);    

    public void CalculateGooseGames() => CalculateRate(_amountGamesGoose);

    public void CalculateDuckRate()
    {
        float rate = (float)_amountVictoriesDuck / (float)_amountGamesDuck * 100;
        _rate = (float)Math.Round(rate, 2);
    }

    public void CalculateGooseRate()
    {
        float rate = (float)_amountVictoriesGoose / (float)_amountGamesGoose * 100;
        _rate = (float)Math.Round(rate, 2);
    }

    public void CalculatePigeonRate()
    {
        float rate = (float)_amountVictoriesPigeon / (float)_amountGamesPigeon * 100;
        _rate = (float)Math.Round(rate, 2);
    }

    public void CalculatePelicanRate()
    {
        if (_amountGamesPelican > 0)
        {
            float rate = (float)_amountVictoriesPelican / (float)_amountGamesPelican * 100;
            _rate = (float)Math.Round(rate, 2);
        }
        else
            _rate = 0;


    }

    public void CalculateDodoRate()
    {
        float rate = (float)_amountVictoriesDodo / (float)_amountGamesDodo * 100;
        _rate = (float)Math.Round(rate, 2);
    }

    public void CalculateTotalWinRate() => CalculateRate(_amountVictoriesDuck + _amountVictoriesGoose + _amountVictoriesPigeon + _amountVictoriesPelican + _amountVictoriesDodo);

    public void CalculateKickedRate(int amount) => CalculateRate(amount);

    private void CalculateRate(int games)
    {
        float rawRate = (float)games / (float)_amountGamesTotal;
        _rate = (float)Math.Round(rawRate*100, 2);
    }


}
