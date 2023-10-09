using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Thief : MonoBehaviour, I_CustomerType
{
    OrderManager OM;
    GameManager GM;
    SaveManager SM;
    OrderButtonObject BtnObjects;
    Button cookBtn;
    Button askBtn;
    TextMeshProUGUI cook, ask;
    List<ESubMatarials> subs;

    int mainMatarialRand;
    public string SpecialAnswer()
    {
        if (GM.Satisfaction >= 70)
        {
            OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.Thief);
            if (SM.isEnglish == false)
                return "������ �� �츰 �� �˾�...";
            else
                return "I think food saved you...";
        }
        else
        {
            OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.Thief);
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.Thief];
            GM.Money -= 30;
            GM.SalesRevenue -= 30;
            if (SM.isEnglish == false)
                return "������ ���� ������.. ��...";
            else
                return "Give it to me quickly.. Well..";
        }
    }

    public void SpecialType()
    {
        BtnObjects = OrderButtonObject.Instance;
        OM = OrderManager.Instance;
        GM = GameManager.Instance;
        SM = SaveManager.Instance;
        cookBtn = BtnObjects.CookingBtn;
        askBtn = BtnObjects.ReAskBtn;
        cook = BtnObjects.BtnCookText;
        ask = BtnObjects.BtnAskText;


        if (GM.shop.ManEatingPlant.isBuy == false)
        {
            NotBuy();
        }
        else
        {
            Buy();
        }
    }

    void RefuseOrder()
    {
        StartCoroutine(RefuseOrderDelay());
        IEnumerator RefuseOrderDelay()
        {
            OrderManager.Instance.StopOrderCoroutine();
            OrderManager.Instance.isNext = true;
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(OrderManager.Instance.ExitAndComein());

        }
    }
    void RefuseOrder(int money)
    {
        StartCoroutine(RefuseOrderDelay());
        IEnumerator RefuseOrderDelay()
        {
            OrderManager.Instance.StopOrderCoroutine();
            OrderManager.Instance.isNext = true;
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            OM.directingManager.DirectingReverse(money);
            StartCoroutine(OrderManager.Instance.ExitAndComein());

        }
    }
    void SucsessCook()
    {
        cookBtn.gameObject.SetActive(false);
        askBtn.gameObject.SetActive(false);

        //�丮
        subs = new List<ESubMatarials> { ESubMatarials.Money };
        GM.ConditionSetting((EMainMatarials)mainMatarialRand, subs, 0, ECookingStyle.None, 1);
        GM.ReturnCook();
    }

    void Buy()
    {
        if (SM.isEnglish == false)
            OM.OrderTalk[0] = $"��! �˹ٻ�! ���� ���� {DrawMainMatarial()} �ϳ� ������";
        else
            OM.OrderTalk[0] = $"��Hey part-timer! Bring one {DrawMainMatarial()} wrapped around money.";
        OM.dialogNumber++;

        if (SM.isEnglish == false)
            cook.text = "�˰ڽ��ϴ�";
        else
            cook.text = "All right";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            if (SM.isEnglish == false)
                OM.AskTalk[0] = "�˰ڽ��ϴ�";
            else
                OM.AskTalk[0] = "All right";
            OM.dialogNumber++;

            //�丮
            SucsessCook();
        });

        if (SM.isEnglish == false)
            ask.text = "��?";
        else
            ask.text = "Pardon?";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            if (SM.isEnglish == false)
                OM.AskTalk[0] = "��?";
            else
                OM.AskTalk[0] = "Pardon?";

            if (SM.isEnglish == false)
                OM.OrderTalk[1] = "��.. �� ���� ���˾� ���? �ʵ� ���� ������?";
            else
                OM.OrderTalk[1] = "Why... don't you understand at once? Do you think I'm easy?";
            OM.isNext = true;

            if (GM.shop.isFinalEvolution == false)
            {
                cookBtn.gameObject.SetActive(false);
                cookBtn.onClick.RemoveAllListeners();
            }
            else
            {
                if (SM.isEnglish == false)
                    cook.text = "���� �Ĺ��� �����ش�.";
                else
                    cook.text = "It shows a cannibal plant.";

                cookBtn.onClick.RemoveAllListeners();
                cookBtn.onClick.AddListener(() =>
                {
                    OM.AskTalk[1] = "���� �Ĺ��� �����ش�.";

                    if (SM.isEnglish == false)
                        OM.OrderTalk[2] = "��..���� ���� ��..����������..";
                    else
                        OM.OrderTalk[2] = "From next time..Don't ignore me..";

                    RefuseOrder();
                });
            }

            if (SM.isEnglish == false)
                ask.text = "�˼��մϴ�.";
            else
                ask.text = "I'm sorry.";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OM.AskTalk[1] = "�˼��մϴ�.";

                if (SM.isEnglish == false)
                    OM.OrderTalk[2] = "���� �׷� ������.. ��..";
                else
                    OM.OrderTalk[2] = "That's what you should have done... Pfft....";

                RefuseOrder((int)GM.Money / 5);
            });
        });
    }

    void NotBuy()
    {
        if (SM.isEnglish == false)
            OM.OrderTalk[0] = "�� ����!";
        else
            OM.OrderTalk[0] = "Give me your money!";

        if (SM.isEnglish == false)
            cook.text = "��?";
        else
            cook.text = "Pardon?";

        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "��?";

            if (SM.isEnglish == false)
                OM.OrderTalk[1] = "���� ���ؾ� �˾�?";
            else
                OM.OrderTalk[1] = "Do I have to tell you?";
            RefuseOrder((int)GM.Money / 3);
        });
        if (SM.isEnglish == false)
            ask.text = "��..��ø���..";
        else
            ask.text = "Wait a minute..";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "��..��ø���..";

            if (SM.isEnglish == false)
                OM.OrderTalk[1] = "������ �����ٸ� ���� ���� �ʵ� �����Ŷ��.. ŪŪ";
            else
                OM.OrderTalk[1] = "If you follow obediently, it's good for me and it's good for you too... hehehe.";
            RefuseOrder((int)GM.Money / 4);
        });
    }

    /// <summary>
    /// ��⸦ ������ ���� ��� �̴� �Լ�
    /// </summary>
    /// <returns></returns>
    string DrawMainMatarial()
    {
        int NullCheck(int num)
        {
            if ((EMainMatarials)num == EMainMatarials.NULL)
                num = NullCheck(UnityEngine.Random.Range(0, Enum.GetValues(typeof(EMainMatarials)).Length));

            return num;
        }

        mainMatarialRand = UnityEngine.Random.Range(0, Enum.GetValues(typeof(EMainMatarials)).Length);
        mainMatarialRand = NullCheck(mainMatarialRand);
        if (SM.isEnglish == false)
        {
            switch ((EMainMatarials)mainMatarialRand)
            {
                case EMainMatarials.Meat:
                    return "���";
                case EMainMatarials.Bread:
                    return "��";
                case EMainMatarials.Noodle:
                    return "��";
                case EMainMatarials.Rice:
                    return "��";
                default:
                    return "��";
            }
        }
        else
        {
            switch ((EMainMatarials)mainMatarialRand)
            {
                case EMainMatarials.Meat:
                    return "meat";
                case EMainMatarials.Bread:
                    return "bread";
                case EMainMatarials.Noodle:
                    return "noodle";
                case EMainMatarials.Rice:
                    return "rice";
                default:
                    return "rice";
            }
        }
    }
}
