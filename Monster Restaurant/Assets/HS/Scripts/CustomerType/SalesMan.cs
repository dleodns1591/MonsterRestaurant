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
        #region ���� ����
        BtnObjects = OrderButtonObject.Instance;
        OM = OrderManager.Instance;
        GM = GameManager.Instance;

        cookBtn = BtnObjects.CookingBtn;
        askBtn = BtnObjects.ReAskBtn;
        cook = BtnObjects.BtnCookText;
        ask = BtnObjects.BtnAskText;
        #endregion
        OM.StopOrderCoroutine();

        OM.OrderTalk[0] = "�ȳ��ϼ���. ���� �ƿ����ͽ� ȸ���� ��������� �ð� �� ���õ��Դϴ�. ���� ���� ���� ����\r\n���� ������ �Դµ�.. �� �� Ȯ���� ���ðڽ��ϱ�?";
        OM.dialogNumber++;
        cook.text = "Ȯ���ϰڽ��ϴ�.";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestSuccess[(int)EeventCustomerType.SalesMan];
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);

            GM.ShopAppearProd();
        });

        ask.text = "�����ּ���.";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.SalesMan];
            OM.OrderTalk[1] = "�̷�.. �Ƿʸ� ���߱���..";

            RefuseOrder();
        });
    }
}
