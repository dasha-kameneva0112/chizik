using System;
using UnityEngine;
using UnityEngine.UI;

public class PopUpCard : MonoBehaviour
{
    public event Action CardSelected;

    [SerializeField] private Button _firstCard;
    [SerializeField] private Button _secondCard;
    [SerializeField] private Button _thirdCard;
    [SerializeField] private Button _button;

    private Vector3 _selectedScale = new Vector3(1.2f, 1.2f, 1f);
    private Vector3 _unselectedScale = new Vector3(0.8f, 0.8f, 0.8f);

    private void Start()
    {
        _firstCard.onClick.AddListener(() => OnCardClick(_firstCard, 1));
        _secondCard.onClick.AddListener(() => OnCardClick(_secondCard, 2));
        _thirdCard.onClick.AddListener(() => OnCardClick(_thirdCard, 3));
    }

    private void OnCardClick(Button card, int numberOfCard)
    {
        ChangeSizeCards(numberOfCard);
        // замнить картинку
        _button.gameObject.SetActive(true);
        CardSelected?.Invoke();
    }

    private void ChangeSizeCards(int number)
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
}
