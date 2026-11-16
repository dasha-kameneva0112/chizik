using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopUpWinController : MonoBehaviour
{
    [SerializeField] private GameObject _popUpWinPrefab;
    [SerializeField] private GameObject _parentForPopUp;
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _blackout;
    [SerializeField] private Button _reward;
    [SerializeField] private int _countOfReward;
    private int _numberCurrentLevel;

    private void Start()
    {
        _numberCurrentLevel = LevelProgress.Instance.CurrentLevelNumber;
        FinishPipe.Instance.OnLevelCompleted += OnWin;
    }

    private void OnWin()
    {
        HideAllPipes();
        _background.GetComponent<Image>().sprite = GameSettings.Instance.CurrentBackgroundSkin.winBackground;
        LevelProgress.Instance.UpdateNumberLevel();

        _reward.gameObject.SetActive(true);
        _reward.GetComponentInChildren<TextMeshProUGUI>().text = "+" + _countOfReward;
        _reward.onClick.AddListener(CreatePopUpReward);
    }

    void CreatePopUpReward()
    {
        Bank.Instance.DepositingMoney(_countOfReward);
        _reward.gameObject.SetActive(false);
        CreatePopUpWin();
    }

    private void CreatePopUpWin()
    {
        _blackout.SetActive(true);
        var popUp = Instantiate(_popUpWinPrefab, _parentForPopUp.transform);
        popUp.GetComponent<PopUpWin>().SetLevel(_numberCurrentLevel);

        popUp.GetComponent<PopUpWin>().NextLevelClick += SwitchToNextLevel;
        popUp.GetComponent<PopUpWin>().MainMenuPopUpWinClick += SwitchToMainMenu;
    }

    private void SwitchToNextLevel()
    {
        _numberCurrentLevel++;
        SceneManager.LoadScene("Level_" + _numberCurrentLevel);
    }

    private void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void HideAllPipes()
    {
        foreach (var pipe in GameObject.FindGameObjectsWithTag("Pipe"))
        {
            pipe.SetActive(false);
        }
    }
}
