using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Localization.Plugins.XLIFF.V20;
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
            if (SaveManager.Instance.isEnglish == false)
                return $"{OM.ChallengeTimeTaken}�ʸ��� �ϼ̳׿�? ����մϴ�. �������� �˹ٸ� �Ͻôµ� ������ ���� ���Դϴ�.!";
            else
            return $"You did it in {OM.ChallengeTimeTaken} seconds? That's great. This won't interfere with your part-time job";
        }
        else
        {
            OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.GroupOrder);
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.GroupOrder];
            StartCoroutine(ChallengeFinish());
            if (SaveManager.Instance.isEnglish == false)
                return $"{LimitTimeSetting()}�� �ȿ� �������� �� ����ø�.. �˹ٸ� �Ͻôµ� ���� ������ ���� ���Դϴ�.";
            else
                return $"If you can't make this much in {LimitTimeSetting()}seconds.. You will have many problems working part-time.";
        }
    }
    public void SpecialType()
    {
        BtnObjects = OrderButtonObject.Instance;
        OM = OrderManager.Instance;
        GM = GameManager.Instance;

        ChallengeBtns = BtnObjects.ChallengeBtns;

        if(SaveManager.Instance.isEnglish == true)
        {
            ChallengeBtns[0].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Easy Mode";
            ChallengeBtns[1].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Normal Mode";
            ChallengeBtns[1].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Normal Mode";
            ChallengeBtns[2].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Hard Mode";
            ChallengeBtns[3].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Bug Mode";

        }

        if (SaveManager.Instance.isEnglish == false)
            OM.OrderTalk[0] = "ç���� ��忡 ���� ���� ȯ���մϴ�. ���Ͻô� ���̵��� �������ּ���.";
        else
            OM.OrderTalk[0] = "Welcome to challenge mode. Please select your desired difficulty level.";
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
                if (SaveManager.Instance.isEnglish == false)
                    OM.OrderTalk[1] = $"{LimitTimeSetting()}�� �ȿ� {DishCountSetting()}���� {MainMatarial()} {CookingStyle()} ���� ������ ����� �ֽʽÿ�";
                else
                    OM.OrderTalk[1] = $"Please Make {DishCountSetting()} {CookingStyle()} {MainMatarial()} foods within {LimitTimeSetting()} seconds.";
            }
            else
            {
                subCount = 7;
                subs = new List<ESubMatarials> { (ESubMatarials)Random.Range(0, Enum.GetNames(typeof(ESubMatarials)).Length - 1 /*NULL ����*/) };
                if (SaveManager.Instance.isEnglish == false)
                    OM.OrderTalk[1] = $"{LimitTimeSetting()}�� �ȿ� {DishCountSetting()}���� {SubString(subs[0])} �� {MainMatarial()} {CookingStyle()} ���� ������ ����� �ֽʽÿ�";
                else
                    OM.OrderTalk[1] = $"Please make {DishCountSetting()} foods {CookingStyle()} with {SubString(subs[0])} in {MainMatarial()} foods within {LimitTimeSetting()} seconds.";
            }

                OM.isNext = true;

            ChallengeBtns[diff - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "�˰ڽ��ϴ�";
            ChallengeBtns[diff - 1].onClick.RemoveAllListeners();
            ChallengeBtns[diff - 1].onClick.AddListener(() =>
            {
                if (SaveManager.Instance.isEnglish == false)
                {
                    if (diff != 4) OM.OrderTalk[0] = $"{LimitTimeSetting()}�� �ȿ� {DishCountSetting()}���� {MainMatarial()}�� {CookingStyle()} ���� ������ ����� �ֽʽÿ�";
                    else OM.OrderTalk[0] = $"{LimitTimeSetting()}�� �ȿ� {DishCountSetting()}���� {SubString(subs[0])} �� {MainMatarial()}�� {CookingStyle()} ���� ������ ����� �ֽʽÿ�";
                }
                else
                {
                    if (diff != 4) OM.OrderTalk[0] = $"Please Make {DishCountSetting()} {CookingStyle()} {MainMatarial()} foods within {LimitTimeSetting()} seconds.";
                    else OM.OrderTalk[0] = $"Please make {DishCountSetting()} foods {CookingStyle()} with {SubString(subs[0])} in {MainMatarial()} foods within {LimitTimeSetting()} seconds.";
                }
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
        if (SaveManager.Instance.isEnglish == false)
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
        else
        {
            switch (cookingStyle)
            {
                case ECookingStyle.Fry:
                    return "fried";
                case ECookingStyle.Boil:
                    return "boiled";
                case ECookingStyle.Roast:
                    return "roasted";
                default:
                    return "";
            }
        }    
            
    }

    string MainMatarial()
    {
        if (SaveManager.Instance.isEnglish == false)
            switch ((EMainMatarials)randomMain)
        {
            case EMainMatarials.Bread:
                return "����";
            case EMainMatarials.Meat:
                return "��⸦";
            case EMainMatarials.Noodle:
                return "����";
            case EMainMatarials.Rice:
                return "����";
            default:
                return "����";
        }
        else
        {
            switch ((EMainMatarials)randomMain)
            {
                case EMainMatarials.Bread:
                    return "bread";
                case EMainMatarials.Meat:
                    return "meat";
                case EMainMatarials.Noodle:
                    return "noodle";
                case EMainMatarials.Rice:
                    return "rice";
                default:
                    return "rice";
            }

        }
    }

    string SubString(ESubMatarials eSub)
    {
        if (SaveManager.Instance.isEnglish == false)
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
        else
        {
            switch (eSub)
            {
                case ESubMatarials.AlienPlant:
                    return "Alien Plant";
                case ESubMatarials.Battery:
                    return "Battery";
                case ESubMatarials.Bismuth:
                    return "Bismuth";
                case ESubMatarials.Bolt:
                    return "Bolt";
                case ESubMatarials.Eyes:
                    return "Eyes";
                case ESubMatarials.Fur:
                    return "fur";
                case ESubMatarials.Jewelry:
                    return "jewelry";
                case ESubMatarials.Money:
                    return "money";
                case ESubMatarials.Paper:
                    return "paper";
                case ESubMatarials.Poop:
                    return "poop";
                case ESubMatarials.Preservatives:
                    return "preservatives";
                case ESubMatarials.Sticker:
                    return "sticker";
                case ESubMatarials.NULL:
                    return "�ܰ� Ǯ��";
                default:
                    return "�ܰ� Ǯ��";
            }
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
        SaveManager.Instance.isChallenge = false;
        SceneManager.LoadScene("SubMain");
    }
}
