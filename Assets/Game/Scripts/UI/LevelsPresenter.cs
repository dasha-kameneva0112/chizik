using UnityEngine.SceneManagement;

public class LevelsPresenter
{
    private readonly LevelsView _levelsView;

    public LevelsPresenter(LevelsView levelsView)
    {
        _levelsView = levelsView;

        _levelsView.FirstLevelButtonClicked += SwitchToFirstLevel;
        _levelsView.SecondLevelButtonClicked += SwitchToSecondLevel;
        _levelsView.ThirdLevelButtonClicked += SwitchToThirdLevel;
        _levelsView.FourthLevelButtonClicked += SwitchToFourthLevel;
    }

    private void SwitchToFirstLevel()
    {
        SceneManager.LoadScene("Level_1");
    }

    private void SwitchToSecondLevel()
    {
        SceneManager.LoadScene("Level_2");
    }

    private void SwitchToThirdLevel()
    {
        SceneManager.LoadScene("Level_3");
    }

    private void SwitchToFourthLevel()
    {
        SceneManager.LoadScene("Level_4");
    }
}
