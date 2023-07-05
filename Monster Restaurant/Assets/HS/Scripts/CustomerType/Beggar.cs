using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Beggar : MonoBehaviour, I_CustomerType
{ 
    Button cookBtn;
    Button askBtn;

    public string SpecialAnswer()
    {
        switch (OrderManager.Instance.Beggar_SuccessPoint)
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

    public void SpecialType(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        switch (OrderManager.Instance.Beggar_SuccessPoint)
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

    void SucsessCook()
    {
            OrderManager.Instance.Beggar_SuccessPoint++;

            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);

        OrderManager.Instance.isBeggar = true;
            //�丮
            GameManager.Instance.ReturnCook();
    }
    void RefuseOrder()
    {
        cookBtn.gameObject.SetActive(false);
        askBtn.gameObject.SetActive(false);
        StartCoroutine(OrderManager.Instance.ExitAndComein());
    }
    void ResetTalk()
    {
        for (int i = 0; i < OrderManager.Instance.OrderTalk.Length; i++)
        {
           // OrderManager.Instance.OrderTalk[i].
        }
    }

    void Point0(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.OrderTalk[0] = "���߿�..�ʹ� ����Ŀ�..";

        cook.text = "��ø���";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��ø���";

            SucsessCook();

            //���� �� ��
            //"ŪŪ..�� ������.. �� ���ڽ��ϴ�."
        });
        ask.text = "����... ��������";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "����... ��������";

            RefuseOrder();

        });
    }

    void Point1(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.OrderTalk[0] = "�ȳ��ϼ���, �� ������, Ȥ��.. �ѹ� �� �ֽ� �� �����Ǳ��?";
        cook.text = "��ø���";
        print(cookBtn);
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��ø���";

            SucsessCook();

            //���� �� ��
            //"�ٽ� �� �� �������� �� ������ �� ���ڽ��ϴ�.. ŪŪ"
        });
        ask.text = "��?";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��?";

            OrderManager.Instance.OrderTalk[1] = "���߿�.. ���� ������ �帱�״�..";

            OrderManager.Instance.isNext = true;

            cook.text = "�˰ڽ��ϴ�";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "�˰ڽ��ϴ�";
                
                SucsessCook();

            });

            ask.text = "��������";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "��������";
                OrderManager.Instance.OrderTalk[2] = "����.. ���ع�����..";

                RefuseOrder();
            });
        });
    }

    void Point2(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.OrderTalk[0] = "�ȳ��ϼ���.. ����� ���õ� �����Ѱ���..?";

        cook.text = "��ø���";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��ø���";
           
            SucsessCook();

            //���� �� ��
            //"ŪŪ �����մϴ�!.."
        });

        ask.text = "��������";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��������";

            OrderManager.Instance.OrderTalk[1] = "���±���.. ����� ������ ������ּż� �����մϴ�.. �����";

            RefuseOrder();

            GameManager.Instance.Money += 10;
            //������ ���� ����
        });
    }

    void Point3(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.OrderTalk[0] = "�ȳ��ϼ���.. ����� �� �Դ��ɷ� �ּ���..";

        cook.text = "��ø���";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��ø���";
            
            SucsessCook();

            //���� �� ��
            //"�����մϴ�!.. ŪŪ"
        });

        ask.text = "��������";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��������";

            OrderManager.Instance.OrderTalk[1] = "���±���.. ����� ������ ������ּż� �����մϴ�.. �����";

            RefuseOrder();

            GameManager.Instance.Money += 20;
            //������ ���� ����
        });
    }

    void Point4(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.OrderTalk[0] = "����� �ƽ���..?";

        cook.text = "��ø���";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��ø���";
            
            SucsessCook();

            //���� �� ��
            //"�����մϴ�!.. ŪŪ"
        });

        ask.text = "��������";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��������";
            OrderManager.Instance.OrderTalk[1] = "���±���.. ����� ������ ������ּż� �����մϴ�.. �����";

            RefuseOrder();

            GameManager.Instance.Money += 30;
            //������ ���� ����
        });
    }

    void Point5(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.OrderTalk[0] = "�������� �ƴϿ��� �ٵ� ������ �Ź� ����� �ּż� �����մϴ�. \n���������� �� �Դ��ɷ� ���� 3�� �����ұ��?";

        askBtn.gameObject.SetActive(false);

        cook.text = "�˰ڽ��ϴ�";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "�˰ڽ��ϴ�";

            SucsessCook();

            GameManager.Instance.Money += 10000000000;
            //��õ���� �볭�� ���� ON
            OrderManager.Instance.EndingProduction(Eending.Rich);
        });
    }
}
