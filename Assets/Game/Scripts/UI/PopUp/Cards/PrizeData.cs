using UnityEngine;

[CreateAssetMenu(menuName = "PrizeData")]
public class PrizeData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public int count;
    public PrizeType type;
}

public enum PrizeType
{
    Brush,
    Hammer,
    Money
}