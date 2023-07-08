using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GroupOrder : MonoBehaviour, I_CustomerType
{
    Button cookBtn;
    Button askBtn;
    int rand;
    public string SpecialAnswer()
    {
        if (GameManager.Instance.Satisfaction >= 55)
        {
            OrderManager.Instance.EeventCustomerSetting((int)EeventCustomerType.GroupOrder);
            OrderManager.Instance.CustomerImg.sprite = OrderManager.Instance.EventGuestSuccess[(int)EeventCustomerType.GroupOrder];
            GameManager.Instance.Money += 700;
            GameManager.Instance.SalesRevenue += 700;
            return "������ ������ּż� �����մϴ�. ���̵��� ������ �ſ���";
        }
        else
        {
            OrderManager.Instance.EeventCustomerSetting((int)EeventCustomerType.GroupOrder);
            OrderManager.Instance.CustomerImg.sprite = OrderManager.Instance.EventGuestFails[(int)EeventCustomerType.GroupOrder];
            GameManager.Instance.Money -= 20;
            GameManager.Instance.SalesRevenue -= 20;
            return "�ʰ� �ֽø� ��ؿ�..! ���� �޽� ����� �� ó���ؼ� ����������, ���� ���帮�ڳ׿�";
        }
    }

    string eMain(EMainMatarials cell)
    {
        switch (cell)
        {
            case EMainMatarials.Noodle:
                return "�Ľ�Ÿ";
            case EMainMatarials.Rice:
                return "��";
            case EMainMatarials.Bread:
                return "�Ļ�";
            case EMainMatarials.Meat:
                return "���";
            default:
                return "";
        }
    }
    void SucsessCook()
    {
        cookBtn.gameObject.SetActive(false);
        askBtn.gameObject.SetActive(false);

        StartCoroutine(Delay());
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(1.5f);
            askBtn.gameObject.SetActive(false);
            askBtn.GetComponent<Image>().enabled = true;
        }

        //�丮
        GameManager.Instance.ReturnCook();

        List<ESubMatarials> subs = new List<ESubMatarials>
        {
            ESubMatarials.NULL
        };
        GameManager.Instance.ConditionSetting((EMainMatarials)rand, subs, 0, ECookingStyle.Roast, 5);
    }

    public void SpecialType(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        GameManager.Instance.isGroupOrder = true;
        rand = UnityEngine.Random.Range(0, 3);

        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();
        
        OrderManager.Instance.OrderTalk[0] = $"�ȳ��ϼ���. �� �ǹ����� ����� ����� ���ϰ� �ֽ��ϴ�. ���� �޽� ��п� ������ ���ܼ� �׷��µ� 30�� �ȿ� 5���� ���� {eMain((EMainMatarials)rand)}�� ����� �ֽ� �� �ֳ��� ?";
        OrderManager.Instance.dialogNumber++;

        cook.text = "�˰ڽ��ϴ�";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "�˰ڽ��ϴ�";
            OrderManager.Instance.dialogNumber++;

            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);

            //�丮
            SucsessCook();
        });


        askBtn.onClick.RemoveAllListeners();
        ask.text = "";
        askBtn.GetComponent<Image>().enabled = false;
    }
}
