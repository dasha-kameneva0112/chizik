using UnityEngine;

public class BonusPipePresenter
{
    public static BonusPipePresenter Instance { get; private set; }

    private BonusPipeModel _model;
    private BonusPipeView _view;

    // Префабы для чистых пайпов
    private GameObject _cleanStraightPipePrefab;
    private GameObject _cleanCornerPipePrefab;
    private AnyLevel _anyLevel;
    private bool _isRemoveEnded;

    public BonusPipePresenter(BonusPipeModel model, BonusPipeView view,
        GameObject cleanStraightPipePrefab, GameObject cleanCornerPipePrefab, AnyLevel anyLevel)
    {
        _model = model;
        _view = view;
        _cleanStraightPipePrefab = cleanStraightPipePrefab;
        _cleanCornerPipePrefab = cleanCornerPipePrefab;
        Instance = this;
        _anyLevel = anyLevel;

        if (_anyLevel.RemoveCount > 0)
            _isRemoveEnded = false;
        else _isRemoveEnded |= true;

            _view.BrushClicked += OnBrushClicked;
        _view.HammerClicked += OnHammerClicked;
        _anyLevel.RemoveIsEnd += UnblockHammer; 

        RefreshView();
    }

    public void UnblockHammer()
    {
        _isRemoveEnded = true;
        _view.UpdateCounts(_model.BrushCount, _model.HammerCount, _isRemoveEnded);
    }

    public void OnPipeClicked(IPipeView pipe)
    {
        if (!_model.IsBonusActive)
        {
            return;
        }

        if (_model.IsBrushActive && pipe.IsDirty)
        {
            _model.UseBrush();

            GameObject cleanPrefab = null;
            if (pipe is BonusApplyPipe bonusPipe)
            {
                switch (bonusPipe.PipeType)
                {
                    case PipeType.DirtyStraightPipe:
                        cleanPrefab = _cleanStraightPipePrefab;
                        break;
                    case PipeType.DirtyCornerPipe:
                        cleanPrefab = _cleanCornerPipePrefab;
                        break;
                }

                if (cleanPrefab != null)
                {
                    var bonus = _view.ReplaceWithCleanPipe(cleanPrefab, bonusPipe);
                    if (bonus != null)
                    {
                        bonus.ClickedPipe += OnPipeClicked;
                    }
                }
            }

            pipe.DestroyPipe();
            RefreshView();
            return;
        }

        if (_model.IsHammerActive && !pipe.IsDirty && !pipe.IsFinishPipe)
        {
            _model.UseHammer();
            pipe.DestroyPipe();
            RefreshView();
        }
    }

    private void OnBrushClicked()
    {
        _model.ToggleBrush();
        RefreshView();
    }

    private void OnHammerClicked()
    {
        _model.ToggleHammer();
        RefreshView();
    }

    private void RefreshView()
    {
        _view.UpdateCounts(_model.BrushCount, _model.HammerCount, _isRemoveEnded);
        _view.SetBrushActive(_model.IsBrushActive);
        _view.SetHammerActive(_model.IsHammerActive);
    }
}
