using System;
using UnityEditor;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour, ILevelProgress
{
    public static LevelProgress Instance => _instance;
    public int CurrentLevelNumber => _currentLevelNumber;
    public bool AbilityToGetPrize => _abilityToGetPrize;

    [SerializeField] private int _startLevelNumber;
    [SerializeField] private int _requirementForGettingPrize;

    private int _currentLevelNumber;
    private const string PrefKey = "LevelProgress_CurrentLevel";

    private static LevelProgress _instance;
    private int _gettingPrize = 0;
    private bool _abilityToGetPrize = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        _currentLevelNumber = PlayerPrefs.GetInt("currentLevel", _startLevelNumber);
    }

    public int GetOpenLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        currentScene = currentScene.Replace("Level_", "");
        if (currentScene == _currentLevelNumber.ToString())
        {
            return _currentLevelNumber;
        }
        else
        {
            return int.Parse(currentScene);
        }
    }

    public void UpdateNumberLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        currentScene = currentScene.Replace("Level_", "");
        if (currentScene == _currentLevelNumber.ToString())
        {
            _currentLevelNumber++;
            _gettingPrize++;
            SaveToPref();
        }

        if(_gettingPrize == _requirementForGettingPrize)
        {
            _abilityToGetPrize = true;
            _gettingPrize = 0;
        }
        else
        {
            _abilityToGetPrize = false;
        }
    }

    private void SaveToPref()
    {
        PlayerPrefs.SetInt("currentLevel", _currentLevelNumber);
        PlayerPrefs.Save();
    }
    private void ResetLevels()
    {
        _currentLevelNumber = _startLevelNumber;

        PlayerPrefs.DeleteKey("currentLevel");
        Debug.Log(_currentLevelNumber);

        PlayerPrefs.Save();
    }
#if UNITY_EDITOR
    [MenuItem("Tools/Levels/Reset Progress", priority = 1)]
    public static void EditorResetLevels()
    {
        LevelProgress progress = FindAnyObjectByType<LevelProgress>();

        progress.ResetLevels();
    }
#endif

}
