using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{

    public int moneyCount;

    public void IncreaseMoneyCount(int value)
    {
        moneyCount += value;
    }

    public void DecreaseMoneyCount(int value)
    {
        moneyCount -= value;
    }

    public bool IsEnoughMoney(int value)
    {
        return (value <= moneyCount);
    }
}