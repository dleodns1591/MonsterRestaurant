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
        //���ѽð�
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

        ChallengeBtns = BtnObjects.ChallengeBtns;

        OM.OrderTalk[0] = $"ç���� ��忡 ���� ���� ȯ���մϴ�. ���Ͻô� ���̵��� �������ּ���.";
        OM.dialogNumber++;
        OM.orderButtonManager.B

        StartCoroutine(ButtonOnDelay());

        for (int i = 1; i < 5; i++) ButtonSetting(i);
    }

    void ButtonSetting(int diff)
    {   
        ChallengeBtns[diff - 1].onClick.AddListener(() =>
        {
            //���̵� ����
            difficulty = diff;

            if (diff != 4)
            {
                subs = new List<ESubMatarials> { ESubMatarials.NULL };
                OM.OrderTalk[1] = $"{LimitTimeSetting()}�� �ȿ� {DishCountSetting()}���� {DrawMainMatarial()}�� {DrawCookingStyle()} ���� ������ ����� �ֽʽÿ�";

            }
            else
            {
                subs = new List<ESubMatarials> { (ESubMatarials)Random.Range(0, Enum.GetNames(typeof(ESubMatarials)).Length - 1 /*NULL ����*/) };
                OM.OrderTalk[1] = $"{LimitTimeSetting()}�� �ȿ� {DishCountSetting()}���� {SubString(subs[0])} �� {DrawMainMatarial()}�� {DrawCookingStyle()} ���� ������ ����� �ֽʽÿ�";
            }

                OM.isNext = true;

            ChallengeBtns[diff - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "�˰ڽ��ϴ�";
            ChallengeBtns[diff - 1].onClick.RemoveAllListeners();
            ChallengeBtns[diff - 1].onClick.AddListener(() =>
            {
                if (diff != 4) OM.OrderTalk[0] = $"{LimitTimeSetting()}�� �ȿ� {DishCountSetting()}���� {DrawMainMatarial()}�� {DrawCookingStyle()} ���� ������ ����� �ֽʽÿ�";
                else OM.OrderTalk[0] = $"{LimitTimeSetting()}�� �ȿ� {DishCountSetting()}���� {SubString(subs[0])} �� {DrawMainMatarial()}�� {DrawCookingStyle()} ���� ������ ����� �ֽʽÿ�";
                //4�� �� ��Ȱ��ȭ
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
        rand = MeatCheck(rand);
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

    string SubString(ESubMatarials eSub)
    {
        switch (eSub)
        {
            case ESubMatarials.AlienPlant:
                return "�ܰ� Ǯ��"; 
            case ESubMatarials.Battery:
                return "��������";
            case ESubMatarials.Bismuth:
                return "�񽺹�Ʈ��";
            case ESubMatarials.Bolt:
                return "��Ʈ��";
            case ESubMatarials.Eyes:
                return "������";
            case ESubMatarials.Fur:
                return "�� ��ġ��";
            case ESubMatarials.Jewelry:
                return "������";
            case ESubMatarials.Money:
                return "����";
            case ESubMatarials.Paper:
                return "���̰�";
            case ESubMatarials.Poop:
                return "����";
            case ESubMatarials.Preservatives:
                return "�������";
            case ESubMatarials.Sticker:
                return "��ƼĿ��";
            case ESubMatarials.NULL:
                return "�ܰ� Ǯ��";
            default:
                return "�ܰ� Ǯ��";
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
