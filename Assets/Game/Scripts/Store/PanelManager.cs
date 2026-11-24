using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private StoreView _store;
    [SerializeField] private GameObject _parent;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private GameObject _parentForPopUp;
    [SerializeField] private GameObject _blackout;

    [Header("Prefabs")]
    [SerializeField] private GameObject _skinItemPrefab;
    [SerializeField] private GameObject _skinPopUpPrefab;

    private List<SkinData> _lastShownSkins; // запоминаем последнюю вкладку

    private SkinManager _skinManager;

    private void OnEnable()
    {
        _skinManager = SkinManager.Instance;

        _blackout.SetActive(false);

        _store.ChizikButtonClick += OnChizik;
        _store.BackgroundButtonClick += OnBackground;
        _store.PipeButtonClick += OnPipe;
        _store.CloseButtonClick += Clear;
    }

    private void OnDisable()
    {
        _store.ChizikButtonClick -= OnChizik;
        _store.BackgroundButtonClick -= OnBackground;
        _store.PipeButtonClick -= OnPipe;
        _store.CloseButtonClick -= Clear;
    }


    private void OnChizik()
    {
        ShowSkins(_skinManager._chizikSkins);
    }

    private void OnBackground()
    {
        ShowSkins(_skinManager._backgroundSkins);
    }

    private void OnPipe()
    {
        ShowSkins(_skinManager._pipeSkins);
    }


    private void ShowSkins(List<SkinData> skins)
    {
        Clear();
        _lastShownSkins = skins;

        foreach (var skin in skins)
        {
            var item = Instantiate(_skinItemPrefab, _parent.transform);
            var skinItem = item.GetComponent<SkinItem>();
            skinItem.Initialize(skin);
            skinItem.UpdateSkinStatus();

            skinItem.SkinClick += OnSkinClick;
        }
        _scrollRect.verticalNormalizedPosition = 1f;
    }

    private void Clear()
    {
        foreach(Transform child in _parent.transform)
        {
            var skinItem = child.GetComponent<SkinItem>();
            skinItem.SkinClick -= OnSkinClick;
            Destroy(child.gameObject);
        }
    }

    private void OnSkinClick(SkinData skin)
    {
        _blackout.SetActive(true);
        var popUp = Instantiate(_skinPopUpPrefab, _parentForPopUp.transform);
        var skinPopUp = popUp.GetComponent<SkinPopUp>();
        skinPopUp.Initialize(skin);

        // обновляем список после любого действия
        skinPopUp.OnUpdateNeeded += (() =>
        {
            if (_lastShownSkins != null)
                ShowSkins(_lastShownSkins);
        });

        skinPopUp.CloseButtonClick += () => _blackout.SetActive(false);
    }
}
