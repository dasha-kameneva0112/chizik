using System;

public interface IBank
{
    int Money { get; }

    event Action<int> CoinsChanged;

    void DebitMoney(int money);
    void DepositMoney(int money);
    bool IsEnoughMoney(int money);
}