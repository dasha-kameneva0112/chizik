using UnityEngine;

public class Bank
{
    public static Bank instance;

    private int moneyPlayer = 0;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        if (instance == null)
        {
            instance = new Bank();
        }
    }

    public Bank()
    {
        if (instance != null)
        {
            Debug.LogError(" ");
            return;
        }
        instance = this;
    }

    public int getMoney()
    {
        return moneyPlayer;
    }

    public void setMoney(int money)
    {
        moneyPlayer = money;
    }

    public void addMoney(int money)
    {
        moneyPlayer += money;
    }

    public bool spendMoney(int money)
    {
        if (moneyPlayer >= money)
        {
            moneyPlayer -= money;
            return true;
        }
        return false;
    }
}
