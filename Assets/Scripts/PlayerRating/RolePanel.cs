using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RolePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textName, _textDescription, _textWinRate;
    [SerializeField] private Image _image;

    public void Initialize(string name, string description, string winRate, Sprite sprite)
    {
        gameObject.SetActive(true);
        _textName.text = name;
        _textDescription.text = description;
        _textWinRate.text = winRate;
        _image.sprite = sprite;
    }
}
