using System;
using NUnit.Framework.Interfaces;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkinPopUp : MonoBehaviour
{
    public event Action BuyButtonClick;
    public event Action SelectButtonClick;
    public event Action CloseButtonClick;
    public event Action OnUpdateNeeded;

    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _button;
    [SerializeField] private Button _closeButton;

    [SerializeField] private float _enableTabPositionForY;

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

        _icon.SetNativeSize();
        float multiplier = 2.5f; 
        _icon.rectTransform.sizeDelta *= multiplier;

        _name.text = skin.displayName;

        Status status = SkinManager.Instance.GetStatus(skin);
        if(status == Status.NotBought)
        {
            _button.GetComponentInChildren<TextMeshProUGUI>().text = "BUY";
            _price.text = skin.price + " $";
            if(skin.price > Bank.Instance.Money)
            {
                _button.interactable = false;
                //var outline = _button.gameObject.GetComponent<Outline>();
                //outline = _button.gameObject.AddComponent<Outline>();
                //outline.effectColor = new Color(0.5f, 0.5f, 0.5f, 1f);
                //outline.effectDistance = new Vector2(4, -4);
            }
        }
        else if(status == Status.Purchased)
        {
            _button.GetComponentInChildren<TextMeshProUGUI>().text = "SELECT";
            _button.interactable = true; 
            _price.gameObject.SetActive(false);

            Vector3 pos = _name.rectTransform.anchoredPosition;
            pos.y = _enableTabPositionForY;
            _name.rectTransform.anchoredPosition = pos;

            multiplier = 1.2f;
            _name.rectTransform.localScale *= multiplier;
        }
        else
        {
            _button.GetComponentInChildren<TextMeshProUGUI>().text = "SELECT";
            _button.interactable = false;
            _price.gameObject.SetActive(false);

            Vector3 pos = _name.rectTransform.anchoredPosition;
            pos.y = _enableTabPositionForY;
            _name.rectTransform.anchoredPosition = pos;

            multiplier = 1.2f;
            _name.rectTransform.localScale *= multiplier;
        }
    }

    private void OnButtonClick()
    {
        var tmp = _button.GetComponentInChildren<TextMeshProUGUI>();
        
        if(tmp.text == "BUY")
        {
            if (SkinManager.Instance.TryBuySkin(_skinData))
            {
                BuyButtonClick?.Invoke();
                OnUpdateNeeded?.Invoke();
            }
        }
        if(tmp.text == "SELECT")
        {
            SkinManager.Instance.SelectSkin(_skinData);
            SelectButtonClick?.Invoke();
        }

        Initialize(_skinData);
        OnUpdateNeeded?.Invoke();
    }

    private void OnCloseClick()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
        CloseButtonClick?.Invoke();
    }
}
