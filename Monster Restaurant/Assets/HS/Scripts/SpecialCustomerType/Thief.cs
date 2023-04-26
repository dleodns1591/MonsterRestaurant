using HS_Tree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EThiefSpeechCase
{
    NormalOrder,


}

public enum EThiefSelectCase
{
    Okey,
    ReOrder,
    What,
    Wait,
    Show,
    Sorry
}

public class Thief : MonoBehaviour, I_CustomerType
{
    public void SpecialType(UIText cook, UIText ask, UIText speech)
    {
        if(Customer.isHoldingFlower == false)
        {
            Bloom(cook, ask, speech);
        }
        else
        {
            NotBloom(cook, ask, speech);
        }    
    }

    void Bloom(UIText cook, UIText ask, UIText speech)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        cook.text = "�˰ڽ��ϴ�";
        cookBtn.onClick.AddListener(() =>
        {
            //�丮
        });
        ask.text = "��?";
        askBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "��.. �� ���� ���˾� ���? �ʵ� ���� ������?";
            if (Customer.isBloom)
            {
                cook.text = "���� �Ĺ��� �����ش�.";
                ask.text = "�˼��մϴ�.";

                cookBtn.onClick.RemoveAllListeners();
                cookBtn.onClick.AddListener(() =>
                {
                    Customer.OrderTalk[2] = "��..���� ���� ��..����������..";

                    //���� �մ�
                });
                askBtn.onClick.RemoveAllListeners();
                askBtn.onClick.AddListener(() =>
                {
                    Customer.OrderTalk[2] = "���� �׷� ������.. ��..";

                    //���� �ݾ� 1 /4 ��
                    //���� �մ�
                });
            }
            else
            {
                Customer.OrderTalk[1] = "��.. �� ���� ���˾� ���? �ʵ� ���� ������?";
                cook.transform.parent.gameObject.SetActive(false);
                ask.text = "�˼��մϴ�.";
                askBtn.onClick.RemoveAllListeners();
                askBtn.onClick.AddListener(() =>
                {
                    Customer.OrderTalk[2] = "���� �׷� ������.. ��..";

                    //���� �ݾ� 1 /4 ��
                    //���� �մ�
                });
            }
        });
    }

    void NotBloom(UIText cook, UIText ask, UIText speech)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        Customer.OrderTalk[0] = "�� ����!";

        cook.text = "��?";
        cookBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "���� ���ؾ� �˾�?";

            //���� �ݾ� 1 / 3����

            //���� �մ�
        });
        ask.text = "��..��ø���..";
        askBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "ŪŪ.. ���� ����";

            //���� �ݾ� 1 / 5����

            //���� �մ�
        });
    }
}
