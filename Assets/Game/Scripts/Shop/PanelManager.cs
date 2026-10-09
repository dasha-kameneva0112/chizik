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

    private void OnEnable()
    {
        _blackout.SetActive(false);
        _store.ChizikButtonClick += SwitchToChizikPanel;
        _store.BackgroundButtonClick += SwitchToBackgroundPanel;
        _store.PipeButtonClick += SwitchToPipePanel;
        _store.CloseButtonClick += SwitchToMainMenu;
        // .SkinClick += OnSkinClick;
    }

    private void SwitchToChizikPanel()
    {
        Clear();
        ShowSkins(_chizikSkins);
    }

    private void SwitchToBackgroundPanel()
    {
        Clear();
        ShowSkins(_backgroundSkins);
    }

    private void SwitchToPipePanel()
    {
        Clear();
        ShowSkins(_pipeSkins);
    }

    private void ShowSkins(SkinData[] skins)
    {
        foreach (var skin in skins)
        {
            var item = Instantiate(_skinItemPrefab, _parent.transform);
            var skinItem = item.GetComponent<SkinItem>();
            skinItem.Initialize(skin);

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

    private void SwitchToMainMenu()
    {
        Clear();
    }

    private void OnSkinClick(SkinData skin)
    {
        _blackout.SetActive(true);
        var popUp = Instantiate(_skinPopUpPrefab, _parentForPopUp.transform);
        var skinPopUp = popUp.GetComponent<SkinPopUp>();
        skinPopUp.Initialize(skin);
        skinPopUp.CloseButtonClick += ClosePopUp;

        // подписки на кнопки 
    }

    private void ClosePopUp()
    {
        _blackout.SetActive(false);
    }

    private void OnDisable()
    {
        _store.ChizikButtonClick -= SwitchToChizikPanel;
        _store.BackgroundButtonClick -= SwitchToBackgroundPanel;
        _store.PipeButtonClick -= SwitchToPipePanel;
        _store.CloseButtonClick -= SwitchToMainMenu;
    }
}
