using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GroupOrder : MonoBehaviour, I_CustomerType
{
    OrderManager OM;
    GameManager GM;
    OrderButtonObject BtnObjects;
    Button cookBtn;
    Button askBtn;
    TextMeshProUGUI cook, ask;

    int randomMain;
    int dishCount;
    ECookingStyle cookingStyle;

    public string SpecialAnswer()
    {
        if (GM.Satisfaction >= OM.GroupOrderTimeLimit)
        {
            OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.GroupOrder);
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestSuccess[(int)EeventCustomerType.GroupOrder];
            GM.Money += 700;
            GM.SalesRevenue += 700;
            return "빠르게 만들어주셔서 감사합니다. 아이들이 좋아할 거예요";
        }
        else
        {
            OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.GroupOrder);
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.GroupOrder];
            GM.Money -= 20;
            GM.SalesRevenue -= 20;
            return "늦게 주시면 어떡해요..! 현재 급식 배분을 잘 처리해서 다행이지만, 돈은 못드리겠네요";
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

        GM.isGroupOrder = true;

        OM.GroupOrderTimeLimit = Random.Range(30, 35);
        dishCount = Random.Range(4, 6);

        OM.OrderTalk[0] = $"안녕하세요. 옆 건물에서 어린이집 교사로 일하고 있습니다. 현재 급식 배분에 문제가 생겨서 그러는데 {OM.GroupOrderTimeLimit}초 안에 {dishCount}개의 {DrawMainMatarial()}을 {DrawCookingStyle()} 만들어 주실 수 있나요 ?";
        OM.dialogNumber++;

        cook.text = "알겠습니다";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "알겠습니다";
            OM.dialogNumber++;

            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);

            //요리
            SucsessCook();
        });

        askBtn.onClick.RemoveAllListeners();
        ask.text = "";
        askBtn.GetComponent<Image>().enabled = false;
    }
    void SucsessCook()
    {
        List<ESubMatarials> subs = new List<ESubMatarials>
        {
            ESubMatarials.NULL
        };
        GM.ConditionSetting((EMainMatarials)randomMain, subs, 0, cookingStyle, dishCount);

        cookBtn.gameObject.SetActive(false);
        askBtn.gameObject.SetActive(false);

        StartCoroutine(Delay());
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(1.5f);
            askBtn.gameObject.SetActive(false);
            askBtn.GetComponent<Image>().enabled = true;
        }

        //요리
        GameManager.Instance.ReturnCook();
    }
    /// <summary>
    /// 튀기기를 제외한 조리 방식을 뽑는 함수
    /// </summary>
    /// <returns></returns>
    string DrawCookingStyle()
    {
        int rand = Random.Range(0, 1);
        if (rand == 0)
        {
            cookingStyle = ECookingStyle.Roast;
            return "구워서";
        }
        else
        {
            cookingStyle = ECookingStyle.Boil;
            return "삶아서";
        }
    }

    /// <summary>
    /// 고기를 제외한 메인 재료 뽑는 함수
    /// </summary>
    /// <returns></returns>
    string DrawMainMatarial()
    {
        int MeatCheck(int num)
        {
            if ((EMainMatarials)num == EMainMatarials.Meat || (EMainMatarials)num == EMainMatarials.NULL)
                num = MeatCheck(Random.Range(0, Enum.GetValues(typeof(EMainMatarials)).Length));

            return num;
        }
        int rand = Random.Range(0, Enum.GetValues(typeof(EMainMatarials)).Length);
        rand =  MeatCheck(rand);
        randomMain = rand;
        switch ((EMainMatarials)rand)
        {
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
