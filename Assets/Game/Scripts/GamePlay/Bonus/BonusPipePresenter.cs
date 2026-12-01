using UnityEngine;

public class BonusPipePresenter
{
    private BonusPipeModel _model;
    private IBonusPipeView _view;
    private BonusManager _manager;

    public BonusPipePresenter(BonusPipeModel model, IBonusPipeView pipe)
    {
        _model = model;
        _view = pipe;
        _manager = BonusManager.Instance;

        _view.OnClicked += HandleViewClicked;
    }

    public void HandleViewClicked()
    {
        if (_manager != null && _manager.IsBonusActive())
        {
            _manager.OnPipeClicked(this);
        }
    }

    public PipeType GetPipeType()
    {
        if (_model.IsFinishPipe == true)
            return PipeType.FinishPipe;
        return _model.Type;
    }

    public bool IsPlayerPlaced() => _model.IsPlayerPlaced;
    public bool IsDirty() => _model.IsDirty();
    public void SetAsPlayerPlaced() => _model.SetAsPlayerPlaced();
    public void SetFinishPipe() => _model.SetFinishPipe();


    public Vector3 GetPosition() => _view.Position;
    public Quaternion GetRotation() => _view.Rotation;
    public void DestroyPipe() => _view.DestroyPipe();
}
