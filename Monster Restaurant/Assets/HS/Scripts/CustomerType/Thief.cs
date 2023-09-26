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
            return "음식이 널 살린 줄 알아...";
        }
        else
        {
            OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.Thief);
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.Thief];
            GM.Money -= 30;
            GM.SalesRevenue -= 30;
            return "빠르게 만들 것이지.. 쳇...";
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

        //요리
        subs = new List<ESubMatarials> { ESubMatarials.Money };
        GM.ConditionSetting((EMainMatarials)mainMatarialRand, subs, 0, ECookingStyle.None, 1);
        GM.ReturnCook();
    }

    void Buy()
    {
        OM.OrderTalk[0] = $"야! 알바생! 돈을 감싼 {DrawMainMatarial()} 하나 가져와";
        OM.dialogNumber++;


        cook.text = "알겠습니다";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "알겠습니다";
            OM.dialogNumber++;

            //요리
            SucsessCook();
        });
        ask.text = "네?";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "네?";
             
            OM.OrderTalk[1] = "왜.. 한 번에 못알아 들어? 너도 내가 만만해?";
            OM.isNext = true;

            if (GM.shop.isFinalEvolution == false)
            {
                cookBtn.gameObject.SetActive(false);
                cookBtn.onClick.RemoveAllListeners();
            }
            else
            {
                cook.text = "식인 식물을 보여준다.";
                cookBtn.onClick.RemoveAllListeners();
                cookBtn.onClick.AddListener(() =>
                {
                    OM.AskTalk[1] = "식인 식물을 보여준다.";
                    OM.OrderTalk[2] = "다..다음 부터 무..무시하지마..";

                    RefuseOrder();
                });
            }

            ask.text = "죄송합니다.";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OM.AskTalk[1] = "죄송합니다.";
                OM.OrderTalk[2] = "진작 그럴 것이지.. 쳇..";

                GM.Money = GM.Money / 5;
                RefuseOrder();
            });
        });
    }

    void NotBuy()
    {
        OM.OrderTalk[0] = "돈 내놔!";

        cook.text = "네?";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "네?";

            OM.OrderTalk[1] = "내가 말해야 알아?";
            GM.Money = GM.Money / 3;
            RefuseOrder();
        });

        ask.text = "잠..잠시만요..";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "잠..잠시만요..";

            OM.OrderTalk[1] = "순순히 따른다면 나도 좋고 너도 좋은거라고.. 큭큭";
            GM.Money = GM.Money / 4;
            RefuseOrder();
        });
    }

    /// <summary>
    /// 고기를 제외한 메인 재료 뽑는 함수
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
                return "고기";
            case EMainMatarials.Bread:
                return "빵";
            case EMainMatarials.Noodle:
                return "면";
            case EMainMatarials.Rice:
                return "밥";
            default:
                return "빵";
        }
    }
}
