using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class BankModelTests
{
    private BankModel _bank;

    [SetUp]
    public void Setup()
    {
        PlayerPrefs.DeleteAll(); // очищаем перед каждым тестом
        _bank = new BankModel(100);
    }

    [Test]
    public void DepositMoney_PositiveValue()
    {
        int result = _bank.DepositMoney(50);
        Assert.AreEqual(150, result);
    }

    [Test]
    public void DepositMoney_NegativeValue()
    {
        PlayerPrefs.DeleteAll();
        _bank = new BankModel(-50);

        int result = _bank.DepositMoney(100);
        Assert.AreEqual(50, result);
    }

    [Test]
    public void DebitMoney_PositiveValue()
    {
        int result = _bank.DebitMoney(50);
        Assert.AreEqual(50, result);
    }

    [Test]
    public void DebitMoney_NegativeValue()
    {
        PlayerPrefs.DeleteAll();
        _bank = new BankModel(0);

        int result = _bank.DebitMoney(100);
        Assert.AreEqual(-100, result);
    }

    [Test]
    public void CheckingEnoughMoney_SufficientBalance()
    {
        bool result = _bank.IsEnoughMoney(50);
        Assert.IsTrue(result);
    }

    [Test]
    public void CheckingEnoughMoney_NotSufficientBalance()
    {
        bool result = _bank.IsEnoughMoney(150);
        Assert.IsFalse(result);
    }
}
