using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelsView : MonoBehaviour
{
    public event Action FirstLevelButtonClicked;
    public event Action SecondLevelButtonClicked;
    public event Action ThirdLevelButtonClicked;
    public event Action FourthLevelButtonClicked;
    public event Action FifthLevelButtonClicked;
    public event Action SixthLevelButtonClicked;
    public event Action SeventhLevelButtonClicked;
    public event Action EightLevelButtonClicked;
    public event Action BackButtonClicked;

    [SerializeField] private Button _firstLevelButton;
    [SerializeField] private Button _secondLevelButton;
    [SerializeField] private Button _thirdLevelButton;
    [SerializeField] private Button _fourthLevelButton;
    [SerializeField] private Button _fifthLevelButton;
    [SerializeField] private Button _sixthLevelButton;
    [SerializeField] private Button _seventhLevelButton;
    [SerializeField] private Button _eightLevelButton;
    [SerializeField] private Button _backButton;

    [Header("Button Sprits")]
    [SerializeField] private Sprite _completedLevel;
    [SerializeField] private Sprite _currentLevel;
    [SerializeField] private Sprite _lockedLevel;

    [Header("Text Colors")]
    [SerializeField] private Color _completedTextColor;  
    [SerializeField] private Color _lockedTextColor;

    private int _numberOfCurrentLevel;

    private void Start()
    {
        _numberOfCurrentLevel = LevelProgress.Instance.CurrentLevelNumber;

        _firstLevelButton.onClick.AddListener(() => OnLevelButtonClick(1));
        _secondLevelButton.onClick.AddListener(() => OnLevelButtonClick(2));
        _thirdLevelButton.onClick.AddListener(() => OnLevelButtonClick(3));
        _fourthLevelButton.onClick.AddListener(() => OnLevelButtonClick(4));
        _fifthLevelButton.onClick.AddListener(() => OnLevelButtonClick(5));
        _sixthLevelButton.onClick.AddListener(() => OnLevelButtonClick(6));
        _seventhLevelButton.onClick.AddListener(() => OnLevelButtonClick(7));
        _eightLevelButton.onClick.AddListener(() => OnLevelButtonClick(8));
        _backButton.onClick.AddListener(OnBackButtonClicked);

        UpdateSpriteOfLevel(_firstLevelButton, 1);
        UpdateSpriteOfLevel(_secondLevelButton, 2);
        UpdateSpriteOfLevel(_thirdLevelButton, 3);
        UpdateSpriteOfLevel(_fourthLevelButton, 4);
        UpdateSpriteOfLevel(_fifthLevelButton, 5);
        UpdateSpriteOfLevel(_sixthLevelButton, 6);
        UpdateSpriteOfLevel(_seventhLevelButton, 7);
        UpdateSpriteOfLevel(_eightLevelButton, 8);
    }

    private void UpdateSpriteOfLevel(Button levelButton, int number)
    {
        if(number < _numberOfCurrentLevel)
        {
            levelButton.GetComponent<Image>().sprite = _completedLevel;
            levelButton.GetComponentInChildren<TextMeshProUGUI>().color = _completedTextColor;
            levelButton.interactable = true;
        }
        if(number > _numberOfCurrentLevel)
        {
            levelButton.GetComponent<Image>().sprite = _lockedLevel;
            levelButton.GetComponentInChildren<TextMeshProUGUI>().color = _lockedTextColor;
            levelButton.interactable = false;
        }
        if(number == _numberOfCurrentLevel)
        {
            levelButton.GetComponent<Image>().sprite = _currentLevel;
            levelButton.GetComponentInChildren<TextMeshProUGUI>().color = _completedTextColor;
            levelButton.interactable = true;
        }
    }

    private void OnLevelButtonClick(int number)
    {
        switch (number)
        {
            case 1:
                FirstLevelButtonClicked?.Invoke();
                break;
            case 2:
                SecondLevelButtonClicked?.Invoke();
                break;
            case 3:
                ThirdLevelButtonClicked?.Invoke();
                break;
            case 4:
                FourthLevelButtonClicked?.Invoke();
                break;
            case 5:
                FifthLevelButtonClicked?.Invoke();
                break;
            case 6:
                SixthLevelButtonClicked?.Invoke();
                break;
            case 7:
                SeventhLevelButtonClicked?.Invoke();
                break;
            case 8:
                EightLevelButtonClicked?.Invoke();
                break;

            default:
                break;
        }
    }

    private void OnBackButtonClicked()
    {
        BackButtonClicked?.Invoke();
    }
}
