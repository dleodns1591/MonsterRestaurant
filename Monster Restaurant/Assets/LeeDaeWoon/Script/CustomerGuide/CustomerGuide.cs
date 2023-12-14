using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class Guide
{
    public enum ECustomer
    {
        [Header("�Ϲ� �մ�")]
        Spinach,        // �ñ�ġ
        Sdh210224,      // sdh210224
        Zeto,           // ����
        ChrisTheGhost,  // ���� ũ����
        Quasar,         // ���̻�
        Axolonaut,      // ��Ȧ�γ�Ʈ
        Quantum,        // 2�� ����ȭ
        Hellios,        // �︮����
        FSM,            // ���ƴٴϴ� ������Ƽ ����
        Garbage,        // ������
        Joker,          // ��Ŀ

        [Header("�̺�Ʈ �մ�")]
        Stella,         // ���ڶ�
        Sock,           // �縻 ������
        Florian,        // �÷θ���
        Dopey,          // ����
        Receid,         // ���õ�
        H30122,         // H-30122

        [Header("����")]
        Undetermined,   // ����
    }

    public string name;
    public ECustomer eCustomer;
    public Sprite profile;
    public Sprite storyKo;
    public Sprite storyEn;
}

public class CustomerGuide : MonoBehaviour
{
    public static CustomerGuide instance;
    void Awake() => instance = this;

    [SerializeField] Image clickBox;
    [SerializeField] Image fade;
    [SerializeField] CanvasGroup guideWindow;

    [Space(5)]
    [SerializeField] Button guideBtn;
    [SerializeField] Button cancleBtn;
    [SerializeField] Button leftArrowBtn;
    [SerializeField] Button rightBtn;
    [SerializeField] Button generalBtn;
    [SerializeField] Button eventBtn;

    [Header("�մ� �ڽ�")]
    [SerializeField] GameObject customerBox;
    public GameObject customerBoxParent;

    public List<Guide> generalList = new List<Guide>();
    public List<Guide> eventList = new List<Guide>();

    public bool isCustomerCheck = false;
    bool isArrow = false;

    [Header("�մ� ���丮")]
    public GameObject customerStoryParent;
    public Image story;
    public RectTransform storyContant;
    public Button backBtn;
    [SerializeField] Image bottomArrow;

    void Start()
    {
        CustomerBoxBtns();
        CustomerStoryBtns();

        CustomerBox(eventList);
        CustomerBox(generalList);
        BottomArrowMove();
    }

    void Update()
    {
        BottomArrowFade();
    }

    // �Ϲ� �մ� �Ǵ� �̺�Ʈ �մ� ����
    void CustomerBox(List<Guide> customerList)
    {
        for (int i = 0; i < customerBox.transform.childCount; i++)
            customerBox.transform.GetChild(i).GetComponent<Image>().sprite = customerList[i].profile;
    }

    #region �մ� ���� ��ư ��ȣ�ۿ� ���� �ڵ�

    // �Ϲ� �մ� Ŭ��
    void GeneralClick()
    {
        clickBox.transform.localPosition = generalBtn.transform.localPosition;

        isCustomerCheck = false;
        CustomerBox(generalList);
    }

    // ȭ��ǥ Ŭ��
    void ArrowClick(List<Guide> customerList, bool isArrow)
    {
        SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

        for (int i = 0; i < 4; i++)
        {
            if (!isArrow)
            {
                customerList.Add(customerList[0]);
                customerList.RemoveAt(0);
            }

            else
            {
                int saveCustomer = customerList.Count;

                customerList.Insert(0, customerList[saveCustomer - 1]);
                customerList.RemoveAt(saveCustomer);
            }
        }
        CustomerBox(customerList);
    }

    // �ʱ�ȭ
    void CustomerReset()
    {
        if (!isCustomerCheck)
        {
            for (int i = 0; i < generalList.Count; i++)
            {
                if (generalList[0].eCustomer != Guide.ECustomer.Spinach)
                    ArrowClick(generalList, isArrow);
            }
        }

        else
        {
            for (int i = 0; i < eventList.Count; i++)
            {
                if (eventList[0].eCustomer != Guide.ECustomer.Stella)
                    ArrowClick(eventList, isArrow);
            }
        }
    }

    // �Ʒ� ȭ��ǥ ���̵�
    void BottomArrowFade()
    {
        if (customerStoryParent.activeSelf)
        {
            if (storyContant.transform.localPosition.y < 10)
                bottomArrow.DOFade(1, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
            else
                bottomArrow.DOFade(0, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
        }
    }

    // �Ʒ� ȭ��ǥ �̵�
    void BottomArrowMove() => bottomArrow.transform.DOLocalMoveY(-350, 0.8f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InBack).SetUpdate(true);


    // ��ư��
    void CustomerBoxBtns()
    {
        // �մ� ���̵� ��ư�� ������ ��
        guideBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            Time.timeScale = 0;

            guideWindow.alpha = 1;
            fade.DOFade(0.5f, 0).SetEase(Ease.Linear).SetUpdate(true);
            guideWindow.gameObject.SetActive(true);

            GeneralClick(); // �Ϲ� �մ� Ŭ��
            CustomerReset(); // �Ϲ� �մ԰� �̺�Ʈ �մ� ���� �ʱ�ȭ

        });

        // ��� ��ư�� ������ ��
        cancleBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            guideWindow.DOFade(0, 0.5f).SetEase(Ease.Linear).SetUpdate(true);
            fade.DOFade(0, 0.5f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                Time.timeScale = 1;

                guideWindow.gameObject.SetActive(false);

                customerBoxParent.SetActive(true);
                customerStoryParent.SetActive(false);
            });
        });

        // ���� ���� ��ư�� ������ ��
        leftArrowBtn.onClick.AddListener(() =>
        {
            if (!isCustomerCheck) // �Ϲ� �մ�
                ArrowClick(generalList, !isArrow);

            else // �̺�Ʈ �մ�
                ArrowClick(eventList, !isArrow);
        });


        // ������ ���� ��ư�� ������ ��
        rightBtn.onClick.AddListener(() =>
        {
            if (!isCustomerCheck) // �Ϲ� �մ�
                ArrowClick(generalList, isArrow);

            else // �̺�Ʈ �մ�
                ArrowClick(eventList, isArrow);
        });

        // �Ϲ� �մ� ��ư�� ������ ��
        generalBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            GeneralClick();
            CustomerReset(); // �Ϲ� �մ԰� �̺�Ʈ �մ� ���� �ʱ�ȭ
        });

        // �̺�Ʈ �մ� ��ư�� ������ ��
        eventBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            clickBox.transform.localPosition = eventBtn.transform.localPosition;

            isCustomerCheck = true;
            CustomerBox(eventList);
            CustomerReset(); // �Ϲ� �մ԰� �̺�Ʈ �մ� ���� �ʱ�ȭ
        });
    }

    void CustomerStoryBtns()
    {
        // ���ư��� ��ư�� ������ ��
        backBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            customerBoxParent.SetActive(true);
            customerStoryParent.SetActive(false);
        });
    }
    #endregion
}
