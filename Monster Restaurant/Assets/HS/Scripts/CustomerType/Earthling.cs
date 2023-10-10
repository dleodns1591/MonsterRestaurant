using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Earthling : MonoBehaviour, I_CustomerType
{
    OrderManager OM;
    GameManager GM;
    SaveManager SM;
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
            if(SM.isEnglish == false)
            {
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
                switch (OM.Earthling_SuccessPoint - 1)
                {
                    case 0:
                        return "thank you I finally found a restaurant where I could eat.";
                    case 1:
                        return "Is it not even this store...";
                    case 2:
                        return "Is it not even this store...";
                    case 3:
                        return "Is it not even this store...";
                    case 4:
                        return "As expected, it feels familiar after eating it over and over again.";
                    default:
                        return "My judgment was not wrong. I felt that friendliness because you were an Earthling.\r\nI'm glad we're fellow Earthlings, but will you leave this planet with me?";
                }
            }
            
        }
        else
        {
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.Human];

            GameManager.Instance.isEarthlingRefuse = true;
            return "I felt somewhat familiar with this store, but it wasn't...";
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
        SM = SaveManager.Instance;
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
        if(SM.isEnglish == false)
        {
            FirstTalking("���� �� ���� �� �� �� �� ������ ������ �ֽ� �� �ֳ���?");

            ButtonAssignment("���ݸ� ��ٷ� �ּ���.", "���� ���� ��Ḹ ���� �ʾƼ� �� �� �� �����ϴ�.", "�� ���Ե� �� �Ǵ°ǰ�...", EMainMatarials.Rice, ECookingStyle.Roast);
        }
        else
        {
            FirstTalking("It seems like I haven't eaten in a while. Can you cook it for me?");

            ButtonAssignment("Please wait a minute.", "I don't think we can because we don't sell only the main ingredients. I'm sorry.", "Is it not even this store...", EMainMatarials.Rice, ECookingStyle.Roast);
        }
    }
    void Point1()
    {
        if (SM.isEnglish == false)
        {
            FirstTalking("�ȳ��ϼ���. �̹����� ��⸦ ������ �ֽ� �� �ֳ���?");

            ButtonAssignment("��", "�̹����� ���� �� �����ϴ�.", "�� ���Ե� �� �Ǵ°ǰ�...", EMainMatarials.Meat, ECookingStyle.Roast);
        }
        else
        {
            FirstTalking("can you grill the meat and give it to me this time?");

            ButtonAssignment("All right.", "I think it's going to be difficult this time.", ".... I'm sorry, I need to find the next store..", EMainMatarials.Meat, ECookingStyle.Roast);
        }
    }
    void Point2()
    {
        if (SM.isEnglish == false)
        {
            FirstTalking("�ȳ��ϼ���.. �̹����� �Ľ�Ÿ�� ������ �ֽ� �� �ֳ���?");

            ButtonAssignment("��", "�̹����� ���� �� �����ϴ�.", "......�˼��մϴ�, ���� ���Ը� ã�ƾ� �ϳ�..", EMainMatarials.Noodle, ECookingStyle.Boil);
        }
        else
        {
            FirstTalking("Hello.. Can you boil the pasta this time?");

            ButtonAssignment("All right.", "I think it's going to be difficult this time.", ".... I'm sorry, I need to find the next store..", EMainMatarials.Noodle, ECookingStyle.Boil);
        }
    }
    void Point3()
    {
        if (SM.isEnglish == false)
        {
            FirstTalking("�ȳ��ϼ���.. �̹����� ���� ������ �ֽ� �� �ֳ���?");

            ButtonAssignment("��", "�̹����� ���� �� �����ϴ�.", "......�˼��մϴ�, ���� ���Ը� ã�ƾ� �ϳ�..", EMainMatarials.Bread, ECookingStyle.Roast);
        }
        else
        {
            FirstTalking("Hello.. Can you bake and give me bread this time?");

            ButtonAssignment("All right.", "I think it's going to be difficult this time.", ".... I'm sorry, I need to find the next store..", EMainMatarials.Bread, ECookingStyle.Roast);
        }
    }
    void Point4()
    {
        if (SM.isEnglish == false)
        {
            FirstTalking("�ȳ��ϼ���.. �̹����� ���� ������ �ֽ� �� �ֳ���?");

            ButtonAssignment("��", "�̹����� ���� �� �����ϴ�.", "�� ���Կ� ��� ģ�ٰ��� ����µ�, �ƴϾ��׿�....", EMainMatarials.Rice, ECookingStyle.Roast);
        }
        else
        {
            FirstTalking("Hello.. Can you cook the rice this time?");

            ButtonAssignment("All right.", "I think it's going to be difficult this time.", "I felt somewhat familiar with this store, but it wasn't...", EMainMatarials.Rice, ECookingStyle.Roast);
        }
    }
    void Point5()
    {

        if (SM.isEnglish == false)
        {
            FirstTalking("�� �Ǵ��� Ʋ���� �ʾҾ��. �� ģ�ٰ��� ����� �������̱� ������ ���������. ���� �������̶� �ݰ����� �׷��µ� ���� �Բ� �� �༺�� ���������Ƿ���?");

            OM.OrderTalk[0] = "�� �Ǵ��� Ʋ���� �ʾҾ��. �� ģ�ٰ��� ����� �������̱� ������ ���������. ���� �������̶� �ݰ����� �׷��µ� ���� �Բ� �� �༺�� ���������Ƿ���?";
        }
        else
        {
            FirstTalking("My judgment was not wrong. I felt that friendliness because you were an Earthling. I'm glad we're fellow Earthlings, but will you leave this planet with me?");

            OM.OrderTalk[0] = "My judgment was not wrong. I felt that friendliness because you were an Earthling. I'm glad we're fellow Earthlings, but will you leave this planet with me?";
        }


        askBtn.GetComponent<Image>().enabled = false;
        askBtn.enabled = false;
        ask.text = "";

        if (SM.isEnglish == false)
            cook.text = "�˰ڽ��ϴ�";
        else
            cook.text = "All right";

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

