using UnityEngine.SceneManagement;

public class PopUpWinPresenter
{
    private readonly PopUpWinModel _popUpWinModel;
    private readonly PopUpWinView _popUpWinView;
    private readonly FinishPipe _finishPipe;
    private readonly IGameSettings _gameSettings;
    private readonly AudioManager _audioManager;

    public PopUpWinPresenter(PopUpWinModel popUpWinModel, PopUpWinView popUpWinView, FinishPipe finishPipe, IGameSettings gameSettings, AudioManager audioManager)
    {
        _popUpWinModel = popUpWinModel;
        _popUpWinView = popUpWinView;
        _finishPipe = finishPipe;
        _gameSettings = gameSettings;
        _audioManager = audioManager;

        _finishPipe.OnLevelCompleted += OnWin;
        _popUpWinView.RewardClicked += OnRewardClicked;
        _popUpWinView.NextLevelClicked += SwitchToNextLevel;
        _popUpWinView.MainMenuPopUpWinClicked += SwitchToMainMenu;
    }

    public void OnWin()
    {
        _popUpWinView.HideAllPipes();
        _popUpWinView.SetWinBackground(_gameSettings.CurrentBackgroundSkin.winBackground);
        _popUpWinModel.CompleteLevel();
        _audioManager.TurnOnSfxWin();
        _popUpWinView.ShowReward(_popUpWinModel.CountOfReward);
    }

    public void OnRewardClicked()
    {
        _popUpWinModel.AddReward();
        _popUpWinView.HideReward();
        _popUpWinView.CreatePopUpWin(_popUpWinModel.NumberCurrentLevel);
    }

    private void SwitchToNextLevel()
    {
        int level = (_popUpWinModel.NumberCurrentLevel) + 1;
        SceneManager.LoadScene("Level_" + level);
    }

    private void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}