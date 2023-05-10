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


        cook.text = "알겠습니다";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "알겠습니다";

            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);

            //요리
            OrderManager.Instance.CookingScene.GetComponent<RectTransform>().DOAnchorPos3DY(0, 1).SetEase(Ease.OutBounce);
        });
        ask.text = "네?";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "네?";

            OrderManager.Instance.isNext = true;

            //시간 깎기
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "알겠습니다";

                cookBtn.gameObject.SetActive(false);
                askBtn.gameObject.SetActive(false);
                //요리
                OrderManager.Instance.CookingScene.GetComponent<RectTransform>().DOAnchorPos3DY(0, 1).SetEase(Ease.OutBounce);
            });
                askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "네?";

                OrderManager.Instance.isNext = true;

                askBtn.gameObject.SetActive(false);

                cookBtn.onClick.RemoveAllListeners();
                cookBtn.onClick.AddListener(() =>
                {
                    OrderManager.Instance.AskTalk[2] = "알겠습니다";

                    cookBtn.gameObject.SetActive(false);
                    askBtn.gameObject.SetActive(false);
                    //요리
                    OrderManager.Instance.CookingScene.GetComponent<RectTransform>().DOAnchorPos3DY(0, 1).SetEase(Ease.OutBounce);
                });
            });
        });
    }
}
