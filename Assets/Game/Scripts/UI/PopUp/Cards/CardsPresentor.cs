using System;
using UnityEngine;

public class CardsPresentor
{
    public event Action PrizeClicked;

    private readonly CardsModel _cardsModel;
    private readonly PopUpCard _popUpCardView;
    private int _selectedIndex;
    private PrizeData _prize;

    public CardsPresentor(CardsModel cardsModel, PopUpCard popUpCard)
    {
        _cardsModel = cardsModel;
        _popUpCardView = popUpCard;

        _popUpCardView.CardSelected += SelectCard;
        _popUpCardView.ButtonClick += (() => PrizeClicked?.Invoke());
    }

    public void SelectCard(int number)
    {
        _selectedIndex = number;
        _popUpCardView.ChangeSizeCards(number);
        _prize = _cardsModel.GetPrize(number);
        _cardsModel.UpdateGameSettings(number);
        _popUpCardView.ReplaceCard(number, _prize);
    }
}
