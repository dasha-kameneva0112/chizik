using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenePresenter : MonoBehaviour
{
    [SerializeField] private MainMenu _menu;
    [SerializeField] private Levels _levels;
    [SerializeField] private Store _store;
    [SerializeField] private Image _background;
    [SerializeField] private Image _chizik;

    private IGameSettings _gameSetting;
    private ILevelProgress _levelProgress;

    private void Start()
    {
        _gameSetting = GameSettings.Instance;
        _levelProgress = LevelProgress.Instance;

        _menu.gameObject.SetActive(true);
        _levels.gameObject.SetActive(false);
        _store.gameObject.SetActive(false);

        _menu.PlayButtonClicked += SwitchToGamePlay;
        _menu.LevelsButtonClicked += SwitchToLevels;
        _menu.StoreButtonClicked += SwitchToStore;
        _levels.BackButtonClicked += SwitchToMenu;
        _store.CloseButtonClick += SwitchToMenu;

        _gameSetting.SkinChanged += ChangeSkin;
        ChangeSkin();
    }

    private void SwitchToGamePlay()
    {
        int numberOfCurrentLevel = _levelProgress.CurrentLevelNumber;

        if (numberOfCurrentLevel == 1)
            SceneManager.LoadScene("Level_1");
        if(numberOfCurrentLevel == 2)
            SceneManager.LoadScene("Level_2");
        if(numberOfCurrentLevel == 3)
            SceneManager.LoadScene("Level_3");
        if( numberOfCurrentLevel == 4)
            SceneManager.LoadScene("Level_4");
    }

    private void SwitchToLevels()
    {
        _menu.gameObject.SetActive(false);
        _levels.gameObject.SetActive(true);
    }

    private void SwitchToStore()
    {
        _menu.gameObject.SetActive(false);
        _store.gameObject.SetActive(true);
        _store.OpenShop();
    }

    private void SwitchToMenu()
    {
        _menu.gameObject.SetActive(true);
        _levels.gameObject.SetActive(false);
        _store.gameObject.SetActive(false);
    }

    private void ChangeSkin()
    {
        SkinData chizik = _gameSetting.CurrentChizikSkin;
        SkinData background = _gameSetting.CurrentBackgroundSkin;

        if (chizik != null)
            _chizik.sprite = chizik.icon;
        if (background != null)
            _background.sprite = background.icon;
    }

    private void OnDestroy()
    {
        if (_gameSetting != null)
        {
            _gameSetting.SkinChanged -= ChangeSkin;
        }
    }
}
