using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
class Guide
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
    public Sprite story;
}

public class CustomerGuide : MonoBehaviour
{
    [SerializeField] Image clickBox;
    [SerializeField] Image fade;
    [SerializeField] CanvasGroup guideWindow;

    [SerializeField] Button guideBtn;
    [SerializeField] Button cancleBtn;
    [SerializeField] Button leftArrowBtn;
    [SerializeField] Button rightBtn;
    [SerializeField] Button generalBtn;
    [SerializeField] Button eventBtn;

    [Header("�մ� �ڽ�")]
    [SerializeField] GameObject customerBox;
    [SerializeField] Button customerBoxBtn;

    [SerializeField] List<Guide> generalList = new List<Guide>();
    [SerializeField] List<Guide> eventList = new List<Guide>();

    bool isArrow = false;
    bool isCustomerCheck = false;

    void Start()
    {
        Btns();

        CustomerBox(eventList);
        CustomerBox(generalList);
    }

    void Update()
    {

    }

    // �Ϲ� �մ� �Ǵ� �̺�Ʈ �մ� ����
    void CustomerBox(List<Guide> customerList)
    {
        for (int i = 0; i < customerBox.transform.childCount; i++)
            customerBox.transform.GetChild(i).GetComponent<Image>().sprite = customerList[i].profile;
    }

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

        if (!isArrow)
        {
            int saveCustomer = customerList.Count;

            for (int i = 0; i < 4; i++)
            {
                customerList.Insert(0, customerList[saveCustomer - 1]);
                customerList.RemoveAt(saveCustomer);
            }
        }

        else
        {
            for (int i = 0; i < 4; i++)
            {
                customerList.Add(customerList[0]);
                customerList.RemoveAt(0);
            }
        }
        CustomerBox(customerList);
    }

    // ��ư��
    void Btns()
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
        });

        // �̺�Ʈ �մ� ��ư�� ������ ��
        eventBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            clickBox.transform.localPosition = eventBtn.transform.localPosition;

            isCustomerCheck = true;
            CustomerBox(eventList);
        });
    }
}
