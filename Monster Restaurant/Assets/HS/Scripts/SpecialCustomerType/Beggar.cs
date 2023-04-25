using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Beggar : MonoBehaviour, I_CustomerType
{
    public void SpecialType(UIText cook, UIText ask, UIText speech)
    {
        
    }
    void Point0(UIText cook, UIText ask)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        Customer.OrderTalk[0] = "���߿�..�ʹ� ����Ŀ�..";
        cook.text = "��ø���";
        cookBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "��..�����մϴ�..ŪŪ";
            Customer.SuccessPoint++;

            //�丮

            //���� �� ��
            //"ŪŪ..�� ������.. �� ���ڽ��ϴ�."
        });
        ask.text = "����... ��������";
        askBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "�׷���..����ϰ� ��ٰ� ���԰� ���ع����ž�!";

            //���� �մ�
        });
    }

    void Point1(UIText cook, UIText ask)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        Customer.OrderTalk[0] = "�ȳ��ϼ���, �� ������, Ȥ��.. (�ֹ� ����)�� �ֽ� �� �����Ǳ��?";
        cook.text = "��ø���";
        cookBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "ŪŪ..";
            Customer.SuccessPoint++;

            //�丮

            //���� �� ��
            //"�ٽ� �� �� �������� �� ������ �� ���ڽ��ϴ�.. ŪŪ"
        });
        ask.text = "��?";
        askBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "���߿�.. ���� ������ �帱�״�..";

            cook.text = "�˰ڽ��ϴ�";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                Customer.OrderTalk[2] = "ŪŪ..";
                Customer.SuccessPoint++;

            });

            ask.text = "��������";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                Customer.OrderTalk[2] = "����.. ���ع�����..";

                //���� �մ�

            });
        });
    }

    void Point2(UIText cook, UIText ask)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        Customer.OrderTalk[0] = "�ȳ��ϼ���.. ����� ���õ� �����Ѱ���..?";

        cook.text = "��ø���";
        cookBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "����! ������̾� ŪŪ..";
            Customer.SuccessPoint++;

            //���� �� ��
            //"ŪŪ �����մϴ�!.."
        });

        ask.text = "��������";
        askBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "���±���.. ����� ������ ������ּż� �����մϴ�.. �����";

            //+ 10$
            //������ ���� ����
        });
    }

    void Point3(UIText cook, UIText ask)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        Customer.OrderTalk[0] = "�ȳ��ϼ���.. ����� �� �Դ��ɷ� �ּ���..";

        cook.text = "��ø���";
        cookBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "�ϰ� �־����ϴ�.. ŪŪ";
            Customer.SuccessPoint++;

            //���� �� ��
            //"�����մϴ�!.. ŪŪ"
        });

        ask.text = "��������";
        askBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "���±���.. ����� ������ ������ּż� �����մϴ�.. �����";

            //+ 20$
            //������ ���� ����
        });
    }   

    void Point4(UIText cook, UIText ask)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        Customer.OrderTalk[0] = "����� �ƽ���..?";

        cook.text = "��ø���";
        cookBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "�ϰ� �־����ϴ�.. ŪŪ";
            Customer.SuccessPoint++;

            //���� �� ��
            //"�����մϴ�!.. ŪŪ"
        });

        ask.text = "��������";
        askBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "���±���.. ����� ������ ������ּż� �����մϴ�.. �����";

            //+ 30$
            //������ ���� ����
        });
    }

    void Point5(UIText cook, UIText ask)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        Customer.OrderTalk[0] = "�������� �ƴϿ��� �ٵ� ������ �Ź� ����� �ּż� �����մϴ�. \n���������� �� �Դ��ɷ� ���� 3�� �����ұ��?";

        askBtn.gameObject.SetActive(false);

        cookBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "�ٽ� �� �� �Ź� ì���ּż� �����մϴ�.";

            //100�� �޷� +
            //��õ���� �볭�� ���� ON
        });
    }
}
