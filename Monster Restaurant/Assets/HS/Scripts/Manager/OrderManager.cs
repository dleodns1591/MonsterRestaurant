using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Object = UnityEngine.Object;
using UnityEngine.SceneManagement;

public class OrderManager : Singleton<OrderManager>
{
    public TextAsset OrderTalkTxt, AnswerTalkTxt;

    [Header("주문 버튼 관련")]
    [SerializeField] private TextMeshProUGUI BtnCookText;
    [SerializeField] private TextMeshProUGUI BtnAskText;
    private Button CookingBtn => BtnCookText.transform.parent.GetComponent<Button>();
    private Button ReAskBtn => BtnAskText.transform.parent.GetComponent<Button>();

    [Header("하루 시간 관련")]
    public Image TimeFill;

    [Header("결과 창 관련")]
    public int firstMoney;
    [SerializeField] private ResultManager resultManager;

    [Header("손님 관련")]
    [SerializeField] public Image CustomerImg;
    [SerializeField] private Sprite[] GuestDefualts, EventGuestDefualts;
    [SerializeField] public Sprite[] GuestSuccess, EventGuestSuccess;
    [SerializeField] public Sprite[] GuestFails, EventGuestFails;
    [SerializeField] public Customer customer;
    private int normalGuestType;

    [Header("손님의 말풍선 관련")]
    [SerializeField] private UIText OrderText;
    [SerializeField] private GameObject NameBallon;
    private Image SpeechBallon => OrderText.transform.parent.GetComponent<Image>();
    private Text NameBallonText => NameBallon.transform.GetComponentInChildren<Text>();

    [Header("요리 당 만족도 관련")]
    public SatisfactionManager satisfactionManager;

    [Header("메모 관련")]
    [SerializeField] private MemoManager memoManager;

    [Header("엔딩 관련")]
    public EndingManager endingManager;

    [Header("기타부타타")]
    public GameObject CookingScene;

    [Header("상점 관련")]
    [SerializeField] private Shop shop;

    [Header("내부 변수들")]
    private Tween TextTween, DayTween;
    public int ReQuestionCount, GuestOfTheDay;
    private List<EeventCustomerType> EventTypes = new List<EeventCustomerType>();
    private Coroutine Ordercoroutine, BuyTextCoroutine;
    private I_CustomerType CustomerType;
    [HideInInspector] public bool isCookingSuccess;
    [HideInInspector] public bool isBeggar;
    [HideInInspector] public int orderType;
    [HideInInspector] public string[] OrderTalk = new string[3], AskTalk = new string[3];
    [HideInInspector] public string AnswerTalk;
    [HideInInspector] public bool isNext;
    [HideInInspector] public int Beggar_SuccessPoint = 0;
    [HideInInspector] public int Earthling_SuccessPoint = 0;
    [HideInInspector] public int dialogNumber;

    private void Start()
    {
        GameManager.Instance.Money = 100;

        shop = GameManager.Instance.shop;

        SoundManager.instance.PlaySoundClip("Ingame_bgm", SoundType.BGM);

        RandomOrderMaterial();
        OrderLoop();
        ShopProduction();
        OrderToCook();
        CookToOrder();
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
    public void EeventCustomerSetting(int randomType)
    {
        CustomerImg.sprite = EventGuestDefualts[randomType];
        NameBallonText.text = NameKoreanReturn(Enum.GetName(typeof(EeventCustomerType), randomType));
    }

    void SetCustomerType(int type)
    {
        Destroy((Object)CustomerType);

        GuestOfTheDay++;

        GameManager.Instance.randomCustomerNum = UnityEngine.Random.Range(0, OrderTalkTxt.text.Split('\n').Length);
        for (int i = 0; i < OrderTalk.Length; i++)
        {
            OrderTalk[i] = RandomOrderSpeech(i)[GameManager.Instance.randomCustomerNum];
        }

        if (EventTypes != null)
            EventTypes.Clear();
        foreach (var item in GameManager.Instance.eventCheck.returnEventCustomer)
        {
            EventTypes.Add(item);
        }

        int types;
        print(GuestOfTheDay);
        if (GuestOfTheDay % 2 == 0 && EventTypes.Count >= GuestOfTheDay / 2 && EventTypes.Count != 0)
        {
            types = (int)EventTypes[(GuestOfTheDay / 2) - 1];
            switch ((EeventCustomerType)types)
            {
                case EeventCustomerType.Human:
                    if (GameManager.Instance.isEarthlingRefuse)
                    {
                        normalGuestType = UnityEngine.Random.Range(0, 8);
                        SetCustomerType(normalGuestType);
                        return;
                    }
                    CustomerType = gameObject.AddComponent<Earthling>();
                    EeventCustomerSetting(types);
                    break;
                case EeventCustomerType.Thief:
                    CustomerType = gameObject.AddComponent<Thief>();
                    EeventCustomerSetting(types);
                    break;
                case EeventCustomerType.Beggar:
                    if (GameManager.Instance.isBeggarRefuse)
                    {
                        normalGuestType = UnityEngine.Random.Range(0, 8);
                        SetCustomerType(normalGuestType);
                        return;
                    }
                    CustomerType = gameObject.AddComponent<Beggar>();
                    EeventCustomerSetting(types);
                    break;
                case EeventCustomerType.GroupOrder:
                    CustomerType = gameObject.AddComponent<GroupOrder>();
                    EeventCustomerSetting(types);
                    break;
                case EeventCustomerType.SalesMan:
                    EeventCustomerSetting(types);
                    CustomerType = gameObject.AddComponent<SalesMan>();
                    break;
                case EeventCustomerType.FoodCleanTester:
                    CustomerType = gameObject.AddComponent<FoodCleanTester>();
                    EeventCustomerSetting(types);
                    break;
            }

            CustomerType.SpecialType(BtnCookText, BtnAskText);
            return;
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
                Destroy((Object)CustomerType);
                break;
        }
        CustomerType.SpecialType(BtnCookText, BtnAskText);
    }
    public string[] RandomOrderSpeech(int OrderSequence)
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
                    return EMainMatarials.NULL;
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
                    return ESubMatarials.NULL;

            }
        }

        ECookingStyle eStyle(string cell)
        {
            switch (cell)
            {
                case "끓기":
                    return ECookingStyle.Boil;
                case "튀기기":
                    return ECookingStyle.Fry;
                case "굽기":
                    return ECookingStyle.Roast;
                default:
                    return ECookingStyle.None;
            }
        }

        string[] line = OrderTalkTxt.text.Split('\n');
        string[] Sentence = new string[line.Length];

        GameManager.Instance.orderSets = new OrderSet[line.Length];
        for (int i = 0; i < line.Length; i++)
        {
            string[] cell = line[i].Split('\t');

            GameManager.Instance.orderSets[i].main = eMain(cell[0]);
            GameManager.Instance.orderSets[i].sub = new List<ESubMatarials>
            {
                eSub(cell[1]),
                eSub(cell[2]),
                eSub(cell[3])
            };
            GameManager.Instance.orderSets[i].style = eStyle(cell[4]);
            GameManager.Instance.orderSets[i].count = int.Parse(cell[5]);
            GameManager.Instance.orderSets[i].dishCount = int.Parse(cell[6]);
        }
        return Sentence;
    }
    /// <summary>
    /// 손님을 받는 이벤트? 들이 시작하는 함수
    /// </summary>
    public void OrderLoop()
    {
        firstMoney = (int)GameManager.Instance.Money;
        GuestOfTheDay = 0;
        ReQuestionCount = 0;

        GameManager.Instance.SalesRevenue = 0;
        GameManager.Instance.MarterialCost = 0;
        GameManager.Instance.TaxCost = 0;
        GameManager.Instance.SettlementCost = 0;
        NextCustomerReady();
        normalGuestType = UnityEngine.Random.Range(0, 9);
        SetCustomerType(normalGuestType);
        Ordercoroutine = StartCoroutine(Order());

        if (DayTween != null)
            DayTween.Kill();

        DayTween = DOTween.To(() => TimeFill.fillAmount, x => TimeFill.fillAmount = x, 0, 120)
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

        memoManager.ResetMemo();

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

    IEnumerator Order()
    {
        if (GameManager.Instance.dayEndCheck)
        {
            resultManager.DayEnd();
            yield break;
        }
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
            print(OrderTalk[i]);
            TextTween = OrderText.DOText(OrderTalk[i], 0.05f * OrderTalk[i].Length);
            while (!isNext)
            {
                yield return null;
            }
            isNext = false;
        }
    }

    #region 게임 루틴 관련

    private void ShopProduction()
    {
        shop.StopBuyBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.shop.ShopCloseBtn();
            StartCoroutine(RefuseOrderDelay());
            IEnumerator RefuseOrderDelay()
            {
                OrderTalk[1] = "이용해 주셔서 감사합니다.";
                isNext = true;
                BtnCookText.transform.parent.gameObject.SetActive(false);
                BtnAskText.transform.parent.gameObject.SetActive(false);
                shop.MouseGuide.SetActive(false);
                shop.StopBuyBtn.gameObject.SetActive(false);
                yield return new WaitForSeconds(1.5f);
                StartCoroutine(ExitAndComein(false));
            }
        });

        GameManager.Instance.WormHoleDraw = () =>
        {
            int rand = UnityEngine.Random.Range(1, 10);
            GameManager.Instance.isEndingOpens[(int)EendingType.WormHole] = true;
            SaveManager.Instance.isEndingOpens[(int)EendingType.WormHole] = true;
            if (rand >= 7)
            {

                endingManager.EndingProduction(EendingType.WormHole_FindHouse);
                GameManager.Instance.isEndingOpens[(int)EendingType.WormHole_FindHouse] = true;
                SaveManager.Instance.isEndingOpens[(int)EendingType.WormHole_FindHouse] = true;
            }
            else
            {

                endingManager.EndingProduction(EendingType.WormHole_SpaceAdventure);
                GameManager.Instance.isEndingOpens[(int)EendingType.WormHole_SpaceAdventure] = true;
                SaveManager.Instance.isEndingOpens[(int)EendingType.WormHole_SpaceAdventure] = true;
            }
        };

        GameManager.Instance.BuyTalking = () =>
        {
            int rand = UnityEngine.Random.Range(0, 2);
            string[] speechs = new string[3] { "탁월한 선택이시네요.", "역시.. 보는 눈이 있으시네요.", "구매해 주셔서 감사합니다." };

            SpeakOrder(speechs[rand]);

            if (BuyTextCoroutine != null)
            {
                StopCoroutine(BuyTextCoroutine);
                OrderText.text = "";
            }

            BuyTextCoroutine = StartCoroutine(Delay());
            IEnumerator Delay()
            {
                yield return new WaitForSeconds(3 + (speechs[rand].Length * 0.05f));

                OrderText.text = "";
            }
        };

        GameManager.Instance.ShopAppearProd = () =>
        {
            StartCoroutine(Delay());
            IEnumerator Delay()
            {
                FadeInOut.instance.FadeOut();
                yield return new WaitForSeconds(FadeInOut.instance.fadeTime);
                GameManager.Instance.shop.ShopOpen();
                FadeInOut.instance.Fade();
                yield return new WaitForSeconds(FadeInOut.instance.fadeTime);
                shop.StopBuyBtn.gameObject.SetActive(true);
                OrderText.text = "";
                SpeakOrder("마음에 드시는 제품 있으시면 구매해주세요.");
                yield return new WaitForSeconds("마음에 드시는 제품 있으시면 구매해주세요.".Length * 0.05f);
                shop.MouseGuide.SetActive(true);
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
            satisfactionManager.LoopStop();

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
            GameManager.Instance.isGroupOrder = false;
            if (!(CustomerType is NormalCustomer))
            {
                EeventCustomerSetting(GameManager.Instance.SpecialType);
                AnswerTalk = CustomerType.SpecialAnswer();
            }

            print(AnswerTalk);
            CookingScene.transform.DOMoveY(-10, 1).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                OrderText.DOText(AnswerTalk, 0.05f * AnswerTalk.Length).OnComplete(() =>
                {
                    StartCoroutine(ExitAndComein(false));
                });
            });
            OrderText.text = "";
        };

    }
    public IEnumerator ExitAndComein(bool isEvent)
    {
        yield return new WaitForSeconds(1.5f);

        SpeechBallon.gameObject.SetActive(false);
        NameBallon.gameObject.SetActive(false);
        //다시 시작

        customer.Exit();

        yield return new WaitForSeconds(1f);
        if (isEvent == false)
        {
            NextCustomerReady();
            normalGuestType = UnityEngine.Random.Range(0, 9);
            SetCustomerType(normalGuestType);
        }
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
            GameManager.Instance.ConditionSetting(order.main, order.sub, order.count, order.style, order.dishCount);


                satisfactionManager.LoopStart();
            CookingScene.transform.DOMoveY(0, 1).SetEase(Ease.OutBounce).OnComplete(() =>
            {
            });
        };
    }
    #endregion

}
