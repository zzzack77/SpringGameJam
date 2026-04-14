using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static event Action<int> OnMoneyUpdated;
    public int money;
    [SerializeField] private int maxMoney = 9999;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnMoneyUpdated?.Invoke(money);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMoney(int moneyAmount)
    {
        money = Mathf.Clamp(money + moneyAmount, 0, maxMoney);
        OnMoneyUpdated?.Invoke(money);
    }

    public void SubtractMoney(int moneyAmount)
    {
        money = Mathf.Clamp(money - moneyAmount, 0, maxMoney);
        OnMoneyUpdated?.Invoke(money);
    }
}
