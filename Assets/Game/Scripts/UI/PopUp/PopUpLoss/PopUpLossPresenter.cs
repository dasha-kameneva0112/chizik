using UnityEngine.SceneManagement;

public class PopUpLossPresenter
{
    private readonly PopUpLossView _popUpLossView;
    private readonly AudioManager _audioManager;
    private readonly FinishPipe _finishPipe;
    private readonly LevelTimer _levelTimer;

    public PopUpLossPresenter(PopUpLossView popUpWinView, FinishPipe finishPipe, AudioManager audioManager, LevelTimer levelTimer)
    {
        _popUpLossView = popUpWinView;
        _finishPipe = finishPipe;
        _audioManager = audioManager;
        _levelTimer = levelTimer;

        _finishPipe.OnLevelFailed += CreatePopUpLoss;

        if (_levelTimer != null)
        {
            _levelTimer.OnTimeUp += CreatePopUpLoss;
        }
        
        _popUpLossView.TryAnainClicked += SwitchToTryAgain;
        _popUpLossView.MainMenuPopUpLossClicked += SwitchToMainMenu;
    }

    private void CreatePopUpLoss()
    {
        _popUpLossView.HideAllPipes();
        _audioManager.TurnOnSfxLoose();
        _popUpLossView.CreatePopUpLoss();
    }

    private void SwitchToTryAgain()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    private void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
