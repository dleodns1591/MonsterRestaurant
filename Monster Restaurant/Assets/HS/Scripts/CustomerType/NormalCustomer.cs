using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum EcustomerType
{
    Alien,
    Hyena,
    Robot,
    Dragon,
    Light,
    FSM
}
public enum EeventCustomerType
{
    Human,
    Thief,

    Beggar,
    Rich,

    GroupOrder,
    SalesMan,
    FoodCleanTester
}
public class NormalCustomer : MonoBehaviour, I_CustomerType
{
    public void SpecialType(UIText cook, UIText ask)
    {
        Button cookBtn = cook.transform.parent.GetComponent<Button>();
        Button askBtn = ask.transform.parent.GetComponent<Button>();


        cook.text = "�˰ڽ��ϴ�";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "�˰ڽ��ϴ�";

            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            //�丮
        });
        ask.text = "��?";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "��?";

            OrderManager.isNext = true;

            //�ð� ���
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OrderManager.AskTalk[1] = "�˰ڽ��ϴ�";

                cookBtn.gameObject.SetActive(false);
                askBtn.gameObject.SetActive(false);
                //�丮
            });
                askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OrderManager.AskTalk[1] = "��?";

                OrderManager.isNext = true;

                askBtn.gameObject.SetActive(false);

                cookBtn.onClick.RemoveAllListeners();
                cookBtn.onClick.AddListener(() =>
                {
                    OrderManager.AskTalk[2] = "�˰ڽ��ϴ�";

                    cookBtn.gameObject.SetActive(false);
                    askBtn.gameObject.SetActive(false);
                    //�丮
                });
            });
        });
    }
}
