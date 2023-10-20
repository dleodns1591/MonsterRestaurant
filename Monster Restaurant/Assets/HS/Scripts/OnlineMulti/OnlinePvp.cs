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

    public string SpecialAnswer()
    {
        //count가 5개이면
        if(true)
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
        yield return new WaitForSeconds(6f);
        if (SM.isEnglish == false)
            OM.orderMessageManager.TalkingText("다른 가게 알바생과 매칭이 잡혔다...!\n자!!! 지금부터 서로 물고 뜯어라 ㅋ");
        else
            OM.orderMessageManager.TalkingText("");
        
        //이거 말하는거 비례해서 초 조정 필요
        yield return new WaitForSeconds(6f);

        OM.orderMessageManager.TalkingText("3");
        yield return new WaitForSeconds(1f);
        OM.orderMessageManager.TalkingText("2");
        yield return new WaitForSeconds(1f);
        OM.orderMessageManager.TalkingText("1");
        yield return new WaitForSeconds(1f);

        SucsessCook();

        List<ESubMatarials> subs = new List<ESubMatarials>
            {
                ESubMatarials.NULL
            };
        GM.ConditionSetting((EMainMatarials)randomMain, subs, 0, cookingStyle, 1);
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
}
