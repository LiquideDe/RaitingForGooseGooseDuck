using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerProperty : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _textName;
    [SerializeField] private Toggle _toggle;
    public event Action<string,bool> ToggleChange;

    private void OnEnable() => _toggle.onValueChanged.AddListener(ChangeShowing);    

    private void OnDisable() => _toggle.onValueChanged.RemoveAllListeners();

    public void Initialize(PlayerOptions player)
    {
        gameObject.SetActive(true);
        _textName.text = player.ShowingName;
        _toggle.isOn = player.IsPlayerShowing;
        _image.sprite = player.Sprite;
    }

    public void DestroyPanel() => Destroy(gameObject);

    private void ChangeShowing(bool isShowing) => ToggleChange?.Invoke(_textName.text, isShowing);
}
