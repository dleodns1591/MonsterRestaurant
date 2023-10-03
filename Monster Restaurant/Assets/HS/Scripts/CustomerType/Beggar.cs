using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Beggar : MonoBehaviour, I_CustomerType
{
    OrderManager OM;
    GameManager GM;
    OrderButtonObject BtnObjects;
    Button cookBtn;
    Button askBtn;
    TextMeshProUGUI cook, ask;

    public string SpecialAnswer()
    {
        OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.Beggar);
        OM.customer.CustomerImg.sprite = OM.customer.EventGuestSuccess[(int)EeventCustomerType.Beggar];
        switch (OM.Beggar_SuccessPoint)
        {
            case 0:
                return "ŪŪ..�� ������.. �� ���ڽ��ϴ�.";
            case 1:
                return "�ٽ� �� �� �������� �� ������ �� ���ڽ��ϴ�.. ŪŪ";
            case 2:
                return "ŪŪ �����մϴ�!..";
            case 3:
                return "�����մϴ�!.. ŪŪ";
            case 4:
                return "�����մϴ�!.. ŪŪ";
            default:
                return " ";
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

        OM.StopOrderCoroutine();

        switch (OM.Beggar_SuccessPoint)
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

    void SucsessCook()
    {
        OM.Beggar_SuccessPoint++;

        cookBtn.gameObject.SetActive(false);
        askBtn.gameObject.SetActive(false);

        OM.isBeggar = true;
        //�丮
        GM.ReturnCook();
        List<ESubMatarials> subs = new List<ESubMatarials>
        {
            ESubMatarials.NULL
        };
        GM.ConditionSetting(EMainMatarials.NULL, subs, 0, ECookingStyle.None, 1);
    }
    void RefuseOrder()
    {
        StartCoroutine(RefuseOrderDelay());
        IEnumerator RefuseOrderDelay()
        {
            GM.isBeggarRefuse = true;
            OM.StopOrderCoroutine();
            OM.isNext = true;
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(OM.ExitAndComein());
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
    void ButtonAssignment(string cookBtnText, string askBtnText, string RefuseText)
    {
        cook.text = cookBtnText;
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = cookBtnText;
            OM.dialogNumber++;

            SucsessCook();
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


            switch (OM.Beggar_SuccessPoint)
            {
                case 2:
                    GM.Money += 10;
                    break;
                case 3:
                    GM.Money += 20;
                    break;
                case 4:
                    GM.Money += 30;
                    break;
                case 5:
                    List<ESubMatarials> subs = new List<ESubMatarials> { ESubMatarials.NULL };

                    OM.dialogNumber++;

                    askBtn.gameObject.SetActive(false);

                    cook.text = "�˰ڽ��ϴ�";
                    cookBtn.onClick.RemoveAllListeners();
                    cookBtn.onClick.AddListener(() =>
                    {
                        OM.AskTalk[0] = "�˰ڽ��ϴ�";
                        OM.dialogNumber++;

                        SucsessCook();
                        GM.ConditionSetting(EMainMatarials.NULL, subs, 0, ECookingStyle.None, 3);

                        GM.Money += 10000000000;
                        //��õ���� �볭�� ���� ON
                        OM.endingManager.EndingProduction(EendingType.Dragon);
                        GM.IsEndingOpens[(int)EendingType.Dragon] = true;
                    });
                    break;
            }
        });
    }

    void Point0()
    {
        FirstTalking("���߿�..�ʹ� ����Ŀ�..");

        ButtonAssignment("��ø���", "����... ��������", "�׷���..����ϰ� ��ٰ� ���԰� ���ع��� �ž�!");
    }

    void Point1()
    {
        FirstTalking("�ȳ��ϼ���, �� ������, Ȥ��.. �ѹ� �� �ֽ� �� �����Ǳ��?");

        cook.text = "��ø���";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "��ø���";
            OM.dialogNumber++;

            SucsessCook();
        });
        ask.text = "��?";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "��?";
            OM.dialogNumber++;

            OM.OrderTalk[1] = "���߿�.. ���� ������ �帱�״�..";
            OM.dialogNumber++;

            OM.isNext = true;

            cook.text = "�˰ڽ��ϴ�";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OM.AskTalk[1] = "�˰ڽ��ϴ�";
                OM.dialogNumber++;

                SucsessCook();

            });

            ask.text = "��������";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OM.AskTalk[1] = "��������";
                OM.dialogNumber++;
                OM.OrderTalk[2] = "����.. ���ع�����..";
                OM.dialogNumber++;

                RefuseOrder();
            });
        });
    }

    void Point2()
    {
        FirstTalking("�ȳ��ϼ���.. ����� ���õ� �����Ѱ���..?");

        ButtonAssignment("��ø���", "��������", "���±���.. ����� ������ ������ּż� �����մϴ�.. �����");
    }

    void Point3()
    {
        FirstTalking("�ȳ��ϼ���.. ����� �� �Դ��ɷ� �ּ���..");

        ButtonAssignment("��ø���", "��������", "���±���.. ����� ������ ������ּż� �����մϴ�.. �����");
    }

    void Point4()
    {
        FirstTalking("����� �ƽ���..?");

        ButtonAssignment("��ø���", "��������", "���±���.. ����� ������ ������ּż� �����մϴ�.. �����");
    }

    void Point5()
    {
        FirstTalking("�������� �ƴϿ��� �ٵ� ������ �Ź� ����� �ּż� �����մϴ�. \n���������� �� �Դ��ɷ� ���� 3�� �����ұ��?");

        ButtonAssignment("", "��������", "");
    }
}
