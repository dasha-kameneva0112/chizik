using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//public class Level : MonoBehaviour
//{
//    [SerializeField] private PopUpWinController PopUpWinView;

//    private void Awake()
//    {
//        new PopUpWinPresenter(new PopUpWinModel(), PopUpWinView);
//    }
//}

public class AnyLevel : MonoBehaviour
{
    public event Action BackButtonClicked;
    public event Action RestartButtonClicked;
    public event Action<int> RemoveCountUpdate;
    public static AnyLevel Instance => _instance;

    [SerializeField] private Button _backButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Image _background;  
    [SerializeField] private TMP_Text removeCountText;
    [SerializeField] private GameField _gameField;
    [SerializeField] private PopUpWinView _popUpWinView;
    [SerializeField] private PopUpLossView _popUpLossView;
    [SerializeField] private PipeInventoryView _pipeInventoryView;

    public int removeCount;

    private static AnyLevel _instance;
    private IGameSettings _gameSettings;

    private void Awake()
    {
        _instance = this;
        removeCount = int.Parse(removeCountText.text);
    }

    private void Start()
    {
        _gameSettings = GameSettings.Instance;

        removeCount = int.Parse(removeCountText.text);
        _backButton.onClick.AddListener(OnBackButtonClicked);
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
        ChangeSkin();

        RemoveCountUpdate?.Invoke(removeCount);

        var popUpWinModel = new PopUpWinModel(Bank.Instance, LevelProgress.Instance, FinishPipe.Instance, _gameSettings, 10);
        new PopUpWinPresenter(popUpWinModel, _popUpWinView, FinishPipe.Instance, _gameSettings);

        new PopUpLossPresenter(_popUpLossView, FinishPipe.Instance);

        var startCounts = new Dictionary<PipeType, int>
        {
            { PipeType.StraightPipe, 5 },
            { PipeType.CornerPipe, 3 },
            { PipeType.TriangularPipe, 2 },
            { PipeType.CruciformPipe, 1 }
        };

        new Inventory(startCounts);
        new PipeInventoryPresenter(Inventory.Instance, _pipeInventoryView, _gameField, FinishPipe.Instance, _gameSettings);
    }

    public void UpdateRemoveCount()
    {
        removeCount--;
        removeCountText.text = removeCount.ToString();
        RemoveCountUpdate?.Invoke(removeCount);
    }

    private void ChangeSkin()
    {
        //SkinData chizik = GameSettings.Instance.CurrentChizikSkin;
        SkinData background = _gameSettings.CurrentBackgroundSkin;
        //SkinData pipe = GameSettings.Instance.CurrentPipeSkin;

        _background.sprite = background.levelSprite;
    }

    private void OnBackButtonClicked()
    {
        BackButtonClicked?.Invoke();
        SceneManager.LoadScene("MainMenu");
    }

    private void OnRestartButtonClicked()
    {
        RestartButtonClicked?.Invoke();
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}

