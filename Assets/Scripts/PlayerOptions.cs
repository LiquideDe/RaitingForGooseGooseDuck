using UnityEngine;

[CreateAssetMenu(fileName = "PlayerOptions", menuName = "Player/PlayerOptions")]
public class PlayerOptions : ScriptableObject
{
    [SerializeField] private string _namePlayer;
    [SerializeField] private Sprite _spriteFullSize;
    [SerializeField] private Sprite _spriteHalfSize;

    public string Name => _namePlayer;
    public Sprite FullSize => _spriteFullSize;
    public Sprite HalfSize => _spriteHalfSize;
}
