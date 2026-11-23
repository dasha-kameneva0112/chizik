using System;
using UnityEngine;

public class Bank : MonoBehaviour, IBank
{
    public event Action<int> CoinsChanged;
    public static Bank Instance => _instance;

    public int Money => _bankPresenter.Money;

    [SerializeField] private int _startMoney = 1000;
    private static Bank _instance;
    private BankPresenter _bankPresenter;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        var model = new BankModel(_startMoney);
        _bankPresenter = new BankPresenter(model);

        _bankPresenter.CoinsChanged += money =>
        {
            CoinsChanged?.Invoke(money);
        };
    }

    public void DepositMoney(int money)
    {
        _bankPresenter.DepositMoney(money);
    }

    public bool IsEnoughMoney(int money)
    {
        return _bankPresenter.IsEnoughMoney(money);
    }

    public void DebitMoney(int money)
    {
        _bankPresenter.DebitMoney(money);
    }
}
