using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ProgressController : MonoBehaviour
{
    public static ProgressController Instance;
    public int ourMoney = 30;
    public TextMeshProUGUI ourMoneyText;

    [Header("Damage settings")]
    public int damageUpgradeCost = 10;
    public int damageLvl = 1;

    public int damagePriceIncrease = 15;

    public TextMeshProUGUI damageCostText;
    public TextMeshProUGUI damageLvlText;

    [Header("Income")]
    public int incomeUpgradeCost = 10;
    public int incomeLvl = 1;

    public int incomePriceIncrease = 15;

    public TextMeshProUGUI incomeCostText;
    public TextMeshProUGUI incomeLvlText;

    [Header("Dispatch settings")]
    public int dispatchUprageCost = 10;
    public int dispatchLvl = 1;

    public int dispatchPriceIncrease = 15;

    public TextMeshProUGUI dispatchCostText;
    public TextMeshProUGUI dispatchLvlText;

    public void Awake()
    {
        Instance = this;
        //ourMoney = PlayerPrefs.GetInt("OurMoney");

        //damageUpgradeCost = PlayerPrefs.GetInt("damageUpgradeCost");
        //damageLvl = PlayerPrefs.GetInt("damageLvl");

        //incomeUpgradeCost = PlayerPrefs.GetInt("incomeUpgradeCost");
        //incomeLvl = PlayerPrefs.GetInt("incomeLvl");
        ////  save data

        damageCostText.text = damageUpgradeCost.ToString();
        damageLvlText.text = "LVL " + damageLvl.ToString();

        incomeCostText.text = incomeUpgradeCost.ToString();
        incomeLvlText.text = "LVL " + incomeLvl.ToString();

        dispatchCostText.text = dispatchUprageCost.ToString();
        dispatchLvlText.text = "LVL " + dispatchLvl.ToString();


        ourMoneyText.text = ourMoney.ToString();
        // data output
    }

    public void AddingMoney(int addMoney)
    {
        ourMoney += addMoney;
        ourMoneyText.text = ourMoney.ToString();
    }

    public void DamageImprovement()
    {
        if (ourMoney < damageUpgradeCost)
            return;

        ourMoney -= damageUpgradeCost;

        Debug.Log("Good");
        damageLvl += 1;
        damageUpgradeCost += damagePriceIncrease;

        damageLvlText.text = "LVL " + damageLvl.ToString();
        damageCostText.text = damageUpgradeCost.ToString();

        ourMoneyText.text = ourMoney.ToString();
        Debug.Log("VeryGood");
        //PlayerPrefs.SetInt("damageUpgradeCost", damageUpgradeCost); 
        //PlayerPrefs.SetInt("damageLvl", damageLvl);
        //PlayerPrefs.SetInt("OurMoney", ourMoney);
    }

    public void IncomeImprovement()
    {
        if (ourMoney < incomeUpgradeCost)
            return;

        ourMoney -= incomeUpgradeCost;

        incomeLvl += 1;
        incomeUpgradeCost += incomePriceIncrease;

        incomeLvlText.text = "LVL " + incomeLvl.ToString();
        incomeCostText.text = incomeUpgradeCost.ToString();

        ourMoneyText.text = ourMoney.ToString();
        //PlayerPrefs.SetInt("incomeUpgradeCost", incomeUpgradeCost);
        //PlayerPrefs.SetInt("incomeLvl", incomeLvl);
        //PlayerPrefs.SetInt("OurMoney", ourMoney);
    }

    public void DispatchImprovement()
    {
        if (ourMoney < dispatchUprageCost)
            return;

        ourMoney -= dispatchUprageCost;

        dispatchLvl += 1;
        dispatchUprageCost += dispatchPriceIncrease;

        dispatchLvlText.text = "LVL " + dispatchLvl.ToString();
        dispatchCostText.text = dispatchUprageCost.ToString();

        ourMoneyText.text = ourMoney.ToString();
        //PlayerPrefs.SetInt("incomeUpgradeCost", incomeUpgradeCost);
        //PlayerPrefs.SetInt("incomeLvl", incomeLvl);
        //PlayerPrefs.SetInt("OurMoney", ourMoney);
    }
}