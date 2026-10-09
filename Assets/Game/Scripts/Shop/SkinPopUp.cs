using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkinPopUp : MonoBehaviour
{
    public event Action<SkinData> BuyButtonClick;
    public event Action<SkinData> SelectButtonClick;
    public event Action CloseButtonClick;

    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _button;
    [SerializeField] private Button _closeButton;

    private SkinData _skinData;

    void Start()
    {
        _button.onClick.AddListener(OnButtonClick);
        _closeButton.onClick.AddListener(OnCloseClick);
    }

    public void Initialize(SkinData skin)
    {
        _skinData = skin;
        _icon.sprite = skin.icon;

        // 1. Сначала ставим родной размер спрайта
        _icon.SetNativeSize();

        // 2. Потом увеличиваем в нужное количество раз (например в 2.5 раза)
        float multiplier = 2.5f; // подбери под себя: 2.5f, 2.8f, 3f — как нравится
        _icon.rectTransform.sizeDelta *= multiplier;

        _name.text = skin.displayName;
        _price.text = skin.price + " $";

        // изменить состояние кнопки
    }

    private void OnButtonClick()
    {
        var tmp = _button.GetComponentInChildren<TextMeshProUGUI>();
        
        if(tmp.text == "BUY")
        {
            BuyButtonClick?.Invoke(_skinData);
        }
        if(tmp.text == "SELECT")
        {
            SelectButtonClick?.Invoke(_skinData);
        }
    }

    private void OnCloseClick()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
        CloseButtonClick?.Invoke();
    }
}
