using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Earthling : MonoBehaviour, I_CustomerType
{
    Button cookBtn;
    Button askBtn;

    void SucsessCook()
    {
        OrderManager.Instance.Earthling_SuccessPoint++;

        cookBtn.gameObject.SetActive(false);
        askBtn.gameObject.SetActive(false);

        //�丮
        GameManager.Instance.ReturnCook();
    }

    public string SpecialAnswer()
    {

        if (GameManager.Instance.Satisfaction >= 40)
        {
            OrderManager.Instance.EeventCustomerSetting((int)EeventCustomerType.Human);
            OrderManager.Instance.CustomerImg.sprite = OrderManager.Instance.EventGuestSuccess[(int)EeventCustomerType.Human];
            switch (OrderManager.Instance.Earthling_SuccessPoint - 1)
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
            OrderManager.Instance.CustomerImg.sprite = OrderManager.Instance.EventGuestFails[(int)EeventCustomerType.Human];

            GameManager.Instance.isEarthlingRefuse = true;
            return "......�˼��մϴ�, ���� ���Ը� ã�ƾ� �ϳ�..";
        }
    }


    public void SpecialType(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.StopOrderCoroutine();

        switch (OrderManager.Instance.Earthling_SuccessPoint)
        {
            case 0:
                Point0(cook, ask);
                break;
            case 1:
                Point1(cook, ask);
                break;
            case 2:
                Point2(cook, ask);
                break;
            case 3:
                Point3(cook, ask);
                break;
            case 4:
                Point4(cook, ask);
                break;
            case 5:
                Point5(cook, ask);
                break;
        }
    }

    void RefuseOrder()
    {
        StartCoroutine(RefuseOrderDelay());
        IEnumerator RefuseOrderDelay()
        {
            for (int i = 0; i < OrderManager.Instance.OrderTalk.Length; i++)
            {
                print(OrderManager.Instance.OrderTalk[i]);
            }

            GameManager.Instance.isEarthlingRefuse = true;
            OrderManager.Instance.isNext = true;
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(OrderManager.Instance.ExitAndComein(true));
            OrderManager.Instance.StopOrderCoroutine();

        }

    }

    void Point0(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "���� �� ���� �� �� �� �� ������ ������ �ֽ� �� �ֳ���?";
        OrderManager.Instance.dialogNumber++;
        cook.text = "���ݸ� ��ٷ� �ּ���.";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "���ݸ� ��ٷ� �ּ���.";
            OrderManager.Instance.dialogNumber++;
            SucsessCook();
            List<ESubMatarials> subs = new List<ESubMatarials>
        {
            ESubMatarials.NULL
        };
            GameManager.Instance.ConditionSetting(EMainMatarials.Rice, subs,     0, ECookingStyle.Roast, 1);
        });
        ask.text = "���� ���� ��Ḹ ���� �ʾƼ� �� �� �� �����ϴ�.";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "���� ���� ��Ḹ ���� �ʾƼ� �� �� �� �����ϴ�.";
            OrderManager.Instance.dialogNumber++;
            OrderManager.Instance.OrderTalk[1] = "�� ���Ե� �� �Ǵ°ǰ�...";
            OrderManager.Instance.dialogNumber++;
            RefuseOrder();
        });
    }
    void Point1(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "�ȳ��ϼ���. Ȥ�� �̹����� ���� ��Ḹ ���ѵ� �ɱ��..?";
        OrderManager.Instance.dialogNumber++;

        cook.text = "��";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��";
            OrderManager.Instance.dialogNumber++;

            OrderManager.Instance.OrderTalk[1] = "�׷��� Ȥ��, �̹����� ��⸦ ������ �ֽ� �� �ֳ���?";
            OrderManager.Instance.dialogNumber++;

            OrderManager.Instance.isNext = true;

            cook.text = "��";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "��";
                OrderManager.Instance.dialogNumber++;
                SucsessCook();
                List<ESubMatarials> subs = new List<ESubMatarials>
                {
                    ESubMatarials.NULL
                };
                GameManager.Instance.ConditionSetting(EMainMatarials.Meat, subs, 0, ECookingStyle.Roast, 1);


                askBtn.gameObject.SetActive(false);
            });

            askBtn.onClick.RemoveAllListeners();
        });

        ask.text = "�̹����� ���� �� �����ϴ�.";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "�̹����� ���� �� �����ϴ�.";
            OrderManager.Instance.dialogNumber++;
            OrderManager.Instance.OrderTalk[1] = "......�˼��մϴ�, ���� ���Ը� ã�ƾ� �ϳ�..";
            OrderManager.Instance.dialogNumber++;

            RefuseOrder();

        });
    }
    void Point2(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "�ȳ��ϼ���.. �̹����� �Ľ�Ÿ�� ������ �ֽ� �� �ֳ���?";
        OrderManager.Instance.dialogNumber++;
        cook.text = "��";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��";
            OrderManager.Instance.dialogNumber++;
            SucsessCook();
            List<ESubMatarials> subs = new List<ESubMatarials>
        {
            ESubMatarials.NULL
        };
            GameManager.Instance.ConditionSetting(EMainMatarials.Noodle, subs, 0, ECookingStyle.Boil, 1);
        });

        ask.text = "�̹����� ���� �� �����ϴ�.";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "�̹����� ���� �� �����ϴ�.";
            OrderManager.Instance.dialogNumber++;
            OrderManager.Instance.OrderTalk[1] = "......�˼��մϴ�, ���� ���Ը� ã�ƾ� �ϳ�..";
            OrderManager.Instance.dialogNumber++;

            RefuseOrder();

        });
    }
    void Point3(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "�ȳ��ϼ���.. �̹����� ���� ������ �ֽ� �� �ֳ���?";
        OrderManager.Instance.dialogNumber++;
        cook.text = "��";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��";
            OrderManager.Instance.dialogNumber++;
            SucsessCook();
            List<ESubMatarials> subs = new List<ESubMatarials>
        {
            ESubMatarials.NULL
        };
            GameManager.Instance.ConditionSetting(EMainMatarials.Bread, subs, 0, ECookingStyle.Roast, 1);
        });
        ask.text = "�̹����� ���� �� �����ϴ�.";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "�̹����� ���� �� �����ϴ�.";
            OrderManager.Instance.dialogNumber++;
            OrderManager.Instance.OrderTalk[1] = "......�˼��մϴ�, ���� ���Ը� ã�ƾ� �ϳ�..";
            OrderManager.Instance.dialogNumber++;

            RefuseOrder();

        });
    }
    void Point4(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "�ȳ��ϼ���.. �̹����� ���� ������ �ֽ� �� �ֳ���?";
        OrderManager.Instance.dialogNumber++;
        cook.text = "��";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��";
            OrderManager.Instance.dialogNumber++;
            SucsessCook();
            List<ESubMatarials> subs = new List<ESubMatarials>
        {
            ESubMatarials.NULL
        };
            GameManager.Instance.ConditionSetting(EMainMatarials.Rice, subs, 0, ECookingStyle.Roast, 1);
        });
        ask.text = "�̹����� ���� �� �����ϴ�.";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "�̹����� ���� �� �����ϴ�.";
            OrderManager.Instance.dialogNumber++;
            OrderManager.Instance.OrderTalk[1] = "�� ���Կ� ��� ģ�ٰ��� ����µ�, �ƴϾ��׿�....";
            OrderManager.Instance.dialogNumber++;

            RefuseOrder();

        });
    }
    void Point5(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "�� �Ǵ��� Ʋ���� �ʾҾ��. �� ģ�ٰ��� ����� �������̱� ������ ���������. ���� �������̶� �ݰ����� �׷��µ� ���� �Բ� �� �༺�� ���������Ƿ���?";

        askBtn.GetComponent<Image>().enabled = false;
        askBtn.enabled = false;
        ask.text = "";
        cook.text = "�˰ڽ��ϴ�";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.EndingProduction(EendingType.LookStar);
            GameManager.Instance.isEndingOpens[(int)EendingType.LookStar] = true;
            SaveManager.Instance.isEndingOpens[(int)EendingType.LookStar] = true;
            askBtn.GetComponent<Image>().enabled = true;
            askBtn.enabled = true;
            askBtn.gameObject.SetActive(false);
        });
    }
}

