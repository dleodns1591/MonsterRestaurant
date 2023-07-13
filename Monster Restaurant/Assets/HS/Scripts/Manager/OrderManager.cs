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

    [Header("�ֹ� ��ư ����")]
    [SerializeField] private TextMeshProUGUI BtnCookText;
    [SerializeField] private TextMeshProUGUI BtnAskText;
    private Button CookingBtn => BtnCookText.transform.parent.GetComponent<Button>();
    private Button ReAskBtn => BtnAskText.transform.parent.GetComponent<Button>();

    [Header("�Ϸ� �ð� ����")]
    public Image TimeFill;

    [Header("��� â ����")]
    public int firstMoney;
    [SerializeField] private ResultManager resultManager;

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
    public SatisfactionManager satisfactionManager;

    [Header("�޸� ����")]
    [SerializeField] private MemoManager memoManager;

    [Header("���� ����")]
    public EndingManager endingManager;

    [Header("��Ÿ��ŸŸ")]
    public GameObject CookingScene;

    [Header("���� ����")]
    [SerializeField] private Shop shop;

    [Header("���� ������")]
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

        memoManager.ResetMemo();

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

    #region ���� ��ƾ ����

    private void ShopProduction()
    {
        shop.StopBuyBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.shop.ShopCloseBtn();
            StartCoroutine(RefuseOrderDelay());
            IEnumerator RefuseOrderDelay()
            {
                OrderTalk[1] = "�̿��� �ּż� �����մϴ�.";
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
                shop.StopBuyBtn.gameObject.SetActive(true);
                OrderText.text = "";
                SpeakOrder("������ ��ô� ��ǰ �����ø� �������ּ���.");
                yield return new WaitForSeconds("������ ��ô� ��ǰ �����ø� �������ּ���.".Length * 0.05f);
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
        //�ٽ� ����

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
