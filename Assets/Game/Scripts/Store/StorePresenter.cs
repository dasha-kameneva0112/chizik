using UnityEngine;

public class StorePresenter
{
    private readonly StoreView _storeView;

    public StorePresenter(StoreView storeView)
    {
        _storeView = storeView;

        _storeView.ChizikButtonClick += OnChizikSelected;
        _storeView.BackgroundButtonClick += OnBackgroundSelected;
        _storeView.PipeButtonClick += OnPipeSelected;
        //_storeView.CloseButtonClick += OnClose;
    }

    private void OnChizikSelected()
    {
        _storeView.SelectChizik();
    }
    private void OnBackgroundSelected()
    {
        _storeView.SelectBackground();
    }

    private void OnPipeSelected()
    {
        _storeView.SelectPipe();
    }
}
