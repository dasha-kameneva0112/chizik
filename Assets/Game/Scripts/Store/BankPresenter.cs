using System;
public class BankPresenter : IBank
{
    private readonly BankModel _bankModel;

    public int Money => _bankModel.Money;
    public event Action<int> CoinsChanged;

    public BankPresenter(BankModel bankModel)
    {
        _bankModel = bankModel;
    }

    public void DepositMoney(int money)
    {
        _bankModel.DepositMoney(money);
        CoinsChanged?.Invoke(_bankModel.Money);
    }

    public bool IsEnoughMoney(int money)
    {
        return _bankModel.IsEnoughMoney(money);
    }

    public void DebitMoney(int money)
    {
        _bankModel.DebitMoney(money);
        CoinsChanged?.Invoke(_bankModel.Money);
    }
}
