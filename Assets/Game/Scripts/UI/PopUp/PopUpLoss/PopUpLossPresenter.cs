using UnityEngine.SceneManagement;

public class PopUpLossPresenter
{
    private readonly PopUpLossModel _popUpLossModel;
    private readonly PopUpLossView _popUpLossView;
    private readonly FinishPipe _finishPipe;

    public PopUpLossPresenter(PopUpLossModel popUpWinModel, PopUpLossView popUpWinView, FinishPipe finishPipe)
    {
        _popUpLossModel = popUpWinModel;
        _popUpLossView = popUpWinView;
        _finishPipe = finishPipe;

        _finishPipe.OnLevelFailed += CreatePopUpLoss;
        
        _popUpLossView.TryAnainClicked += SwitchToTryAgain;
        _popUpLossView.MainMenuPopUpLossClicked += SwitchToMainMenu;
    }

    private void CreatePopUpLoss()
    {
        _popUpLossView.HideAllPipes();
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
