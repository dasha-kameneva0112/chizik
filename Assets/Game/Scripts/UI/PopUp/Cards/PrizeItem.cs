using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrizeItem : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _count;
    [SerializeField] private PrizeType _type;

    private PrizeData _data;

    public void Initialize(PrizeData data)
    {
        _data = data;
        _type = _data.type;
        _count.text = "+" + _data.count;
        _icon.sprite = _data.icon;
        //_icon.SetNativeSize();
        //if (_type != PrizeType.Money)
        //{
        //    
        //    float multiplier = 4.0f;
        //    _icon.rectTransform.sizeDelta *= multiplier;
        //}

    }
}
