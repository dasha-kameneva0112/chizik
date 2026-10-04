using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopUpEndOfGame : MonoBehaviour
{
    [SerializeField] private Button _win;
    [SerializeField] private Button _leave;

    [Header("Prefabs")]
    [SerializeField] private GameObject _popUpWinPrefab;
    [SerializeField] private GameObject _popUpLossPrefab;

    [SerializeField] private GameObject _parentForPopUp;
    [SerializeField] private GameObject _blackout;

    [SerializeField] private Button _reward;
    [SerializeField] private int _countOfReward;

    private int _numberCurrentLevel = 3;

    void Start()
    {
        // привязываем к событиям
        // победа => CreatePopUpWin;
        // поражение CreatePopUpLoss;

        // временно - кнопки 
        _win.onClick.AddListener(OnWin);
        _leave.onClick.AddListener(OnLoss);
    }

    void OnWin()
    {
        _blackout.SetActive(true);
        _reward.interactable = true;
        _reward.GetComponentInChildren<TextMeshProUGUI>().text = "+" + _countOfReward;
        _reward.onClick.AddListener(CreatePopUpReward);
    }
    void CreatePopUpReward()
    {
        Bank.Instance.AddMoney(_countOfReward);
        CreatePopUpWin();
    }
    void OnLoss()
    {
        CreatePopUpLoss();
    }

    private void CreatePopUpWin()
    {
        _blackout.SetActive(true);
        var popUp = Instantiate(_popUpWinPrefab, _parentForPopUp.transform);
        popUp.GetComponent<PopUpWin>().SetLevel(_numberCurrentLevel);

        popUp.GetComponent<PopUpWin>().NextLevelClick += SwitchToNextLevel;
        popUp.GetComponent<PopUpWin>().MainMenuPopUpWinClick += SwitchToMainMenu;
    }

    private void CreatePopUpLoss()
    {
        _blackout.SetActive(true);
        var popUp = Instantiate(_popUpLossPrefab, _parentForPopUp.transform);

        popUp.GetComponent<PopUpLoss>().TryAgainClick += SwitchToTryAgain;
        popUp.GetComponent<PopUpLoss>().MainMenuPopUpLossClick += SwitchToMainMenu;
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

    private void SwitchToTryAgain()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
