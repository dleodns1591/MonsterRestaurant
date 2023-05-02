using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Beggar : MonoBehaviour, I_CustomerType
{
    public void SpecialType(UIText cook, UIText ask)
    {
        switch (OrderManager.SuccessPoint)
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
    void Point0(UIText cook, UIText ask)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        OrderManager.OrderTalk[0] = "���߿�..�ʹ� ����Ŀ�..";
        cook.text = "��ø���";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "��ø���";

            OrderManager.OrderTalk[1] = "��..�����մϴ�..ŪŪ";
            OrderManager.SuccessPoint++;

            //�丮

            //���� �� ��
            //"ŪŪ..�� ������.. �� ���ڽ��ϴ�."
        });
        ask.text = "����... ��������";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "����... ��������";

            OrderManager.OrderTalk[1] = "�׷���..����ϰ� ��ٰ� ���԰� ���ع����ž�!";

            //���� �մ�
        });
    }

    void Point1(UIText cook, UIText ask)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        OrderManager.OrderTalk[0] = "�ȳ��ϼ���, �� ������, Ȥ��.. (�ֹ� ����)�� �ֽ� �� �����Ǳ��?";
        cook.text = "��ø���";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "��ø���";

            OrderManager.OrderTalk[1] = "ŪŪ..";
            OrderManager.SuccessPoint++;

            //�丮

            //���� �� ��
            //"�ٽ� �� �� �������� �� ������ �� ���ڽ��ϴ�.. ŪŪ"
        });
        ask.text = "��?";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "��?";

            OrderManager.OrderTalk[1] = "���߿�.. ���� ������ �帱�״�..";

            cook.text = "�˰ڽ��ϴ�";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OrderManager.AskTalk[1] = "�˰ڽ��ϴ�";
                OrderManager.OrderTalk[2] = "ŪŪ..";
                OrderManager.SuccessPoint++;

            });

            ask.text = "��������";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OrderManager.AskTalk[1] = "��������";
                OrderManager.OrderTalk[2] = "����.. ���ع�����..";

                //���� �մ�

            });
        });
    }

    void Point2(UIText cook, UIText ask)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        OrderManager.OrderTalk[0] = "�ȳ��ϼ���.. ����� ���õ� �����Ѱ���..?";

        cook.text = "��ø���";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "��ø���";
            OrderManager.OrderTalk[1] = "����! ������̾� ŪŪ..";
            OrderManager.SuccessPoint++;

            //���� �� ��
            //"ŪŪ �����մϴ�!.."
        });

        ask.text = "��������";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "��������";

            OrderManager.OrderTalk[1] = "���±���.. ����� ������ ������ּż� �����մϴ�.. �����";

            //+ 10$
            //������ ���� ����
        });
    }

    void Point3(UIText cook, UIText ask)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        OrderManager.OrderTalk[0] = "�ȳ��ϼ���.. ����� �� �Դ��ɷ� �ּ���..";

        cook.text = "��ø���";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "��ø���";
            OrderManager.OrderTalk[1] = "�ϰ� �־����ϴ�.. ŪŪ";
            OrderManager.SuccessPoint++;

            //���� �� ��
            //"�����մϴ�!.. ŪŪ"
        });

        ask.text = "��������";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "��������";

            OrderManager.OrderTalk[1] = "���±���.. ����� ������ ������ּż� �����մϴ�.. �����";

            //+ 20$
            //������ ���� ����
        });
    }

    void Point4(UIText cook, UIText ask)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        OrderManager.OrderTalk[0] = "����� �ƽ���..?";

        cook.text = "��ø���";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "��ø���";
            OrderManager.OrderTalk[1] = "�ϰ� �־����ϴ�.. ŪŪ";
            OrderManager.SuccessPoint++;

            //���� �� ��
            //"�����մϴ�!.. ŪŪ"
        });

        ask.text = "��������";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "��������";
            OrderManager.OrderTalk[1] = "���±���.. ����� ������ ������ּż� �����մϴ�.. �����";

            //+ 30$
            //������ ���� ����
        });
    }

    void Point5(UIText cook, UIText ask)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        OrderManager.OrderTalk[0] = "�������� �ƴϿ��� �ٵ� ������ �Ź� ����� �ּż� �����մϴ�. \n���������� �� �Դ��ɷ� ���� 3�� �����ұ��?";

        askBtn.gameObject.SetActive(false);

        cook.text = "�˰ڽ��ϴ�";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "�˰ڽ��ϴ�";

            OrderManager.OrderTalk[1] = "�ٽ� �� �� �Ź� ì���ּż� �����մϴ�.";

            //100�� �޷� +
            //��õ���� �볭�� ���� ON
        });
    }
}
