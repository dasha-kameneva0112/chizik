using System;
using UnityEngine;
using UnityEngine.UI;

public class PopUpCard : MonoBehaviour
{
    public event Action<int> CardSelected;
    public event Action ButtonClick;

    [SerializeField] private Button _firstCard;
    [SerializeField] private Button _secondCard;
    [SerializeField] private Button _thirdCard;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _selectedCardPrefab;

    private Vector3 _selectedScale = new Vector3(1.2f, 1.2f, 1f);
    private Vector3 _unselectedScale = new Vector3(0.8f, 0.8f, 0.8f);

    private void Start()
    {
        //_firstCard.onClick.AddListener(() => OnCardClick(_firstCard, 1));
        //_secondCard.onClick.AddListener(() => OnCardClick(_secondCard, 2));
        //_thirdCard.onClick.AddListener(() => OnCardClick(_thirdCard, 3));

        _firstCard.onClick.AddListener(() => CardSelected?.Invoke(1));
        _secondCard.onClick.AddListener(() => CardSelected?.Invoke(2));
        _thirdCard.onClick.AddListener(() => CardSelected?.Invoke(3));
        _button.gameObject.SetActive(false);
        _button.onClick.AddListener(ButtonClicked);
    }

    //public void OnCardClick(Button card, int numberOfCard)
    //{
    //    ChangeSizeCards(numberOfCard);
    //    // замнить картинку
    //    _button.gameObject.SetActive(true);
    //    //CardSelected?.Invoke();
    //}

    public void ReplaceCard(int number, PrizeData prize)
    {
        Button currentButton = null;
        if (number == 1)
        {
            currentButton = _firstCard;
        }
        if (number == 2)
        {
            currentButton = _secondCard;
        }
        if(number == 3)
        {
            currentButton = _thirdCard;
        }
        if (currentButton == null) return;

        var card = Instantiate(_selectedCardPrefab, currentButton.transform);
        card.GetComponent<PrizeItem>().Initialize(prize);
        currentButton.interactable = false;
        currentButton.GetComponent<Image>().sprite = null;
        currentButton.GetComponent <Image>().color = new Color(0,0,0);
        _button.gameObject.SetActive(true);
    }

    public void ChangeSizeCards(int number)
    {
        if(number == 1)
        {
            _firstCard.transform.localScale = _selectedScale;
            _secondCard.transform.localScale = _unselectedScale;
            _thirdCard.transform.localScale = _unselectedScale;
        }
        if(number == 2)
        {
            _firstCard.transform.localScale = _unselectedScale;
            _secondCard.transform.localScale = _selectedScale;
            _thirdCard.transform.localScale = _unselectedScale;
        }
        if(number == 3)
        {
            _firstCard.transform.localScale = _unselectedScale;
            _secondCard.transform.localScale = _unselectedScale;
            _thirdCard.transform.localScale = _selectedScale;
        }
    }

    public void ButtonClicked()
    {
        _button.gameObject.SetActive(false);
        _firstCard.gameObject.SetActive(false);
        _secondCard.gameObject.SetActive(false);
        _thirdCard.gameObject.SetActive(false);

        ButtonClick?.Invoke();
    }
}
