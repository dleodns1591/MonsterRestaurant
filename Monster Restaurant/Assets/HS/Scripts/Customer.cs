using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;

public enum EcustomerType
{
    Alien,
    Hyena,
    Robot,
    Dragon,

    EventCustomer
}
public enum EeventCustomerType
{
    GroupOrder,
    FoodCleanTester,
    SalesMan,
    Thief
}


public class Customer : MonoBehaviour
{
    [SerializeField]
    private Transform[] SlowMovingPos, OrderPos;
    [SerializeField]
    private Transform FastMovingPos;
    [SerializeField]
    private Sprite[] GuestDefualts;
    [SerializeField, Tooltip("배경 위에 보이기 하기 위한")]
    private GameObject BackgroundCanvas;
    [SerializeField]
    private GameObject CookingScene;
    [SerializeField]
    private RectTransform MemoPaper;
    [SerializeField]
    private FadeInOut fadeInOut;
    [SerializeField]
    private UIText OrderText, ReAskText, CookingText;
    [SerializeField]
    private UIText[] MemoTexts;
    [SerializeField]
    private RandomText RT;
    [SerializeField]
    private Button CookingBtn, ReAskBtn, MemoBtn;
    [SerializeField]
    private Image MemoPaperBackground;



    private readonly Vector2[] MemoOnTextSizes = { new Vector2(-72.51f, 80.92996f), new Vector2(-3, 6.999878f), new Vector2(-72.51f, -64.00003f), new Vector2(-3, -138), new Vector2(-72.51f, -204) };
    private readonly Vector2[] MemoOffTextSizes = { new Vector2(132, -9), new Vector2(-135, 3), new Vector2(130, -36), new Vector2(-145, -29), new Vector2(131, -65) };
    private bool playerDetect = false;
    private int curCustomerType;
    private int reAskCount = 0;
    private string[] memo = new string[5];
    private Tween TextTween;
    private string[] OrderTalk = new string[3];


    private void Start()
    {
        StartCoroutine(Moving());
    }

    /// <summary>
    /// 처음 등장부터 주문전까지 이동하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator Moving()
    {
        yield return new WaitForSeconds(fadeInOut.fadeTime);

        float delayTime = 0.5f;

        Image CustomerImg = gameObject.GetComponent<Image>();
        //랜덤으로 손님 뽑기
        curCustomerType = Random.Range(0, System.Enum.GetValues(typeof(EcustomerType)).Length);
        CustomerImg.sprite = GuestDefualts[(int)(EcustomerType)curCustomerType];

        //일반 손님 중에 이벤트 손님 경우가 뽑힐 경우
        if (curCustomerType == System.Enum.GetValues(typeof(EcustomerType)).Length - 1)
        {
            //손님 순서 다음부터가 이벤트 손님

            //이벤트 손님 뽑기
            curCustomerType = Random.Range(System.Enum.GetValues(typeof(EcustomerType)).Length - 1,
                System.Enum.GetValues(typeof(EcustomerType)).Length + System.Enum.GetValues(typeof(EeventCustomerType)).Length - 1);
            print(curCustomerType);
            CustomerImg.sprite = GuestDefualts[curCustomerType];
        }


        //와리가리 움직임
        for (int i = 0; i < SlowMovingPos.Length; i++)
        {
            if (i != SlowMovingPos.Length - 1)
                transform.DOMove(SlowMovingPos[i].position, delayTime);
            else
                transform.DOMove(SlowMovingPos[i].position, 0.25f);

            yield return new WaitForSeconds(delayTime);
        }

        //빠르게 이동할때 가면 감지
        playerDetect = true;

        //빠르게 이동
        transform.DOMove(FastMovingPos.position, delayTime);
        CustomerImg.DOColor(new Color(1, 1, 1, 0), delayTime);

        yield return new WaitForSeconds(1.5f);

        //주문 테이블쪽 이동
        gameObject.transform.parent = BackgroundCanvas.transform;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 1000);
        CustomerImg.sprite = GuestDefualts[(int)(EcustomerType)curCustomerType];
        transform.position = OrderPos[0].position;
        transform.DOMove(OrderPos[1].position, delayTime);
        CustomerImg.DOColor(new Color(1, 1, 1, 1), delayTime);

        yield return new WaitForSeconds(delayTime);

        //주문시작
        playerDetect = false;
        StartCoroutine(Order());

    }

    /// <summary>
    /// 이벤트 손님 확인
    /// </summary>
    void EventCustomerCheck(EeventCustomerType type)
    {
        switch (type)
        {
            case EeventCustomerType.GroupOrder:
                OrderTalk[0] = "아직 준비되지 않은 손님 유형입니다.";
                OrderTalk[1] = "아직 준비되지 않은 손님 유형입니다.";
                OrderTalk[2] = "아직 준비되지 않은 손님 유형입니다.";
                break;

            case EeventCustomerType.FoodCleanTester:
                #region 버튼 관련 전환
                CookingText.text = "네!";
                ReAskText.text = "아니요";
                CookingBtn.onClick.RemoveAllListeners();
                ReAskBtn.onClick.RemoveAllListeners();
                CookingBtn.onClick.AddListener(() =>
                {
                    CookingText.text = "매일이죠!";
                    ReAskText.text = "2주마다 합니다!";
                    OrderText.text = "";
                    TextTween = OrderText.DOText(OrderTalk[1], 0.05f * OrderTalk[1].Length);

                    CookingBtn.onClick.RemoveAllListeners();
                    CookingBtn.onClick.AddListener(() =>
                    {
                        CookingText.text = "가방 두고 가셨네 ㅎ";
                        ReAskText.text = "앗 먼지가...!";
                        OrderText.text = "";
                        TextTween = OrderText.DOText(OrderTalk[2], 0.05f * OrderTalk[2].Length);

                        CookingBtn.onClick.AddListener(() =>
                        {
                            CookingBtn.onClick.RemoveAllListeners();
                            print("다음 손님");
                        });
                    });
                });
                ReAskBtn.onClick.AddListener(() =>
                {
                    print("엔딩");
                });
                #endregion
                OrderTalk[0] = "식품 위생관리 위원회에서 나왔습니다. 잠시 주방을 검사해도 되겠습니까?";
                OrderTalk[1] = "흐음... 깨끗하군요! 혹시 요리를 하실때 청소는 몇주마다 하시나요?";
                OrderTalk[2] = "오... 대단하시네요! 그러면 이만 ㄱ... 어라?";
                break;

            case EeventCustomerType.SalesMan:
                OrderTalk[0] = "끼끼낄...나랑 거래 한번 해볼래..?키킼";
                CookingText.text = "네";
                ReAskText.text = "안삽니다. 가세요.";
                #region 버튼 관련 전환
                CookingBtn.onClick.RemoveAllListeners();
                ReAskBtn.onClick.RemoveAllListeners();
                CookingBtn.onClick.AddListener(() =>
                {
                    print("상점 창이 열린다");
                });
                ReAskBtn.onClick.AddListener(() =>
                {
                    print("다음 손님");
                });
                #endregion
                break;

            case EeventCustomerType.Thief:

                break;

            default:
                //RandomText에서 랜덤 외계어를 가져와서 주문 대화를 만듦
                OrderTalk[0] = "메인 재료 " + RT.FirstTexts[Random.Range(0, 20)] + " 부재료 얼만큼 " + RT.MiddleTexts[Random.Range(0, 20)] + " 조리방법 " + RT.LastTexts[Random.Range(0, 20)];
                OrderTalk[1] = "메인 재료 부재료 얼만큼 " + RT.FirstTexts[Random.Range(0, 20)] + " 조리방법 " + RT.LastTexts[Random.Range(0, 20)];
                OrderTalk[2] = "!메인 재료!로 !부재료! !얼만큼! 넣어서 !조리방법! 해주세요 '^'..";
                return;
        }
    }
    /// <summary>
    /// 주문 상호작용
    /// </summary>
    /// <returns></returns>
    IEnumerator Order()
    {
        CookingBtn.onClick.RemoveAllListeners();
        CookingBtn.onClick.AddListener(() =>
        {
            CookingScene.GetComponent<RectTransform>().DOAnchorPos3DY(0, 1).SetEase(Ease.OutBounce);

            CookingBtn.gameObject.SetActive(false);
            ReAskBtn.gameObject.SetActive(false);
            MemoBtn.gameObject.SetActive(true);
            MemoPaper.gameObject.SetActive(false);
            MemoPaperBackground.gameObject.SetActive(false);
            MemoPaperBackground.color = new Color(0, 0, 0, 0);
        });

        ReAskBtn.onClick.RemoveAllListeners();
        ReAskBtn.onClick.AddListener(() =>
        {
            if (reAskCount == 0)
            {
                memo[1] = "네?";
                OrderText.text = "";
                ReAskText.text = "뭐라고요?";
                memo[2] = OrderTalk[1];
                TextTween = OrderText.DOText(OrderTalk[1], 0.05f * OrderTalk[1].Length);
                reAskCount++;
            }
            else
            {
                memo[3] = "뭐라고요?";
                OrderText.text = "";
                memo[4] = OrderTalk[2];
                TextTween.Kill();
                OrderText.DOText(OrderTalk[2], 0.05f * OrderTalk[2].Length);

                ReAskText.text = "네?";
                ReAskBtn.gameObject.SetActive(false);
            }

        });

        OrderText.gameObject.SetActive(true);

        if (curCustomerType == (int)EcustomerType.Robot)
        {
            string temp2 = "!메인 재료!로 !부재료! !얼만큼! 넣어서 !조리방법! 해주세요";
            memo[0] = temp2;
            OrderText.DOText(temp2, 0.05f * temp2.Length);

            yield return new WaitForSeconds((temp2.Length * 0.05f) + 1f);
            CookingBtn.gameObject.SetActive(true);
        }
        else
        {
            //특별 손님인지 확인
            EventCustomerCheck((EeventCustomerType)(curCustomerType - System.Enum.GetValues(typeof(EeventCustomerType)).Length));

            memo[0] = OrderTalk[0];
            OrderText.DOText(OrderTalk[0], 0.05f * OrderTalk[0].Length);

            yield return new WaitForSeconds((OrderTalk[0].Length * 0.05f) + 1f);

            CookingBtn.gameObject.SetActive(true);
            ReAskBtn.gameObject.SetActive(true);
        }
    }

    public void MemoOn()
    {
        MemoPaper.gameObject.SetActive(true);
        MemoPaperBackground.gameObject.SetActive(true);
        MemoPaperBackground.DOFade(163 / 255f, 0.5f);
            MemoPaper.DOSizeDelta(new Vector2(650, 549), 0.3f).SetEase(Ease.OutQuint);
            MemoPaper.DOAnchorPos(new Vector2(-242.47f, 0), 0.3f).SetEase(Ease.OutQuint);
        for (int i = 0; i < MemoTexts.Length; i++)
        {
            MemoTexts[i].gameObject.SetActive(true);
            MemoTexts[i].text = memo[i];
            MemoTexts[i].rectTransform.DOAnchorPos(MemoOnTextSizes[i], 0.3f).SetEase(Ease.OutQuint);
        }
    }
    public void MemoOff()
    {
        for (int i = 0; i < MemoTexts.Length; i++)
        {
            MemoTexts[i].text = memo[i];

            MemoTexts[i].rectTransform.DOAnchorPos(MemoOffTextSizes[i], 0.3f).SetEase(Ease.OutQuint);
        }
        StartCoroutine(MemoTextOff());
        MemoPaper.DOSizeDelta(new Vector2(150, 120), 0.3f);
        MemoPaper.DOAnchorPos(new Vector2(-492.47f, 0), 0.3f);
        
        MemoPaperBackground.DOColor(new Color(0, 0, 0, 0), 0.3f);

        IEnumerator MemoTextOff()
        {
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < MemoTexts.Length; i++)
            {
                MemoTexts[i].gameObject.SetActive(false);
            }
            MemoPaper.gameObject.SetActive(false);
            MemoPaperBackground.gameObject.SetActive(false);
        }
    }
}
