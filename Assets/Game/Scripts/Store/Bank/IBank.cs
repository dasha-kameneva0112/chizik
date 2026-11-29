using System;

public interface IBank
{
    int Money { get; }

    void DebitMoney(int money);
    void DepositMoney(int money);
    bool IsEnoughMoney(int money);
}