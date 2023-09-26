using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SalesMan : MonoBehaviour, I_CustomerType
{
    OrderManager OM;
    GameManager GM;
    OrderButtonObject BtnObjects;
    Button cookBtn;
    Button askBtn;
    TextMeshProUGUI cook, ask;

    void RefuseOrder()
    {
        StartCoroutine(RefuseOrderDelay());
        IEnumerator RefuseOrderDelay()
        {
            OM.StopOrderCoroutine();
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
        OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.SalesMan);
        #region 변수 설정
        BtnObjects = OrderButtonObject.Instance;
        OM = OrderManager.Instance;
        GM = GameManager.Instance;

        cookBtn = BtnObjects.CookingBtn;
        askBtn = BtnObjects.ReAskBtn;
        cook = BtnObjects.BtnCookText;
        ask = BtnObjects.BtnAskText;
        #endregion
        OM.StopOrderCoroutine();

        OM.OrderTalk[0] = "안녕하세요. 몬스터 아웃핏터스 회사의 영업사원을 맡게 된 리시드입니다. 제가 아주 좋은 물건\r\n들을 가지고 왔는데.. 한 번 확인해 보시겠습니까?";
        OM.dialogNumber++;
        cook.text = "확인하겠습니다.";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestSuccess[(int)EeventCustomerType.SalesMan];
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);

            GM.ShopAppearProd();
        });

        ask.text = "나가주세요.";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.SalesMan];
            OM.OrderTalk[1] = "이런.. 실례를 범했군요..";

            RefuseOrder();
        });
    }
}
