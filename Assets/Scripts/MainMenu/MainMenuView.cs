using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] Button _buttonGooseRate, _buttonTotalRate, _buttonExit;
    public event Action GoToGooseRate, GoToTotalRate, Exit;

    private void OnEnable()
    {
        _buttonExit.onClick.AddListener(ExitPressed);
        _buttonGooseRate.onClick.AddListener(GooseRatePressed);
        _buttonTotalRate.onClick.AddListener(TotalRatePressed);
    }

    private void OnDisable()
    {
        _buttonExit.onClick.RemoveAllListeners();
        _buttonGooseRate.onClick.RemoveAllListeners();
        _buttonTotalRate.onClick.RemoveAllListeners();
    }

    public void DestroyView() => Destroy(gameObject);

    private void TotalRatePressed() => GoToTotalRate?.Invoke();

    private void GooseRatePressed() => GoToGooseRate?.Invoke();

    private void ExitPressed() => Exit?.Invoke();
}
