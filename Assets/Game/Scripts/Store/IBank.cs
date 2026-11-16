using System;

public interface IBank
{
    int Money { get; }

    event Action<int> CoinsChanged;

    void DebitingMoney(int money);
    void DepositingMoney(int money);
    bool IsEnoughMoney(int money);
}