using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinItem : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private SkinType _type;

    public static SkinItem UsedSkin => _usedSkin;

    private static SkinItem _usedSkin;

    public void Initialize(SkinData skin)
    {
        _icon.sprite = skin.icon;
        _name.text = skin.displayName;
        _price.text = skin.price + " $";
        _type = skin.type;
    }
}


