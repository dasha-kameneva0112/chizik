using System;
public class BankPresenter : IBank
{
    private readonly BankModel _bankModel;
    private readonly BankView _bankView;

    public int Money => _bankModel.Money;
    public static BankPresenter Instance => _instance;
    public event Action<int> CoinsChanged;

    private static BankPresenter _instance;

    public BankPresenter(BankModel bankModel, BankView bankView)
    {
        _bankModel = bankModel;
        _bankView = bankView;
        _bankView.ShowMoney(bankModel.Money);

        _instance = this;
    }

    public void DepositMoney(int money)
    {
        int newMoney = _bankModel.DepositMoney(money);
        _bankView.ShowMoney(newMoney);
    }

    public bool IsEnoughMoney(int money)
    {
        return _bankModel.IsEnoughMoney(money);
    }

    public void DebitMoney(int money)
    {
        int newMoney = _bankModel.DebitMoney(money);
        _bankView.ShowMoney(newMoney);
    }
}
