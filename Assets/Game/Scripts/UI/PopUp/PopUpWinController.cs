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

    private Bank _bank; //SOLID -- 

    private void Start()
    {
        _bank = Bank.Instance;

        _numberCurrentLevel = LevelProgress.Instance.GetOpenLevel();
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
        _bank.DepositMoney(_countOfReward);
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

//using TMPro;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class Level : MonoBehaviour
//{
//    private void Start()
//    {
//        new PopUpWinController();
//    }
//}

//public class Resources : MonoBehaviour
//{

//}

//MV *  ->MVC->MVP->MVVM

//    Model View  Presenter


//public class PopUpWinModel
//{
//    private int _countOfReward;
//    private int _numberCurrentLevel;
//}

//public class PopUpWinView : MonoBehaviour
//{
//    [SerializeField] private GameObject _popUpWinPrefab;
//    [SerializeField] private GameObject _parentForPopUp;
//    [SerializeField] private GameObject _background;
//    [SerializeField] private GameObject _blackout;
//    [SerializeField] private Button _reward;
//}


//public class PopUpWinPresenter
//{
//    private readonly PopUpWinModel popUpWinModel;
//    private readonly PopUpWinView popUpWinView;

//}


//public class Level : MonoBehaviour
//{
//    private void Start()
//    {
//        new PopUpWinController();
//    }
//}

//public class Resources : MonoBehaviour
//{

//}

//MV *  ->MVC->MVP->MVVM

//    Model View  Presenter


//public class PopUpWinModel
//{
//    private int _countOfReward;
//    private int _numberCurrentLevel;
//}

//public class PopUpWinView : MonoBehaviour
//{
//    [SerializeField] private GameObject _popUpWinPrefab;
//    [SerializeField] private GameObject _parentForPopUp;
//    [SerializeField] private GameObject _background;
//    [SerializeField] private GameObject _blackout;
//    [SerializeField] private Button _reward;
//}


//public class PopUpWinPresenter
//{
//    private readonly PopUpWinModel _popUpWinModel;
//    private readonly PopUpWinView _popUpWinView;

//    public PopUpWinPresenter(PopUpWinModel popUpWinModel, PopUpWinView popUpWinView)
//    {
//        _popUpWinModel = popUpWinModel;
//        _popUpWinView = popUpWinView;
//    }

//    public void OnWin()
//    {

//    }
//}

//public class Level : MonoBehaviour
//{
//    [SerializeField] private PopUpWinView PopUpWinView;

//    private void Awake()
//    {
//        new PopUpWinPresenter(new PopUpWinModel(), PopUpWinView);
//    }
//}


//public class PopUpWinController
//{
//    [SerializeField] private GameObject _popUpWinPrefab;
//    [SerializeField] private GameObject _parentForPopUp;
//    [SerializeField] private GameObject _background;
//    [SerializeField] private GameObject _blackout;
//    [SerializeField] private Button _reward;
//    [SerializeField] private int _countOfReward;
//    private int _numberCurrentLevel;

//    private IBank _bank;

//    public PopUpWinController(IBank bank)
//    {
//        _bank = bank;
//    }

//    private void Start()
//    {
//        _numberCurrentLevel = LevelProgress.Instance.GetOpenLevel();
//        FinishPipe.Instance.OnLevelCompleted += OnWin;
//    }

//    private void OnWin()
//    {
//        HideAllPipes();
//        _background.GetComponent<Image>().sprite = GameSettings.Instance.CurrentBackgroundSkin.winBackground;
//        LevelProgress.Instance.UpdateNumberLevel();

//        _reward.gameObject.SetActive(true);
//        _reward.GetComponentInChildren<TextMeshProUGUI>().text = "+" + _countOfReward;
//        _reward.onClick.AddListener(CreatePopUpReward);
//    }

//    void CreatePopUpReward()
//    {
//        _bank.DepositingMoney(_countOfReward);
//        _reward.gameObject.SetActive(false);
//        CreatePopUpWin();
//    }

//    private void CreatePopUpWin()
//    {
//        _blackout.SetActive(true);
//        var popUp = Instantiate(_popUpWinPrefab, _parentForPopUp.transform);
//        popUp.GetComponent<PopUpWin>().SetLevel(_numberCurrentLevel);

//        popUp.GetComponent<PopUpWin>().NextLevelClick += SwitchToNextLevel;
//        popUp.GetComponent<PopUpWin>().MainMenuPopUpWinClick += SwitchToMainMenu;
//    }

//    private void SwitchToNextLevel()
//    {
//        _numberCurrentLevel++;
//        SceneManager.LoadScene("Level_" + _numberCurrentLevel);
//    }

//    private void SwitchToMainMenu()
//    {
//        SceneManager.LoadScene("MainMenu");
//    }

//    private void HideAllPipes()
//    {
//        foreach (var pipe in GameObject.FindGameObjectsWithTag("Pipe"))
//        {
//            pipe.SetActive(false);
//        }
//    }
//}
