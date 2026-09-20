using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
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
        // загрузка скинов
        Enable(_chizikButton);
        Disable(_backgroundButton);
        Disable(_pipeButton);
    }

    private void OnBackgroundButtonClick()
    {
        // загрузка скинов
        Enable(_backgroundButton);
        Disable(_chizikButton);
        Disable(_pipeButton);
    }

    private void OnPipeButtonClick()
    {
        // загрузка скинов
        Enable(_pipeButton);
        Disable(_backgroundButton);
        Disable(_chizikButton);
    }

    private void Enable(Button btn)
    {
        var tmp1 = btn.GetComponent<Image>();
        Vector3 pos1 = tmp1.rectTransform.anchoredPosition;
        pos1.y = _enableTabPositionForY;
        tmp1.rectTransform.anchoredPosition = pos1;
        tmp1.sprite = _selected;

        var tmp = btn.GetComponentInChildren<TextMeshProUGUI>();
        Vector3 pos = tmp.rectTransform.anchoredPosition;
        pos.y = _enableTabPositionForY;
        tmp.rectTransform.anchoredPosition = pos;
    }

    private void Disable(Button btn)
    {
        var tmp1 = btn.GetComponent<Image>();
        tmp1.sprite = _normal;
        Vector3 pos1 = tmp1.rectTransform.anchoredPosition;
        pos1.y = _disableTabPositionForY;
        tmp1.rectTransform.anchoredPosition = pos1;
        
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
