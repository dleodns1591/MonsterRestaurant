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
        #region ���� ����
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
            OM.OrderTalk[0] = "�ȳ��ϼ���. ���� �ƿ����ͽ� ȸ���� ��������� �ð� �� ���õ��Դϴ�. ���� ���� ���� ����\r\n���� ������ �Դµ�.. �� �� Ȯ���� ���ðڽ��ϱ�?";
            cook.text = "Ȯ���ϰڽ��ϴ�.";

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
            ask.text = "�����ּ���.";
            else
            ask.text = "Please leave.";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.SalesMan];
            if(SM.isEnglish == false)
                OM.OrderTalk[1] = "�̷�.. �Ƿʸ� ���߱���..";
                else
            OM.OrderTalk[1] = "Oh, my... I've been rude..";

            RefuseOrder();
        });
    }
}
