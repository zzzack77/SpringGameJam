using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static event Action<int> OnMoneyUpdated;
    public static Action<int> OnMoneyAdded;
    public static Action<int> OnMoneySubtracted;
    public int money = 100;
    [SerializeField] private int maxMoney = 1000000;

    private void OnEnable()
    {
        OnMoneyAdded += AddMoney;
        OnMoneySubtracted += SubtractMoney;
    }

    private void OnDisable()
    {
        OnMoneyAdded -= AddMoney;
        OnMoneySubtracted -= SubtractMoney;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnMoneyUpdated?.Invoke(money);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddMoney(int moneyAmount)
    {
        money = Mathf.Clamp(money + moneyAmount, 0, maxMoney);
        OnMoneyUpdated?.Invoke(money);
    }

    private void SubtractMoney(int moneyAmount)
    {
        money = Mathf.Clamp(money - moneyAmount, 0, maxMoney);
        OnMoneyUpdated?.Invoke(money);
    }
}
