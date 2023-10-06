using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    int subCount;

    int difficulty;
    public string SpecialAnswer()
    {
        //제한시간
        if (GM.Satisfaction >= (100 - LimitTimeSetting()))
        {
            OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.GroupOrder);
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestSuccess[(int)EeventCustomerType.GroupOrder];
            StartCoroutine(ChallengeFinish());
            return $"{OM.ChallengeTimeTaken}초만에 완성하셨습니다!";
        }
        else
        {
            OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.GroupOrder);
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.GroupOrder];
            StartCoroutine(ChallengeFinish());
            return $"엄청나게 형편없군요 ㅋ";
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

        StartCoroutine(ButtonOnDelay());

        for (int i = 1; i < 5; i++) ButtonSetting(i);
    }

    void ButtonSetting(int diff)
    {   
        ChallengeBtns[diff - 1].onClick.AddListener(() =>
        {
            foreach (var item in ChallengeBtns) { item.gameObject.SetActive(false); }
            OM.perfectMade = 100;
            randomMain = Random.Range(0, Enum.GetValues(typeof(EMainMatarials)).Length - 1);
            cookingStyle = (ECookingStyle)Random.Range(0, Enum.GetValues(typeof(ECookingStyle)).Length - 1);
            //난이도 설정
            difficulty = diff;

            ChallengeBtns[diff - 1].gameObject.SetActive(true);

            if (diff != 4)
            {
                subCount = 0;
                subs = new List<ESubMatarials> { ESubMatarials.NULL };
                OM.OrderTalk[1] = $"{LimitTimeSetting()}초 안에 {DishCountSetting()}개의 {MainMatarial()}을 {CookingStyle()} 만든 음식을 만들어 주십시오";

            }
            else
            {
                subCount = 7;
                subs = new List<ESubMatarials> { (ESubMatarials)Random.Range(0, Enum.GetNames(typeof(ESubMatarials)).Length - 1 /*NULL 제외*/) };
                OM.OrderTalk[1] = $"{LimitTimeSetting()}초 안에 {DishCountSetting()}개의 {SubString(subs[0])} 들어간 {MainMatarial()}을 {CookingStyle()} 만든 음식을 만들어 주십시오";
            }

                OM.isNext = true;

            ChallengeBtns[diff - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "알겠습니다";
            ChallengeBtns[diff - 1].onClick.RemoveAllListeners();
            ChallengeBtns[diff - 1].onClick.AddListener(() =>
            {
                if (diff != 4) OM.OrderTalk[0] = $"{LimitTimeSetting()}초 안에 {DishCountSetting()}개의 {MainMatarial()}을 {CookingStyle()} 만든 음식을 만들어 주십시오";
                else OM.OrderTalk[0] = $"{LimitTimeSetting()}초 안에 {DishCountSetting()}개의 {SubString(subs[0])} 들어간 {MainMatarial()}을 {CookingStyle()} 만든 음식을 만들어 주십시오";
                //4개 다 비활성화
                foreach (var item in ChallengeBtns) { item.gameObject.SetActive(false); }

                SucsessCook();
            });
        });
    }

    void SucsessCook()
    {
        foreach(var item in ChallengeBtns) { item.gameObject.SetActive(false); }

        GameManager.Instance.ReturnCook();
        GM.ConditionSetting((EMainMatarials)randomMain, subs, subCount, cookingStyle, DishCountSetting());
    }

    string CookingStyle()
    {
        switch (cookingStyle)
        {
            case ECookingStyle.Fry:
                return "튀겨서";
            case ECookingStyle.Boil:
                return "삶아서";
            case ECookingStyle.Roast:
                return "구워서";
            default:
                return "";
        }
    }

    string MainMatarial()
    {
        switch ((EMainMatarials)randomMain)
        {
            case EMainMatarials.Bread:
                return "빵";
            case EMainMatarials.Meat:
                return "고기";
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
                OM.ChallengeTimeLimit = 60;
                return 60;
            case 2:
                OM.ChallengeTimeLimit = 40;
                return 40;
            case 3:
                OM.ChallengeTimeLimit = 25;
                return 25;
            case 4:
                OM.ChallengeTimeLimit = 40;
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
        yield return new WaitForSeconds(5.4f);
        foreach (var item in ChallengeBtns) { item.gameObject.SetActive(true); }
    }

    IEnumerator ChallengeFinish()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("SubMain");
    }
}
