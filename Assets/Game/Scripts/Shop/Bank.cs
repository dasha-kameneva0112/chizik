using System;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public event Action<int> CoinsChanged;
    public int Money => _money;
    public static Bank Instance => _instance;

    private static Bank _instance;
    [SerializeField] private int _startMoney = 100;
    private int _money;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        _money = _startMoney;
    }

    public void AddMoney(int money)
    {
        _money += money;
        CoinsChanged?.Invoke(_money);
    }

    public bool SpendMoney(int money)
    {
        if (_money >= money)
        {
            _money -= money;
            CoinsChanged?.Invoke(_money);
            return true;
        }
        return false;
    }
}
