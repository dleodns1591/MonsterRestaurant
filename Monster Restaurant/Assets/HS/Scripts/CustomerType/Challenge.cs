using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

        OM.GroupOrderTimeLimit = Random.Range(30, 35);

        OM.OrderTalk[0] = $"ç���� ��忡 ���� ���� ȯ���մϴ�. ���Ͻô� ���̵��� �������ּ���.";
        OM.dialogNumber++;

        #region
        //1�� ��ư �ؽ�Ʈ
        ChallengeBtns[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "�������";
        ChallengeBtns[0].onClick.AddListener(() =>
        {
            //���̵� ����
            difficulty = 1;

            subs = new List<ESubMatarials> { ESubMatarials.NULL };
            OM.OrderTalk[1] = $"{LimitTimeSetting()}�� �ȿ� {DishCountSetting()}���� {DrawMainMatarial()}�� {DrawCookingStyle()} ���� ������ ����� �ֽʽÿ�";
            OM.isNext = true;

            //1�� ��ư �ؽ�Ʈ
            ChallengeBtns[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "�˰ڽ��ϴ�";
            ChallengeBtns[0].onClick.RemoveAllListeners();
            ChallengeBtns[0].onClick.AddListener(() =>
            {
                OM.OrderTalk[0] = $"{LimitTimeSetting()}�� �ȿ� {DishCountSetting()}���� {DrawMainMatarial()}�� {DrawCookingStyle()} ���� ������ ����� �ֽʽÿ�";

                //4�� �� ��Ȱ��ȭ
                foreach (var item in ChallengeBtns) { item.gameObject.SetActive(false); }

                SucsessCook();
            });
        });
        #endregion

    }

    void ButtonSetting(int diff)
    {
        ChallengeBtns[diff - 1].onClick.AddListener(() =>
        {
            //���̵� ����
            difficulty = diff;

            if(diff != 4) subs = new List<ESubMatarials> { ESubMatarials.NULL };
            else subs = new List<ESubMatarials> { (ESubMatarials)Random.Range(0, Enum.GetNames(typeof(ESubMatarials)).Length) };

            OM.OrderTalk[1] = $"{LimitTimeSetting()}�� �ȿ� {DishCountSetting()}���� {DrawMainMatarial()}�� {DrawCookingStyle()} ���� ������ ����� �ֽʽÿ�";
            OM.isNext = true;

            //1�� ��ư �ؽ�Ʈ
            ChallengeBtns[diff - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "�˰ڽ��ϴ�";
            ChallengeBtns[diff - 1].onClick.RemoveAllListeners();
            ChallengeBtns[diff - 1].onClick.AddListener(() =>
            {
                OM.OrderTalk[0] = $"{LimitTimeSetting()}�� �ȿ� {DishCountSetting()}���� {DrawMainMatarial()}�� {DrawCookingStyle()} ���� ������ ����� �ֽʽÿ�";

                //4�� �� ��Ȱ��ȭ
                foreach (var item in ChallengeBtns) { item.gameObject.SetActive(false); }

                SucsessCook();
            });
        });
    }

    void SucsessCook()
    {
        GM.ConditionSetting((EMainMatarials)randomMain, subs, 7, cookingStyle, DishCountSetting());

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

    string SubString()
    {
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

        switch (ESubMatarials)
        {
            case ESubMatarials.AlienPlant:
                break;
            case ESubMatarials.Battery:
                break;
            case ESubMatarials.Bismuth:
                break;
            case ESubMatarials.Bolt:
                break;
            case ESubMatarials.Eyes:
                break;
            case ESubMatarials.Fur:
                break;
            case ESubMatarials.Jewelry:
                break;
            case ESubMatarials.Money:
                break;
            case ESubMatarials.Paper:
                break;
            case ESubMatarials.Poop:
                break;
            case ESubMatarials.Preservatives:
                break;
            case ESubMatarials.Sticker:
                break;
            case ESubMatarials.NULL:
                break;
            default:
                break;
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
}
