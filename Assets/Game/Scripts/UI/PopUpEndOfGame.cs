using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopUpEndOfGame : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _popUpWinPrefab;
    [SerializeField] private GameObject _popUpLossPrefab;

    [SerializeField] private GameObject _parentForPopUp;
    [SerializeField] private GameObject _blackout;

    [SerializeField] private Button _reward;
    [SerializeField] private int _countOfReward;

    private int _numberCurrentLevel;

    private void Start()
    {
        _numberCurrentLevel = GetCurrentLevel();

        FinishPipe.Instance.OnLevelCompleted += OnWin;
        FinishPipe.Instance.OnLevelFailed += CreatePopUpLoss;
    }

    private int GetCurrentLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (!sceneName.StartsWith("Level_")) return 1;

        string numberPart = sceneName.Replace("Level_", "");

        if (int.TryParse(numberPart, out int level))
            return level;

        return 1;
    }

    public void OnWin()
    {
        _reward.gameObject.SetActive(true);
        _reward.GetComponentInChildren<TextMeshProUGUI>().text = "+" + _countOfReward;
        _reward.onClick.AddListener(CreatePopUpReward);
    }

    void CreatePopUpReward()
    {
        // Bank.Instance.AddMoney(_countOfReward);
        _reward.gameObject.SetActive(false);
        Canvas canvas = _reward.GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            canvas.sortingOrder = 999;
            canvas.overrideSorting = true; // Игнорировать родительский sorting
        }
        CreatePopUpWin();
    }

    private void HideAllPipes()
    {
        foreach (var pipe in GameObject.FindGameObjectsWithTag("Pipe"))
        {
            pipe.SetActive(false);
        }
    }

    private void CreatePopUpWin()
    {
        HideAllPipes();
        _blackout.SetActive(true);
        var popUp = Instantiate(_popUpWinPrefab, _parentForPopUp.transform);
        popUp.GetComponent<PopUpWin>().SetLevel(_numberCurrentLevel);

        popUp.GetComponent<PopUpWin>().NextLevelClick += SwitchToNextLevel;
        popUp.GetComponent<PopUpWin>().MainMenuPopUpWinClick += SwitchToMainMenu;
    }

    private void CreatePopUpLoss()
    {
        HideAllPipes();
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
