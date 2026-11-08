using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    public static LevelProgress Instance => _instance;
    public int CurrentLevelNumber => _currentLevelNumber;

    [SerializeField] private int _currentLevelNumber = 3;
    
    private static LevelProgress _instance;

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

    public void UpdateNumberLevel()
    {
        _currentLevelNumber++;
    }
}
