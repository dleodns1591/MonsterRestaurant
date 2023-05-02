using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OrderManager : MonoBehaviour
{
    [Header("Button Related")]
    [SerializeField] private UIText BtnCookText, BtnAskText;
    private Button CookingBtn => BtnCookText.transform.GetComponentInParent<Button>();
    private Button ReAskBtn => BtnAskText.transform.GetComponentInParent<Button>();
    [Header("정리 필요")]
    [SerializeField] private Image TimeFill, CustomerImg;
    [SerializeField] private Text OrderText;
    [SerializeField] private RandomText RT;
    [SerializeField] private Sprite[] GuestDefualts;
    private Tween TextTween, DayTween;
    private I_CustomerType CustomerType;

    public static Text MoneyText;
    public static string[] OrderTalk = new string[3], AskTalk;
    public static bool isNext;
    public static bool isBloom;
    public static bool isHoldingFlower;
    public static int SuccessPoint;

    void SetCustomerType(int type)
    {
        NextCustomerReady();

        OrderTalk[0] = "메인 재료 " + RT.FirstTexts[UnityEngine.Random.Range(0, 20)] + " 부재료 얼만큼 " + RT.MiddleTexts[UnityEngine.Random.Range(0, 20)] + " 조리방법 " + RT.LastTexts[UnityEngine.Random.Range(0, 20)];
        OrderTalk[1] = "메인 재료 부재료 얼만큼 " + RT.FirstTexts[UnityEngine.Random.Range(0, 20)] + " 조리방법 " + RT.LastTexts[UnityEngine.Random.Range(0, 20)];
        OrderTalk[2] = "!메인 재료!로 !부재료! !얼만큼! 넣어서 !조리방법! 해주세요 '^'..";

        CustomerType = gameObject.AddComponent<NormalCustomer>();

        switch ((EcustomerType)type)
        {
            case EcustomerType.Alien:
                CustomerImg.sprite = GuestDefualts[type];
                break;
            case EcustomerType.Hyena:
                CustomerImg.sprite = GuestDefualts[type];
                break;
            case EcustomerType.Robot:
                CustomerImg.sprite = GuestDefualts[type];
                break;
            case EcustomerType.Dragon:
                CustomerImg.sprite = GuestDefualts[type];
                break;
            case EcustomerType.Light:
                CustomerImg.sprite = GuestDefualts[type];
                break;
            case EcustomerType.FSM:
                CustomerImg.sprite = GuestDefualts[type];
                break;
            default:
                int randomType = UnityEngine.Random.Range(0, Enum.GetValues(typeof(EeventCustomerType)).Length);
                switch ((EeventCustomerType)randomType)
                {
                    case EeventCustomerType.Human:
                        break;
                    case EeventCustomerType.Thief:
                        CustomerType = gameObject.AddComponent<Thief>();
                        break;
                    case EeventCustomerType.Beggar:
                        break;
                    case EeventCustomerType.Rich:
                        break;
                    case EeventCustomerType.GroupOrder:
                        break;
                    case EeventCustomerType.SalesMan:
                        break;
                    case EeventCustomerType.FoodCleanTester:
                        break;
                }
                CustomerType.SpecialType(BtnCookText, BtnAskText);
                break;
        }
    }

    /// <summary>
    /// 손님을 받는 이벤트? 들이 시작하는 함수
    /// </summary>
    void OrderLoop()
    {
        SetCustomerType(UnityEngine.Random.Range(0, Enum.GetValues(typeof(EcustomerType)).Length + 1));
        StartCoroutine(Order());

        if (DayTween != null)
            DayTween.Kill();
        TimeFill.fillAmount = 1;

        DayTween = DOTween.To(() => TimeFill.fillAmount, x => TimeFill.fillAmount = x, 0, 100)
        .OnComplete(() => //시간이 다 지났을때
        {
            //손님 화내면서 나가기

            OrderLoop();
        });
    }

    void NextCustomerReady()
    {
        Array.Clear(OrderTalk, 0, OrderTalk.Length);
        Array.Clear(AskTalk, 0, AskTalk.Length);

        OrderText.text = "";
        BtnCookText.text = "";
        BtnAskText.text = "";
        CookingBtn.gameObject.SetActive(true);
        ReAskBtn.gameObject.SetActive(true);

    }


    public IEnumerator Order()
    {
        for (int i = 0; i < OrderTalk.Length; i++)
        {
            if (OrderTalk[i].Equals(""))
            {
                continue;
            }

            while (!isNext)
            {
                yield return null;
            }
            TextTween.Kill();
            TextTween = OrderText.DOText(OrderTalk[i], 0.05f * OrderTalk[i].Length);
        }
    }
}
