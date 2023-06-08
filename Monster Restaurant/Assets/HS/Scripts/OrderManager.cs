using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using static Unity.Burst.Intrinsics.Arm;
using UnityEditor.Localization.Plugins.XLIFF.V20;

public class OrderManager : Singleton<OrderManager>
{
    public TextAsset txt;

    [Header("Button Related")]
    [SerializeField] private UIText BtnCookText, BtnAskText;
    private Button CookingBtn => BtnCookText.transform.parent.GetComponent<Button>();
    private Button ReAskBtn => BtnAskText.transform.parent.GetComponent<Button>();
    [Header("정리 필요")]
    [SerializeField] private Image TimeFill, CustomerImg;
    [SerializeField] private UIText OrderText;
    private Image SpeechBallon => OrderText.transform.parent.GetComponent<Image>();
    [SerializeField] private GameObject NameBallon;
    private Text NameBallonText => NameBallon.transform.GetComponentInChildren<Text>();
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
    private int randomCustomerNum;
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
        randomCustomerNum = UnityEngine.Random.Range(0, txt.text.Split('\n').Length);
        string order = RandomOrder()[randomCustomerNum];
        OrderTalk[0] = order;
        OrderTalk[1] = order;
        OrderTalk[2] = order;

        CustomerType = gameObject.AddComponent<NormalCustomer>();


        string NameKoreanReturn(string name)
        {
            switch (name)
            {
                case "Alien":
                    return "퀘이사";
                    break;
                case "Hyena":
                    return "제토";
                    break;
                case "Robot":
                    return "sdh210224";
                    break;
                case "Dragon":
                    return "시금치";
                    break;
                case "Light":
                    return "2차 양지화";
                    break;
                case "FSM":
                    return "날스괴";
                    break;  
                case "Chris":
                    return "유령 크리스";
                    break;
                case "Demon":
                    return "헬리오스";
                    break;
                case "Holotle":
                    return "아홀로노트";
                    break;
                case "Thief":
                    return "도주";
                    break;
                case "Beggar":
                    return "양말 아저씨";
                    break;
                case "Rich":
                    return "양말 아저씨";
                    break;
                case "GroupOrder":
                    return "플로리안";
                    break;
                case "SalesMan":
                    return "리시드";
                    break;
                case "FoodCleanTester":
                    return "H-30122";
                    break;
                default:
                    return "";
                    break;
            }
        }
        void NormalCustomerSetting()
        {
            CustomerImg.sprite = GuestDefualts[type];
            NameBallonText.text = NameKoreanReturn(Enum.GetName(typeof(EcustomerType), type));
        }
        void EeventCustomerSetting(int randomType)
        {
            CustomerImg.sprite = EventGuestDefualts[randomType];
            NameBallonText.text = NameKoreanReturn(Enum.GetName(typeof(EeventCustomerType), randomType));
        }

        switch ((EcustomerType)type)
        {
            case EcustomerType.Alien:
                NormalCustomerSetting();
                break;
            case EcustomerType.Hyena:
                NormalCustomerSetting();
                break;
            case EcustomerType.Robot:
                NormalCustomerSetting();
                break;
            case EcustomerType.Dragon:
                NormalCustomerSetting();
                break;
            case EcustomerType.Light:
                NormalCustomerSetting();
                break;
            case EcustomerType.FSM:
                NormalCustomerSetting();
                break;
            case EcustomerType.Chris:
                NormalCustomerSetting();
                break;
            case EcustomerType.Demon:
                NormalCustomerSetting();
                break;
            case EcustomerType.Holotle:
                NormalCustomerSetting();
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
                        EeventCustomerSetting(randomType);
                        break;
                    case EeventCustomerType.Beggar:
                        CustomerType = gameObject.AddComponent<Beggar>();
                        EeventCustomerSetting(randomType);
                        break;
                    case EeventCustomerType.Rich:
                        EeventCustomerSetting(randomType);
                        break;
                    case EeventCustomerType.GroupOrder:
                        EeventCustomerSetting(randomType);
                        break;
                    case EeventCustomerType.SalesMan:
                        EeventCustomerSetting(randomType);
                        break;
                    case EeventCustomerType.FoodCleanTester:
                        CustomerType = gameObject.AddComponent<FoodCleanTester>();
                        EeventCustomerSetting(randomType);
                        break;
                }
                break;
        }
        CustomerType.SpecialType(BtnCookText, BtnAskText);
    }
    string[] RandomOrder()
    {
        print(txt.text);
        string[] line = txt.text.Split('\n');
        string[] Sentence = new string[line.Length];

        //엔터로 나눔
        for (int i = 1; i < line.Length; i++)
        {
            string[] cell = line[i].Split('\t');

            Sentence[i] = cell[5];
        }
        return Sentence;
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

    /// <summary>
    /// 손님 이름 반환하는 함수
    /// </summary>
    /// <returns></returns>
    string CustomerName(int type)
    {
        switch (type)
        {
            
        }
        return "";
    }


    IEnumerator Order()
    {
        NextCustomerReady();
        SetCustomerType(UnityEngine.Random.Range(0, 8));
        yield return StartCoroutine(customer.Moving());
        ReAskBtn.gameObject.SetActive(true);
        CookingBtn.gameObject.SetActive(true);
        SpeechBallon.gameObject.SetActive(true);
        NameBallon.gameObject.SetActive(true);

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

    #region 메모 관련
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
    #endregion

    #region 게임 루틴 관련

    public void NextJuMun()
    {

    }
    #endregion

}
