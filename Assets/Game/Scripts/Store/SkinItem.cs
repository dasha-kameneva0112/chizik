using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class SkinItem : MonoBehaviour
{
    public event Action<SkinData> SkinClick;

    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private SkinType _type;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _background;

    [Header("Skins Sprits")]
    [SerializeField] private Sprite _selected;
    [SerializeField] private Sprite _normal;

    //public static SkinItem UsedSkin => _usedSkin;
    //private static SkinItem _usedSkin;

    private SkinData _usedData;
    [SerializeField] private float _boughtPositionForY;

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

    public void UpdateSkinStatus()
    {
        Status status = SkinManager.Instance.GetStatus(_usedData);
        if(status == Status.Selected)
        {
            _background.GetComponent<Image>().sprite = _selected;
            _price.gameObject.SetActive(false);

            Vector3 pos = _name.rectTransform.anchoredPosition;
            pos.y = _boughtPositionForY;
            _name.rectTransform.anchoredPosition = pos;
        }
        if(status == Status.Purchased)
        {
            _background.GetComponent<Image>().sprite = _normal;
            _price.gameObject.SetActive(false);

            Vector3 pos = _name.rectTransform.anchoredPosition;
            pos.y = _boughtPositionForY;
            _name.rectTransform.anchoredPosition = pos;
        }
    }
}


