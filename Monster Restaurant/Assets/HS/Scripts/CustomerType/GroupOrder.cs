using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GroupOrder : MonoBehaviour, I_CustomerType
{
    OrderManager OM;
    GameManager GM;
    SaveManager SM;
    OrderButtonObject BtnObjects;
    Button cookBtn;
    Button askBtn;
    TextMeshProUGUI cook, ask;

    int randomMain;
    int dishCount;
    ECookingStyle cookingStyle;

    public string SpecialAnswer()
    {
        if (GM.Satisfaction >= (100 - OM.GroupOrderTimeLimit))
        {
            OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.GroupOrder);
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestSuccess[(int)EeventCustomerType.GroupOrder];
            OM.directingManager.Directing(700);

            if (SM.isEnglish == false)
                return "������ ������ּż� �����մϴ�. ���̵��� ������ �ſ���";
            else
                return "Thank you for making it so quickly. Kids will love it";
        }
        else
        {
            OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.GroupOrder);
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.GroupOrder];
            GM.Money -= 20;
            GM.SalesRevenue -= 20;
            if (SM.isEnglish == false)
                return "�ʰ� �ֽø� ��ؿ�..! ���� �޽� ����� �� ó���ؼ� ����������, ���� �� �帮�ڳ׿�..";
            else
                return "What if you give it to me late..! I'm glad we're handling the meal distribution right now, but I can't give you money.";
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

        GM.isGroupOrder = true;

        OM.GroupOrderTimeLimit = Random.Range(30, 35);
        dishCount = Random.Range(4, 6);

        if (SM.isEnglish == false)
            OM.OrderTalk[0] = $"�ȳ��ϼ���. �� �ǹ����� ����� ����� ���ϰ� �ֽ��ϴ�. ���� �޽� ��п� ������ ���ܼ� �׷��µ� {OM.GroupOrderTimeLimit}�� �ȿ� {dishCount}���� {DrawMainMatarial()}�� {DrawCookingStyle()} ����� �ֽ� �� �ֳ��� ?";
        else
            OM.OrderTalk[0] = $"��hello. I work as a daycare teacher in the building next door. This is because there is currently a problem with meal distribution. Can you {DrawCookingStyle()} {dishCount} {DrawMainMatarial()} in {OM.GroupOrderTimeLimit} sencond?";

        OM.dialogNumber++;

        if (SM.isEnglish == false)
            cook.text = "�˰ڽ��ϴ�";
        else
            cook.text = "All right";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            if (SM.isEnglish == false)
                OM.AskTalk[0] = "�˰ڽ��ϴ�";
            else
                OM.AskTalk[0] = "All right";
            OM.dialogNumber++;

            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);

            //�丮
            SucsessCook();

            List<ESubMatarials> subs = new List<ESubMatarials>
            {
                ESubMatarials.NULL
            };
            GM.ConditionSetting((EMainMatarials)randomMain, subs, 0, cookingStyle, dishCount);
        });

        askBtn.onClick.RemoveAllListeners();
        ask.text = "";
        askBtn.GetComponent<Image>().enabled = false;
    }
    void SucsessCook()
    {

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
            if (SM.isEnglish == false)
                return "������";
            else
                return "roast";
        }
        else
        {
            cookingStyle = ECookingStyle.Boil;
            if (SM.isEnglish == false)
                return "��Ƽ�";
            else
                return "boil";
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
        if (SM.isEnglish == false)
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
        }
        else
        {
            switch ((EMainMatarials)rand)
            {
                case EMainMatarials.Bread:
                    return "bread";
                case EMainMatarials.Noodle:
                    return "noodle";
                case EMainMatarials.Rice:
                    return "rice";
                default:
                    return "rice";
            }
        }
    }
}
