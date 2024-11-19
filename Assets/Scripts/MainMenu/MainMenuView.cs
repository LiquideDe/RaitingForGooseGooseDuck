using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] Button _buttonGooseRate, _buttonTotalRate, _buttonExit, _buttonProperty;
    [SerializeField] Transform _gooseRate, _neutralRate, _duckRate;
    [SerializeField] TextMeshProUGUI _textGoose, _textNeutral, _textDuck;

    public Transform GooseRate => _gooseRate; 
    public Transform NeutralRate  => _neutralRate; 
    public Transform DuckRate => _duckRate;
    public TextMeshProUGUI TextGoose => _textGoose;
    public TextMeshProUGUI TextNeutral => _textNeutral;
    public TextMeshProUGUI TextDuck => _textDuck;

    public event Action GoToGooseRate, GoToTotalRate, Exit, GoToProperty;

    private void OnEnable()
    {
        _buttonExit.onClick.AddListener(ExitPressed);
        _buttonGooseRate.onClick.AddListener(GooseRatePressed);
        _buttonTotalRate.onClick.AddListener(TotalRatePressed);
        _buttonProperty.onClick.AddListener(Property);
    }    

    private void OnDisable()
    {
        _buttonExit.onClick.RemoveAllListeners();
        _buttonGooseRate.onClick.RemoveAllListeners();
        _buttonTotalRate.onClick.RemoveAllListeners();
        _buttonProperty.onClick.RemoveAllListeners();
    }

    public void DestroyView() => Destroy(gameObject);

    private void TotalRatePressed() => GoToTotalRate?.Invoke();

    private void GooseRatePressed() => GoToGooseRate?.Invoke();

    private void ExitPressed() => Exit?.Invoke();

    private void Property() => GoToProperty?.Invoke();
}
