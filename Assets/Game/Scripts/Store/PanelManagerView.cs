using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelManagerView : MonoBehaviour
{
    public event Action OnChizik;
    public event Action OnBackground;
    public event Action OnPipe; 
    public event Action ClearClicked;

    [SerializeField] private StoreView _store;
    [SerializeField] private GameObject _parent;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private GameObject _parentForPopUp;
    [SerializeField] private GameObject _blackout;

    [Header("Prefabs")]
    [SerializeField] private GameObject _skinItemPrefab;
    [SerializeField] private GameObject _skinPopUpPrefab;

    private void OnEnable()
    {
        _blackout.SetActive(false);

        _store.ChizikButtonClick += ( () => OnChizik?.Invoke());
        _store.BackgroundButtonClick += (() => OnBackground?.Invoke());
        _store.PipeButtonClick += (() => OnPipe?.Invoke());
        _store.CloseButtonClick += Clear;
    }

    public void Clear()
    {
        foreach (Transform child in _parent.transform)
        {
            var skinItem = child.GetComponent<SkinItem>();
            //skinItem.SkinClick -= OnSkinClick;
            Destroy(child.gameObject);
        }
    }

    public SkinItem CreateItem(SkinData skin)
    {
        var item = Instantiate(_skinItemPrefab, _parent.transform);
        var skinItem = item.GetComponent<SkinItem>();
        skinItem.Initialize(skin);

        return skinItem;
    }

    public void Scroll()
    {
        _scrollRect.verticalNormalizedPosition = 1f;
    }

    public SkinPopUp ShowSkin(SkinData skin, ISkinManager skinManager, IBank bank)
    {
        _blackout.SetActive(true);
        var popUp = Instantiate(_skinPopUpPrefab, _parentForPopUp.transform);
        var skinPopUp = popUp.GetComponent<SkinPopUp>();
        skinPopUp.Initialize(skin, skinManager, bank);
        skinPopUp.CloseButtonClick += () => _blackout.SetActive(false);

        return skinPopUp;
    }

    private void OnDisable()
    {
        _store.ChizikButtonClick -= OnChizik;
        _store.BackgroundButtonClick -= OnBackground;
        _store.PipeButtonClick -= OnPipe;
        _store.CloseButtonClick -= Clear;
    }
}
