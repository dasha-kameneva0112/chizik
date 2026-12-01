using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AnyLevel 
{
    public event Action BackButtonClicked;
    public event Action RestartButtonClicked;
    public event Action<int> RemoveCountUpdate;
    public static AnyLevel Instance => _instance;
    public int RemoveCount => _removeCount;

    private static AnyLevel _instance;
    private IGameSettings _gameSettings;
    private readonly AnyLevelView _anyLevelView;
    private int _removeCount;

    public AnyLevel(AnyLevelView anyLevelView, int removeCount, IGameSettings gameSettings)
    {
        _anyLevelView = anyLevelView;
        _instance = this;
        _removeCount = removeCount;
        _gameSettings = gameSettings;

        _anyLevelView.SetRemoveCountText(_removeCount);
        _anyLevelView.ChangeSkin(_gameSettings.CurrentBackgroundSkin);
        _anyLevelView.OnBackButtonClick += OnBackButtonClicked;
        _anyLevelView.OnRestartButtonClick += OnRestartButtonClicked;
    }

    public void UpdateRemoveCount()
    {
        _removeCount--;
        _anyLevelView.SetRemoveCountText(_removeCount);
        RemoveCountUpdate?.Invoke(RemoveCount);
    }

    private void OnBackButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnRestartButtonClicked()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
