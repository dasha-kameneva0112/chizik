using System;
using UnityEngine;
using UnityEngine.UI;

public class SceneView : MonoBehaviour
{
    public event Action PlayButtonClicked;
    public event Action StoreButtonClicked;
    public event Action LevelsButtonClicked;
    public event Action BackButtonClicked;
    public event Action StoreClosedButtonClicked;

    [SerializeField] private MainMenu _menu;
    [SerializeField] private Levels _levels;
    [SerializeField] private Store _store;
    [SerializeField] private Image _background;
    [SerializeField] private Image _chizik;

    private void Awake()
    {
        _menu.PlayButtonClicked += () => PlayButtonClicked?.Invoke();
        _menu.LevelsButtonClicked += () => LevelsButtonClicked?.Invoke();
        _menu.StoreButtonClicked += () => StoreButtonClicked?.Invoke();

        _levels.BackButtonClicked += () => BackButtonClicked?.Invoke();
        _store.CloseButtonClick += () => StoreClosedButtonClicked?.Invoke();
    }

    private void Start()
    {
        _menu.gameObject.SetActive(true);
        _levels.gameObject.SetActive(false);
        _store.gameObject.SetActive(false);
    }

    public void SwitchToLevels()
    {
        _menu.gameObject.SetActive(false);
        _levels.gameObject.SetActive(true);
    }

    public void SwitchToStore()
    {
        _menu.gameObject.SetActive(false);
        _store.gameObject.SetActive(true);
        //_store.OpenShop();
    }

    public void SwitchToMenu()
    {
        _menu.gameObject.SetActive(true);
        _levels.gameObject.SetActive(false);
        _store.gameObject.SetActive(false);
    }

    public void SetChizhik(Sprite sprite)
    {
        _chizik.sprite = sprite;
    }

    public void SetBackground(Sprite sprite)
    {
        _background.sprite = sprite;
    }

}
