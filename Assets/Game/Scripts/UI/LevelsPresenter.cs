using UnityEngine.SceneManagement;

public class LevelsPresenter
{
    private readonly LevelsView _levelsView;

    public LevelsPresenter(LevelsView levelsView)
    {
        _levelsView = levelsView;

        _levelsView.FirstLevelButtonClicked += (() => SwitchToLevel(1));
        _levelsView.SecondLevelButtonClicked += (() => SwitchToLevel(2));
        _levelsView.ThirdLevelButtonClicked += (() => SwitchToLevel(3));
        _levelsView.FourthLevelButtonClicked += (() => SwitchToLevel(4));
        _levelsView.FifthLevelButtonClicked += (() => SwitchToLevel(5));
        _levelsView.SixthLevelButtonClicked += (() => SwitchToLevel(6));
        _levelsView.SeventhLevelButtonClicked += (() => SwitchToLevel(7));
        _levelsView.EightLevelButtonClicked += (() => SwitchToLevel(8));
    }

    private void SwitchToLevel(int number)
    {
        switch(number)
        {
            case 1:
                SceneManager.LoadScene("Level_1");
                break;
            case 2:
                SceneManager.LoadScene("Level_2");
                break;
            case 3:
                SceneManager.LoadScene("Level_3");
                break;
            case 4:
                SceneManager.LoadScene("Level_4");
                break;
            case 5:
                SceneManager.LoadScene("Level_5");
                break;
            case 6:
                SceneManager.LoadScene("Level_6");
                break;
            case 7:
                SceneManager.LoadScene("Level_7");
                break;
            case 8:
                SceneManager.LoadScene("Level_8");
                break;

            default:
                break;
        }
    }
}
