using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnlinePvp : MonoBehaviour, I_CustomerType
{
    OrderManager OM;
    GameManager GM;
    SaveManager SM;
    OrderButtonObject BtnObjects;
    Button cookBtn;
    Button askBtn;
    TextMeshProUGUI cook, ask;

    int randomMain;
    ECookingStyle cookingStyle;
    List<ESubMatarials> subs;

    public string SpecialAnswer()
    {
        //count�� 5���̸�
        if (true)
        {
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestSuccess[(int)EeventCustomerType.GroupOrder];
            if (SM.isEnglish == false)
                return "�� �̱� ����";
            else
                return "";
        }
        else
        {
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.GroupOrder];
            if (SM.isEnglish == false)
                return "�� �� ����";
            else
                return "";
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

        StartCoroutine(CookStartDelay());
    }

    IEnumerator CookStartDelay()
    {
        subs = new List<ESubMatarials> { (ESubMatarials)UnityEngine.Random.Range(0, Enum.GetNames(typeof(ESubMatarials)).Length - 1 /*NULL ����*/) };
        cookingStyle = (ECookingStyle)UnityEngine.Random.Range(0, Enum.GetValues(typeof(ECookingStyle)).Length - 1);

        yield return new WaitForSeconds(6f);

        if (SM.isEnglish == false)
            OM.orderMessageManager.TalkingText($"�ٸ� ���� �˹ٻ��� ��Ī�� �������ϴ�!\n�̹� ��ῡ�� ������ �丮��.. {CookingStyle()} ���� {SubString(this.subs[0])} {DrawCookingStyle()}�Դϴ�!!!\n�Ƿ��� ������ ����̱� ������ �޸� ����� �������� �ʴ´ٴ� �� �˾��ֽø� �����ϰڽ��ϴ�");
        else
            OM.orderMessageManager.TalkingText("");

        yield return new WaitForSeconds(7.7f);

        OM.orderMessageManager.TalkingText("��! ���ݺ��� �����ϰڽ��ϴ�.");
        yield return new WaitForSeconds(1.8f);
        OM.orderMessageManager.TalkingText("3");
        yield return new WaitForSeconds(1f);
        OM.orderMessageManager.TalkingText("2");
        yield return new WaitForSeconds(1f);
        OM.orderMessageManager.TalkingText("1");
        yield return new WaitForSeconds(1f);

        SucsessCook();

        GM.ConditionSetting((EMainMatarials)randomMain, subs, 1, cookingStyle, 1);
    }

    void SucsessCook()
    {
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
        int NullCheck(int num)
        {
            if ((EMainMatarials)num == EMainMatarials.NULL)
                num = NullCheck(UnityEngine.Random.Range(0, Enum.GetValues(typeof(EMainMatarials)).Length));

            return num;
        }

        randomMain = UnityEngine.Random.Range(0, Enum.GetValues(typeof(EMainMatarials)).Length);
        randomMain = NullCheck(randomMain);
        switch ((EMainMatarials)randomMain)
        {
            case EMainMatarials.Meat:
                return "���";
            case EMainMatarials.Bread:
                return "��";
            case EMainMatarials.Noodle:
                return "���";
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
                return "�ܰ�Ǯ";
            case ESubMatarials.Battery:
                return "������";
            case ESubMatarials.Bismuth:
                return "�񽺹�Ʈ";
            case ESubMatarials.Bolt:
                return "��Ʈ";
            case ESubMatarials.Eyes:
                return "����";
            case ESubMatarials.Fur:
                return "�й�ġ";
            case ESubMatarials.Jewelry:
                return "����";
            case ESubMatarials.Money:
                return "��";
            case ESubMatarials.Paper:
                return "����";
            case ESubMatarials.Poop:
                return "��";
            case ESubMatarials.Preservatives:
                return "�����";
            case ESubMatarials.Sticker:
                return "��ƼĿ";
            case ESubMatarials.NULL:
                return "�ܰ�Ǯ";
            default:
                return "�ܰ�Ǯ";
        }
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
}
