using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum EendingType
{
    Mine,
    Salve,
    Loser,
    Eating,
    LookStar,
    WormHole_FindHouse,
    WormHole_SpaceAdventure,
    Dragon,
}

public struct OrderSet
{
    public EMainMatarials main;
    public List<ESubMatarials> sub;
    public int count;
    public ECookingStyle style;
    public int dishCount;
}


public class GameManager : Singleton<GameManager>
{
    public OrderSet[] orderSets;
    private bool[] isEndingOpens = new bool[Enum.GetValues(typeof(EendingType)).Length];
    public bool[] IsEndingOpens
    {
        get
        {
            return isEndingOpens;
        }

        set
        {

            if (!IsEndingOpensArrayContent(isEndingOpens, value))
            {
                for (int i = 0; i < isEndingOpens.Length; i++)
                {
                    if (isEndingOpens[i] != value[i])
                    {
                        SaveManager.Instance.isEndingOpens[i] = true;
                    }
                }

                isEndingOpens = value;
            }

        }
    }

    private bool IsEndingOpensArrayContent(bool[] arr1, bool[] arr2)
    {
        if (arr1.Length != arr2.Length)
            return false;

        for (int i = 0; i < arr1.Length; i++)
        {
            if (arr1[i] != arr2[i])
                return false;
        }

        return true;
    }

    [SerializeField] private Text MoneyText;
    private float money;
    public float Money
    {
        get { return money; }
        set
        {
            money = value;
            print(money);
            MoneyText.text = ((int)Mathf.Ceil(Money)).ToString();
            if (money < 0)
            {
                //거지 엔딩
            }
        }
    }

    private int day = 1;

    public int Day
    {
        get { return day; }
        set
        {
            day = value;
        }
    }

    public int BasicRevenue = 200, SalesRevenue = 2890, TaxCost = 289, SettlementCost;
    public float MarterialCost;
    public bool isBeggarRefuse, isEarthlingRefuse, isGroupOrder;
    public bool dayEndCheck = false;
    public int CanNotMask;
    public int HumanLike;

    private int satisfaction = 100;
    public int Satisfaction
    {
        get { return satisfaction; }

        set
        {

            if (!OrderManager.Instance.isBeggar)
                satisfaction = value;
            else
                satisfaction = 100;

            if (satisfaction < 0)
            {
                satisfaction = 0;
                ReturnOrder();
                OrderManager.Instance.satisfactionManager.LoopStop();
            }
        }
    }

    [Tooltip("OrderSet 배열에서의 사용하는 인덱스")] public int randomCustomerNum;
    public int SpecialType;
    public bool BuyCheck(float price)
    {
        if (money >= price)
        {
            Money -= price;
            MarterialCost += price;
            return true;
        }
        else
        {
            OrderManager.Instance.endingManager.EndingProduction(EendingType.Eating);
            IsEndingOpens[(int)EendingType.Eating] = true;
        }
        return false;
    }

    public Action ReturnSpecialOrder;
    public Action ReturnOrder;
    public Action ReturnCook;
    public Action<Color, float, float, float, bool, bool> PvpFailEffect;
    public Action<EMainMatarials, List<ESubMatarials>, int, ECookingStyle, int> ConditionSetting;
    public Action ShopAppearProd;
    public Action BuyTalking;
    public Action WormHoleDraw;

    public Shop shop;
    public ReturnEvent eventCheck;
}

