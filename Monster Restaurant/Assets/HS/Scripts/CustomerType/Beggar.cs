using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Beggar : MonoBehaviour, I_CustomerType
{
    OrderManager OM;
    GameManager GM;
    SaveManager SM;
    OrderButtonObject BtnObjects;
    Button cookBtn;
    Button askBtn;
    TextMeshProUGUI cook, ask;

    public string SpecialAnswer()
    {
        OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.Beggar);
        OM.customer.CustomerImg.sprite = OM.customer.EventGuestSuccess[(int)EeventCustomerType.Beggar];

        if (SM.isEnglish == false)
        {
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
        else
        {
            switch (OM.Beggar_SuccessPoint)
            {
                case 0:
                    return "Wow, this grace.... I'll pay back.";
                case 1:
                    return "Once again, I'll make sure to return this favor. LOL";
                case 2:
                    return "Thank you!";
                case 3:
                    return "Thank you!.. Tsk tsk";
                case 4:
                    return "Thank you!.. Tsk tsk";
                default:
                    return " ";
            }
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

        if (SM.isEnglish == false)
        {

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
        else
        {
            switch (OM.Beggar_SuccessPoint)
            {
                case 0:
                    Point0Eng();
                    break;
                case 1:
                    Point1Eng();
                    break;
                case 2:
                    Point2Eng();
                    break;
                case 3:
                    Point3Eng();
                    break;
                case 4:
                    Point4Eng();
                    break;
                case 5:
                    Point5Eng();
                    break;
            }
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
                    OM.directingManager.Directing(10, 4);
                    break;
                case 3:
                    OM.directingManager.Directing(20, 6);

                    break;
                case 4:
                    OM.directingManager.Directing(30, 8);

                    break;
                case 5:
                    List<ESubMatarials> subs = new List<ESubMatarials> { ESubMatarials.NULL };

                    OM.dialogNumber++;

                    askBtn.gameObject.SetActive(false);

                    if (SaveManager.Instance.isEnglish == false)
                        cook.text = "�˰ڽ��ϴ�";
                    else
                        cook.text = "Wait a Minute";

                    cookBtn.onClick.RemoveAllListeners();
                    cookBtn.onClick.AddListener(() =>
                    {
                        if (SaveManager.Instance.isEnglish == false)
                            OM.AskTalk[0] = "�˰ڽ��ϴ�";
                        else
                            OM.AskTalk[0] = "Wait a Minute";
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

    void Point0Eng()
    {
        FirstTalking("Please, I'm so hungry..");

        ButtonAssignment("Wait a Minute", "Oh... Get out", "If you live that strict, the store will go bust!");
    }

    void Point1Eng()
    {
        FirstTalking("Hello, it's me again. Could you please give me some food?");

        cook.text = "Wait a Minute";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "Wait a Minute";
            OM.dialogNumber++;

            SucsessCook();
        });
        ask.text = "Pardon?";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "Pardon?";
            OM.dialogNumber++;

            OM.OrderTalk[1] = "Please, I'll give you the money one day..";
            OM.dialogNumber++;

            OM.isNext = true;

            cook.text = "All right.";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OM.AskTalk[1] = "All right.";
                OM.dialogNumber++;

                SucsessCook();

            });

            ask.text = "Please get out.";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OM.AskTalk[1] = "Please get out.";
                OM.dialogNumber++;
                OM.OrderTalk[2] = "The store... ruin it...";
                OM.dialogNumber++;

                RefuseOrder();
            });
        });
    }

    void Point2Eng()
    {
        FirstTalking("Hello, boss. Is it possible today as well?");

        ButtonAssignment("Wait a Minute", "Please get out.", "Until now... Thank you for making food for free... Boss.");
    }

    void Point3Eng()
    {
        FirstTalking("Hello.. Boss, please give me what I always eat..");

        ButtonAssignment("Wait a Minute", "Please get out.", "Until now... Thank you for making food for free... Boss.");
    }

    void Point4Eng()
    {
        FirstTalking("Boss, you know..?");

        ButtonAssignment("Wait a Minute", "Please get out.", "Thank you for making free food so far, boss.");
    }

    void Point5Eng()
    {
        FirstTalking("It must not have been easy, but thank you for providing free food every time. \nLastly, is it possible to eat 3 dishes with what I always eat?");

        ButtonAssignment("", "Please get out.", "");
    }
}
