using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopUpEndOfGame : MonoBehaviour
{
    public static PopUpEndOfGame instance;

    [Header("Prefabs")]
    [SerializeField] private GameObject _popUpWinPrefab;
    [SerializeField] private GameObject _popUpLossPrefab;

    [SerializeField] private GameObject _parentForPopUp;
    [SerializeField] private GameObject _blackout;

    [SerializeField] private Button _reward;
    [SerializeField] private int _countOfReward;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _numberCurrentLevel = getCurrentLevel();
    }
    public void OnWin()
    {
        CreatePopUpWin();
    }
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
    public void OnLoss()
    {
        CreatePopUpLoss();
    }

    private int getCurrentLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        return int.Parse(sceneName.Replace("Level_", ""));
    }

    private void hideAllPipes()
    {
        foreach (var pipe in GameObject.FindGameObjectsWithTag("Pipe"))
        {
            pipe.SetActive(false);
        }
    }

    private void CreatePopUpWin()
    {
        hideAllPipes();
        _blackout.SetActive(true);
        var popUp = Instantiate(_popUpWinPrefab, _parentForPopUp.transform);
        popUp.GetComponent<PopUpWin>().SetLevel(_numberCurrentLevel);

        popUp.GetComponent<PopUpWin>().NextLevelClick += SwitchToNextLevel;
        popUp.GetComponent<PopUpWin>().MainMenuPopUpWinClick += SwitchToMainMenu;
    }

    private void CreatePopUpLoss()
    {
        hideAllPipes();
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
