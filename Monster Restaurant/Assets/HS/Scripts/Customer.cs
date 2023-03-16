using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

public enum EcustomerType
{
    Alien,
    Hyena,
    Robot,
    Dragon,

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
    private Transform FastMovingPos, MemoPivot;
    [SerializeField]
    private Sprite[] GuestDefualts;
    [SerializeField, Tooltip("배경 위에 보이기 하기 위한")]
    private GameObject BackgroundCanvas;
    [SerializeField]
    private GameObject CookingScene, MemoPaper;
    [SerializeField]
    private FadeInOut fadeInOut;
    [SerializeField]
    private UIText OrderText, ReAskText;
    [SerializeField]
    private UIText[] MemoTexts;
    [SerializeField]
    private RandomText RT;
    [SerializeField]
    private Button CookingBtn, ReAskBtn, MemoBtn;

    private bool playerDetect = false;
    private int curCustomerType;
    private int reAskCount = 0;
    private string[] memo = new string[5];
    private Tween TextTween;


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
    /// 주문 상호작용
    /// </summary>
    /// <returns></returns>
    IEnumerator Order()
    {
        CookingBtn.onClick.AddListener(() =>
        {
            CookingScene.GetComponent<RectTransform>().DOAnchorPos3DY(0, 1).SetEase(Ease.OutBounce);

            CookingBtn.gameObject.SetActive(false);
            ReAskBtn.gameObject.SetActive(false);
            MemoBtn.gameObject.SetActive(true);
                    MemoPaper.SetActive(false);
            MemoBtn.onClick.AddListener(() =>
            {
                for (int i = 0; i < MemoTexts.Length; i++)
                {
                    MemoPaper.SetActive(true);
                    MemoTexts[i].text = memo[i];
                    MemoPaper.transform.DOScale(new Vector3(1,1), 0.6f).SetEase(Ease.OutQuint);
                }
            });
        });

        ReAskBtn.onClick.AddListener(() =>
        {
            if (reAskCount == 0)
            {
                memo[1] = "네?";
                OrderText.text = "";
                ReAskText.text = "뭐라고요?";
                string ReOrder = "메인 재료 부재료 얼만큼 " + RT.FirstTexts[Random.Range(0, 20)] + " 조리방법 " + RT.LastTexts[Random.Range(0, 20)];
                memo[2] = ReOrder;
                TextTween = OrderText.DOText(ReOrder, 0.05f * ReOrder.Length);
                reAskCount++;
            }
            else
            {
                memo[3] = "뭐라고요?";
                OrderText.text = "";
                string LastOrder = "!메인 재료!로 !부재료! !얼만큼! 넣어서 !조리방법! 해주세요 '^'..";
                memo[4] = LastOrder;
                TextTween.Kill();
                OrderText.DOText(LastOrder, 0.05f * LastOrder.Length);

                ReAskText.text = "네?";
                ReAskBtn.gameObject.SetActive(false);
                //CookingBtn.transform.position
            }

        });

        //memo = new string[memo.Length];
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
            //RandomText에서 랜덤 외계어를 가져와서 주문 대화를 만듦
            string temp = "메인 재료 " + RT.FirstTexts[Random.Range(0, 20)] + " 부재료 얼만큼 " + RT.MiddleTexts[Random.Range(0, 20)] + " 조리방법 " + RT.LastTexts[Random.Range(0, 20)];
            memo[0] = temp;
            OrderText.DOText(temp, 0.05f * temp.Length);

            yield return new WaitForSeconds((temp.Length * 0.05f) + 1f);

            CookingBtn.gameObject.SetActive(true);
            ReAskBtn.gameObject.SetActive(true);
        }
    }
}
