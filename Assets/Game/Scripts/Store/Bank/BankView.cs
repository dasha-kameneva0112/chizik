using TMPro;
using UnityEngine;

public class BankView : MonoBehaviour
{
    [SerializeField] private TMP_Text _countCoins;
    //[SerializeField] private readonly int _startMoney = 1000;

    public void ShowMoney(int coins)
    {
        _countCoins.text = coins.ToString();
    }
}
