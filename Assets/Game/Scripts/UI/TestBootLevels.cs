using System.Collections.Generic;
using UnityEngine;

public class TestBootLevels : MonoBehaviour
{
    private IGameSettings _gameSettings;
    private ILevelProgress _levelProgress;
    private IBank _bank;
    private AudioManager _audioManager;

    [SerializeField] private GameField _gameField;
    [SerializeField] private PopUpWinView _popUpWinView;
    [SerializeField] private PopUpLossView _popUpLossView;
    [SerializeField] private PipeInventoryView _pipeInventoryView;
    [SerializeField] private FinishPipe _finishPipe;
    [SerializeField] private AnyLevelView _anyLevelView;
    [SerializeField] private BonusPipeView _view;
    [SerializeField] private int _countRemove;
    [SerializeField] private PrizeData[] _allPrizeDatas;
    [SerializeField] private LevelTimer _levelTimer;

    [Header("Prefabs")]
    [SerializeField] private GameObject _cleanStraightPipePrefab;
    [SerializeField] private GameObject _cleanCornerPipePrefab;

    private void Start()
    {
        _gameSettings = GameSettings.Instance;
        _levelProgress = LevelProgress.Instance;
        _bank = BankPresenter.Instance;
        _audioManager = AudioManager.Instance;

        var startCounts = GetStartCountOfPipeInInventory();
        var inventory = new InventoryModel(startCounts);
        new PipeInventoryPresenter(inventory, _pipeInventoryView, _gameField, _finishPipe, _gameSettings);

        var popUpWinModel = new PopUpWinModel(_bank, _gameSettings, _levelProgress, 10, _allPrizeDatas);
        new PopUpWinPresenter(popUpWinModel, _popUpWinView, _finishPipe, _gameSettings, _audioManager);

        new PopUpLossPresenter(_popUpLossView, _finishPipe, _audioManager, _levelTimer);

        var anyLevel = new AnyLevel(_anyLevelView, _countRemove, _gameSettings);
        
        var bonusPipeModel = new BonusPipeModel(_gameSettings.CountOfBrush, _gameSettings.CountOfHammer, inventory);
        var presenter = new BonusPipePresenter(bonusPipeModel, _view, _cleanStraightPipePrefab, _cleanCornerPipePrefab, anyLevel, _audioManager);

        var pipes = FindObjectsOfType<BonusApplyPipe>();
        foreach (var pipe in pipes)
        {
            pipe.ClickedPipe += presenter.OnPipeClicked;
        }
    }

    private Dictionary<PipeType, int> GetStartCountOfPipeInInventory()
    {
        Dictionary<PipeType, int> startCounts = new();
        int currentLevel = _levelProgress.GetOpenLevel();

        switch(currentLevel)
        {
            case 1:
                startCounts = new Dictionary<PipeType, int>
                {
                    { PipeType.StraightPipe, 3 },
                    { PipeType.CornerPipe, 2 },
                    { PipeType.TriangularPipe, 2 },
                    { PipeType.CruciformPipe, 5 }
                };
                break;
            case 2:
                startCounts = new Dictionary<PipeType, int>
                {
                    { PipeType.StraightPipe, 3 },
                    { PipeType.CornerPipe, 4 },
                    { PipeType.TriangularPipe, 3 },
                    { PipeType.CruciformPipe, 3 }
                };
                break;
            case 3:
                startCounts = new Dictionary<PipeType, int>
                {
                    { PipeType.StraightPipe, 3 },
                    { PipeType.CornerPipe, 5 },
                    { PipeType.TriangularPipe, 3 },
                    { PipeType.CruciformPipe, 2 }
                };
                break;
            case 4:
                startCounts = new Dictionary<PipeType, int>
                {
                    { PipeType.StraightPipe, 3 },
                    { PipeType.CornerPipe, 4 },
                    { PipeType.TriangularPipe, 3 },
                    { PipeType.CruciformPipe, 3 }
                };
                break;
            case 5:
                startCounts = new Dictionary<PipeType, int>
                {
                    { PipeType.StraightPipe, 6 },
                    { PipeType.CornerPipe, 4 },
                    { PipeType.TriangularPipe, 1 },
                    { PipeType.CruciformPipe, 2 }
                };
                break;
            case 6:
                startCounts = new Dictionary<PipeType, int>
                {
                    { PipeType.StraightPipe, 8 },
                    { PipeType.CornerPipe, 6 },
                    { PipeType.TriangularPipe, 0 },
                    { PipeType.CruciformPipe, 1 }
                };
                break;
            case 7:
                startCounts = new Dictionary<PipeType, int>
                {
                    { PipeType.StraightPipe, 6 },
                    { PipeType.CornerPipe, 5 },
                    { PipeType.TriangularPipe, 1 },
                    { PipeType.CruciformPipe, 2 }
                };
                break;
            case 8:
                startCounts = new Dictionary<PipeType, int>
                {
                    { PipeType.StraightPipe, 8 },
                    { PipeType.CornerPipe, 6 },
                    { PipeType.TriangularPipe, 0 },
                    { PipeType.CruciformPipe, 3 }
                };
                break;
        }
        return startCounts;
    }
}