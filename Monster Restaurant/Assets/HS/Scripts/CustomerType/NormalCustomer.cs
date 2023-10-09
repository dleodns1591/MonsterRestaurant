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
    Holotle,
    Trash,
    Joker
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
    SaveManager SM;
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
        SM = SaveManager.Instance;
        cookBtn = BtnObjects.CookingBtn;
        askBtn = BtnObjects.ReAskBtn;
        cook = BtnObjects.BtnCookText;
        ask = BtnObjects.BtnAskText;

        OM.StopOrderCoroutine();

        OM.dialogNumber++;

        if (SM.isEnglish == false)
            KR();
        else
            Eng();
    }

    void KR()
    {
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

    void Eng()
    {
        CookBtnChangeEng();
        ask.text = "Pardon?";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "Pardon?";
            ReAskContent();

            CookBtnChangeEng();
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OM.AskTalk[1] = "Pardon?";
                ReAskContent();

                askBtn.gameObject.SetActive(false);

                CookBtnChangeEng();
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
    void CookBtnChangeEng()
    {
        cook.text = "Wait a Minute";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[ReaskCount] = "Wait a Minute";
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
