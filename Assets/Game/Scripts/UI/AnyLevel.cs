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
    [SerializeField] private Image _background;  
    [SerializeField] private TMP_Text removeCountText;
    public int removeCount;

    private void Awake()
    {
        instance = this;
        removeCount = int.Parse(removeCountText.text);
    }

    private void Start()
    {
        removeCount = int.Parse(removeCountText.text);
        _backButton.onClick.AddListener(OnBackButtonClicked);
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
        ChangeSkin();

        RemoveCountUpdate?.Invoke(removeCount);
    }

    public void UpdateRemoveCount()
    {
        removeCount--;
        removeCountText.text = removeCount.ToString();
        RemoveCountUpdate?.Invoke(removeCount);
    }

    private void ChangeSkin()
    {
        //SkinData chizik = GameSettings.Instance.CurrentChizikSkin;
        SkinData background = GameSettings.Instance.CurrentBackgroundSkin;
        //SkinData pipe = GameSettings.Instance.CurrentPipeSkin;

        _background.sprite = background.levelSprite;
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

