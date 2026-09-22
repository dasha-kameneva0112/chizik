using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    public event Action FirstLevelButtonClicked;
    public event Action SecondLevelButtonClicked;
    public event Action ThirdLevelButtonClicked;
    public event Action FourthLevelButtonClicked;
    public event Action BackButtonClicked;

    [SerializeField] private Button _firstLevelButton;
    [SerializeField] private Button _secondLevelButton;
    [SerializeField] private Button _thirdLevelButton;
    [SerializeField] private Button _fourthLevelButton;
    [SerializeField] private Button _backButton;

    [Header("Button Sprits")]
    [SerializeField] private Sprite _completedLevel;
    [SerializeField] private Sprite _currentLevel;
    [SerializeField] private Sprite _lockedLevel;

    [Header("Text Colors")]
    [SerializeField] private Color _completedTextColor;  
    [SerializeField] private Color _lockedTextColor;       

    private int _numberCurrentLevel = 3; 

    private void Start()
    {
        _firstLevelButton.onClick.AddListener(OnFirstLevelButtonClick);
        _secondLevelButton.onClick.AddListener(OnSecondLevelButtonClick);
        _thirdLevelButton.onClick.AddListener(OnThirdLevelButtonClick);
        _fourthLevelButton.onClick.AddListener(OnFourthLevelButtonClicked);
        _backButton.onClick.AddListener(OnBackButtonClicked);

        UpdateSpriteOfLevel(_firstLevelButton, 1);
        UpdateSpriteOfLevel(_secondLevelButton, 2);
        UpdateSpriteOfLevel(_thirdLevelButton, 3);
        UpdateSpriteOfLevel(_fourthLevelButton, 4); 
    }

    private void UpdateSpriteOfLevel(Button levelButton, int number)
    {
        if(number < _numberCurrentLevel)
        {
            levelButton.GetComponent<Image>().sprite = _completedLevel;
            levelButton.GetComponentInChildren<TextMeshProUGUI>().color = _completedTextColor;
            levelButton.interactable = true;
        }
        if(number > _numberCurrentLevel)
        {
            levelButton.GetComponent<Image>().sprite = _lockedLevel;
            levelButton.GetComponentInChildren<TextMeshProUGUI>().color = _lockedTextColor;
            levelButton.interactable = false;
        }
        if(number == _numberCurrentLevel)
        {
            levelButton.GetComponent<Image>().sprite = _currentLevel;
            levelButton.GetComponentInChildren<TextMeshProUGUI>().color = _completedTextColor;
            levelButton.interactable = true;
        }
    }

    private void OnFirstLevelButtonClick()
    {
        FirstLevelButtonClicked?.Invoke();
    }

    private void OnSecondLevelButtonClick()
    {
        SecondLevelButtonClicked?.Invoke();
    }

    private void OnThirdLevelButtonClick()
    {
        ThirdLevelButtonClicked?.Invoke();
    }

    private void OnFourthLevelButtonClicked()
    {
        FourthLevelButtonClicked?.Invoke();
    }

    private void OnBackButtonClicked()
    {
        BackButtonClicked?.Invoke();
    }
}
