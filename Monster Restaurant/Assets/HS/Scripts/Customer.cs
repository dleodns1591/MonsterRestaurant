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
    [SerializeField, Tooltip("��� ���� ���̱� �ϱ� ����")]
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
    /// ó�� ������� �ֹ������� �̵��ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator Moving()
    {
        yield return new WaitForSeconds(fadeInOut.fadeTime);

        float delayTime = 0.5f;

        Image CustomerImg = gameObject.GetComponent<Image>();
        //�������� �մ� �̱�
        curCustomerType = Random.Range(0, System.Enum.GetValues(typeof(EcustomerType)).Length);
        CustomerImg.sprite = GuestDefualts[(int)(EcustomerType)curCustomerType];

        //�Ϲ� �մ� �߿� �̺�Ʈ �մ� ��찡 ���� ���
        if (curCustomerType == System.Enum.GetValues(typeof(EcustomerType)).Length - 1)
        {
            //�մ� ���� �������Ͱ� �̺�Ʈ �մ�

            //�̺�Ʈ �մ� �̱�
            curCustomerType = Random.Range(System.Enum.GetValues(typeof(EcustomerType)).Length - 1,
                System.Enum.GetValues(typeof(EcustomerType)).Length + System.Enum.GetValues(typeof(EeventCustomerType)).Length - 1);
            print(curCustomerType);
            CustomerImg.sprite = GuestDefualts[curCustomerType];
        }


        //�͸����� ������
        for (int i = 0; i < SlowMovingPos.Length; i++)
        {
            if (i != SlowMovingPos.Length - 1)
                transform.DOMove(SlowMovingPos[i].position, delayTime);
            else
                transform.DOMove(SlowMovingPos[i].position, 0.25f);

            yield return new WaitForSeconds(delayTime);
        }

        //������ �̵��Ҷ� ���� ����
        playerDetect = true;

        //������ �̵�
        transform.DOMove(FastMovingPos.position, delayTime);
        CustomerImg.DOColor(new Color(1, 1, 1, 0), delayTime);

        yield return new WaitForSeconds(1.5f);

        //�ֹ� ���̺��� �̵�
        gameObject.transform.parent = BackgroundCanvas.transform;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 1000);
        CustomerImg.sprite = GuestDefualts[(int)(EcustomerType)curCustomerType];
        transform.position = OrderPos[0].position;
        transform.DOMove(OrderPos[1].position, delayTime);
        CustomerImg.DOColor(new Color(1, 1, 1, 1), delayTime);

        yield return new WaitForSeconds(delayTime);

        //�ֹ�����
        playerDetect = false;
        StartCoroutine(Order());

    }

    /// <summary>
    /// �̺�Ʈ �մ� Ȯ��
    /// </summary>
    void EventCustomerCheck(EeventCustomerType type)
    {
        switch (type)
        {
            case EeventCustomerType.GroupOrder:
                OrderTalk[0] = "���� �غ���� ���� �մ� �����Դϴ�.";
                OrderTalk[1] = "���� �غ���� ���� �մ� �����Դϴ�.";
                OrderTalk[2] = "���� �غ���� ���� �մ� �����Դϴ�.";
                break;

            case EeventCustomerType.FoodCleanTester:
                #region ��ư ���� ��ȯ
                CookingText.text = "��!";
                ReAskText.text = "�ƴϿ�";
                CookingBtn.onClick.RemoveAllListeners();
                ReAskBtn.onClick.RemoveAllListeners();
                CookingBtn.onClick.AddListener(() =>
                {
                    CookingText.text = "��������!";
                    ReAskText.text = "2�ָ��� �մϴ�!";
                    OrderText.text = "";
                    TextTween = OrderText.DOText(OrderTalk[1], 0.05f * OrderTalk[1].Length);

                    CookingBtn.onClick.RemoveAllListeners();
                    CookingBtn.onClick.AddListener(() =>
                    {
                        CookingText.text = "���� �ΰ� ���̳� ��";
                        ReAskText.text = "�� ������...!";
                        OrderText.text = "";
                        TextTween = OrderText.DOText(OrderTalk[2], 0.05f * OrderTalk[2].Length);

                        CookingBtn.onClick.AddListener(() =>
                        {
                            CookingBtn.onClick.RemoveAllListeners();
                            print("���� �մ�");
                        });
                    });
                });
                ReAskBtn.onClick.AddListener(() =>
                {
                    print("����");
                });
                #endregion
                OrderTalk[0] = "��ǰ �������� ����ȸ���� ���Խ��ϴ�. ��� �ֹ��� �˻��ص� �ǰڽ��ϱ�?";
                OrderTalk[1] = "����... �����ϱ���! Ȥ�� �丮�� �ϽǶ� û�Ҵ� ���ָ��� �Ͻó���?";
                OrderTalk[2] = "��... ����Ͻó׿�! �׷��� �̸� ��... ���?";
                break;

            case EeventCustomerType.SalesMan:
                OrderTalk[0] = "������...���� �ŷ� �ѹ� �غ���..?Ű�f";
                CookingText.text = "��";
                ReAskText.text = "�Ȼ�ϴ�. ������.";
                #region ��ư ���� ��ȯ
                CookingBtn.onClick.RemoveAllListeners();
                ReAskBtn.onClick.RemoveAllListeners();
                CookingBtn.onClick.AddListener(() =>
                {
                    print("���� â�� ������");
                });
                ReAskBtn.onClick.AddListener(() =>
                {
                    print("���� �մ�");
                });
                #endregion
                break;

            case EeventCustomerType.Thief:

                break;

            default:
                //RandomText���� ���� �ܰ� �����ͼ� �ֹ� ��ȭ�� ����
                OrderTalk[0] = "���� ��� " + RT.FirstTexts[Random.Range(0, 20)] + " ����� ��ŭ " + RT.MiddleTexts[Random.Range(0, 20)] + " ������� " + RT.LastTexts[Random.Range(0, 20)];
                OrderTalk[1] = "���� ��� ����� ��ŭ " + RT.FirstTexts[Random.Range(0, 20)] + " ������� " + RT.LastTexts[Random.Range(0, 20)];
                OrderTalk[2] = "!���� ���!�� !�����! !��ŭ! �־ !�������! ���ּ��� '^'..";
                return;
        }
    }
    /// <summary>
    /// �ֹ� ��ȣ�ۿ�
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
                memo[1] = "��?";
                OrderText.text = "";
                ReAskText.text = "������?";
                memo[2] = OrderTalk[1];
                TextTween = OrderText.DOText(OrderTalk[1], 0.05f * OrderTalk[1].Length);
                reAskCount++;
            }
            else
            {
                memo[3] = "������?";
                OrderText.text = "";
                memo[4] = OrderTalk[2];
                TextTween.Kill();
                OrderText.DOText(OrderTalk[2], 0.05f * OrderTalk[2].Length);

                ReAskText.text = "��?";
                ReAskBtn.gameObject.SetActive(false);
            }

        });

        OrderText.gameObject.SetActive(true);

        if (curCustomerType == (int)EcustomerType.Robot)
        {
            string temp2 = "!���� ���!�� !�����! !��ŭ! �־ !�������! ���ּ���";
            memo[0] = temp2;
            OrderText.DOText(temp2, 0.05f * temp2.Length);

            yield return new WaitForSeconds((temp2.Length * 0.05f) + 1f);
            CookingBtn.gameObject.SetActive(true);
        }
        else
        {
            //Ư�� �մ����� Ȯ��
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
