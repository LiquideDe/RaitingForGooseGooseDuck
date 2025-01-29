using UnityEngine;

[CreateAssetMenu(fileName = "ImageHolder", menuName = "Holder/ImageHolder")]
public class RoleImages : ScriptableObject
{
    [field: SerializeField] public Sprite _roleSprite { get; private set; }
    [field: SerializeField] public string _roleName { get; private set; }
}
