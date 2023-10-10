using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SalesMan : MonoBehaviour, I_CustomerType
{
    OrderManager OM;
    GameManager GM;
    SaveManager SM;
    OrderButtonObject BtnObjects;
    Button cookBtn;
    Button askBtn;
    TextMeshProUGUI cook, ask;

    void RefuseOrder()
    {
        StartCoroutine(RefuseOrderDelay());
        IEnumerator RefuseOrderDelay()
        {
            OM.isNext = true;
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(OM.ExitAndComein());
        }
    }

    public string SpecialAnswer()
    {
        return " ";
    }

    public void SpecialType()
    {
        #region 변수 설정
        BtnObjects = OrderButtonObject.Instance;
        OM = OrderManager.Instance;
        GM = GameManager.Instance;
        SM = SaveManager.Instance;
        cookBtn = BtnObjects.CookingBtn;
        askBtn = BtnObjects.ReAskBtn;
        cook = BtnObjects.BtnCookText;
        ask = BtnObjects.BtnAskText;
        #endregion
        OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.SalesMan);
        OM.StopOrderCoroutine();

        if (SM.isEnglish == false)
        {
            OM.OrderTalk[0] = "안녕하세요. 몬스터 아웃핏터스 회사의 영업사원을 맡게 된 리시드입니다. 제가 아주 좋은 물건\r\n들을 가지고 왔는데.. 한 번 확인해 보시겠습니까?";
            cook.text = "확인하겠습니다.";

        }
        else
        {
            OM.OrderTalk[0] = "Hello, I'm Lisid, and I'm a sales representative for Monster Outfitters. I brought some very good\r\nstuff.. Would you like to check it out?";
            cook.text = "Let me check.";
        }
        
        OM.dialogNumber++;
        
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestSuccess[(int)EeventCustomerType.SalesMan];
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);

            GM.ShopAppearProd();
        });

        if(SM.isEnglish == false)
            ask.text = "나가주세요.";
            else
            ask.text = "Please leave.";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.SalesMan];
            if(SM.isEnglish == false)
                OM.OrderTalk[1] = "이런.. 실례를 범했군요..";
                else
            OM.OrderTalk[1] = "Oh, my... I've been rude..";

            RefuseOrder();
        });
    }
}
