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
    [SerializeField] private int damageIncreasesCounter = 0;

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

    public List<MainButton> bottomButtons = new List<MainButton>();

    public void Awake()
    {
        Instance = this;

        InitializeCost();
        moneyCountText.text = moneyCount.ToString();
    }

    private void InitializeCost()
    {
        UpdateDamage();
        UpdateIncome();
        UpdateDispatch();
    }

    private void UpdateDispatch()
    {
        dispatchCostText.text = dispatchUprageCost.ToString();
        dispatchLvlText.text = "LVL " + dispatchLvl.ToString();
        bottomButtons[0].cost = dispatchUprageCost;
    }

    private void UpdateIncome()
    {
        incomeCostText.text = incomeUpgradeCost.ToString();
        incomeLvlText.text = "LVL " + incomeLvl.ToString();
        bottomButtons[2].cost = incomeUpgradeCost;
    }

    private void UpdateDamage()
    {
        damageCostText.text = damageUpgradeCost.ToString();
        damageLvlText.text = "LVL " + damageLvl.ToString();
        bottomButtons[1].cost = damageUpgradeCost;
    }

    public void IncreaseCurrency()
    {
        moneyCount += incomeLvl;
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

        damageIncreasesCounter++;

        if (damageIncreasesCounter == 3)
            PlatesSpawner.Instance.IncreasePlateValue();

        if (damageIncreasesCounter >= 3)
        {
            damageIncreasesCounter = 0;
        }

        UpdateDamage();

        moneyCountText.text = moneyCount.ToString();
    }

    public void IncomeImprovement()
    {
        if (moneyCount < incomeUpgradeCost)
            return;

        moneyCount -= incomeUpgradeCost;

        incomeLvl += 1;
        incomeUpgradeCost += incomePriceIncrease;

        UpdateIncome();

        moneyCountText.text = moneyCount.ToString();
    }

    public void DispatchImprovement()
    {
        if (moneyCount < dispatchUprageCost && dispatchLvl < 60)
            return;

        moneyCount -= dispatchUprageCost;

        dispatchLvl += 1;
        tankTurret.DecreaseDelayBetweenShoot();
        dispatchUprageCost += dispatchPriceIncrease;

        UpdateDispatch();

        moneyCountText.text = moneyCount.ToString();
    }
}