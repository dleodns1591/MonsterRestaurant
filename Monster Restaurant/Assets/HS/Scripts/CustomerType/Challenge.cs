using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Localization.Plugins.XLIFF.V20;
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
            if (SaveManager.Instance.isEnglish == false)
                return $"{OM.ChallengeTimeTaken}초만에 하셨네요? 대단합니다. 이정도면 알바를 하시는데 지장은 없을 것입니다.!";
            else
            return $"You did it in {OM.ChallengeTimeTaken} seconds? That's great. This won't interfere with your part-time job";
        }
        else
        {
            OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.GroupOrder);
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.GroupOrder];
            StartCoroutine(ChallengeFinish());
            if (SaveManager.Instance.isEnglish == false)
                return $"{LimitTimeSetting()}초 안에 이정도도 못 만드시면.. 알바를 하시는데 많은 문제가 생길 것입니다.";
            else
                return $"If you can't make this much in {LimitTimeSetting()}seconds.. You will have many problems working part-time.";
        }
    }
    public void SpecialType()
    {
        BtnObjects = OrderButtonObject.Instance;
        OM = OrderManager.Instance;
        GM = GameManager.Instance;

        ChallengeBtns = BtnObjects.ChallengeBtns;

        if(SaveManager.Instance.isEnglish == true)
        {
            ChallengeBtns[0].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Easy Mode";
            ChallengeBtns[1].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Normal Mode";
            ChallengeBtns[1].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Normal Mode";
            ChallengeBtns[2].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Hard Mode";
            ChallengeBtns[3].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Bug Mode";

        }

        if (SaveManager.Instance.isEnglish == false)
            OM.OrderTalk[0] = "챌린지 모드에 오신 것을 환영합니다. 원하시는 난이도를 선택해주세요.";
        else
            OM.OrderTalk[0] = "Welcome to challenge mode. Please select your desired difficulty level.";
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
                if (SaveManager.Instance.isEnglish == false)
                    OM.OrderTalk[1] = $"{LimitTimeSetting()}초 안에 {DishCountSetting()}개의 {MainMatarial()} {CookingStyle()} 만든 음식을 만들어 주십시오";
                else
                    OM.OrderTalk[1] = $"Please Make {DishCountSetting()} {CookingStyle()} {MainMatarial()} foods within {LimitTimeSetting()} seconds.";
            }
            else
            {
                subCount = 7;
                subs = new List<ESubMatarials> { (ESubMatarials)Random.Range(0, Enum.GetNames(typeof(ESubMatarials)).Length - 1 /*NULL 제외*/) };
                if (SaveManager.Instance.isEnglish == false)
                    OM.OrderTalk[1] = $"{LimitTimeSetting()}초 안에 {DishCountSetting()}개의 {SubString(subs[0])} 들어간 {MainMatarial()} {CookingStyle()} 만든 음식을 만들어 주십시오";
                else
                    OM.OrderTalk[1] = $"Please make {DishCountSetting()} foods {CookingStyle()} with {SubString(subs[0])} in {MainMatarial()} foods within {LimitTimeSetting()} seconds.";
            }

                OM.isNext = true;

            ChallengeBtns[diff - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "알겠습니다";
            ChallengeBtns[diff - 1].onClick.RemoveAllListeners();
            ChallengeBtns[diff - 1].onClick.AddListener(() =>
            {
                if (SaveManager.Instance.isEnglish == false)
                {
                    if (diff != 4) OM.OrderTalk[0] = $"{LimitTimeSetting()}초 안에 {DishCountSetting()}개의 {MainMatarial()}을 {CookingStyle()} 만든 음식을 만들어 주십시오";
                    else OM.OrderTalk[0] = $"{LimitTimeSetting()}초 안에 {DishCountSetting()}개의 {SubString(subs[0])} 들어간 {MainMatarial()}을 {CookingStyle()} 만든 음식을 만들어 주십시오";
                }
                else
                {
                    if (diff != 4) OM.OrderTalk[0] = $"Please Make {DishCountSetting()} {CookingStyle()} {MainMatarial()} foods within {LimitTimeSetting()} seconds.";
                    else OM.OrderTalk[0] = $"Please make {DishCountSetting()} foods {CookingStyle()} with {SubString(subs[0])} in {MainMatarial()} foods within {LimitTimeSetting()} seconds.";
                }
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
        if (SaveManager.Instance.isEnglish == false)
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
        else
        {
            switch (cookingStyle)
            {
                case ECookingStyle.Fry:
                    return "fried";
                case ECookingStyle.Boil:
                    return "boiled";
                case ECookingStyle.Roast:
                    return "roasted";
                default:
                    return "";
            }
        }    
            
    }

    string MainMatarial()
    {
        if (SaveManager.Instance.isEnglish == false)
            switch ((EMainMatarials)randomMain)
        {
            case EMainMatarials.Bread:
                return "빵을";
            case EMainMatarials.Meat:
                return "고기를";
            case EMainMatarials.Noodle:
                return "면을";
            case EMainMatarials.Rice:
                return "밥을";
            default:
                return "빵을";
        }
        else
        {
            switch ((EMainMatarials)randomMain)
            {
                case EMainMatarials.Bread:
                    return "bread";
                case EMainMatarials.Meat:
                    return "meat";
                case EMainMatarials.Noodle:
                    return "noodle";
                case EMainMatarials.Rice:
                    return "rice";
                default:
                    return "rice";
            }

        }
    }

    string SubString(ESubMatarials eSub)
    {
        if (SaveManager.Instance.isEnglish == false)
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
        else
        {
            switch (eSub)
            {
                case ESubMatarials.AlienPlant:
                    return "Alien Plant";
                case ESubMatarials.Battery:
                    return "Battery";
                case ESubMatarials.Bismuth:
                    return "Bismuth";
                case ESubMatarials.Bolt:
                    return "Bolt";
                case ESubMatarials.Eyes:
                    return "Eyes";
                case ESubMatarials.Fur:
                    return "fur";
                case ESubMatarials.Jewelry:
                    return "jewelry";
                case ESubMatarials.Money:
                    return "money";
                case ESubMatarials.Paper:
                    return "paper";
                case ESubMatarials.Poop:
                    return "poop";
                case ESubMatarials.Preservatives:
                    return "preservatives";
                case ESubMatarials.Sticker:
                    return "sticker";
                case ESubMatarials.NULL:
                    return "외계 풀이";
                default:
                    return "외계 풀이";
            }
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
        SaveManager.Instance.isChallenge = false;
        SceneManager.LoadScene("SubMain");
    }
}
