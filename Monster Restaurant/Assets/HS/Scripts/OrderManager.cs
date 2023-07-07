using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

[Serializable]
public struct EndingType
{
    [TextArea] public string Speech;
    public Sprite EndingSpr;
}

public enum Eending
{
    Bankruptcy,
    Earth,
    Rich
}

public class OrderManager : Singleton<OrderManager>
{
    public TextAsset OrderTalkTxt, AnswerTalkTxt;

    [Header("주문 버튼 관련")]
    [SerializeField] private TextMeshProUGUI BtnCookText;
    [SerializeField] private TextMeshProUGUI BtnAskText;
    private Button CookingBtn => BtnCookText.transform.parent.GetComponent<Button>();
    private Button ReAskBtn => BtnAskText.transform.parent.GetComponent<Button>();

    [Header("하루 시간 관련")]
    [SerializeField] private Image TimeFill;

    [Header("손님 관련")]
    [SerializeField] private Image CustomerImg;
    [SerializeField] private Sprite[] GuestDefualts, EventGuestDefualts;
    [SerializeField] private Sprite[] GuestSuccess, EventGuestSuccess;
    [SerializeField] private Sprite[] GuestFails, EventGuestFails;
    [SerializeField] public Customer customer;
    private int normalGuestType;

    [Header("손님의 말풍선 관련")]
    [SerializeField] private UIText OrderText;
    [SerializeField] private GameObject NameBallon;
    private Image SpeechBallon => OrderText.transform.parent.GetComponent<Image>();
    private Text NameBallonText => NameBallon.transform.GetComponentInChildren<Text>();

    [Header("요리 당 만족도 관련")]
    [SerializeField] private Sprite[] FaceSprites;
    [SerializeField] private Image FaceImage;
    [SerializeField] private Text EmotionText;

    [Header("UI 관련")]
    [SerializeField] private Text MoneyText;

    [Header("결과 창 관련")]
    [SerializeField] private GameObject RevenuePopup;
    [SerializeField] private Button NextButton;
    [SerializeField] private Text Principal, BasicRevenue, SalesRevenue, MarterialCost, TaxCost, SettlementCost, Total;
    [SerializeField] private Text DayText;

    [Header("메모 관련")]
    [SerializeField] private RectTransform MemoPaper;
    [SerializeField] private UIText[] MemoTexts;
    [SerializeField] private Image MemoPaperBackground;
    private readonly Vector2[] MemoOnTextSizes = { new Vector2(-72.51f, 80.92996f), new Vector2(-3, 6.999878f), new Vector2(-72.51f, -64.00003f), new Vector2(-3, -138), new Vector2(-72.51f, -204) };

    [Header("기타부타타")]
    public GameObject CookingScene;

    [Header("엔딩 관련")]
    [SerializeField] private Image EndingImg;
    [SerializeField] private Text EndingExplanTxt;
    public EndingType[] endingTypes;
    private GameObject EndingCanvas => EndingImg.transform.parent.gameObject;

    [Header("상점 관련")]
    [SerializeField] private GameObject CounterDesk;
    [SerializeField] private Button StopBuyBtn;
    [SerializeField] private Button[] BuyBtns;
    [SerializeField] private GameObject RightClick, LeftClick;


    [Header("내부 변수들")]
    private Tween TextTween, DayTween;
    private Coroutine SatisfactionCoroutine, Ordercoroutine;
    private int firstMoney;
    private I_CustomerType CustomerType;
    private bool isSatisfactionStop;
    [HideInInspector] public bool isCookingSuccess;
    [HideInInspector] public bool isBeggar;
    [HideInInspector] public int orderType;
    [HideInInspector] public string[] OrderTalk = new string[3], AskTalk = new string[3];
    [HideInInspector] public string AnswerTalk;
    [HideInInspector] public bool isNext;
    [HideInInspector] public bool isBloom;
    [HideInInspector] public bool isHoldingFlower;
    [HideInInspector] public int Beggar_SuccessPoint = 0;
    [HideInInspector] public int Earthling_SuccessPoint = 0;
    [HideInInspector] public int dialogNumber;

    private void Start()
    {
        RandomOrderMaterial();
        OrderLoop();
        ShopProduction();
        OrderToCook();
        CookToOrder();
    }
    private void Update()
    {
        MoneyText.text = ((int)GameManager.Instance.Money).ToString();
    }

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
            case "Human":
                return "스텔라";
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

    EcustomerType NameToEnumReturn(string name)
    {
        switch (name)
        {
            case "퀘이사":
                return EcustomerType.Alien;
            case "제토":
                return EcustomerType.Hyena;
            case "sdh210224":
                return EcustomerType.Robot;
            case "시금치":
                return EcustomerType.Dragon;
            case "2차 양자화":
                return EcustomerType.Light;
            case "날스괴":
                return EcustomerType.FSM;
            case "유령 크리스":
                return EcustomerType.Chris;
            case "헬리오스":
                return EcustomerType.Demon;
            case "아홀로노트":
                return EcustomerType.Holotle;
            default:
                return EcustomerType.Alien;
        }
    }
    void NormalCustomerSetting(int type)
    {
        CustomerImg.sprite = GuestDefualts[type];
        NameBallonText.text = NameKoreanReturn(Enum.GetName(typeof(EcustomerType), type));
    }
    void EeventCustomerSetting(int randomType)
    {
        CustomerImg.sprite = EventGuestDefualts[randomType];
        NameBallonText.text = NameKoreanReturn(Enum.GetName(typeof(EeventCustomerType), randomType));
    }

    void SetCustomerType(int type)
    {
        GameManager.Instance.randomCustomerNum = UnityEngine.Random.Range(0, OrderTalkTxt.text.Split('\n').Length);
        for (int i = 0; i < OrderTalk.Length; i++)
        {
            OrderTalk[i] = RandomOrderSpeech(i)[GameManager.Instance.randomCustomerNum];
        }

        CustomerType = gameObject.AddComponent<NormalCustomer>();


        switch ((EcustomerType)type)
        {
            case EcustomerType.Alien:
                NormalCustomerSetting(type);
                break;
            case EcustomerType.Hyena:
                NormalCustomerSetting(type);
                break;
            case EcustomerType.Robot:
                NormalCustomerSetting(type);
                break;
            case EcustomerType.Dragon:
                NormalCustomerSetting(type);
                break;
            case EcustomerType.Light:
                NormalCustomerSetting(type);
                break;
            case EcustomerType.FSM:
                NormalCustomerSetting(type);
                break;
            case EcustomerType.Chris:
                NormalCustomerSetting(type);
                break;
            case EcustomerType.Demon:
                NormalCustomerSetting(type);
                break;
            case EcustomerType.Holotle:
                NormalCustomerSetting(type);
                break;
            default:
                int randomType = 0;
                GameManager.Instance.SpecialType = 0;
                switch ((EeventCustomerType)randomType)
                {
                    case EeventCustomerType.Human:
                        if (GameManager.Instance.isEarthlingRefuse)
                        {
                            normalGuestType = UnityEngine.Random.Range(0, 8);
                            SetCustomerType(normalGuestType);

                            return;
                        }
                        CustomerType = gameObject.AddComponent<Earthling>();
                        EeventCustomerSetting(randomType);
                        break;
                    case EeventCustomerType.Thief:
                        CustomerType = gameObject.AddComponent<Thief>();
                        EeventCustomerSetting(randomType);
                        break;
                    case EeventCustomerType.Beggar:
                        if (GameManager.Instance.isBeggarRefuse)
                        {
                            normalGuestType = UnityEngine.Random.Range(0, 8);
                            SetCustomerType(normalGuestType);

                            return;
                        }
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
    string[] RandomOrderSpeech(int OrderSequence)
    {
        string[] line = OrderTalkTxt.text.Split('\n');
        string[] Sentence = new string[line.Length];

        for (int i = 0; i < line.Length; i++)
        {
            string[] cell = line[i].Split('\t');

            Sentence[i] = cell[7 + (OrderSequence * 2)];
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

        ECookingStyle eStyle(string cell)
        {
            return ECookingStyle.Fry;
        }

        string[] line = OrderTalkTxt.text.Split('\n');
        string[] Sentence = new string[line.Length];

        GameManager.Instance.orderSets = new OrderSet[line.Length];
        for (int i = 0; i < line.Length; i++)
        {
            string[] cell = line[i].Split('\t');

            GameManager.Instance.orderSets[i].main = eMain(cell[0]);
            GameManager.Instance.orderSets[i].sub = new List<ESubMatarials>() { ESubMatarials.AlienPlant, ESubMatarials.AlienPlant, ESubMatarials.AlienPlant };
            GameManager.Instance.orderSets[i].sub[0] = eSub(cell[1]);
            GameManager.Instance.orderSets[i].sub[1] = eSub(cell[2]);
            GameManager.Instance.orderSets[i].sub[2] = eSub(cell[3]);
            GameManager.Instance.orderSets[i].style = eStyle(cell[4]);
            GameManager.Instance.orderSets[i].count = int.Parse(cell[5]);
            GameManager.Instance.orderSets[i].dishCount = int.Parse(cell[6]);
        }
        return Sentence;
    }
    /// <summary>
    /// 손님을 받는 이벤트? 들이 시작하는 함수
    /// </summary>
    void OrderLoop()
    {
        firstMoney = (int)GameManager.Instance.Money;
        GameManager.Instance.SalesRevenue = 0;
        GameManager.Instance.MarterialCost = 0;
        GameManager.Instance.TaxCost = 0;
        GameManager.Instance.SettlementCost = 0;
        Ordercoroutine = StartCoroutine(Order());

        if (DayTween != null)
            DayTween.Kill();
        TimeFill.fillAmount = 1;

        DayTween = DOTween.To(() => TimeFill.fillAmount, x => TimeFill.fillAmount = x, 0, 180)
        .OnComplete(() => //시간이 다 지났을때
        {
            GameManager.Instance.dayEndCheck = true;
            //손님 화내면서 나가기
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

    /// <summary>
    /// 숫자 오르는 애니메이션
    /// </summary>
    /// <param name="targetNumber">원하는 수치</param>
    /// <param name="animationDuration">원하는 속도</param>
    /// <param name="numberText">적용될 텍스트 UI</param>
    void NumberAnimation(int targetNumber, float animationDuration, Text numberText)
    {
        int currentNumber = 0;
        DOTween.To(() => currentNumber, x => currentNumber = x, targetNumber, animationDuration)
            .SetEase(Ease.Linear)
            .OnUpdate(() => numberText.text = currentNumber.ToString());
    }

    void DayEnd()
    {
        FadeInOut.Instance.LittleFadeOut();
        FadeInOut.Instance.RevenueFadeOut();

        GameManager.Instance.shop.PurchaseDayCheck();
        GameManager.Instance.Money += 200;
        GameManager.Instance.TaxCost = GameManager.Instance.SalesRevenue / 10;
        GameManager.Instance.Money -= GameManager.Instance.TaxCost;
        GameManager.Instance.Money -= GameManager.Instance.SettlementCost;
        StartCoroutine(NumberAni());
        IEnumerator NumberAni()
        {
            yield return new WaitForSeconds(FadeInOut.Instance.fadeTime);
            BasicRevenue.text = "";
            SalesRevenue.text = "";
            MarterialCost.text = "";
            TaxCost.text = "";
            SettlementCost.text = "";
            Total.text = "";
            for (int i = 0; i < RevenuePopup.transform.childCount; i++)
            {
                RevenuePopup.transform.GetChild(i).gameObject.SetActive(true);
            }
            NextButton.gameObject.SetActive(false);

            NumberAnimation(firstMoney, 1.3f, Principal);
            yield return new WaitForSeconds(1.5f);
            NumberAnimation(GameManager.Instance.BasicRevenue, 1.3f, BasicRevenue);
            yield return new WaitForSeconds(1.5f);
            NumberAnimation(GameManager.Instance.SalesRevenue, 1.3f, SalesRevenue);
            yield return new WaitForSeconds(1.5f);
            NumberAnimation((int)GameManager.Instance.MarterialCost, 1.3f, MarterialCost);
            yield return new WaitForSeconds(1.5f);
            NumberAnimation(GameManager.Instance.TaxCost, 1.3f, TaxCost);
            yield return new WaitForSeconds(1.5f);
            NumberAnimation(GameManager.Instance.SettlementCost, 1.3f, SettlementCost);
            yield return new WaitForSeconds(1.5f);
            NumberAnimation((int)GameManager.Instance.Money, 1.3f, Total);
            yield return new WaitForSeconds(2.0f);
            NextButton.gameObject.SetActive(true);
            NextButton.onClick.RemoveAllListeners();
            NextButton.onClick.AddListener(() =>
            {
                NextButton.gameObject.SetActive(false);
                RevenuePopup.GetComponent<RectTransform>().DOAnchorPosY(865, 2.5f).OnComplete(() =>
                {
                    for (int i = 0; i < RevenuePopup.transform.childCount; i++)
                    {
                        RevenuePopup.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    TimeFill.fillAmount = 1;

                    IEnumerator DayProduction()
                    {
                        GameManager.Instance.Day++;
                        DayText.text = $"{GameManager.Instance.Day}일차....!";
                        DayText.DOFade(1, 1);
                        yield return new WaitForSeconds(1.5f);
                        FadeInOut.Instance.LittleFade();
                        DayText.DOFade(0, FadeInOut.Instance.fadeTime);
                        yield return new WaitForSeconds(FadeInOut.Instance.fadeTime);
                    }
                    StartCoroutine(DayProduction());
                    StartCoroutine(Reset());
                });
            });
            IEnumerator Reset()
            {
                yield return new WaitForSeconds(FadeInOut.Instance.fadeTime);
                RevenuePopup.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                RevenuePopup.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                GameManager.Instance.dayEndCheck = false;
                OrderLoop();
            }
        }
    }

    IEnumerator Order()
    {
        if (GameManager.Instance.dayEndCheck)
        {
            DayEnd();
            yield break;
        }
        NextCustomerReady();
        normalGuestType = UnityEngine.Random.Range(0, 8);
        SetCustomerType(9);
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
        MemoPaper.DOAnchorPos(new Vector2(91, 0), 0.2f).SetEase(Ease.OutQuint).OnComplete(OnMemoTexts);
        void OnMemoTexts()
        {
            for (int i = 0; i < dialogNumber; i++)
            {
                MemoTexts[i].gameObject.SetActive(true);
            }
        }
        int OrderCheck = 0;
        int AskCheck = 0;
        for (int i = 0; i < dialogNumber; i++)
        {
            if (i % 2 != 0)
            {
                print("asd");
                MemoTexts[i].text = AskTalk[AskCheck];
                print(AskTalk[AskCheck]);
                AskCheck++;
            }
            else
            {
                MemoTexts[i].text = OrderTalk[OrderCheck];
                print(AskTalk[OrderCheck]);
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
        MemoPaper.DOAnchorPos(new Vector2(-158, 0), 0.3f);

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

    private void ShopProduction()
    {
        GameManager.Instance.ShopAppearProd = () =>
        {
            StartCoroutine(Delay());
            IEnumerator Delay()
            {
                FadeInOut.Instance.Fade();
                GameManager.Instance.shop.ShopOpen();
                yield return new WaitForSeconds(FadeInOut.Instance.fadeTime);
                FadeInOut.Instance.FadeOut();
                yield return new WaitForSeconds(FadeInOut.Instance.fadeTime);
                OrderText.text = "";
                SpeakOrder("마음에 드시는 제품 있으시면 구매해주세요.");
            }

        };
    }

    public void SpeakOrder(string speech)
    {
        OrderText.text = "";
        if (TextTween != null)
            TextTween.Kill();
        TextTween = OrderText.DOText(speech, 0.05f * speech.Length);
    }

    public void CookToOrder()
    {
        GameManager.Instance.ReturnOrder = () =>
        {

            string[] line = AnswerTalkTxt.text.Split('\n');

            string[,] SucsessTalk = new string[Enum.GetValues(typeof(EcustomerType)).Length, 3];
            string[,] FailTalk = new string[Enum.GetValues(typeof(EcustomerType)).Length, 3];


            int sucsessCnt = 0;
            int FailCnt = 0;

            string formerName = "";
            for (int i = 0; i < line.Length; i++)
            {
                string[] cell = line[i].Split('\t');

                if (formerName != cell[0])
                {
                    sucsessCnt = 0;
                    FailCnt = 0;
                }

                formerName = cell[0];
                if (cell[1] == "성공")
                {
                    SucsessTalk[(int)NameToEnumReturn(cell[0]), sucsessCnt] = cell[2];
                    print(sucsessCnt);
                    sucsessCnt++;
                }
                else if (cell[1] == "실패")
                {
                    FailTalk[(int)NameToEnumReturn(cell[0]), FailCnt] = cell[2];
                    FailCnt++;
                }

            }

            if (!isBeggar)
            {

                if (GameManager.Instance.Satisfaction >= 40)
                    isCookingSuccess = true;
                else
                    isCookingSuccess = false;

                //if 성공 실패
                if (isCookingSuccess)
                {
                    GameManager.Instance.Money += 50;
                    GameManager.Instance.SalesRevenue += 50;
                    CustomerImg.sprite = GuestSuccess[normalGuestType];
                    AnswerTalk = SucsessTalk[normalGuestType, UnityEngine.Random.Range(0, 2)];
                }
                else
                {
                    GameManager.Instance.Money += 20;
                    GameManager.Instance.SalesRevenue += 20;
                    int rand = UnityEngine.Random.Range(1, 5);
                    if (rand == 1)
                        GameManager.Instance.SettlementCost += 100;
                    CustomerImg.sprite = GuestFails[normalGuestType];
                    AnswerTalk = FailTalk[normalGuestType, UnityEngine.Random.Range(0, 2)];
                }
            }
            else
            {
                CustomerImg.sprite = EventGuestSuccess[(int)EeventCustomerType.Beggar];
            }

            isBeggar = false;
            if (!CustomerType.SpecialAnswer().Equals(""))
            {
                EeventCustomerSetting(GameManager.Instance.SpecialType);
                AnswerTalk = CustomerType.SpecialAnswer();
            }

            print(AnswerTalk);
            CookingScene.transform.DOMoveY(-10, 1).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                OrderText.DOText(AnswerTalk, 0.05f * AnswerTalk.Length).OnComplete(() =>
                {
                    StartCoroutine(ExitAndComein());
                });
            });
            OrderText.text = "";
        };

    }
    public IEnumerator ExitAndComein()
    {
        if (SatisfactionCoroutine != null)
        {
            isSatisfactionStop = true;
        }

        yield return new WaitForSeconds(1.5f);

        SpeechBallon.gameObject.SetActive(false);
        NameBallon.gameObject.SetActive(false);
        //다시 시작

        customer.Exit();

        yield return new WaitForSeconds(1f);
        EmotionText.text = "100%";
        FaceImage.sprite = FaceSprites[(int)EFaceType.Happy];
        Ordercoroutine = StartCoroutine(Order());
    }

    public void StopOrderCoroutine()
    {
        if (Ordercoroutine != null)
            StopCoroutine(Ordercoroutine);
    }

    public void OrderToCook()
    {
        GameManager.Instance.ReturnCook = () =>
        {
            MapScrollMG.Instance.StartSet();
            OrderSet order = GameManager.Instance.orderSets[GameManager.Instance.randomCustomerNum];
            GameManager.Instance.asd(order.main, order.sub, order.count, order.style, order.dishCount);

            GameManager.Instance.Satisfaction = 100;
            CookingScene.transform.DOMoveY(0, 1).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                SatisfactionCoroutine = StartCoroutine(SatisfactionUpdate());
                if (isBeggar)
                {
                        StopCoroutine(SatisfactionUpdate());
                }
            });
        };
    }

    private IEnumerator SatisfactionUpdate()
    {
        print(GameManager.Instance.Satisfaction);
        if (GameManager.Instance.Satisfaction <= 60)
            FaceImage.sprite = FaceSprites[(int)EFaceType.Umm];
        if (GameManager.Instance.Satisfaction <= 20)
            FaceImage.sprite = FaceSprites[(int)EFaceType.Angry];
        yield return new WaitForSeconds(1f);
        if (GameManager.Instance.Satisfaction <= 0 || isSatisfactionStop == true)
        {
            isSatisfactionStop = false;
            yield break;
        }
        GameManager.Instance.Satisfaction--;
        EmotionText.text = $"{GameManager.Instance.Satisfaction}%";
        SatisfactionCoroutine = StartCoroutine(SatisfactionUpdate());
    }

    public void EndingProduction(Eending endingType)
    {
        StartCoroutine(EndingDelay(endingTypes[(int)endingType].Speech, endingTypes[(int)endingType].EndingSpr));

        IEnumerator EndingDelay(string speech, Sprite spr)
        {
            bool isEndLine = false;
            EndingCanvas.SetActive(true);
            FadeInOut.Instance.FadeOut();
            EndingImg.sprite = spr;
            EndingImg.DOFade(1, FadeInOut.Instance.fadeTime);
            yield return new WaitForSeconds(FadeInOut.Instance.fadeTime + 1);

            StartCoroutine(Typing(speech));
            IEnumerator Typing(string str)
            {
                string[] line = str.Split('\n');
                var wait = new WaitForSeconds(0.05f);
                yield return wait;
                for (int i = 0; i < line.Length; i++)
                {
                    print(line[i]);
                    EndingExplanTxt.DOText(line[i], 0.05f * line[i].Length).OnComplete(() =>
                    {
                        isEndLine = true;
                    });
                    while(true)
                    {
                        yield return null;
                        if (Input.GetMouseButtonDown(0) && isEndLine)
                            break;
                    }
                    isEndLine = false;
                    EndingExplanTxt.text = "";
                }
            }
        }
    }
    #endregion

}
