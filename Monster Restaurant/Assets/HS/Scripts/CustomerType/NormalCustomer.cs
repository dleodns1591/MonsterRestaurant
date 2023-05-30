using DG.Tweening;
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
    FSM,
    Chris,
    Demon,
    Holotle
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

        OrderManager.Instance.dialogNumber++;

        cook.text = "�˰ڽ��ϴ�";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "�˰ڽ��ϴ�";
            OrderManager.Instance.dialogNumber++;

            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);

            //�丮
            OrderManager.Instance.CookingScene.transform.DOMoveY(0, 1).SetEase(Ease.OutBounce);
        });
        ask.text = "��?";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "��?";
            OrderManager.Instance.dialogNumber++;

            OrderManager.Instance.isNext = true;
            //�մ��� ��ȭ�ϴϱ�
            OrderManager.Instance.dialogNumber++;

            //�ð� ���
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "�˰ڽ��ϴ�";
                OrderManager.Instance.dialogNumber++;

                cookBtn.gameObject.SetActive(false);
                askBtn.gameObject.SetActive(false);
                //�丮
                OrderManager.Instance.CookingScene.transform.DOMoveY(0, 1).SetEase(Ease.OutBounce);
            });
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "��?";
                OrderManager.Instance.dialogNumber++;

                OrderManager.Instance.isNext = true;
                //�մ��� ��ȭ�ϴϱ�
                OrderManager.Instance.dialogNumber++;

                askBtn.gameObject.SetActive(false);

                cookBtn.onClick.RemoveAllListeners();
                cookBtn.onClick.AddListener(() =>
                {
                    OrderManager.Instance.AskTalk[2] = "�˰ڽ��ϴ�";

                    cookBtn.gameObject.SetActive(false);
                    askBtn.gameObject.SetActive(false);
                    //�丮
                    OrderManager.Instance.CookingScene.transform.DOMoveY(0, 1).SetEase(Ease.OutBounce);
                });
            });
        });
    }
}
