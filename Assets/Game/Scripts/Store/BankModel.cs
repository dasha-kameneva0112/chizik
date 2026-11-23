using UnityEngine;

public class BankModel
{
    [SerializeField] private readonly int _startMoney = 1000;
    private int _money;

    public int Money => _money;

    public BankModel(int money)
    {
        _startMoney = money;
        _money = PlayerPrefs.GetInt("coins", _startMoney);
    }

    public void DepositMoney(int money)
    {
        _money += money;
        SaveMoney();
    }

    public bool IsEnoughMoney(int money)
    {
        if (_money >= money)
        {
            return true;
        }
        return false;
    }

    public void DebitMoney(int money)
    {
        _money -= money;
        SaveMoney();
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt("coins", _money);
        PlayerPrefs.Save();
    }
}
