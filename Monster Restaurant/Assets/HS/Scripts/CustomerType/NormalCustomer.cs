using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    FoodCleanTester,

    NULL
}
public class NormalCustomer : MonoBehaviour, I_CustomerType
{
    OrderManager OM;
    GameManager GM;
    OrderButtonObject BtnObjects;
    Button cookBtn;
    Button askBtn;
    TextMeshProUGUI cook, ask;

    int ReaskCount;

    public string SpecialAnswer()
    {
        return "";
    }

    public void SpecialType()
    {
        ReaskCount = 0;

        BtnObjects = OrderButtonObject.Instance;
        OM = OrderManager.Instance;
        GM = GameManager.Instance;

        cookBtn = BtnObjects.CookingBtn;
        askBtn = BtnObjects.ReAskBtn;
        cook = BtnObjects.BtnCookText;
        ask = BtnObjects.BtnAskText;

        OM.StopOrderCoroutine();

        OM.dialogNumber++;

        CookBtnChange();
        ask.text = "네?";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "네?";
            ReAskContent();

            CookBtnChange();
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OM.AskTalk[1] = "네?";
                ReAskContent();

                askBtn.gameObject.SetActive(false);

                CookBtnChange();
            });
        });
    }

    void CookBtnChange()
    {
        cook.text = "알겠습니다";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[ReaskCount] = "알겠습니다";
            OM.dialogNumber++;

            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);

            GM.ReturnCook();
        });
    }
    
    void ReAskContent()
    {
        ReaskCount++;
        OM.dialogNumber++;
        OM.ReQuestionCount++;
        OM.isNext = true;
        OM.dialogNumber++;
        GM.Satisfaction -= 20;
    }
}
