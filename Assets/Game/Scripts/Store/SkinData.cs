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

    [Header("Only pipe")]
    public Sprite cornerPipe;
    public Sprite triangularPipe;
    public Sprite cruciformPipe;
}

public enum SkinType
{
    Chizik,
    Background,
    Pipe
}