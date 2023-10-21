using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    OrderManager OM;
    GameManager GM;
    Customer customer;

    public bool isCurrentEventType;

    List<EeventCustomerType> EventTypes = new List<EeventCustomerType>();

    private void Awake()
    {
        OM = OrderManager.Instance;
        GM = GameManager.Instance;
        customer = OM.customer;
    }

    public void SetCustomerType(int type)
    {
        if(SaveManager.Instance.isChallenge == true)
        {
            Destroy((UnityEngine.Object)OM.CustomerType);
            OM.CustomerType = gameObject.AddComponent<Challenge>();
            GM.Money = 10000;
            EeventCustomerSetting((int)EeventCustomerType.GroupOrder);
            OM.CustomerType.SpecialType();
            return;
        }

        Destroy((UnityEngine.Object)OM.CustomerType);

        OM.GuestOfTheDay++;

        GM.randomCustomerNum = UnityEngine.Random.Range(0, OM.OrderTalkTxt.text.Split('\n').Length);
        for (int i = 0; i < OM.OrderTalk.Length; i++)
        {
            OM.OrderTalk[i] = OM.RandomOrderSpeech(i)[GM.randomCustomerNum];
        }

        if (EventTypes != null)
            EventTypes.Clear();

        foreach (var item in GameManager.Instance.eventCheck.returnEventCustomer)
        {
            EventTypes.Add(item);
        }

        int types;
        if (OM.GuestOfTheDay % 2 == 0 && EventTypes.Count >= OM.GuestOfTheDay / 2 && EventTypes.Count != 0)
        {
            isCurrentEventType = true;

            types = (int)EventTypes[(OM.GuestOfTheDay / 2) - 1];
            switch ((EeventCustomerType)types)
            {
                case EeventCustomerType.Human:
                    if (GameManager.Instance.isEarthlingRefuse)
                    {
                        OM.NormalGuestType = UnityEngine.Random.Range(0, Enum.GetValues(typeof(EcustomerType)).Length);
                        SetCustomerType(OM.NormalGuestType);
                        return;
                    }
                    OM.CustomerType = gameObject.AddComponent<Earthling>();
                    EeventCustomerSetting(types);
                    break;
                case EeventCustomerType.Thief:
                    OM.CustomerType = gameObject.AddComponent<Thief>();
                    EeventCustomerSetting(types);
                    break;
                case EeventCustomerType.Beggar:
                    if (GameManager.Instance.isBeggarRefuse)
                    {
                        OM.NormalGuestType = UnityEngine.Random.Range(0, Enum.GetValues(typeof(EcustomerType)).Length);
                        SetCustomerType(OM.NormalGuestType);
                        return;
                    }
                    OM.CustomerType = gameObject.AddComponent<Beggar>();
                    EeventCustomerSetting(types);
                    break;
                case EeventCustomerType.GroupOrder:
                    OM.CustomerType = gameObject.AddComponent<GroupOrder>();
                    EeventCustomerSetting(types);
                    break;
                case EeventCustomerType.SalesMan:
                    EeventCustomerSetting(types);
                    OM.CustomerType = gameObject.AddComponent<SalesMan>();
                    break;
                case EeventCustomerType.FoodCleanTester:
                    OM.CustomerType = gameObject.AddComponent<FoodCleanTester>();
                    EeventCustomerSetting(types);
                    break;
            }

            OM.CustomerType.SpecialType();
            return;
        }

        isCurrentEventType = false;
        OM.CustomerType = gameObject.AddComponent<NormalCustomer>();
        NormalCustomerSetting(type);
        OM.CustomerType.SpecialType();
    }
    void NormalCustomerSetting(int type)
    {
        customer.CustomerImg.sprite = customer.GuestDefualts[type];
        OM.orderMessageManager.NameBallonSetting(NameKoreanReturn(Enum.GetName(typeof(EcustomerType), type)));
    }
    public void EeventCustomerSetting(int type)
    {
        customer.CustomerImg.sprite = customer.EventGuestDefualts[type];
        OM.orderMessageManager.NameBallonSetting(NameKoreanReturn(Enum.GetName(typeof(EeventCustomerType), type)));
    }

    string NameKoreanReturn(string name)
    {
        if (SaveManager.Instance.isEnglish == false)
        {
            switch (name)
            {
                case "Alien":
                    return "���̻�";
                case "Hyena":
                    return "����";
                case "Robot":
                    return "sdh210224";
                case "Dragon":
                    return "�ñ�ġ";
                case "Light":
                    return "2�� ����ȭ";
                case "FSM":
                    return "������";
                case "Chris":
                    return "���� ũ����";
                case "Demon":
                    return "�︮����";
                case "Holotle":
                    return "��Ȧ�γ�Ʈ";
                case "Human":
                    return "���ڶ�";
                case "Thief":
                    return "����";
                case "Beggar":
                    return "�縻 ������";
                case "Rich":
                    return "�縻 ������";
                case "GroupOrder":
                    return "�÷θ���";
                case "SalesMan":
                    return "���õ�";
                case "FoodCleanTester":
                    return "H-30122";
                case "Trash":
                    return "������";
                case "Joker":
                    return "��Ŀ";
                default:
                    return "";
            }
        }
        else return name;
    }

    public EcustomerType NameToEnumReturn(string name)
    {
        switch (name)
        {
            case "���̻�":
                return EcustomerType.Alien;
            case "����":
                return EcustomerType.Hyena;
            case "sdh210224":
                return EcustomerType.Robot;
            case "�ñ�ġ":
                return EcustomerType.Dragon;
            case "2�� ����ȭ":
                return EcustomerType.Light;
            case "������":
                return EcustomerType.FSM;
            case "���� ũ����":
                return EcustomerType.Chris;
            case "�︮����":
                return EcustomerType.Demon;
            case "��Ȧ�γ�Ʈ":
                return EcustomerType.Holotle;
            case "������":
                return EcustomerType.Trash;
            case "��Ŀ":
                return EcustomerType.Joker;
            default:
                return EcustomerType.Alien;
        }
    }
}

