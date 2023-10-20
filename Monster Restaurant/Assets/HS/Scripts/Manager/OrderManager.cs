using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core.Easing;

public class OrderManager : Singleton<OrderManager>
{
    public TextAsset OrderTalkTxt, AnswerTalkTxt;

    [Header("�ֹ� ��ư ����")]
    [SerializeField] public OrderButtonManager orderButtonManager;

    [Header("�Ϸ� �ð� ����")]
    public Image TimeFill;

    [Header("��� â ����")]
    public int firstMoney;
    [SerializeField] private ResultManager resultManager;

    [Header("�մ� ����")]
    [SerializeField] public Customer customer;
    [SerializeField] public CustomerManager customerManager;

    [Header("�¶��� PVP ����")]
    [SerializeField] public CustomerManagerOnlineVer onlineVer;

    public I_CustomerType CustomerType;

    public int NormalGuestType
    {
        get
        {
            return normalGuestType;
        }
        set
        {
            normalGuestType = value;
        }
    }
    private int normalGuestType;

    [Header("������ �°� ��� ���� ����")]
    [SerializeField] private MaterialSetting materialSetting;

    [Header("�մ��� ��� ����")]
    [SerializeField] public OrderMessageManager orderMessageManager;

    [Header("�丮 �� ������ ����")]
    public SatisfactionManager satisfactionManager;

    [Header("�� ���� ����")]
    public DirectingManager directingManager;

    [Header("�޸� ����")]
    [SerializeField] private MemoManager memoManager;

    [Header("���� ����")]
    public EndingManager endingManager;

    [Header("��Ÿ��ŸŸ")]
    public GameObject CookingScene;

    [Header("���� ����")]
    [SerializeField] private Shop shop;

    [Header("ç���� ����")]
    [HideInInspector] public int ChallengeTimeTaken;
    [HideInInspector] public int ChallengeTimeLimit;

    [Header("���� ������")]
    private Tween DayTween;
    public int ReQuestionCount, GuestOfTheDay;
    private Coroutine Ordercoroutine;
    [HideInInspector] public bool isCookingSuccess;
    [HideInInspector] public bool isBeggar;
    [HideInInspector] public int perfectMade;
    [HideInInspector] public int GroupOrderTimeLimit;
    [HideInInspector] public int orderType;
    [HideInInspector] public string[] OrderTalk = new string[3], AskTalk = new string[3];
    [HideInInspector] public string AnswerTalk;
    [HideInInspector] public bool isNext;
    [HideInInspector] public int Beggar_SuccessPoint = 0;
    [HideInInspector] public int Earthling_SuccessPoint = 0;
    [HideInInspector] public int dialogNumber;

    private void Start()
    {
        GameManager.Instance.IsEndingOpens = SaveManager.Instance.isEndingOpens;
        GameManager.Instance.Money = 100;
        shop = GameManager.Instance.shop;
        perfectMade = 1;
        SoundManager.instance.PlaySoundClip("Ingame_bgm", SoundType.BGM);

        materialSetting.RandomOrderMaterial();
        OrderLoop();
    }
    public string[] RandomOrderSpeech(int OrderSequence)
    {
        string[] line = OrderTalkTxt.text.Split('\n');
        string[] Sentence = new string[line.Length];

        for (int i = 0; i < line.Length; i++)
        {
            string[] cell = line[i].Split('\t');

            if (SaveManager.Instance.isEnglish == false)
                Sentence[i] = cell[7 + (OrderSequence * 2)];
            else
                Sentence[i] = cell[8 + (OrderSequence * 2)];

        }
        return Sentence;
    }

    /// <summary>
    /// �մ��� �޴� ������� �����ϴ� �Լ�
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
        normalGuestType = UnityEngine.Random.Range(0, Enum.GetValues(typeof(EcustomerType)).Length);

        if (SaveManager.Instance.isPvp)
        {
            onlineVer.SetCustomerType();
        }
        else
        {
            if (SaveManager.Instance.isChallenge == false) customerManager.SetCustomerType(normalGuestType);
            else customerManager.SetCustomerType(0);
        }

        Ordercoroutine = StartCoroutine(Order());

        if (DayTween != null)
            DayTween.Kill();

        DayTween = DOTween.To(() => TimeFill.fillAmount, x => TimeFill.fillAmount = x, 0, 120)
        .OnComplete(() => //�ð��� �� ��������
        {
            GameManager.Instance.dayEndCheck = true;
        });
    }

    void NextCustomerReady()
    {
        Array.Clear(OrderTalk, 0, OrderTalk.Length);
        Array.Clear(AskTalk, 0, AskTalk.Length);

        memoManager.ResetMemo();

        orderMessageManager.ResetText();
        orderButtonManager.ResetButttonText();
    }
    IEnumerator Order()
    {
        if (GameManager.Instance.dayEndCheck)
        {
            resultManager.DayEnd();
            yield break;
        }
        yield return StartCoroutine(customer.Moving());

        if (SaveManager.Instance.isChallenge == false) orderButtonManager.ButtonSetActive(true);

        orderMessageManager.BallonSetActive(true);

        if (SaveManager.Instance.isPvp == true) yield break;

        for (int i = 0; i < OrderTalk.Length; i++)
        {
            print(OrderTalk.Length);
            if (OrderTalk[i].Equals(""))
            {
                continue;
            }

            orderMessageManager.StopTalking();
            orderMessageManager.ResetText();

            orderMessageManager.TalkingText(OrderTalk[i]);

            while (isNext == false)
            {
                yield return null;

                if (isNext == true)
                {
                    break;
                }
            }
            isNext = false;
        }
    }

    public void SpeakOrder(string speech)
    {
        orderMessageManager.StopTalking();
        orderMessageManager.ResetText();
        orderMessageManager.TalkingText(speech);
    }
    public IEnumerator ExitAndComein()
    {
        yield return new WaitForSeconds(1.5f);

        orderMessageManager.BallonSetActive(false);
        //�ٽ� ����

        customer.Exit();

        yield return new WaitForSeconds(1f);

        NextCustomerReady();
        normalGuestType = UnityEngine.Random.Range(0, Enum.GetValues(typeof(EcustomerType)).Length);
        customerManager.SetCustomerType(normalGuestType);

        Ordercoroutine = StartCoroutine(Order());
    }

    /// <summary>
    /// ����� ������ ���� �մ� �ȹް� ������ �Լ� (���� �ʿ���!!)
    /// </summary>
    /// <returns></returns>
    public IEnumerator PvpEnd()
    {
        yield return new WaitForSeconds(1.5f);

        onlineVer.EndBtnsSetActive(true);
    }

    public void StopOrderCoroutine()
    {
        if (Ordercoroutine != null)
            StopCoroutine(Ordercoroutine);
    }
}
