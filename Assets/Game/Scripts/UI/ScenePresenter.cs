using UnityEngine.SceneManagement;

public class ScenePresenter
{
    private readonly IGameSettings _gameSetting;
    private readonly ILevelProgress _levelProgress;
    private readonly SceneView _sceneView;

    public ScenePresenter(IGameSettings gameSetting, ILevelProgress levelProgress, SceneView sceneView)
    {
        _gameSetting = gameSetting;
        _levelProgress = levelProgress;
        _sceneView = sceneView;

        _sceneView.PlayButtonClicked += SwitchToGamePlay;
        _sceneView.LevelsButtonClicked += _sceneView.SwitchToLevels;
        _sceneView.StoreButtonClicked += _sceneView.SwitchToStore;
        _sceneView.BackButtonClicked += _sceneView.SwitchToMenu;
        _sceneView.StoreClosedButtonClicked += _sceneView.SwitchToMenu;

        _gameSetting.SkinChanged += ChangeSkin;
        ChangeSkin();
    }

    private void SwitchToGamePlay()
    {
        int numberOfCurrentLevel = _levelProgress.CurrentLevelNumber;

        if (numberOfCurrentLevel == 1)
            SceneManager.LoadScene("Level_1");
        if(numberOfCurrentLevel == 2)
            SceneManager.LoadScene("Level_2");
        if(numberOfCurrentLevel == 3)
            SceneManager.LoadScene("Level_3");
        if( numberOfCurrentLevel == 4)
            SceneManager.LoadScene("Level_4");
        if (numberOfCurrentLevel == 5)
            SceneManager.LoadScene("Level_5");
        if (numberOfCurrentLevel == 6)
            SceneManager.LoadScene("Level_6");
        if (numberOfCurrentLevel == 7)
            SceneManager.LoadScene("Level_7");
        if (numberOfCurrentLevel == 8)
            SceneManager.LoadScene("Level_8");
    }

    private void ChangeSkin()
    {
        SkinData chizik = _gameSetting.CurrentChizikSkin;
        SkinData background = _gameSetting.CurrentBackgroundSkin;

        if (chizik != null)
            _sceneView.SetChizhik(chizik.icon);
        if (background != null)
            _sceneView.SetBackground(background.icon);
    }

    public void Dispose()
    {
        if (_gameSetting != null)
        {
            _gameSetting.SkinChanged -= ChangeSkin;
        }
    }
}
