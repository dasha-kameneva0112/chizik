using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameSettings : MonoBehaviour
{
    public event Action SkinChanged;
    public static GameSettings Instance => _instance;
    public SkinData CurrentChizikSkin => _currentChizikSkin;
    public SkinData CurrentBackgroundSkin => _currentBackgroundSkin;
    public SkinData CurrentPipeSkin => _currentPipeSkin;

    [Header("Basic skins")]
    [SerializeField] private SkinData _chizikBasicSkin;
    [SerializeField] private SkinData _backgroundBasicSkin;
    [SerializeField] private SkinData _pipeBasicSkin;

    private SkinData _currentChizikSkin;
    private SkinData _currentBackgroundSkin;
    private SkinData _currentPipeSkin;
    private static GameSettings _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SkinManager.Instance.SelectionSkin += ChangeCurrentSkin;
        _currentChizikSkin = SkinManager.Instance.GetSelectedSkin(SkinType.Chizik);
        _currentBackgroundSkin = SkinManager.Instance.GetSelectedSkin(SkinType.Background);
        _currentPipeSkin = SkinManager.Instance.GetSelectedSkin(SkinType.Pipe);

        SkinChanged?.Invoke();
    }

    private void ChangeCurrentSkin(SkinData skin)
    {
        if(skin.type == SkinType.Chizik)
        {
            _currentChizikSkin = skin;
        }
        if (skin.type == SkinType.Background)
        {
            _currentBackgroundSkin = skin;
        }
        if (skin.type == SkinType.Pipe)
        {
            _currentPipeSkin = skin;
        }

        SkinChanged?.Invoke();
    }
}
