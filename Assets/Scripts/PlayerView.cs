using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerView : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] TextMeshProUGUI _textNameAndPos;

    public void Initialize(string positionAndName, Sprite sprite)
    {
        _textNameAndPos.text = positionAndName;
        _image.sprite = sprite;
    }
}
