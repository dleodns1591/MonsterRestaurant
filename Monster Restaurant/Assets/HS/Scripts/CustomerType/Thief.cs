using HS_Tree;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Thief : MonoBehaviour, I_CustomerType
{
    public string SpecialAnswer()
    {
        if (OrderManager.Instance.isCookingSuccess)
        {
            return "��... ���� ���п� �� �� �˾ƶ�..";
        }
        else
            return "����� ���� ������.. ��...";
    }

    public void SpecialType(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        if(OrderManager.Instance.isHoldingFlower == false)
        {
            NotBloom(cook, ask);
        }
        else
        {
            Bloom(cook, ask);
        }    
    }

    void Bloom(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        Button cookBtn = cook.transform.parent.GetComponent<Button>();
        Button askBtn = ask.transform.parent.GetComponent<Button>();


        cook.text = "�˰ڽ��ϴ�";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "�˰ڽ��ϴ�";

            //�丮
            GameManager.Instance.ReturnCook();
        });
        ask.text = "��?";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��?";

            OrderManager.Instance.OrderTalk[1] = "��.. �� ���� ���˾� ���? �ʵ� ���� ������?";
            OrderManager.Instance.isNext = true;
            if (OrderManager.Instance.isBloom)
            {
                cook.text = "���� �Ĺ��� �����ش�.";
                ask.text = "�˼��մϴ�.";

                cookBtn.onClick.RemoveAllListeners();
                cookBtn.onClick.AddListener(() =>
                {
                    OrderManager.Instance.AskTalk[1] = "���� �Ĺ��� �����ش�.";
                    OrderManager.Instance.OrderTalk[2] = "��..���� ���� ��..����������..";

                    OrderManager.Instance.isNext = true;

                    //���� �մ�
                    OrderManager.Instance.ExitAndComein();
                });
                askBtn.onClick.RemoveAllListeners();
                askBtn.onClick.AddListener(() =>
                {
                    OrderManager.Instance.AskTalk[1] = "�˼��մϴ�.";
                    OrderManager.Instance.OrderTalk[2] = "���� �׷� ������.. ��..";

                    OrderManager.Instance.isNext = true;

                    //���� �ݾ� 1 /4 ��
                    GameManager.Instance.Money = GameManager.Instance.Money / 4;
                    //���� �մ�
                    OrderManager.Instance.ExitAndComein();
                });
            }
            else
            {
                OrderManager.Instance.OrderTalk[1] = "��.. �� ���� ���˾� ���? �ʵ� ���� ������?";
                cook.transform.parent.gameObject.SetActive(false);
                ask.text = "�˼��մϴ�.";
                askBtn.onClick.RemoveAllListeners();
                askBtn.onClick.AddListener(() =>
                {
                    OrderManager.Instance.AskTalk[1] = "�˼��մϴ�.";
                    OrderManager.Instance.OrderTalk[2] = "���� �׷� ������.. ��..";

                    askBtn.gameObject.SetActive(false);
                    OrderManager.Instance.isNext = true;

                    //���� �ݾ� 1 /4 ��
                    GameManager.Instance.Money = GameManager.Instance.Money / 4;
                    //���� �մ�
                    OrderManager.Instance.ExitAndComein();
                });
            }
        });
    }

    void NotBloom(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        Button cookBtn = cook.transform.parent.GetComponent<Button>();
        Button askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "�� ����!";

        cook.text = "��?";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��?";

            OrderManager.Instance.OrderTalk[1] = "���� ���ؾ� �˾�?";
            OrderManager.Instance.isNext = true;

            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            //���� �ݾ� 1 /3 ��
            GameManager.Instance.Money = GameManager.Instance.Money / 3;
            //���� �մ�
            OrderManager.Instance.ExitAndComein();
        });
        ask.text = "��..��ø���..";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��..��ø���..";

            OrderManager.Instance.OrderTalk[1] = "ŪŪ.. ���� ����";
            OrderManager.Instance.isNext = true;

            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            //���� �ݾ� 1 /5 ��
            GameManager.Instance.Money = GameManager.Instance.Money / 5;
            //���� �մ�
            OrderManager.Instance.ExitAndComein();
        });
        OrderManager.Instance.OrderTalk[2] = "";
    }
}
