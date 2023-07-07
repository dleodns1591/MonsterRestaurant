using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

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
    private float money = 100000;
    public float Money
    {
        get { return money; }
        set
        {
            money = value;
            //OrderManager.Instance.MoneyText.text = money.ToString() + " $";
            if (money < 0)
            {
                //���� ����
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
    public bool Bloom;
    public bool isBeggarRefuse, isEarthlingRefuse;
    public bool dayEndCheck = false;
    public int CanNotMask;
    public int HumanLike;

    private int satisfaction = 100;
    public int Satisfaction
    {
        get
        {
            return satisfaction;
        }
        set
        {
            if (!OrderManager.Instance.isBeggar)
                satisfaction = value;

            if (satisfaction < 0)
                satisfaction = 0;
        }
    }

    [Tooltip("OrderSet �迭������ ����ϴ� �ε���")] public int randomCustomerNum;
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
            OrderManager.Instance.EndingProduction(Eending.Bankruptcy);
        }
        return false;
    }

    public Action ReturnSpecialOrder;
    public Action ReturnOrder;
    public Action ReturnCook;
    public Action<EMainMatarials, List<ESubMatarials>, int, ECookingStyle, int> asd;
    public Action ShopAppearProd;

    public Shop shop;
}

