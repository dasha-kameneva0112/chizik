using UnityEngine;

public class BankModel
{
    private readonly int _startMoney;
    private int _money;

    public int Money => _money;

    public BankModel(int money)
    {
        _startMoney = money;
        _money = PlayerPrefs.GetInt("coins", _startMoney);
    }

    public int DepositMoney(int money)
    {
        _money += money;
        SaveMoney();
        return _money;
    }

    public bool IsEnoughMoney(int money)
    {
        if (_money >= money)
        {
            return true;
        }
        return false;
    }

    public int DebitMoney(int money)
    {
        _money -= money;
        SaveMoney();
        return _money;
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt("coins", _money);
        PlayerPrefs.Save();
    }
}
