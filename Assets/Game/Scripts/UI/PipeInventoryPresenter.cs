public class PipeInventoryPresenter
{
    private readonly IInventory _inventory;
    private readonly PipeInventoryView _pipeInventoryView;
    private GameField _gameField;
    private FinishPipe _finishPipe;
    private IGameSettings _gameSettings;

    public PipeInventoryPresenter(Inventory inventory, PipeInventoryView pipeInventoryView, GameField gameField, FinishPipe finishPipe, IGameSettings gameSettings)
    {
        _inventory = inventory;
        _pipeInventoryView = pipeInventoryView;
        _gameField = gameField;
        _finishPipe = finishPipe;
        _gameSettings = gameSettings;

        // начальная синхронизация
        foreach (var pipe in _inventory.PipeSegments)
        {
            _pipeInventoryView.UpdateCount(pipe.Key, pipe.Value);
        }
        _pipeInventoryView.ChangeSkin(_gameSettings.CurrentPipeSkin);

        _pipeInventoryView.OnStraightPipeCLicked += OnStraightPipeCLick;
        _pipeInventoryView.OnCornerPipeClicked += OnCornerPipeClick;
        _pipeInventoryView.OnTriangularPipeClicked += OnTriangularPipeClick;
        _pipeInventoryView.OnCruciformPipeClicked += OnCruciformPipeClick;
        
        _gameSettings.SkinChanged += (() => _pipeInventoryView.ChangeSkin(_gameSettings.CurrentPipeSkin));
        _inventory.PipeCountChanged += (type, count) =>
        {
            _pipeInventoryView.UpdateCount(type, count);
        }; 
    }

    public void OnStraightPipeCLick()
    {
        if (_inventory.IsEnough(PipeType.StraightPipe))
        {
            _inventory.Use(PipeType.StraightPipe);
            _gameField.StartPlacingPipe(_pipeInventoryView.GetPipePrefab(PipeType.StraightPipe));
        }
    }

    public void OnCornerPipeClick()
    {
        if (_inventory.IsEnough(PipeType.CornerPipe))
        {
            _inventory.Use(PipeType.CornerPipe);
            _gameField.StartPlacingPipe(_pipeInventoryView.GetPipePrefab(PipeType.CornerPipe));
        }
    }

    public void OnTriangularPipeClick()
    {
        if (_inventory.IsEnough(PipeType.TriangularPipe))
        {
            _inventory.Use(PipeType.TriangularPipe);
            _gameField.StartPlacingPipe(_pipeInventoryView.GetPipePrefab(PipeType.TriangularPipe));
        }
    }

    public void OnCruciformPipeClick()
    {
        if (_inventory.IsEnough(PipeType.CruciformPipe))
        {
            _inventory.Use(PipeType.CruciformPipe);
            _gameField.StartPlacingPipe(_pipeInventoryView.GetPipePrefab(PipeType.CruciformPipe));
        }
    }

}