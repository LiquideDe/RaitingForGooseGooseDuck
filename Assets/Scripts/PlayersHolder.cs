using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayersHolder", menuName = "Holder/PlayersHolder")]
public class PlayersHolder : ScriptableObject
{
    [SerializeField] private List<PlayerOptions> _players = new List<PlayerOptions>();

    public List<PlayerOptions> Players => _players;
}
