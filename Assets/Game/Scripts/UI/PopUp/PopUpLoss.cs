using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpLoss : MonoBehaviour
{
    public event Action TryAgainClick;
    public event Action MainMenuPopUpLossClick;

    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private Button _mainMenuButton;

    void Start()
    {
        _tryAgainButton.onClick.AddListener(OnTryAgainClick);
        _mainMenuButton.onClick.AddListener(OnMainMenuClick);
    }

    private void OnTryAgainClick()
    {
        TryAgainClick?.Invoke();
    }

    private void OnMainMenuClick()
    {
        MainMenuPopUpLossClick?.Invoke();
    }
}
