using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerRatingView : View
{
    [SerializeField] private TextMeshProUGUI _textName, _textTotalGames, _textDeath, _textKicked, _textKickedAsPeacefull;
    [SerializeField] private Image _imagePortrait;
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private RolePanel _rolePanelPrefab;
    [SerializeField] private Transform _content;
    [SerializeField] private RoleImages[] roleImages;

    private List<RolePanel> _rolePanels = new List<RolePanel>();

    public void ShowCharacter(Sprite sprite, string textName, string totalGames, string death, string kicked, string kickedAsPeacefull)
    {
        _scrollbar.value = 0;
        _textName.text = textName;
        _textTotalGames.text = totalGames;
        _textDeath.text = death;
        _textKicked.text = kicked;
        _textKickedAsPeacefull.text = kickedAsPeacefull;
        _imagePortrait.sprite = sprite;

        foreach (RolePanel rolePanel in _rolePanels)
        {
            Destroy(rolePanel.gameObject);
        }
        _rolePanels.Clear();
    }

    public void ShowProfession(string nameProfession, string textDescriptionGames, string howOften)
    {
        RolePanel rolePanel = Instantiate(_rolePanelPrefab, _content);
        rolePanel.Initialize(nameProfession, textDescriptionGames, howOften, GetSprite(nameProfession));
        _rolePanels.Add(rolePanel);
    }

    private Sprite GetSprite(string name) 
    {
        foreach (var item in roleImages)
        {
            if (string.Compare(item._roleName, name, true) == 0)
                return item._roleSprite;
        }

        return null;
    }


}
