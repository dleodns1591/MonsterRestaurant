using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomerManagerOnlineVer : MonoBehaviour
{
    OrderManager OM;
    GameManager GM;
    Customer customer;


    private void Awake()
    {
        OM = OrderManager.Instance;
        GM = GameManager.Instance;
        customer = OM.customer;
    }

    public void EndBtnsSetActive(bool active)
    {
        if (SaveManager.Instance.isEnglish == false)
            OrderButtonObject.Instance.BtnCookText.text = "����ϱ�";
        else
            OrderButtonObject.Instance.BtnCookText.text = "Continue";

        OrderButtonObject.Instance.CookingBtn.onClick.RemoveAllListeners();
        OrderButtonObject.Instance.CookingBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("OnlinePvp");
        });

        if (SaveManager.Instance.isEnglish == false)
            OrderButtonObject.Instance.BtnAskText.text = "���ư���";
        else
            OrderButtonObject.Instance.BtnAskText.text = "Go Back";
        OrderButtonObject.Instance.ReAskBtn.onClick.RemoveAllListeners();
        OrderButtonObject.Instance.ReAskBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Title");
        });

        OM.orderButtonManager.ButtonSetActive(active);
    }

    public void SetCustomerType()
    {
        OM.CustomerType = gameObject.AddComponent<OnlinePvp>();
        EeventCustomerSetting((int)EeventCustomerType.GroupOrder);

        GM.Money = 100000;

        OM.CustomerType.SpecialType();
        return;
    }
    public void EeventCustomerSetting(int type)
    {
        customer.CustomerImg.sprite = customer.EventGuestDefualts[type];
        OM.orderMessageManager.NameBallonSetting(NameKoreanReturn("GroupOrder"));
    }
    string NameKoreanReturn(string name)
    {
        if (SaveManager.Instance.isEnglish == false) return "�÷θ���";
        else return name;
    }
}

