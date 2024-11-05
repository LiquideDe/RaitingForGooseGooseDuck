using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabHolder", menuName = "Holder/PrefabHolder")]
public class PrefabHolder : ScriptableObject
{
    [SerializeField] private GameObject _mainMenu, _playerRate, _totalRate;
    public GameObject Get(TypeScene typeScene)
    {
        switch (typeScene)
        {
            case TypeScene.MainMenu:
                return _mainMenu;

            case TypeScene.Player:
                return _playerRate;

            case TypeScene.TotalRaitePlayers:
                return _totalRate; 

            default:
                throw new System.Exception($"Не нашли {typeScene}");
        }
    }
}
