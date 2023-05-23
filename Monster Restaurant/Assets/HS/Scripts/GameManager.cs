using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private float money;
    public float Money
    {
        get { return money; }
        set 
        {
            money = value;
            OrderManager.Instance.MoneyText.text = money.ToString() + " $";
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
