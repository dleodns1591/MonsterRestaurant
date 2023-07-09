using HS_Tree;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Thief : MonoBehaviour, I_CustomerType
{
    Button cookBtn;
    Button askBtn;
    int rand;
    public string SpecialAnswer()
    {
        if (OrderManager.Instance.isCookingSuccess)
        {
            return "��... ���� ���п� �� �� �˾ƶ�..";
        }
        else
        {
            GameManager.Instance.Money -= 10;
            GameManager.Instance.SalesRevenue -= 10;
            return "����� ���� ������.. ��...";
        }
    }

    public void SpecialType(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.StopOrderCoroutine();
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();
        rand = UnityEngine.Random.Range(0, OrderManager.Instance.OrderTalkTxt.text.Split('\n').Length);

        if (GameManager.Instance.shop.isFinalEvolution == false)
        {
            NotBloom(cook, ask);
        }
        else
        {
            Bloom(cook, ask);
        }
    }

    void RefuseOrder()
    {
        StartCoroutine(RefuseOrderDelay());
        IEnumerator RefuseOrderDelay()
        {
            OrderManager.Instance.StopOrderCoroutine();
            OrderManager.Instance.isNext = true;
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(OrderManager.Instance.ExitAndComein(true));
        }

    }
    void SucsessCook()
    {
        cookBtn.gameObject.SetActive(false);
        askBtn.gameObject.SetActive(false);
        
        //�丮
        GameManager.Instance.ReturnCook();
            GameManager.Instance.ConditionSetting(GameManager.Instance.orderSets[rand].main, GameManager.Instance.orderSets[rand].sub, GameManager.Instance.orderSets[rand].count, GameManager.Instance.orderSets[rand].style, GameManager.Instance.orderSets[rand].dishCount);
    }

    void Bloom(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.OrderTalk[0] = OrderManager.Instance.RandomOrderSpeech(0)[rand];

        OrderManager.Instance.dialogNumber++;

        cook.text = "�˰ڽ��ϴ�";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "�˰ڽ��ϴ�";
            OrderManager.Instance.dialogNumber++;

            //�丮
            SucsessCook();
        });
        ask.text = "��?";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��?";

            OrderManager.Instance.OrderTalk[1] = "��.. �� ���� ���˾� ���? �ʵ� ���� ������?";
            OrderManager.Instance.isNext = true;

            if (GameManager.Instance.shop.isFinalEvolution == false)
            {
                cookBtn.gameObject.SetActive(false);
                cookBtn.onClick.RemoveAllListeners();
            }
            else
            {
                cook.text = "���� �Ĺ��� �����ش�.";
                cookBtn.onClick.RemoveAllListeners();
                cookBtn.onClick.AddListener(() =>
                {
                    OrderManager.Instance.AskTalk[1] = "���� �Ĺ��� �����ش�.";
                    OrderManager.Instance.OrderTalk[2] = "��..���� ���� ��..����������..";

                    RefuseOrder();
                });
            }

            ask.text = "�˼��մϴ�.";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "�˼��մϴ�.";
                OrderManager.Instance.OrderTalk[2] = "���� �׷� ������.. ��..";

                GameManager.Instance.Money = GameManager.Instance.Money / 4;
                RefuseOrder();
            });
        });
    }

    void NotBloom(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        Button cookBtn = cook.transform.parent.GetComponent<Button>();
        Button askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "�� ����!";

        cook.text = "��?";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            print("asd");
            OrderManager.Instance.AskTalk[0] = "��?";

            OrderManager.Instance.OrderTalk[1] = "���� ���ؾ� �˾�?";
            GameManager.Instance.Money = GameManager.Instance.Money / 3;
            RefuseOrder();
        });
        ask.text = "��..��ø���..";

        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��..��ø���..";

            OrderManager.Instance.OrderTalk[1] = "ŪŪ.. ���� ����";
            GameManager.Instance.Money = GameManager.Instance.Money / 5;
            RefuseOrder();
        });
    }
}
