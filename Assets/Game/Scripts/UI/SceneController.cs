using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private MainMenu _menu;
    [SerializeField] private Levels _levels;
    [SerializeField] private Store _store;
    [SerializeField] private Image _background;
    [SerializeField] private Image _chizik;

    private void Start()
    {
        _menu.gameObject.SetActive(true);
        _levels.gameObject.SetActive(false);
        _store.gameObject.SetActive(false);

        _menu.PlayButtonClicked += SwitchToGamePlay;
        _menu.LevelsButtonClicked += SwitchToLevels;
        _menu.StoreButtonClicked += SwitchToStore;
        _levels.BackButtonClicked += SwitchToMenu;
        _store.CloseButtonClick += SwitchToMenu;

        GameSettings.Instance.SkinChanged += ChangeSkin;
        ChangeSkin();
    }

    private void SwitchToGamePlay()
    {
        int numberOfCurrentLevel = LevelProgress.Instance.CurrentLevelNumber;

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
        SkinData chizik = GameSettings.Instance.CurrentChizikSkin;
        SkinData background = GameSettings.Instance.CurrentBackgroundSkin;

        if (chizik != null)
            _chizik.sprite = chizik.icon;
        if (background != null)
            _background.sprite = background.icon;
    }

    private void OnDestroy()
    {
        if (GameSettings.Instance != null)
        {
            GameSettings.Instance.SkinChanged -= ChangeSkin;
        }
    }
}
