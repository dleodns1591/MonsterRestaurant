using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using Random = UnityEngine.Random;

public class Challenge : MonoBehaviour, I_CustomerType
{
    OrderManager OM;
    GameManager GM;
    OrderButtonObject BtnObjects;
    Button[] ChallengeBtns;

    int randomMain;
    ECookingStyle cookingStyle;
    List<ESubMatarials> subs;

    int difficulty;
    public string SpecialAnswer()
    {
        //제한시간
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

        ChallengeBtns = BtnObjects.ChallengeBtns;

        OM.OrderTalk[0] = $"챌린지 모드에 오신 것을 환영합니다. 원하시는 난이도를 선택해주세요.";
        OM.dialogNumber++;
        OM.orderButtonManager.B

        StartCoroutine(ButtonOnDelay());

        for (int i = 1; i < 5; i++) ButtonSetting(i);
    }

    void ButtonSetting(int diff)
    {   
        ChallengeBtns[diff - 1].onClick.AddListener(() =>
        {
            //난이도 설정
            difficulty = diff;

            if (diff != 4)
            {
                subs = new List<ESubMatarials> { ESubMatarials.NULL };
                OM.OrderTalk[1] = $"{LimitTimeSetting()}초 안에 {DishCountSetting()}개의 {DrawMainMatarial()}을 {DrawCookingStyle()} 만든 음식을 만들어 주십시오";

            }
            else
            {
                subs = new List<ESubMatarials> { (ESubMatarials)Random.Range(0, Enum.GetNames(typeof(ESubMatarials)).Length - 1 /*NULL 제외*/) };
                OM.OrderTalk[1] = $"{LimitTimeSetting()}초 안에 {DishCountSetting()}개의 {SubString(subs[0])} 들어간 {DrawMainMatarial()}을 {DrawCookingStyle()} 만든 음식을 만들어 주십시오";
            }

                OM.isNext = true;

            ChallengeBtns[diff - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "알겠습니다";
            ChallengeBtns[diff - 1].onClick.RemoveAllListeners();
            ChallengeBtns[diff - 1].onClick.AddListener(() =>
            {
                if (diff != 4) OM.OrderTalk[0] = $"{LimitTimeSetting()}초 안에 {DishCountSetting()}개의 {DrawMainMatarial()}을 {DrawCookingStyle()} 만든 음식을 만들어 주십시오";
                else OM.OrderTalk[0] = $"{LimitTimeSetting()}초 안에 {DishCountSetting()}개의 {SubString(subs[0])} 들어간 {DrawMainMatarial()}을 {DrawCookingStyle()} 만든 음식을 만들어 주십시오";
                //4개 다 비활성화
                foreach (var item in ChallengeBtns) { item.gameObject.SetActive(false); }

                SucsessCook();
            });
        });
    }

    void SucsessCook()
    {
        GM.ConditionSetting((EMainMatarials)randomMain, subs, 7, cookingStyle, DishCountSetting());
        foreach(var item in ChallengeBtns) { item.gameObject.SetActive(false); }

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
        rand = MeatCheck(rand);
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

    string SubString(ESubMatarials eSub)
    {
        switch (eSub)
        {
            case ESubMatarials.AlienPlant:
                return "외계 풀이"; 
            case ESubMatarials.Battery:
                return "건전지가";
            case ESubMatarials.Bismuth:
                return "비스무트가";
            case ESubMatarials.Bolt:
                return "너트가";
            case ESubMatarials.Eyes:
                return "눈알이";
            case ESubMatarials.Fur:
                return "털 뭉치가";
            case ESubMatarials.Jewelry:
                return "보석이";
            case ESubMatarials.Money:
                return "돈이";
            case ESubMatarials.Paper:
                return "종이가";
            case ESubMatarials.Poop:
                return "똥이";
            case ESubMatarials.Preservatives:
                return "방부제가";
            case ESubMatarials.Sticker:
                return "스티커가";
            case ESubMatarials.NULL:
                return "외계 풀이";
            default:
                return "외계 풀이";
        }
    }
    int LimitTimeSetting()
    {
        switch (difficulty)
        {
            case 1:
                return 60;
            case 2:
                return 40;
            case 3:
                return 25;
            case 4:
                return 40;
            default:
                return 1;
        }
    }
    int DishCountSetting()
    {
        switch (difficulty)
        {
            case 1:
                return 5;
            case 2:
                return 5;
            case 3:
                return 5;
            case 4:
                return 7;
            default:
                return 1;
        }
    }

    IEnumerator ButtonOnDelay()
    {
        yield return new WaitForSeconds(5);
        foreach (var item in ChallengeBtns) { item.gameObject.SetActive(true); }
    }
}
