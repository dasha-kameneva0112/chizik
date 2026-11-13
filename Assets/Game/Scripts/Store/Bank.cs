using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public event Action<int> CoinsChanged;
    public int Money => _money;
    public static Bank Instance => _instance;

    private static Bank _instance;
    [SerializeField] private int _startMoney = 1000;
    private int _money;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        _money = PlayerPrefs.GetInt("coins", _startMoney);
    }

    public void DepositingMoney(int money)
    {
        _money += money;
        SaveMoney();
        CoinsChanged?.Invoke(_money);
    }

    public bool IsEnoughMoney(int money)
    {
        if (_money >= money)
        {
            return true;
        }
        return false;
    }

    public void DebitingMoney(int money)
    {
        _money -= money;
        SaveMoney();
        CoinsChanged?.Invoke(_money);
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt("coins", _money);
        PlayerPrefs.Save();
    }
}
