using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        //���ѽð�
        if (GM.Satisfaction >= (100 - LimitTimeSetting()))
        {
            OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.GroupOrder);
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestSuccess[(int)EeventCustomerType.GroupOrder];
            StartCoroutine(ChallengeFinish());
            return $"{OM.ChallengeTimeTaken}�ʸ��� �ϼ��ϼ̽��ϴ�!";
        }
        else
        {
            OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.GroupOrder);
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.GroupOrder];
            StartCoroutine(ChallengeFinish());
            return $"��û���� ��������� ��";
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
            //���̵� ����
            difficulty = diff;

            ChallengeBtns[diff - 1].gameObject.SetActive(true);

            if (diff != 4)
            {
                subCount = 0;
                subs = new List<ESubMatarials> { ESubMatarials.NULL };
                OM.OrderTalk[1] = $"{LimitTimeSetting()}�� �ȿ� {DishCountSetting()}���� {MainMatarial()}�� {CookingStyle()} ���� ������ ����� �ֽʽÿ�";

            }
            else
            {
                subCount = 7;
                subs = new List<ESubMatarials> { (ESubMatarials)Random.Range(0, Enum.GetNames(typeof(ESubMatarials)).Length - 1 /*NULL ����*/) };
                OM.OrderTalk[1] = $"{LimitTimeSetting()}�� �ȿ� {DishCountSetting()}���� {SubString(subs[0])} �� {MainMatarial()}�� {CookingStyle()} ���� ������ ����� �ֽʽÿ�";
            }

                OM.isNext = true;

            ChallengeBtns[diff - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "�˰ڽ��ϴ�";
            ChallengeBtns[diff - 1].onClick.RemoveAllListeners();
            ChallengeBtns[diff - 1].onClick.AddListener(() =>
            {
                if (diff != 4) OM.OrderTalk[0] = $"{LimitTimeSetting()}�� �ȿ� {DishCountSetting()}���� {MainMatarial()}�� {CookingStyle()} ���� ������ ����� �ֽʽÿ�";
                else OM.OrderTalk[0] = $"{LimitTimeSetting()}�� �ȿ� {DishCountSetting()}���� {SubString(subs[0])} �� {MainMatarial()}�� {CookingStyle()} ���� ������ ����� �ֽʽÿ�";
                //4�� �� ��Ȱ��ȭ
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
        switch (cookingStyle)
        {
            case ECookingStyle.Fry:
                return "Ƣ�ܼ�";
            case ECookingStyle.Boil:
                return "��Ƽ�";
            case ECookingStyle.Roast:
                return "������";
            default:
                return "";
        }
    }

    string MainMatarial()
    {
        switch ((EMainMatarials)randomMain)
        {
            case EMainMatarials.Bread:
                return "��";
            case EMainMatarials.Meat:
                return "���";
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
        SceneManager.LoadScene("SubMain");
    }
}
