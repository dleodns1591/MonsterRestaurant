using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    [SerializeField] private Text MonetText;
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
    [HideInInspector] public string[] OrderTalk = new string[3], AskTalk = new string[3];
    [HideInInspector] public bool isNext;
    [HideInInspector] public bool isBloom;
    [HideInInspector] public bool isHoldingFlower;
    [HideInInspector] public int SuccessPoint;
    [HideInInspector] public int dialogNumber;

    private void Start()
    {
        RandomOrderMaterial();
        OrderLoop();
        OrderToCook();
        CookToOrder();
    }
    private void Update()
    {
        MonetText.text = GameManager.Instance.Money.ToString();
    }
    void SetCustomerType(int type)
    {
        randomCustomerNum = UnityEngine.Random.Range(0, txt.text.Split('\n').Length);
        string order = RandomOrderSpeech()[randomCustomerNum];
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
                case "Hyena":
                    return "제토";
                case "Robot":
                    return "sdh210224";
                case "Dragon":
                    return "시금치";
                case "Light":
                    return "2차 양지화";
                case "FSM":
                    return "날스괴";
                case "Chris":
                    return "유령 크리스";
                case "Demon":
                    return "헬리오스";
                case "Holotle":
                    return "아홀로노트";
                case "Thief":
                    return "도주";
                case "Beggar":
                    return "양말 아저씨";
                case "Rich":
                    return "양말 아저씨";
                case "GroupOrder":
                    return "플로리안";
                case "SalesMan":
                    return "리시드";
                case "FoodCleanTester":
                    return "H-30122";
                default:
                    return "";
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
    string[] RandomOrderSpeech()
    {
        string[] line = txt.text.Split('\n');
        string[] Sentence = new string[line.Length];

        for (int i = 1; i < line.Length; i++)
        {
            string[] cell = line[i].Split('\t');

            Sentence[i] = cell[5];
        }
        return Sentence;
    }

    string[] RandomOrderMaterial()
    {
        EMainMatarials eMain(string cell)
        {
            switch (cell)
            {
                case "면":
                    return EMainMatarials.Noodle;
                case "밥":
                    return EMainMatarials.Rice;
                case "빵":
                    return EMainMatarials.Bread;
                case "고기":
                    return EMainMatarials.Meat;
                default:
                    return EMainMatarials.Bread;
            }
        }
        ESubMatarials eSub(string cell)
        {
            switch (cell)
            {
                case "스티커":
                    return ESubMatarials.Sticker;
                case "똥":
                    return ESubMatarials.Poop;
                case "너트":
                    return ESubMatarials.Bolt;
                case "방부제":
                    return ESubMatarials.Preservatives;
                case "종이":
                    return ESubMatarials.Paper;
                case "돈":
                    return ESubMatarials.Money;
                case "보석":
                    return ESubMatarials.Jewelry;
                case "눈알":
                    return ESubMatarials.Eyes;
                case "건전지":
                    return ESubMatarials.Battery;
                case "털 뭉치":
                    return ESubMatarials.Fur;
                case "비스무트":
                    return ESubMatarials.Bismuth;
                case "외계 풀":
                    return ESubMatarials.AlienPlant;
                default:
                    return ESubMatarials.Battery;

            }
        }

        string[] line = txt.text.Split('\n');
        string[] Sentence = new string[line.Length];

        GameManager.Instance.orderSets = new OrderSet[line.Length];
        for (int i = 1; i < line.Length; i++)
        {
            string[] cell = line[i].Split('\t');

           GameManager.Instance.orderSets[i].main = eMain(cell[0]);
           GameManager.Instance.orderSets[i].sub = eSub(cell[1]);
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

    public void AAA()
    {
        GameManager.Instance.ReturnOreder();
    }

    public void CookToOrder()
    {
        GameManager.Instance.ReturnOreder = () =>
        {
            CookingScene.transform.DOMoveY(-10, 1).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                string EMSEE = "베리베리 나이스베리 이대운 바보 ㅋㅋ";
                OrderText.DOText(EMSEE, 0.05f * EMSEE.Length).OnComplete(() =>
                {
                    StartCoroutine(ASD());
                });
            });
            OrderText.text = "";
        };

        IEnumerator ASD()
        {
            yield return new WaitForSeconds(1.5f);

            SpeechBallon.gameObject.SetActive(false);
            NameBallon.gameObject.SetActive(false);
            //다시 시작

            customer.Exit();

            yield return new WaitForSeconds(1f);
            StartCoroutine(Order());
        }
    }
    public void OrderToCook()
    {
        GameManager.Instance.ReturnCook = () =>
        {
            CookingScene.transform.DOMoveY(0, 1).SetEase(Ease.OutBounce);
        };
    }

    #endregion

}
