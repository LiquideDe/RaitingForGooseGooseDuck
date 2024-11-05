using UnityEngine;

[CreateAssetMenu(fileName = "PlayerOptions", menuName = "Player/PlayerOptions")]
public class PlayerOptions : ScriptableObject
{
    [SerializeField] private string _namePlayer;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _showingName;

    public string Name => _namePlayer;
    public Sprite Sprite => _sprite;
    public string ShowingName => _showingName; 
}
