using HS_Tree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thief : MonoBehaviour, I_CustomerType
{
    public void SpecialType(UIText cook, UIText ask)
    {
        if(Customer.isHoldingFlower == false)
        {
            Bloom(cook, ask);
        }
        else
        {
            NotBloom(cook, ask);
        }    
    }

    void Bloom(UIText cook, UIText ask)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        cook.text = "�˰ڽ��ϴ�";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "�˰ڽ��ϴ�";
            //�丮
        });
        ask.text = "��?";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "��?";

            OrderManager.OrderTalk[1] = "��.. �� ���� ���˾� ���? �ʵ� ���� ������?";
            if (Customer.isBloom)
            {
                cook.text = "���� �Ĺ��� �����ش�.";
                ask.text = "�˼��մϴ�.";

                cookBtn.onClick.RemoveAllListeners();
                cookBtn.onClick.AddListener(() =>
                {
                    OrderManager.AskTalk[1] = "���� �Ĺ��� �����ش�.";
                    OrderManager.OrderTalk[2] = "��..���� ���� ��..����������..";

                    //���� �մ�
                });
                askBtn.onClick.RemoveAllListeners();
                askBtn.onClick.AddListener(() =>
                {
                    OrderManager.AskTalk[1] = "�˼��մϴ�.";
                    OrderManager.OrderTalk[2] = "���� �׷� ������.. ��..";

                    //���� �ݾ� 1 /4 ��
                    //���� �մ�
                });
            }
            else
            {
                OrderManager.OrderTalk[1] = "��.. �� ���� ���˾� ���? �ʵ� ���� ������?";
                cook.transform.parent.gameObject.SetActive(false);
                ask.text = "�˼��մϴ�.";
                askBtn.onClick.RemoveAllListeners();
                askBtn.onClick.AddListener(() =>
                {
                    OrderManager.AskTalk[1] = "�˼��մϴ�.";
                    OrderManager.OrderTalk[2] = "���� �׷� ������.. ��..";

                    //���� �ݾ� 1 /4 ��
                    //���� �մ�
                });
            }
        });
    }

    void NotBloom(UIText cook, UIText ask)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        OrderManager.OrderTalk[0] = "�� ����!";

        cook.text = "��?";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "��?";

            OrderManager.OrderTalk[1] = "���� ���ؾ� �˾�?";

            //���� �ݾ� 1 / 3����

            //���� �մ�
        });
        ask.text = "��..��ø���..";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "��..��ø���..";

            OrderManager.OrderTalk[1] = "ŪŪ.. ���� ����";

            //���� �ݾ� 1 / 5����

            //���� �մ�
        });
    }
}
