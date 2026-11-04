using TMPro;
using UnityEngine;

public class BankView : MonoBehaviour
{
    [SerializeField] private TMP_Text _countCoins;

    private void Start()
    {
        _countCoins.text = Bank.Instance.Money.ToString();
        Bank.Instance.CoinsChanged += OnCoinsChanged;
    }

    private void OnCoinsChanged(int coins)
    {
        _countCoins.text = coins.ToString();
    }
}
