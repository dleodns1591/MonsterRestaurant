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
    [Header("���� �ʿ�")]
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
                case "��":
                    return EMainMatarials.Noodle;
                case "��":
                    return EMainMatarials.Rice;
                case "��":
                    return EMainMatarials.Bread;
                case "���":
                    return EMainMatarials.Meat;
                default:
                    return EMainMatarials.Bread;
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
    /// �մ��� �޴� �̺�Ʈ? ���� �����ϴ� �Լ�
    /// </summary>
    void OrderLoop()
    {
        StartCoroutine(Order());

        if (DayTween != null)
            DayTween.Kill();
        TimeFill.fillAmount = 1;

        DayTween = DOTween.To(() => TimeFill.fillAmount, x => TimeFill.fillAmount = x, 0, 100)
        .OnComplete(() => //�ð��� �� ��������
        {
            //�մ� ȭ���鼭 ������

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

    #region �޸� ����
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

    #region ���� ��ƾ ����

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
                string EMSEE = "�������� ���̽����� �̴�� �ٺ� ����";
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
            //�ٽ� ����

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
