using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    private int balance = 750;
    public int income = 0;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gainMoney(int value)
    {
        balance += value;
        income += value;
    }

    public void loseMoney(int value)
    {
        balance = Mathf.Max(0, balance - value);
    }

    public bool canAfford(int price)
    {
        return balance >= price;
    }

    public int getBalance()
    {
        return balance;
    }
}
