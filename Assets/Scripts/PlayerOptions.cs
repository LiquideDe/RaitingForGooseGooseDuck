using UnityEngine;

[CreateAssetMenu(fileName = "PlayerOptions", menuName = "Player/PlayerOptions")]
public class PlayerOptions : ScriptableObject
{
    [SerializeField] private string _namePlayer;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _showingName;
    [SerializeField] private bool _isPlayerShowing = true;

    public string Name => _namePlayer;
    public Sprite Sprite => _sprite;
    public string ShowingName => _showingName;

    public bool IsPlayerShowing { get => _isPlayerShowing; set => _isPlayerShowing =value;  }
}
