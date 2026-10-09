using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinItem : MonoBehaviour
{
    public event Action<SkinData> SkinClick;

    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private SkinType _type;
    [SerializeField] private Button _button;

    //public static SkinItem UsedSkin => _usedSkin;
    //private static SkinItem _usedSkin;

    private SkinData _usedData;

    private void Start()
    {
        _button.onClick.AddListener(OnSkinClick);
    }

    private void OnSkinClick()
    {
        SkinClick?.Invoke(_usedData);
    }

    public void Initialize(SkinData skin)
    {
        _usedData = skin;
        _icon.sprite = skin.icon;
        _name.text = skin.displayName;
        _price.text = skin.price + " $";
        _type = skin.type;
    }
}


