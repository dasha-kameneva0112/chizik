using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameSettings : MonoBehaviour, IGameSettings
{
    public event Action SkinChanged;
    public static GameSettings Instance => _instance;
    public SkinData CurrentChizikSkin => _currentChizikSkin;
    public SkinData CurrentBackgroundSkin => _currentBackgroundSkin;
    public SkinData CurrentPipeSkin => _currentPipeSkin;
    public int CountOfBrush => _countOfBrush;
    public int CountOfHammer => _countOfHammer;

    [SerializeField] private int _countOfBrush;
    [SerializeField] private int _countOfHammer;

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

        _currentChizikSkin = _chizikBasicSkin;
        _currentBackgroundSkin = _backgroundBasicSkin;
        _currentPipeSkin = _pipeBasicSkin;
    }

    private void Start()
    {
        SkinManager.Instance.SelectionSkin += ChangeCurrentSkin;
        _currentChizikSkin = SkinManager.Instance.GetSelectedSkin(SkinType.Chizik);
        _currentBackgroundSkin = SkinManager.Instance.GetSelectedSkin(SkinType.Background);
        _currentPipeSkin = SkinManager.Instance.GetSelectedSkin(SkinType.Pipe);

        SkinChanged?.Invoke();
    }

    public void ChangeCurrentSkin(SkinData skin)
    {
        if (skin.type == SkinType.Chizik)
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

    public void UpdateCountOfSponge(int count)
    {
        _countOfBrush = _countOfBrush + count;
    }

    public void UpdateCountOfHammer(int count)
    {
        _countOfHammer = _countOfHammer + count;
    }
}
