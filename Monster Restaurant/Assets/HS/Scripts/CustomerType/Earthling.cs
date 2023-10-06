using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Earthling : MonoBehaviour, I_CustomerType
{
    OrderManager OM;
    GameManager GM;
    OrderButtonObject BtnObjects;
    Button cookBtn;
    Button askBtn;
    TextMeshProUGUI cook, ask;
    List<ESubMatarials> subs;

    void SucsessCook()
    {
        OM.Earthling_SuccessPoint++;

        cookBtn.gameObject.SetActive(false);
        askBtn.gameObject.SetActive(false);

        //�丮
        GM.ReturnCook();
    }

    void RefuseOrder()
    {
        StartCoroutine(RefuseOrderDelay());
        IEnumerator RefuseOrderDelay()
        {
            GM.isEarthlingRefuse = true;
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
        OM.perfectMade = 1;

        if (GM.Satisfaction >= 40)
        {
            OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.Human);
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestSuccess[(int)EeventCustomerType.Human];
            switch (OM.Earthling_SuccessPoint - 1)
            {
                case 0:
                    return "�����մϴ�. ���� ���� �� �ִ� ���Ը� ã�ҳ׿�.";
                case 1:
                    return "�̹����� �����մϴ�...! ������ �� ���ڽ��ϴ�.";
                case 2:
                    return "�̹����� �����մϴ�...! ������ �� ���ڽ��ϴ�.";
                case 3:
                    return "�̹����� �����մϴ�...! ������ �� ���ڽ��ϴ�.";
                case 4:
                    return "����, ��� �Ծ�� ģ�ٰ��� ��׿�.";
                default:
                    return "�� �Ǵ��� Ʋ���� �ʾҾ��. �� ģ�ٰ��� ����� �������̱� ������ ���������.\r\n���� �������̶� �ݰ����� �׷��µ� ���� �Բ� �� �༺�� ���������Ƿ���?";
            }
        }
        else
        {
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.Human];

            GameManager.Instance.isEarthlingRefuse = true;
            return "......�˼��մϴ�, ���� ���Ը� ã�ƾ� �ϳ�..";
        }
    }

    void FirstTalking(string speech)
    {
        OM.OrderTalk[0] = speech;
        OM.dialogNumber++;
    }

    /// <summary>
    /// ��ư�� �̺�Ʈ �־��ִ� �Լ�
    /// </summary>
    /// <param name="cookBtnText">1��° ��ư�� ���� ��</param>
    /// <param name="askBtnText">2��° ��ư�� ���� ��</param>
    /// <param name="RefuseText">2��° ��ư�� ������ ������ ��</param>
    /// <param name="main">���ϴ� �������</param>
    /// <param name="style">���ϴ� ��ŷ���</param>
    void ButtonAssignment(string cookBtnText, string askBtnText, string RefuseText, EMainMatarials main, ECookingStyle style)
    {
        cook.text = cookBtnText;
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = cookBtnText;
            OM.dialogNumber++;
            SucsessCook();
            GM.ConditionSetting(main, subs, 0, style, 1);
        });
        ask.text = askBtnText;
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = askBtnText;
            OM.dialogNumber++;
            OM.OrderTalk[1] = RefuseText;
            OM.dialogNumber++;
            RefuseOrder();
        });
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

        OM.perfectMade = 100;

        subs = new List<ESubMatarials> { ESubMatarials.NULL };

        OM.StopOrderCoroutine();

        switch (OM.Earthling_SuccessPoint)
        {
            case 0:
                Point0();
                break;
            case 1:
                Point1();
                break;
            case 2:
                Point2();
                break;
            case 3:
                Point3();
                break;
            case 4:
                Point4();
                break;
            case 5:
                Point5();
                break;
        }
    }


    void Point0()
    {
        FirstTalking("���� �� ���� �� �� �� �� ������ ������ �ֽ� �� �ֳ���?");

        ButtonAssignment("���ݸ� ��ٷ� �ּ���.", "���� ���� ��Ḹ ���� �ʾƼ� �� �� �� �����ϴ�.", "�� ���Ե� �� �Ǵ°ǰ�...", EMainMatarials.Rice, ECookingStyle.Roast);
    }
    void Point1()
    {
        FirstTalking("�ȳ��ϼ���. Ȥ�� �̹����� ���� ��Ḹ ���ѵ� �ɱ��..");

        ButtonAssignment("��", "�̹����� ���� �� �����ϴ�.", "......�˼��մϴ�, ���� ���Ը� ã�ƾ� �ϳ�..", EMainMatarials.Meat, ECookingStyle.Roast);
    }
    void Point2()
    {
        FirstTalking("�ȳ��ϼ���.. �̹����� �Ľ�Ÿ�� ������ �ֽ� �� �ֳ���?");

        ButtonAssignment("��", "�̹����� ���� �� �����ϴ�.", "......�˼��մϴ�, ���� ���Ը� ã�ƾ� �ϳ�..", EMainMatarials.Noodle, ECookingStyle.Boil);
    }
    void Point3()
    {
        FirstTalking("�ȳ��ϼ���.. �̹����� ���� ������ �ֽ� �� �ֳ���?");

        ButtonAssignment("��", "�̹����� ���� �� �����ϴ�.", "......�˼��մϴ�, ���� ���Ը� ã�ƾ� �ϳ�..", EMainMatarials.Bread, ECookingStyle.Roast);
    }
    void Point4()
    {
        FirstTalking("�ȳ��ϼ���.. �̹����� ���� ������ �ֽ� �� �ֳ���?");

        ButtonAssignment("��", "�̹����� ���� �� �����ϴ�.", "�� ���Կ� ��� ģ�ٰ��� ����µ�, �ƴϾ��׿�....", EMainMatarials.Rice, ECookingStyle.Roast);
    }
    void Point5()
    {
        FirstTalking("�� �Ǵ��� Ʋ���� �ʾҾ��. �� ģ�ٰ��� ����� �������̱� ������ ���������. ���� �������̶� �ݰ����� �׷��µ� ���� �Բ� �� �༺�� ���������Ƿ���?");

        OM.OrderTalk[0] = "�� �Ǵ��� Ʋ���� �ʾҾ��. �� ģ�ٰ��� ����� �������̱� ������ ���������. ���� �������̶� �ݰ����� �׷��µ� ���� �Բ� �� �༺�� ���������Ƿ���?";

        askBtn.GetComponent<Image>().enabled = false;
        askBtn.enabled = false;
        ask.text = "";

        cook.text = "�˰ڽ��ϴ�";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.endingManager.EndingProduction(EendingType.LookStar);
            GM.IsEndingOpens[(int)EendingType.LookStar] = true;
            askBtn.GetComponent<Image>().enabled = true;
            askBtn.enabled = true;
            askBtn.gameObject.SetActive(false);
        });
    }
}

