using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public event Action ChizikButtonClick;
    public event Action BackgroundButtonClick;
    public event Action PipeButtonClick;
    public event Action CloseButtonClick;

    [SerializeField] private Button _chizikButton;
    [SerializeField] private Button _backgroundButton;
    [SerializeField] private Button _pipeButton;
    [SerializeField] private Button _closeButton;

    [Header("Button Sprits")]
    [SerializeField] private Sprite _selected;
    [SerializeField] private Sprite _normal;

    [Header("Position y for tab")]
    [SerializeField] private float _enableTabPositionForY;
    [SerializeField] private float _disableTabPositionForY;

    [Header("Height for tab")]
    [SerializeField] private float _enableTabHeight;
    [SerializeField] private float _disableTabHeight;

    void Start()
    {
        _chizikButton.onClick.AddListener(OnChizikButtonClick);
        _backgroundButton.onClick.AddListener(OnBackgroundButtonClick);
        _pipeButton.onClick.AddListener(OnPipeButtonClick);
        _closeButton.onClick.AddListener(OnCloseButtonClick);

        OnChizikButtonClick();
    }

    private void OnChizikButtonClick()
    {
        Enable(_chizikButton);
        Disable(_backgroundButton);
        Disable(_pipeButton);

        ChizikButtonClick?.Invoke();
    }

    private void OnBackgroundButtonClick()
    {
        Enable(_backgroundButton);
        Disable(_chizikButton);
        Disable(_pipeButton);

        BackgroundButtonClick?.Invoke();
    }

    private void OnPipeButtonClick()
    {
        Enable(_pipeButton);
        Disable(_backgroundButton);
        Disable(_chizikButton);

        PipeButtonClick?.Invoke();
    }

    private void Enable(Button btn)
    {
        var tmp1 = btn.GetComponent<Image>();
        tmp1.sprite = _selected;

        RectTransform rt = btn.GetComponent<RectTransform>(); 
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, _enableTabHeight);

        var tmp = btn.GetComponentInChildren<TextMeshProUGUI>();
        Vector3 pos = tmp.rectTransform.anchoredPosition;
        pos.y = _enableTabPositionForY;
        tmp.rectTransform.anchoredPosition = pos;
    }

    private void Disable(Button btn)
    {
        var tmp1 = btn.GetComponent<Image>();
        tmp1.sprite = _normal;

        RectTransform rt = btn.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, _disableTabHeight);

        var tmp = btn.GetComponentInChildren<TextMeshProUGUI>();
        Vector3 pos = tmp.rectTransform.anchoredPosition;
        pos.y = _disableTabPositionForY;
        tmp.rectTransform.anchoredPosition = pos;
    }

    private void OnCloseButtonClick()
    {
        CloseButtonClick?.Invoke();
    }
}
