using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneController : MonoBehaviour
{
    [SerializeField] private Levels _levels;

    private void Start()
    {
        _levels.FirstLevelButtonClicked += SwitchToFirstLevel;
        _levels.SecondLevelButtonClicked += SwitchToSecondLevel;
        _levels.ThirdLevelButtonClicked += SwitchToThirdLevel;
        _levels.FourthLevelButtonClicked += SwitchToFoutyhLevel;
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

    private void SwitchToFoutyhLevel()
    {
        SceneManager.LoadScene("Level_4");
    }
}
