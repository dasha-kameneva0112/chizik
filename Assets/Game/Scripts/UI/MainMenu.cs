using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public event Action PlayButtonClicked;
    public event Action LevelsButtonClicked;
    public event Action StoreButtonClicked;
    public event Action ExitButtonClicked;
    public event Action MusicButtonClicked;
    public event Action SfxButtonClicked;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _levelsButton;
    [SerializeField] private Button _storeButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _sfxButton;

    private void Start()
    {
        _playButton.onClick.AddListener(OnPlayButtonCLick);
        _levelsButton.onClick.AddListener(OnLevelsButtonClick);
        _storeButton.onClick.AddListener(OnStoreButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _musicButton.onClick.AddListener(OnMusicButtonClick);
        _sfxButton.onClick.AddListener(OnSfxButtonClick);
    }

    private void OnPlayButtonCLick()
    {
        PlayButtonClicked?.Invoke();
    }

    private void OnLevelsButtonClick()
    {
        LevelsButtonClicked?.Invoke();
    }

    private void OnStoreButtonClicked()
    {
        StoreButtonClicked?.Invoke();
    }

    private void OnExitButtonClick()
    {
        ExitButtonClicked?.Invoke();
    }

    private void OnMusicButtonClick()
    {
        MusicButtonClicked?.Invoke();
    }

    private void OnSfxButtonClick()
    {
        SfxButtonClicked?.Invoke();
    }
}
