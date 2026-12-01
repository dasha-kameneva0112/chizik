using System.Collections.Generic;
using UnityEngine;

public class TestBootLevels : MonoBehaviour
{
    private IGameSettings _gameSettings;
    private ILevelProgress _levelProgress;
    private IBank _bank;

    [SerializeField] private GameField _gameField;
    [SerializeField] private PopUpWinView _popUpWinView;
    [SerializeField] private PopUpLossView _popUpLossView;
    [SerializeField] private PipeInventoryView _pipeInventoryView;
    [SerializeField] private FinishPipe _finishPipe;
    [SerializeField] private AnyLevelView _anyLevelView;

    private void Start()
    {
        _gameSettings = GameSettings.Instance;
        _levelProgress = LevelProgress.Instance;
        _bank = BankPresenter.Instance;

        var popUpWinModel = new PopUpWinModel(_bank, _levelProgress, _finishPipe, _gameSettings, 10);
        new PopUpWinPresenter(popUpWinModel, _popUpWinView, _finishPipe, _gameSettings);
        
        new PopUpLossPresenter(_popUpLossView, FinishPipe.Instance);
        var startCounts = new Dictionary<PipeType, int>
        {
            { PipeType.StraightPipe, 5 },
            { PipeType.CornerPipe, 3 },
            { PipeType.TriangularPipe, 2 },
            { PipeType.CruciformPipe, 1 }
        };

        new Inventory(startCounts);
        new PipeInventoryPresenter(Inventory.Instance, _pipeInventoryView, _gameField, _finishPipe, _gameSettings);

        //var view = GetComponent<PipeMovementView>();
        //new PipeMovementPresenter(view);

        new AnyLevel(_anyLevelView, 2, _gameSettings);
    }
}
