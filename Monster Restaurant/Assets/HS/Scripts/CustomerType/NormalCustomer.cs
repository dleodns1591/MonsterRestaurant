using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EcustomerType
{
    Alien,
    Hyena,
    Robot,
    Dragon,
    Light,
    FSM
}
public enum EeventCustomerType
{
    Human,
    Thief,

    Beggar,
    Rich,

    GroupOrder,
    SalesMan,
    FoodCleanTester
}
public class NormalCustomer : MonoBehaviour, I_CustomerType
{
    public void SpecialType(UIText cook, UIText ask)
    {
        cook.text = "�˰ڽ��ϴ�";
        ask.text = "��?";
    }
}
