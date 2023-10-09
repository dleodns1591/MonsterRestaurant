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
                return "음식이 널 살린 줄 알아...";
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
                return "빠르게 만들 것이지.. 쳇...";
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

        //요리
        subs = new List<ESubMatarials> { ESubMatarials.Money };
        GM.ConditionSetting((EMainMatarials)mainMatarialRand, subs, 0, ECookingStyle.None, 1);
        GM.ReturnCook();
    }

    void Buy()
    {
        if (SM.isEnglish == false)
            OM.OrderTalk[0] = $"야! 알바생! 돈을 감싼 {DrawMainMatarial()} 하나 가져와";
        else
            OM.OrderTalk[0] = $"”Hey part-timer! Bring one {DrawMainMatarial()} wrapped around money.";
        OM.dialogNumber++;

        if (SM.isEnglish == false)
            cook.text = "알겠습니다";
        else
            cook.text = "All right";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            if (SM.isEnglish == false)
                OM.AskTalk[0] = "알겠습니다";
            else
                OM.AskTalk[0] = "All right";
            OM.dialogNumber++;

            //요리
            SucsessCook();
        });

        if (SM.isEnglish == false)
            ask.text = "네?";
        else
            ask.text = "Pardon?";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            if (SM.isEnglish == false)
                OM.AskTalk[0] = "네?";
            else
                OM.AskTalk[0] = "Pardon?";

            if (SM.isEnglish == false)
                OM.OrderTalk[1] = "왜.. 한 번에 못알아 들어? 너도 내가 만만해?";
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
                    cook.text = "식인 식물을 보여준다.";
                else
                    cook.text = "It shows a cannibal plant.";

                cookBtn.onClick.RemoveAllListeners();
                cookBtn.onClick.AddListener(() =>
                {
                    OM.AskTalk[1] = "식인 식물을 보여준다.";

                    if (SM.isEnglish == false)
                        OM.OrderTalk[2] = "다..다음 부터 무..무시하지마..";
                    else
                        OM.OrderTalk[2] = "From next time..Don't ignore me..";

                    RefuseOrder();
                });
            }

            if (SM.isEnglish == false)
                ask.text = "죄송합니다.";
            else
                ask.text = "I'm sorry.";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OM.AskTalk[1] = "죄송합니다.";

                if (SM.isEnglish == false)
                    OM.OrderTalk[2] = "진작 그럴 것이지.. 쳇..";
                else
                    OM.OrderTalk[2] = "That's what you should have done... Pfft....";

                RefuseOrder((int)GM.Money / 5);
            });
        });
    }

    void NotBuy()
    {
        if (SM.isEnglish == false)
            OM.OrderTalk[0] = "돈 내놔!";
        else
            OM.OrderTalk[0] = "Give me your money!";

        if (SM.isEnglish == false)
            cook.text = "네?";
        else
            cook.text = "Pardon?";

        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "네?";

            if (SM.isEnglish == false)
                OM.OrderTalk[1] = "내가 말해야 알아?";
            else
                OM.OrderTalk[1] = "Do I have to tell you?";
            RefuseOrder((int)GM.Money / 3);
        });
        if (SM.isEnglish == false)
            ask.text = "잠..잠시만요..";
        else
            ask.text = "Wait a minute..";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "잠..잠시만요..";

            if (SM.isEnglish == false)
                OM.OrderTalk[1] = "순순히 따른다면 나도 좋고 너도 좋은거라고.. 큭큭";
            else
                OM.OrderTalk[1] = "If you follow obediently, it's good for me and it's good for you too... hehehe.";
            RefuseOrder((int)GM.Money / 4);
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
        if (SM.isEnglish == false)
        {
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
