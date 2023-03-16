using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public enum EcustomerType
{
    Alien,
    Hyena,
    Robot,
    GroupOrder
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
    private FadeInOut fadeInOut;
    [SerializeField]
    private Text OrderText;
    [SerializeField]
    private RandomText RT;
    [SerializeField]
    private Button CookingBtn, ReAskBtn;

    bool playerDetect = false;

    int curCustomerType;
    int reAskCount = 0;

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

    IEnumerator Order()
    {
        OrderText.gameObject.SetActive(true);
        string temp = "���� ��� " + RT.FirstTexts[Random.Range(0, 20)] + " ����� ��ŭ " + RT.MiddleTexts[Random.Range(0, 20)] + " ������� " + RT.LastTexts[Random.Range(0, 20)];
        OrderText.DOText(temp, 0.05f * temp.Length);

        yield return new WaitForSeconds((temp.Length * 0.05f) + 1f);

        CookingBtn.onClick.AddListener(() =>
        {
            CookingScene.GetComponent<RectTransform>().DOAnchorPos3DY(0, 1).SetEase(Ease.OutBounce);
        });
        ReAskBtn.onClick.AddListener(() =>
        {
            if (reAskCount == 0)
            {
                OrderText.text = "";
                string ReOrder = "���� ��� ����� ��ŭ " + RT.FirstTexts[Random.Range(0, 20)] + " ������� " + RT.LastTexts[Random.Range(0, 20)];
                OrderText.DOText(ReOrder, 0.05f * ReOrder.Length);
                reAskCount++;
            }
            else
            {
                OrderText.text = "";
                string LastOrder = "!���� ���!�� !�����! !��ŭ! �־ !�������! ���ּ��� '^'..";
                OrderText.DOText(LastOrder, 0.05f * LastOrder.Length);

                ReAskBtn.gameObject.SetActive(false);
                //CookingBtn.transform.position
            }
            
        });
        CookingBtn.gameObject.SetActive(true);
        ReAskBtn.gameObject.SetActive(true);
    }
}
