using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ProgressController : MonoBehaviour
{
    public static ProgressController Instance;

    public int moneyCount = 30;
    public TextMeshProUGUI moneyCountText;

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

    public TankTurretShooting tankTurret;

    public void Awake()
    {
        Instance = this;

        InitializeCost();
        moneyCountText.text = moneyCount.ToString();
    }

    private void InitializeCost()
    {
        damageCostText.text = damageUpgradeCost.ToString();
        damageLvlText.text = "LVL " + damageLvl.ToString();

        incomeCostText.text = incomeUpgradeCost.ToString();
        incomeLvlText.text = "LVL " + incomeLvl.ToString();

        dispatchCostText.text = dispatchUprageCost.ToString();
        dispatchLvlText.text = "LVL " + dispatchLvl.ToString();
    }

    public void AddMoney(int addMoney)
    {
        moneyCount += addMoney;
        moneyCountText.text = moneyCount.ToString();
    }

    public void DamageImprovement()
    {
        if (moneyCount < damageUpgradeCost)
            return;

        moneyCount -= damageUpgradeCost;

        damageLvl += 1;
        tankTurret.SetNewProjectileDamage(damageLvl);
        damageUpgradeCost += damagePriceIncrease;

        damageLvlText.text = "LVL " + damageLvl.ToString();
        damageCostText.text = damageUpgradeCost.ToString();

        moneyCountText.text = moneyCount.ToString();
    }

    public void IncomeImprovement()
    {
        if (moneyCount < incomeUpgradeCost)
            return;

        moneyCount -= incomeUpgradeCost;

        incomeLvl += 1;
        incomeUpgradeCost += incomePriceIncrease;

        incomeLvlText.text = "LVL " + incomeLvl.ToString();
        incomeCostText.text = incomeUpgradeCost.ToString();

        moneyCountText.text = moneyCount.ToString();
    }

    public void DispatchImprovement()
    {
        if (moneyCount < dispatchUprageCost)
            return;

        moneyCount -= dispatchUprageCost;

        dispatchLvl += 1;
        dispatchUprageCost += dispatchPriceIncrease;

        dispatchLvlText.text = "LVL " + dispatchLvl.ToString();
        dispatchCostText.text = dispatchUprageCost.ToString();

        moneyCountText.text = moneyCount.ToString();
    }
}