using UnityEngine;

public class PipeMovementPresenter
{
    private readonly PipeMovementView _view;
    private readonly AnyLevel _anyLevel;
    private readonly Inventory _inventory;
    private readonly BonusApplyPipe _appliedPipe;

    public PipeMovementPresenter (PipeMovementView view)
    {
        _view = view;
        _anyLevel = AnyLevel.Instance;
        _inventory = Inventory.Instance;
        _appliedPipe = view.GetComponent<BonusApplyPipe>();

        _view.RightClick += OnRightClick;
        _view.RemovePressed += OnRemovePressed;
    }

    private void OnRightClick(Vector2 worldPos)
    {
        if (_view.Contains(worldPos))
        {
            _view.RotatePipe();
        }
    }

    private void OnRemovePressed(Vector2 worldPos)
    {
        if (!_view.Contains(worldPos))
            return;

        if (_anyLevel.removeCount <= 0)
            return;

        _anyLevel.UpdateRemoveCount();
        _inventory.Delete(_appliedPipe.Presenter.GetPipeType());
        _view.DestroyPipe();
    }
}
