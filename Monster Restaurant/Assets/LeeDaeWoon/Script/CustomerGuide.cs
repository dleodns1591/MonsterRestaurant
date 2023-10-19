using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    [SerializeField] List<Sprite> generalList = new List<Sprite>();
    [SerializeField] List<Sprite> eventList = new List<Sprite>();
    bool isCustomerCheck = false;

    void Start()
    {
        Btns();
    }

    void Update()
    {

    }

    void Btns()
    {
        // �մ� ���̵� ��ư�� ������ ��
        guideBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 0;

            guideWindow.alpha = 1;
            fade.DOFade(0.5f, 0).SetEase(Ease.Linear).SetUpdate(true);
            guideWindow.gameObject.SetActive(true);
        });

        // ��� ��ư�� ������ ��
        cancleBtn.onClick.AddListener(() =>
        {
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
            if(!isCustomerCheck)
            {

            }

            else
            {

            }
        });


        // ������ ���� ��ư�� ������ ��
        leftArrowBtn.onClick.AddListener(() =>
        {

        });

        // �Ϲ� �մ� ��ư�� ������ ��
        generalBtn.onClick.AddListener(() =>
        {
            clickBox.transform.localPosition = generalBtn.transform.localPosition;

            isCustomerCheck = false;
            CustomerBox(generalList);
        });

        // �̺�Ʈ �մ� ��ư�� ������ ��
        eventBtn.onClick.AddListener(() =>
        {
            clickBox.transform.localPosition = eventBtn.transform.localPosition;

            isCustomerCheck = true;
            CustomerBox(eventList);
        });
    }

    void CustomerBox(List<Sprite> customerList)
    {
        for (int i = 0; i < customerBox.transform.childCount; i++)
            customerBox.transform.GetChild(i).GetComponent<Image>().sprite = customerList[i];
    }
}
