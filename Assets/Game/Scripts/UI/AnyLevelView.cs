using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnyLevelView : MonoBehaviour
{
    public event Action OnBackButtonClick;
    public event Action OnRestartButtonClick;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _removeCountText;

    private void Start()
    {
        _backButton.onClick.AddListener(() => OnBackButtonClick?.Invoke());
        _restartButton.onClick.AddListener(() => OnRestartButtonClick?.Invoke());
    }

    public void SetRemoveCountText(int  removeCount)
    {
        _removeCountText.text = removeCount.ToString();
    }

    public void ChangeSkin(SkinData skinData)
    {
        _background.sprite = skinData.levelSprite;
    }
}