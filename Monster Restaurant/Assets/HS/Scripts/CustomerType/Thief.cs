using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.UI;

public class Thief : MonoBehaviour, I_CustomerType
{
    OrderManager OM;
    GameManager GM;
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
            return "������ �� �츰 �� �˾�...";
        }
        else
        {
            OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.Thief);
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.Thief];
            GM.Money -= 30;
            GM.SalesRevenue -= 30;
            return "������ ���� ������.. ��...";
        }
    }

    public void SpecialType()
    {
        BtnObjects = OrderButtonObject.Instance;
        OM = OrderManager.Instance;
        GM = GameManager.Instance;

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
        OM.OrderTalk[0] = $"��! �˹ٻ�! ���� ���� {DrawMainMatarial()} �ϳ� ������";
        OM.dialogNumber++;


        cook.text = "�˰ڽ��ϴ�";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "�˰ڽ��ϴ�";
            OM.dialogNumber++;

            //�丮
            SucsessCook();
        });
        ask.text = "��?";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "��?";
             
            OM.OrderTalk[1] = "��.. �� ���� ���˾� ���? �ʵ� ���� ������?";
            OM.isNext = true;

            if (GM.shop.isFinalEvolution == false)
            {
                cookBtn.gameObject.SetActive(false);
                cookBtn.onClick.RemoveAllListeners();
            }
            else
            {
                cook.text = "���� �Ĺ��� �����ش�.";
                cookBtn.onClick.RemoveAllListeners();
                cookBtn.onClick.AddListener(() =>
                {
                    OM.AskTalk[1] = "���� �Ĺ��� �����ش�.";
                    OM.OrderTalk[2] = "��..���� ���� ��..����������..";

                    RefuseOrder();
                });
            }

            ask.text = "�˼��մϴ�.";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OM.AskTalk[1] = "�˼��մϴ�.";
                OM.OrderTalk[2] = "���� �׷� ������.. ��..";

                GM.Money = GM.Money / 5;
                RefuseOrder();
            });
        });
    }

    void NotBuy()
    {
        OM.OrderTalk[0] = "�� ����!";

        cook.text = "��?";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "��?";

            OM.OrderTalk[1] = "���� ���ؾ� �˾�?";
            GM.Money = GM.Money / 3;
            RefuseOrder();
        });

        ask.text = "��..��ø���..";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "��..��ø���..";

            OM.OrderTalk[1] = "������ �����ٸ� ���� ���� �ʵ� �����Ŷ��.. ŪŪ";
            GM.Money = GM.Money / 4;
            RefuseOrder();
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
}
