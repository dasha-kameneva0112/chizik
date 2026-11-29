using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

public class PanelManager 
{
    private List<SkinData> _lastShownSkins; // запоминаем последнюю вкладку
    private ISkinManager _skinManager;
    private IBank _bank;
    private PanelManagerView _panelManagerView;

    public PanelManager(PanelManagerView panelManagerView, ISkinManager skinManager, IBank bank)
    {
        _panelManagerView = panelManagerView;
        _skinManager = skinManager;
        _bank = bank;

        _panelManagerView.OnChizik += OnChizik;
        _panelManagerView.OnBackground += OnBackground;
        _panelManagerView.OnPipe += OnPipe;
    }

    private void OnChizik()
    {
        ShowSkins(_skinManager.Chiziks);
    }

    private void OnBackground()
    {
        ShowSkins(_skinManager.BackGrounds);
    }

    private void OnPipe()
    {
        ShowSkins(_skinManager.Pipes);
    }

    private void ShowSkins(List<SkinData> skins)
    {
        _panelManagerView.Clear();
        _lastShownSkins = skins;

        foreach (var skin in skins)
        {
            var skinItem = _panelManagerView.CreateItem(skin);
            skinItem.UpdateSkinStatus();
            skinItem.SkinClick += OnSkinClick;
        }
        _panelManagerView.Scroll();
    }

    private void OnSkinClick(SkinData skin)
    {
        var skinPopUp = _panelManagerView.ShowSkin(skin, _skinManager, _bank);

        // обновляем список после любого действия
        skinPopUp.OnUpdateNeeded += (() =>
        {
            if (_lastShownSkins != null)
                ShowSkins(_lastShownSkins);
        });
    }
}
