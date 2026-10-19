using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AnyLevel : MonoBehaviour
{
    public event Action BackButtonClicked;
    public event Action RestartButtonClicked;
    public event Action<int> RemoveCountUpdate;
    public static AnyLevel instance;

    [SerializeField] private Button _backButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameObject _background;  
    [SerializeField] private TMP_Text removeCountText;
    public int removeCount;

    private void Awake()
    {
        instance = this;
        removeCount = int.Parse(removeCountText.text);
    }

    public void updateRemoveCount()
    {
        removeCount--;
        removeCountText.text = removeCount.ToString();
        RemoveCountUpdate?.Invoke(removeCount);
    }

    private void Start()
    {
        removeCount = int.Parse(removeCountText.text);
        _backButton.onClick.AddListener(OnBackButtonClicked);
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
        SkinManager.Instance.SelectionSkin += ChangeSkin;

        RemoveCountUpdate?.Invoke(removeCount);
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

