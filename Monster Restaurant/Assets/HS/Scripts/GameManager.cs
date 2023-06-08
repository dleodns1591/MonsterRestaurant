using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool Bloom;
    public int CanNotMask;
    public int HumanLike;


    public bool BuyCheck(float price)
    {
        return (money >= price);
    }
}
