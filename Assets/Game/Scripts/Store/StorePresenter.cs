using UnityEngine;

public class StorePresenter
{
    private readonly StoreView _storeView;
    private readonly MainMenu _mainMenu;

    public StorePresenter(StoreView storeView, MainMenu mainMenu)
    {
        _storeView = storeView;
        _mainMenu = mainMenu;

        _mainMenu.StoreButtonClicked += _storeView.OpenShop;

        _storeView.ChizikButtonClick += OnChizikSelected;
        _storeView.BackgroundButtonClick += OnBackgroundSelected;
        _storeView.PipeButtonClick += OnPipeSelected;
        _storeView.CloseButtonClick += OnClose;
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

    private void OnClose()
    {

    }
}
