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

    [Header("Skins")]
    [SerializeField] private SkinData[] _chizikSkins;
    [SerializeField] private SkinData[] _backgroundSkins;
    [SerializeField] private SkinData[] _pipeSkins;
    [SerializeField] private GameObject _skinItemPrefab;

    void Start()
    {
        _store.ChizikButtonClick += SwitchToChizikPanel;
        _store.BackgroundButtonClick += SwitchToBackgroundPanel;
        _store.PipeButtonClick += SwitchToPipePanel;
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
        _scrollRect.verticalNormalizedPosition = 1f;
        foreach (var skin in skins)
        {
            var item = Instantiate(_skinItemPrefab, _parent.transform);
            item.GetComponent<SkinItem>().Initialize(skin);
        }
    }

    private void Clear()
    {
        foreach(Transform child in _parent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
