using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private Store _store;
    [SerializeField] private GameObject _parent;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private GameObject _parentForPopUp;
    [SerializeField] private GameObject _blackout;

    [Header("Skins")]
    [SerializeField] private SkinData[] _chizikSkins;
    [SerializeField] private SkinData[] _backgroundSkins;
    [SerializeField] private SkinData[] _pipeSkins;

    [Header("Prefabs")]
    [SerializeField] private GameObject _skinItemPrefab;
    [SerializeField] private GameObject _skinPopUpPrefab;

    private SkinData[] _lastShownSkins; // запоминаем последнюю вкладку

    private void OnEnable()
    {
        _blackout.SetActive(false);
        _store.ChizikButtonClick += (() => ShowSkins(_chizikSkins));
        _store.BackgroundButtonClick += (() => ShowSkins(_backgroundSkins));
        _store.PipeButtonClick += (() => ShowSkins(_pipeSkins));
        _store.CloseButtonClick += Clear;
    }

    private void Awake()
    {
        SkinManager.Instance.RegisterSkins(_chizikSkins);
        SkinManager.Instance.RegisterSkins(_backgroundSkins);
        SkinManager.Instance.RegisterSkins(_pipeSkins);

        SkinManager.Instance.LoadSkinsFromPrefs();
    }

    private void ShowSkins(SkinData[] skins)
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

    private void OnDisable()
    {
        _store.ChizikButtonClick -= (() => ShowSkins(_chizikSkins));
        _store.BackgroundButtonClick -= (() => ShowSkins(_backgroundSkins));
        _store.PipeButtonClick -= (() => ShowSkins(_pipeSkins));
        _store.CloseButtonClick -= Clear;
    }
}
