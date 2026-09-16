using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private MainMenu _menu;
    [SerializeField] private Levels _levels;
    // store

    private int _numberCurrentLevel = 3;

    private void Start()
    {
        _menu.gameObject.SetActive(true);
        _levels.gameObject.SetActive(false);
        // _store.gameObject.SetActive(false);

        _menu.PlayButtonClicked += SwitchToGamePlay;
        _menu.LevelsButtonClicked += SwitchToLevels;
        _menu.StoreButtonClicked += SwitchToStore;
        _levels.BackButtonClicked += SwitchToMenu;
        // переключение между сценами _levels.
    }

    private void SwitchToGamePlay()
    {
        if(_numberCurrentLevel == 1)
            SceneManager.LoadScene("Level_1");
        if(_numberCurrentLevel == 2)
            SceneManager.LoadScene("Level_2");
        if(_numberCurrentLevel == 3)
            SceneManager.LoadScene("Level_3");
        if( _numberCurrentLevel == 4)
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
        // _store.gameObject.SetActive(true);
    }

    private void SwitchToMenu()
    {
        _menu.gameObject.SetActive(true);
        _levels.gameObject.SetActive(false);
    }
}
