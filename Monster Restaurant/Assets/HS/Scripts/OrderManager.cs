using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Object = UnityEngine.Object;
using UnityEngine.SceneManagement;

[Serializable]
public struct EndingType
{
    [TextArea] public string Speech;
    public Sprite EndingSpr;
}

public class OrderManager : Singleton<OrderManager>
{
    public TextAsset OrderTalkTxt, AnswerTalkTxt;

    [Header("�ֹ� ��ư ����")]
    [SerializeField] private TextMeshProUGUI BtnCookText;
    [SerializeField] private TextMeshProUGUI BtnAskText;
    private Button CookingBtn => BtnCookText.transform.parent.GetComponent<Button>();
    private Button ReAskBtn => BtnAskText.transform.parent.GetComponent<Button>();

    [Header("�Ϸ� �ð� ����")]
    [SerializeField] private Image TimeFill;

    [Header("�մ� ����")]
    [SerializeField] public Image CustomerImg;
    [SerializeField] private Sprite[] GuestDefualts, EventGuestDefualts;
    [SerializeField] public Sprite[] GuestSuccess, EventGuestSuccess;
    [SerializeField] public Sprite[] GuestFails, EventGuestFails;
    [SerializeField] public Customer customer;
    private int normalGuestType;

    [Header("�մ��� ��ǳ�� ����")]
    [SerializeField] private UIText OrderText;
    [SerializeField] private GameObject NameBallon;
    private Image SpeechBallon => OrderText.transform.parent.GetComponent<Image>();
    private Text NameBallonText => NameBallon.transform.GetComponentInChildren<Text>();

    [Header("�丮 �� ������ ����")]
    [SerializeField] private Sprite[] FaceSprites;
    [SerializeField] private Image FaceImage;
    [SerializeField] private Text EmotionText;

    [Header("UI ����")]
    [SerializeField] private Text MoneyText;

    [Header("��� â ����")]
    [SerializeField] private GameObject RevenuePopup;
    [SerializeField] private Button NextButton;
    [SerializeField] private Text Principal, BasicRevenue, SalesRevenue, MarterialCost, TaxCost, SettlementCost, Total;
    [SerializeField] private Text DayText;

    [Header("�޸� ����")]
    [SerializeField] private RectTransform MemoPaper;
    [SerializeField] private TextMeshProUGUI[] MemoTexts;
    [SerializeField] private Image MemoPaperBackground;
    private readonly Vector2[] MemoOnTextSizes = { new Vector2(-72.51f, 80.92996f), new Vector2(-3, 6.999878f), new Vector2(-72.51f, -64.00003f), new Vector2(-3, -138), new Vector2(-72.51f, -204) };

    [Header("��Ÿ��ŸŸ")]
    public GameObject CookingScene;

    [Header("���� ����")]
    [SerializeField] private Image EndingImg;
    [SerializeField] private Text EndingExplanTxt;
    public EndingType[] endingTypes;
    private GameObject EndingCanvas => EndingImg.transform.parent.gameObject;

    [Header("���� ����")]
    public Button StopBuyBtn;
    public GameObject MouseGuide;

    [Header("���� ������")]
    private int EndingDate = 20;
    private Tween TextTween, DayTween;
    public int ReQuestionCount;
    private List<EeventCustomerType> EventTypes = new List<EeventCustomerType>();
    private Coroutine SatisfactionCoroutine, Ordercoroutine, BuyTextCoroutine;
    private int firstMoney, GuestOfTheDay;
    private I_CustomerType CustomerType;
    public bool isSatisfactionStop;
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
        SoundManager.instance.PlaySoundClip("Ingame_bgm", SoundType.BGM);

        RandomOrderMaterial();
        OrderLoop();
        ShopProduction();
        OrderToCook();
        CookToOrder();
    }
    private void Update()
    {
        MoneyText.text = ((int)GameManager.Instance.Money).ToString();

        if(Input.GetKeyDown(KeyCode.O))
        {
            EndingDate = 1;
        }
        else if(Input.GetKeyDown(KeyCode.P))
        {
            EndingDate = 20;
        }
        else if(Input.GetKeyDown(KeyCode.L))
        {
            GameManager.Instance.Money += 1000;
        }
    }

    string NameKoreanReturn(string name)
    {
        switch (name)
        {
            case "Alien":
                return "���̻�";
            case "Hyena":
                return "����";
            case "Robot":
                return "sdh210224";
            case "Dragon":
                return "�ñ�ġ";
            case "Light":
                return "2�� ����ȭ";
            case "FSM":
                return "������";
            case "Chris":
                return "���� ũ����";
            case "Demon":
                return "�︮����";
            case "Holotle":
                return "��Ȧ�γ�Ʈ";
            case "Human":
                return "���ڶ�";
            case "Thief":
                return "����";
            case "Beggar":
                return "�縻 ������";
            case "Rich":
                return "�縻 ������";
            case "GroupOrder":
                return "�÷θ���";
            case "SalesMan":
                return "���õ�";
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
            case "���̻�":
                return EcustomerType.Alien;
            case "����":
                return EcustomerType.Hyena;
            case "sdh210224":
                return EcustomerType.Robot;
            case "�ñ�ġ":
                return EcustomerType.Dragon;
            case "2�� ����ȭ":
                return EcustomerType.Light;
            case "������":
                return EcustomerType.FSM;
            case "���� ũ����":
                return EcustomerType.Chris;
            case "�︮����":
                return EcustomerType.Demon;
            case "��Ȧ�γ�Ʈ":
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
                case "��":
                    return EMainMatarials.Noodle;
                case "��":
                    return EMainMatarials.Rice;
                case "��":
                    return EMainMatarials.Bread;
                case "���":
                    return EMainMatarials.Meat;
                default:
                    return EMainMatarials.NULL;
            }
        }
        ESubMatarials eSub(string cell)
        {
            switch (cell)
            {
                case "��ƼĿ":
                    return ESubMatarials.Sticker;
                case "��":
                    return ESubMatarials.Poop;
                case "��Ʈ":
                    return ESubMatarials.Bolt;
                case "�����":
                    return ESubMatarials.Preservatives;
                case "����":
                    return ESubMatarials.Paper;
                case "��":
                    return ESubMatarials.Money;
                case "����":
                    return ESubMatarials.Jewelry;
                case "����":
                    return ESubMatarials.Eyes;
                case "������":
                    return ESubMatarials.Battery;
                case "�� ��ġ":
                    return ESubMatarials.Fur;
                case "�񽺹�Ʈ":
                    return ESubMatarials.Bismuth;
                case "�ܰ� Ǯ":
                    return ESubMatarials.AlienPlant;
                default:
                    return ESubMatarials.NULL;

            }
        }

        ECookingStyle eStyle(string cell)
        {
            switch (cell)
            {
                case "����":
                    return ECookingStyle.Boil;
                case "Ƣ���":
                    return ECookingStyle.Fry;
                case "����":
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
    /// �մ��� �޴� �̺�Ʈ? ���� �����ϴ� �Լ�
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

        DayTween = DOTween.To(() => TimeFill.fillAmount, x => TimeFill.fillAmount = x, 0, 120)
        .OnComplete(() => //�ð��� �� ��������
        {
            GameManager.Instance.dayEndCheck = true;
            //�մ� ȭ���鼭 ������
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
    /// �մ� �̸� ��ȯ�ϴ� �Լ�
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
    /// ���� ������ �ִϸ��̼�
    /// </summary>
    /// <param name="targetNumber">���ϴ� ��ġ</param>
    /// <param name="animationDuration">���ϴ� �ӵ�</param>
    /// <param name="numberText">����� �ؽ�Ʈ UI</param>
    void NumberAnimation(int targetNumber, float animationDuration, Text numberText)
    {
        int currentNumber = 0;
        DOTween.To(() => currentNumber, x => currentNumber = x, targetNumber, animationDuration)
            .SetEase(Ease.Linear)
            .OnUpdate(() => numberText.text = currentNumber.ToString());
    }

    void DayEnd()
    {
        if(GameManager.Instance.Day >= EndingDate)
        {
            if (GameManager.Instance.Money < 2500)
            {

                EndingProduction(EendingType.Loser);
                GameManager.Instance.isEndingOpens[(int)EendingType.Loser] = true;
                SaveManager.Instance.isEndingOpens[(int)EendingType.Loser] = true;
            }
            else if(GameManager.Instance.Money < 5000)
            {

                EndingProduction(EendingType.Salve);
                GameManager.Instance.isEndingOpens[(int)EendingType.Salve] = true;
                SaveManager.Instance.isEndingOpens[(int)EendingType.Salve] = true;
            }
            else
            {

                EndingProduction(EendingType.Mine);
                GameManager.Instance.isEndingOpens[(int)EendingType.Mine] = true;
                SaveManager.Instance.isEndingOpens[(int)EendingType.Mine] = true;
            }

            return;
        }
        FadeInOut.instance.LittleFadeOut();
        FadeInOut.instance.RevenueFadeOut();

        GameManager.Instance.shop.PurchaseDayCheck();
        GameManager.Instance.Money += 200;
        GameManager.Instance.TaxCost = GameManager.Instance.SalesRevenue / 10;
        GameManager.Instance.Money -= GameManager.Instance.TaxCost;
        GameManager.Instance.Money -= GameManager.Instance.SettlementCost;
        StartCoroutine(NumberAni());
        IEnumerator NumberAni()
        {
            yield return new WaitForSeconds(FadeInOut.instance.fadeTime);
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
                        GameManager.Instance.eventCheck.Check();
                        DayText.text = $"{GameManager.Instance.Day}����....!";
                        DayText.DOFade(1, 1);
                        yield return new WaitForSeconds(1.5f);
                        FadeInOut.instance.LittleFade();
                        DayText.DOFade(0, FadeInOut.instance.fadeTime);
                        yield return new WaitForSeconds(FadeInOut.instance.fadeTime);
                    }
                    StartCoroutine(DayProduction());
                    StartCoroutine(Reset());
                });
            });
            IEnumerator Reset()
            {
                yield return new WaitForSeconds(FadeInOut.instance.fadeTime);
                RevenuePopup.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                RevenuePopup.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                GameManager.Instance.dayEndCheck = false;
                GuestOfTheDay = 0;
                isSatisfactionStop = false;
                ReQuestionCount = 0;
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
        SetCustomerType(normalGuestType);
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

    #region �޸� ����
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
                print("ConditionSetting");
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

    #region ���� ��ƾ ����

    private void ShopProduction()
    {
        StopBuyBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.shop.ShopCloseBtn();
            StartCoroutine(RefuseOrderDelay());
            IEnumerator RefuseOrderDelay()
            {
                OrderTalk[1] = "�̿��� �ּż� �����մϴ�.";
                isNext = true;
                BtnCookText.transform.parent.gameObject.SetActive(false);
                BtnAskText.transform.parent.gameObject.SetActive(false);
                MouseGuide.SetActive(false);
                StopBuyBtn.gameObject.SetActive(false);
                yield return new WaitForSeconds(1.5f);
                StartCoroutine(ExitAndComein());
            }
        });

        GameManager.Instance.WormHoleDraw = () =>
        {
            int rand = UnityEngine.Random.Range(1, 10);
            GameManager.Instance.isEndingOpens[(int)EendingType.WormHole] = true;
            SaveManager.Instance.isEndingOpens[(int)EendingType.WormHole] = true;
            if (rand >= 7)
            {

                EndingProduction(EendingType.WormHole_FindHouse);
                GameManager.Instance.isEndingOpens[(int)EendingType.WormHole_FindHouse] = true;
                SaveManager.Instance.isEndingOpens[(int)EendingType.WormHole_FindHouse] = true;
            }
            else
            {

                EndingProduction(EendingType.WormHole_SpaceAdventure);
                GameManager.Instance.isEndingOpens[(int)EendingType.WormHole_SpaceAdventure] = true;
                SaveManager.Instance.isEndingOpens[(int)EendingType.WormHole_SpaceAdventure] = true;
            }
        };

        GameManager.Instance.BuyTalking = () =>
        {
            int rand = UnityEngine.Random.Range(0, 2);
            string[] speechs = new string[3] { "Ź���� �����̽ó׿�.", "����.. ���� ���� �����ó׿�.", "������ �ּż� �����մϴ�." };

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
                StopBuyBtn.gameObject.SetActive(true);
                OrderText.text = "";
                SpeakOrder("������ ��ô� ��ǰ �����ø� �������ּ���.");
                yield return new WaitForSeconds("������ ��ô� ��ǰ �����ø� �������ּ���.".Length * 0.05f);
                MouseGuide.SetActive(true);
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
                if (cell[1] == "����")
                {
                    SucsessTalk[(int)NameToEnumReturn(cell[0]), sucsessCnt] = cell[2];
                    sucsessCnt++;
                }
                else if (cell[1] == "����")
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

                //if ���� ����
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
                    MemoOff();
                });
            });
            OrderText.text = "";
        };

    }
    public IEnumerator ExitAndComein()
    {
        yield return new WaitForSeconds(1.5f);

        SpeechBallon.gameObject.SetActive(false);
        NameBallon.gameObject.SetActive(false);
        //�ٽ� ����

        customer.Exit();

        yield return new WaitForSeconds(1f);
        EmotionText.text = "100%";
        GameManager.Instance.Satisfaction = 100;
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
            GameManager.Instance.ConditionSetting(order.main, order.sub, order.count, order.style, order.dishCount);

            GameManager.Instance.Satisfaction = 100;
            EmotionText.text = $"{GameManager.Instance.Satisfaction}%";

            if (SatisfactionCoroutine != null)
                StopCoroutine(SatisfactionCoroutine);

            CookingScene.transform.DOMoveY(0, 1).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                SatisfactionCoroutine = StartCoroutine(SatisfactionUpdate());
                if (isBeggar)
                {
                    StopCoroutine(SatisfactionCoroutine);
                }
            });
        };
    }

    private IEnumerator SatisfactionUpdate()
    {
        while (true)
        {
            if(ReQuestionCount != 0)
            {
                GameManager.Instance.Satisfaction -= (ReQuestionCount * 8);
                ReQuestionCount = 0;
            }
            if (!GameManager.Instance.isGroupOrder)
            {
                if (GameManager.Instance.Satisfaction <= 60)
                    FaceImage.sprite = FaceSprites[(int)EFaceType.Umm];
                if (GameManager.Instance.Satisfaction <= 20)
                    FaceImage.sprite = FaceSprites[(int)EFaceType.Angry];
            }
            else
            {
                if (GameManager.Instance.Satisfaction <= 55)
                    FaceImage.sprite = FaceSprites[(int)EFaceType.Angry];
            }
            yield return new WaitForSeconds(1f);
            if (isSatisfactionStop == true)
            {
                EmotionText.text = $"{GameManager.Instance.Satisfaction}%";
                FaceImage.sprite = FaceSprites[(int)EFaceType.Angry];
                isSatisfactionStop = false;
                yield break;
            }
            GameManager.Instance.Satisfaction--;
            EmotionText.text = $"{GameManager.Instance.Satisfaction}%";
        }
    }

    public void EndingProduction(EendingType endingType)
    {
        SoundManager.instance.PlaySoundClip("Ending_bgm", SoundType.BGM);
        StartCoroutine(EndingDelay(endingTypes[(int)endingType].Speech, endingTypes[(int)endingType].EndingSpr));

        IEnumerator EndingDelay(string speech, Sprite spr)
        {
            bool isEndLine = false;
            EndingCanvas.SetActive(true);
            FadeInOut.instance.FadeOut();
            EndingImg.sprite = spr;
            EndingImg.DOFade(1, FadeInOut.instance.fadeTime);
            yield return new WaitForSeconds(FadeInOut.instance.fadeTime + 1);

            StartCoroutine(Typing(speech));
            IEnumerator Typing(string str)
            {
                string[] line = str.Split('\n');
                var wait = new WaitForSeconds(0.05f);
                yield return wait;
                for (int i = 0; i < line.Length; i++)
                {
                    //print(line.Length);
                    EndingExplanTxt.text = "";
                    EndingExplanTxt.DOText(line[i], 0.05f * line[i].Length).OnComplete(() =>
                    {
                        isEndLine = true;
                    });
                    while (true)
                    {
                        yield return null;
                        if (Input.GetMouseButtonDown(0) && isEndLine)
                            break;
                    }
                    isEndLine = false;
                }
                    if (Input.GetMouseButtonDown(0))
                    {
                        SceneManager.LoadScene("Title");
                    }
            }
        }
    }
    #endregion

}
