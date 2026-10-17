using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnyLevel : MonoBehaviour
{
    public event Action BackButtonClicked;
    public event Action RestartButtonClicked;

    [SerializeField] private Button _backButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameObject _background;  

    private void Start()
    {
        _backButton.onClick.AddListener(OnBackButtonClicked);
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
        SkinManager.Instance.SelectionSkin += ChangeSkin;
    }

    private void ChangeSkin(SkinData skin)
    {
        if (skin.type == SkinType.Background)
        {
            _background.GetComponent<Image>().sprite = skin.icon;
        }
        //if( skin.type == SkinType.Pipe)
        //{
        //    _pipePrefab.GetComponentInChildren<Image>().sprite = skin.icon; 
        //}
    }

    private void OnBackButtonClicked()
    {
        BackButtonClicked?.Invoke();
        SceneManager.LoadScene("MainMenu");
    }

    private void OnRestartButtonClicked()
    {
        RestartButtonClicked?.Invoke();
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}

