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
        [Header("일반 손님")]
        Spinach,        // 시금치
        Sdh210224,      // sdh210224
        Zeto,           // 제토
        ChrisTheGhost,  // 유령 크리스
        Quasar,         // 퀘이사
        Axolonaut,      // 아홀로노트
        Quantum,        // 2차 양자화
        Hellios,        // 헬리오스
        FSM,            // 날아다니는 스파케티 괴물
        Garbage,        // 쓰레기
        Joker,          // 조커

        [Header("이벤트 손님")]
        Stella,         // 스텔라
        Sock,           // 양말 아저씨
        Florian,        // 플로리안
        Dopey,          // 도주
        Receid,         // 리시드
        H30122,         // H-30122

        [Header("미정")]
        Undetermined,   // 미정
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

    [Header("손님 박스")]
    [SerializeField] GameObject customerBox;
    public GameObject customerBoxParent;

    public List<Guide> generalList = new List<Guide>();
    public List<Guide> eventList = new List<Guide>();

    public bool isCustomerCheck = false;
    bool isArrow = false;

    [Header("손님 스토리")]
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

    // 일반 손님 또는 이벤트 손님 적용
    void CustomerBox(List<Guide> customerList)
    {
        for (int i = 0; i < customerBox.transform.childCount; i++)
            customerBox.transform.GetChild(i).GetComponent<Image>().sprite = customerList[i].profile;
    }

    #region 손님 도감 버튼 상호작용 관련 코드

    // 일반 손님 클릭
    void GeneralClick()
    {
        clickBox.transform.localPosition = generalBtn.transform.localPosition;

        isCustomerCheck = false;
        CustomerBox(generalList);
    }

    // 화살표 클릭
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

    // 초기화
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

    // 아래 화살표 페이드
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

    // 아래 화살표 이동
    void BottomArrowMove() => bottomArrow.transform.DOLocalMoveY(-350, 0.8f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InBack).SetUpdate(true);


    // 버튼들
    void CustomerBoxBtns()
    {
        // 손님 가이드 버튼을 눌렀을 시
        guideBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            Time.timeScale = 0;

            guideWindow.alpha = 1;
            fade.DOFade(0.5f, 0).SetEase(Ease.Linear).SetUpdate(true);
            guideWindow.gameObject.SetActive(true);

            GeneralClick(); // 일반 손님 클릭
            CustomerReset(); // 일반 손님과 이벤트 손님 순서 초기화

        });

        // 취소 버튼을 눌렀을 시
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

        // 왼쪽 방향 버튼을 눌렀을 시
        leftArrowBtn.onClick.AddListener(() =>
        {
            if (!isCustomerCheck) // 일반 손님
                ArrowClick(generalList, !isArrow);

            else // 이벤트 손님
                ArrowClick(eventList, !isArrow);
        });


        // 오른쪽 방향 버튼을 눌렀을 시
        rightBtn.onClick.AddListener(() =>
        {
            if (!isCustomerCheck) // 일반 손님
                ArrowClick(generalList, isArrow);

            else // 이벤트 손님
                ArrowClick(eventList, isArrow);
        });

        // 일반 손님 버튼을 눌렀을 시
        generalBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            GeneralClick();
            CustomerReset(); // 일반 손님과 이벤트 손님 순서 초기화
        });

        // 이벤트 손님 버튼을 눌렀을 시
        eventBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            clickBox.transform.localPosition = eventBtn.transform.localPosition;

            isCustomerCheck = true;
            CustomerBox(eventList);
            CustomerReset(); // 일반 손님과 이벤트 손님 순서 초기화
        });
    }

    void CustomerStoryBtns()
    {
        // 돌아가기 버튼을 눌렀을 시
        backBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            customerBoxParent.SetActive(true);
            customerStoryParent.SetActive(false);
        });
    }
    #endregion
}
