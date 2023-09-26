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
            return "������ ������ּż� �����մϴ�. ���̵��� ������ �ſ���";
        }
        else
        {
            OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.GroupOrder);
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.GroupOrder];
            GM.Money -= 20;
            GM.SalesRevenue -= 20;
            return "�ʰ� �ֽø� ��ؿ�..! ���� �޽� ����� �� ó���ؼ� ����������, ���� ���帮�ڳ׿�";
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

        OM.OrderTalk[0] = $"�ȳ��ϼ���. �� �ǹ����� ����� ����� ���ϰ� �ֽ��ϴ�. ���� �޽� ��п� ������ ���ܼ� �׷��µ� {OM.GroupOrderTimeLimit}�� �ȿ� {dishCount}���� {DrawMainMatarial()}�� {DrawCookingStyle()} ����� �ֽ� �� �ֳ��� ?";
        OM.dialogNumber++;

        cook.text = "�˰ڽ��ϴ�";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "�˰ڽ��ϴ�";
            OM.dialogNumber++;

            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);

            //�丮
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

        //�丮
        GameManager.Instance.ReturnCook();
    }
    /// <summary>
    /// Ƣ��⸦ ������ ���� ����� �̴� �Լ�
    /// </summary>
    /// <returns></returns>
    string DrawCookingStyle()
    {
        int rand = Random.Range(0, 1);
        if (rand == 0)
        {
            cookingStyle = ECookingStyle.Roast;
            return "������";
        }
        else
        {
            cookingStyle = ECookingStyle.Boil;
            return "��Ƽ�";
        }
    }

    /// <summary>
    /// ��⸦ ������ ���� ��� �̴� �Լ�
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
                return "��";
            case EMainMatarials.Noodle:
                return "��";
            case EMainMatarials.Rice:
                return "��";
            default:
                return "��";
        }
    }
}
