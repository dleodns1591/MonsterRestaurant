using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public struct OrderSet
{
    public EMainMatarials main;
    public ESubMatarials sub;
    public int count;
    //조리방식
    public bool half;
}


public class GameManager : Singleton<GameManager>
{
    public OrderSet[] orderSets;
    private float money = 1000;
    public float Money
    {
        get { return money; }
        set 
        {
            money = value;
            //OrderManager.Instance.MoneyText.text = money.ToString() + " $";
            if(money < 0)
            {
                //거지 엔딩
            }
        }
    }

    private int day;

    public int Day
    {
        get { return day; }
        set 
        {
            day = value;
        }
    }

    public int BasicRevenue = 200, SalesRevenue = 2890, MarterialCost = 563, TaxCost = 289, SettlementCost;
    public bool Bloom;
    public bool dayEndCheck = false;
    public int CanNotMask;
    public int HumanLike;

    [Tooltip("OrderSet 배열에서의 사용하는 인덱스")] public int randomCustomerNum;
    public bool BuyCheck(float price)
    {
        return (money >= price);
    }

    public Action ReturnOreder;
    public Action ReturnCook;
}

