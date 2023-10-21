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
                    return "퀘이사";
                case "Hyena":
                    return "제토";
                case "Robot":
                    return "sdh210224";
                case "Dragon":
                    return "시금치";
                case "Light":
                    return "2차 양지화";
                case "FSM":
                    return "날스괴";
                case "Chris":
                    return "유령 크리스";
                case "Demon":
                    return "헬리오스";
                case "Holotle":
                    return "아홀로노트";
                case "Human":
                    return "스텔라";
                case "Thief":
                    return "도주";
                case "Beggar":
                    return "양말 아저씨";
                case "Rich":
                    return "양말 아저씨";
                case "GroupOrder":
                    return "플로리안";
                case "SalesMan":
                    return "리시드";
                case "FoodCleanTester":
                    return "H-30122";
                case "Trash":
                    return "쓰레기";
                case "Joker":
                    return "조커";
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
            case "퀘이사":
                return EcustomerType.Alien;
            case "제토":
                return EcustomerType.Hyena;
            case "sdh210224":
                return EcustomerType.Robot;
            case "시금치":
                return EcustomerType.Dragon;
            case "2차 양자화":
                return EcustomerType.Light;
            case "날스괴":
                return EcustomerType.FSM;
            case "유령 크리스":
                return EcustomerType.Chris;
            case "헬리오스":
                return EcustomerType.Demon;
            case "아홀로노트":
                return EcustomerType.Holotle;
            case "쓰레기":
                return EcustomerType.Trash;
            case "조커":
                return EcustomerType.Joker;
            default:
                return EcustomerType.Alien;
        }
    }
}

