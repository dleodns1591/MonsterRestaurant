using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OrderManager : Singleton<OrderManager>
{
    [Header("Button Related")]
    [SerializeField] private UIText BtnCookText, BtnAskText;
    private Button CookingBtn => BtnCookText.transform.parent.GetComponent<Button>();
    private Button ReAskBtn => BtnAskText.transform.parent.GetComponent<Button>();
    [Header("정리 필요")]
    [SerializeField] private Image TimeFill, CustomerImg;
    [SerializeField] private UIText OrderText;
    private Image SpeechBallon => OrderText.transform.parent.GetComponent<Image>();
    [SerializeField] private RandomText RT;
    [SerializeField] private Sprite[] GuestDefualts, EventGuestDefualts;
    [SerializeField] private Transform[] SlowMovingPos, OrderPos;
    [SerializeField] private GameObject Guest;
    [SerializeField] private Customer customer;

    [SerializeField] private RectTransform MemoPaper;
    [SerializeField] private UIText[] MemoTexts;
    [SerializeField] private Image MemoPaperBackground;
    public GameObject CookingScene;

    private readonly Vector2[] MemoOnTextSizes = { new Vector2(-72.51f, 80.92996f), new Vector2(-3, 6.999878f), new Vector2(-72.51f, -64.00003f), new Vector2(-3, -138), new Vector2(-72.51f, -204) };

    private Tween TextTween, DayTween;
    private I_CustomerType CustomerType;
    [HideInInspector] public Text MoneyText;
    [HideInInspector] public string[] OrderTalk = new string[3], AskTalk = new string[3];
    [HideInInspector] public bool isNext;
    [HideInInspector] public bool isBloom;
    [HideInInspector] public bool isHoldingFlower;
    [HideInInspector] public int SuccessPoint;
    [HideInInspector] public int dialogNumber;

    private void Start()
    {
        OrderLoop();
    }

    void SetCustomerType(int type)
    {
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
            case EcustomerType.Chris:
                CustomerImg.sprite = GuestDefualts[type];
                break;
            case EcustomerType.Demon:
                CustomerImg.sprite = GuestDefualts[type];
                break;
            case EcustomerType.Holotle:
                CustomerImg.sprite = GuestDefualts[type];
                break;
            default:
                int randomType = 6;
                randomType = 6;
                switch ((EeventCustomerType)randomType)
                {
                    case EeventCustomerType.Human:
                        break;
                    case EeventCustomerType.Thief:
                        CustomerType = gameObject.AddComponent<Thief>();
                        CustomerImg.sprite = EventGuestDefualts[randomType];
                        break;
                    case EeventCustomerType.Beggar:
                        CustomerType = gameObject.AddComponent<Beggar>();
                        CustomerImg.sprite = EventGuestDefualts[randomType];
                        break;
                    case EeventCustomerType.Rich:
                        break;
                    case EeventCustomerType.GroupOrder:
                        break;
                    case EeventCustomerType.SalesMan:
                        break;
                    case EeventCustomerType.FoodCleanTester:
                        CustomerType = gameObject.AddComponent<FoodCleanTester>();
                        CustomerImg.sprite = EventGuestDefualts[randomType];
                        break;
                }
                break;
        }
        CustomerType.SpecialType(BtnCookText, BtnAskText);
    }

    /// <summary>
    /// 손님을 받는 이벤트? 들이 시작하는 함수
    /// </summary>
    void OrderLoop()
    {
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

        dialogNumber = 0;

        for (int i = 0; i < MemoTexts.Length; i++)
        {
            MemoTexts[i].text = "";
        }
        OrderText.text = "";
        BtnCookText.text = "";
        BtnAskText.text = "";
    }


    private IEnumerator Order()
    {
        NextCustomerReady();
        SetCustomerType(UnityEngine.Random.Range(9, 9));
        yield return StartCoroutine(customer.Moving());
        ReAskBtn.gameObject.SetActive(true);
        CookingBtn.gameObject.SetActive(true);
        SpeechBallon.gameObject.SetActive(true);

        for (int i = 0; i < OrderTalk.Length; i++)
        {
            if (OrderTalk[i].Equals(""))
            {
                continue;
            }
            if (TextTween != null)
                TextTween.Kill();
            OrderText.text = "";
            TextTween = OrderText.DOText(OrderTalk[i], 0.05f * OrderTalk[i].Length);

            while (!isNext)
            {
                yield return null;
            }
            isNext = false;
        }
    }

    public void MemoOn()
    {
        MemoPaper.gameObject.SetActive(true);
        MemoPaperBackground.gameObject.SetActive(true);
        MemoPaperBackground.DOFade(163 / 255f, 0.5f);
        MemoPaper.DOSizeDelta(new Vector2(650, 549), 0.3f).SetEase(Ease.OutQuint);
        MemoPaper.DOAnchorPos(new Vector2(-242.47f, 0), 0.2f).SetEase(Ease.OutQuint).OnComplete(OnMemoTexts);
        void OnMemoTexts()
        {
            for (int i = 0; i < dialogNumber; i++)
            {
                print(dialogNumber);
                MemoTexts[i].gameObject.SetActive(true);
            }
        }

        int OrderCheck = 0;
        int AskCheck = 0;
        for (int i = 0; i < dialogNumber; i++)
        {
            if (i % 2 != 0)
            {
                MemoTexts[i].text = AskTalk[AskCheck];
                AskCheck++;
            }
            else
            {
                MemoTexts[i].text = OrderTalk[OrderCheck];
                OrderCheck++;
            }
            MemoTexts[i].rectTransform.DOAnchorPos(MemoOnTextSizes[i], 0.3f).SetEase(Ease.OutQuint);
        }
    }
    public void MemoOff()
    {
        for (int i = 0; i < MemoTexts.Length; i++)
        {
            MemoTexts[i].gameObject.SetActive(false);
        }
        StartCoroutine(MemoTextOff());
        MemoPaper.DOSizeDelta(new Vector2(150, 120), 0.3f);
        MemoPaper.DOAnchorPos(new Vector2(-492.47f, 0), 0.3f);

        MemoPaperBackground.DOColor(new Color(0, 0, 0, 0), 0.3f);

        IEnumerator MemoTextOff()
        {
            yield return new WaitForSeconds(0.3f);
            MemoPaper.gameObject.SetActive(false);
            MemoPaperBackground.gameObject.SetActive(false);
        }
    }
}
