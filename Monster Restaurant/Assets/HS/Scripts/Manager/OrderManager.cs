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

    [Header("주문 버튼 관련")]
    [SerializeField] public OrderButtonManager orderButtonManager;

    [Header("하루 시간 관련")]
    public Image TimeFill;

    [Header("결과 창 관련")]
    public int firstMoney;
    [SerializeField] private ResultManager resultManager;

    [Header("손님 관련")]
    [SerializeField] public Customer customer;
    [SerializeField] public CustomerManager customerManager;

    [Header("온라인 PVP 전용")]
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

    [Header("엑셀에 맞게 재료 세팅 관련")]
    [SerializeField] private MaterialSetting materialSetting;

    [Header("손님의 대사 관련")]
    [SerializeField] public OrderMessageManager orderMessageManager;

    [Header("요리 당 만족도 관련")]
    public SatisfactionManager satisfactionManager;

    [Header("돈 연출 관련")]
    public DirectingManager directingManager;

    [Header("메모 관련")]
    [SerializeField] private MemoManager memoManager;

    [Header("엔딩 관련")]
    public EndingManager endingManager;

    [Header("기타부타타")]
    public GameObject CookingScene;

    [Header("상점 관련")]
    [SerializeField] private Shop shop;

    [Header("챌린지 관련")]
    [HideInInspector] public int ChallengeTimeTaken;
    [HideInInspector] public int ChallengeTimeLimit;

    [Header("내부 변수들")]
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
    /// 손님을 받는 내용들이 시작하는 함수
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
        .OnComplete(() => //시간이 다 지났을때
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
        //다시 시작

        customer.Exit();

        yield return new WaitForSeconds(1f);

        NextCustomerReady();
        normalGuestType = UnityEngine.Random.Range(0, Enum.GetValues(typeof(EcustomerType)).Length);
        customerManager.SetCustomerType(normalGuestType);

        Ordercoroutine = StartCoroutine(Order());
    }

    /// <summary>
    /// 대결이 끝나면 다음 손님 안받고 끝나는 함수 (구현 필요함!!)
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
