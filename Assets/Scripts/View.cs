using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public abstract class View : MonoBehaviour
{
    [SerializeField] private Button _buttonNext, _buttonPrev, _buttonExit;
    public event Action Next, Prev, Exit;

    private void OnEnable()
    {
        _buttonNext.onClick.AddListener(NextPressed);
        _buttonPrev.onClick.AddListener(PrevPressed);
        _buttonExit.onClick.AddListener(ExitPressed);
    }

    private void OnDisable()
    {
        _buttonNext.onClick.RemoveAllListeners();
        _buttonPrev.onClick.RemoveAllListeners();
        _buttonExit.onClick.RemoveAllListeners();
    }

    public void DestroyView() => Destroy(gameObject);

    private void ExitPressed() => Exit?.Invoke();

    private void PrevPressed() => Prev?.Invoke();

    private void NextPressed() => Next?.Invoke();
}
