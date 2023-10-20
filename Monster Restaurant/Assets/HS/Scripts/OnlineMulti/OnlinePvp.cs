using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnlinePvp : MonoBehaviour, I_CustomerType
{
    OrderManager OM;
    GameManager GM;
    SaveManager SM;
    OrderButtonObject BtnObjects;
    Button cookBtn;
    Button askBtn;
    TextMeshProUGUI cook, ask;

    int randomMain;
    ECookingStyle cookingStyle;
    List<ESubMatarials> subs;

    public string SpecialAnswer()
    {
        //count가 5개이면
        if (true)
        {
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestSuccess[(int)EeventCustomerType.GroupOrder];
            if (SM.isEnglish == false)
                return "너 이김 ㅋㅋ";
            else
                return "";
        }
        else
        {
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.GroupOrder];
            if (SM.isEnglish == false)
                return "너 짐 ㅋㅋ";
            else
                return "";
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



        OM.StopOrderCoroutine();

        StartCoroutine(CookStartDelay());
    }

    IEnumerator CookStartDelay()
    {
        subs = new List<ESubMatarials> { (ESubMatarials)UnityEngine.Random.Range(0, Enum.GetNames(typeof(ESubMatarials)).Length - 1 /*NULL 제외*/) };
        cookingStyle = (ECookingStyle)UnityEngine.Random.Range(0, Enum.GetValues(typeof(ECookingStyle)).Length - 1);

        yield return new WaitForSeconds(6f);

        if (SM.isEnglish == false)
            OM.orderMessageManager.TalkingText($"다른 가게 알바생과 매칭이 잡혔습니다!\n이번 대결에서 조리할 요리는.. {CookingStyle()} 만든 {SubString(this.subs[0])} {DrawCookingStyle()}입니다!!!\n실력을 다투는 대결이기 때문에 메모 기능은 지원하지 않는다는 점 알아주시면 감사하겠습니다");
        else
            OM.orderMessageManager.TalkingText("");

        yield return new WaitForSeconds(7.7f);

        OM.orderMessageManager.TalkingText("자! 지금부터 시작하겠습니다.");
        yield return new WaitForSeconds(1.8f);
        OM.orderMessageManager.TalkingText("3");
        yield return new WaitForSeconds(1f);
        OM.orderMessageManager.TalkingText("2");
        yield return new WaitForSeconds(1f);
        OM.orderMessageManager.TalkingText("1");
        yield return new WaitForSeconds(1f);

        SucsessCook();

        GM.ConditionSetting((EMainMatarials)randomMain, subs, 1, cookingStyle, 1);
    }

    void SucsessCook()
    {
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
        int NullCheck(int num)
        {
            if ((EMainMatarials)num == EMainMatarials.NULL)
                num = NullCheck(UnityEngine.Random.Range(0, Enum.GetValues(typeof(EMainMatarials)).Length));

            return num;
        }

        randomMain = UnityEngine.Random.Range(0, Enum.GetValues(typeof(EMainMatarials)).Length);
        randomMain = NullCheck(randomMain);
        switch ((EMainMatarials)randomMain)
        {
            case EMainMatarials.Meat:
                return "고기";
            case EMainMatarials.Bread:
                return "빵";
            case EMainMatarials.Noodle:
                return "라면";
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
                return "외계풀";
            case ESubMatarials.Battery:
                return "건전지";
            case ESubMatarials.Bismuth:
                return "비스무트";
            case ESubMatarials.Bolt:
                return "너트";
            case ESubMatarials.Eyes:
                return "눈알";
            case ESubMatarials.Fur:
                return "털뭉치";
            case ESubMatarials.Jewelry:
                return "보석";
            case ESubMatarials.Money:
                return "돈";
            case ESubMatarials.Paper:
                return "종이";
            case ESubMatarials.Poop:
                return "똥";
            case ESubMatarials.Preservatives:
                return "방부제";
            case ESubMatarials.Sticker:
                return "스티커";
            case ESubMatarials.NULL:
                return "외계풀";
            default:
                return "외계풀";
        }
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
}
