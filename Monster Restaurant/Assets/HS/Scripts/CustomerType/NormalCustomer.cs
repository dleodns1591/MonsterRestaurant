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


        cook.text = "알겠습니다";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "알겠습니다";

            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            //요리
        });
        ask.text = "네?";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "네?";

            OrderManager.isNext = true;

            //시간 깎기
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OrderManager.AskTalk[1] = "알겠습니다";

                cookBtn.gameObject.SetActive(false);
                askBtn.gameObject.SetActive(false);
                //요리
            });
                askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OrderManager.AskTalk[1] = "네?";

                OrderManager.isNext = true;

                askBtn.gameObject.SetActive(false);

                cookBtn.onClick.RemoveAllListeners();
                cookBtn.onClick.AddListener(() =>
                {
                    OrderManager.AskTalk[2] = "알겠습니다";

                    cookBtn.gameObject.SetActive(false);
                    askBtn.gameObject.SetActive(false);
                    //요리
                });
            });
        });
    }
}
