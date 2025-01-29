using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textNameAndPlace;
    [SerializeField] private Image _image;

    public void Initialize(Player player, string ed)
    {
        gameObject.SetActive(true);
        _image.sprite = player.Sprite;
        _textNameAndPlace.text = $"{player.Place} - {player.ShowingName} ({player.WinRate}{ed})";
    }

    public void DestroyPanel() => Destroy(gameObject);
}
