using UnityEngine;

[CreateAssetMenu(menuName = "Store/Skin Data")]
public class SkinData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public int price;
    public SkinType type;

    [Header("Only background")]
    public Sprite levelSprite;
}

public enum SkinType
{
    Chizik,
    Background,
    Pipe
}