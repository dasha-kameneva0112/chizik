using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpWin : MonoBehaviour
{
    public event Action NextLevelClick;
    public event Action MainMenuPopUpWinClick;

    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private TMP_Text _level;

    void Start()
    {
        _nextLevelButton.onClick.AddListener(OnNextLevelClick);
        _mainMenuButton.onClick.AddListener(OnMainMenuClick);
    }

    public void SetLevel(int numberOfLevel)
    {
        _level.text = "LEVEL " + numberOfLevel;
    }

    private void OnNextLevelClick()
    {
        NextLevelClick?.Invoke();
    }

    private void OnMainMenuClick()
    {
        MainMenuPopUpWinClick?.Invoke();
    }
}