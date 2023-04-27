using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] Button startBtn;

    [Header("������")]
    [SerializeField] GameObject content;
    [SerializeField] Button contentBtn;
    bool isContent = false;

    [Header("����")]
    [SerializeField] CanvasGroup settingWindow;
    [SerializeField] Button settingBtn;
    [SerializeField] Button settingCancleBtn;
    [SerializeField] Button bgmBtn;
    [SerializeField] Button sfxBtn;
    [SerializeField] Button vibrationBtn;
    [SerializeField] Button notificationBtn;

    bool isBGM = false;
    bool isSFX = false;
    bool isVirbration = false;
    bool isNotification = false;

    void Start()
    {
        Btns();
    }

    void Update()
    {
        
    }

    void Btns()
    {
        // ���� ��ư�� ������ ���
        startBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });

        // ������ ��ư�� ������ ���
        contentBtn.onClick.AddListener(() =>
        {
            if(!isContent)
            {
                isContent = true;
                for (int i = 1; i < 5; i++)
                    content.transform.GetChild(i -1).DOLocalMoveY(i * -200, 0.8f).SetEase(Ease.OutBack);
            }

            else
            {
                isContent = false;
                for (int i = 1; i < 5; i++)
                    content.transform.GetChild(i - 1).DOLocalMoveY(0, 0.8f).SetEase(Ease.InBack);
            }
        });

        settingBtn.onClick.AddListener(() =>
        {
            settingWindow.alpha = 1;
            settingWindow.gameObject.SetActive(true);
        });

        settingCancleBtn.onClick.AddListener(() =>
        {
            settingWindow.DOFade(0, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                settingWindow.gameObject.SetActive(false);
            });
        });

        // ����� ��ư�� ������ ��
        bgmBtn.onClick.AddListener(() =>
        {
            if(!isBGM)
            {
                isBGM = true;
                bgmBtn.GetComponent<Image>().color = Color.gray;
                Debug.Log("BGM OFF");
            }

            else
            {
                isBGM = false;
                bgmBtn.GetComponent<Image>().color = Color.white;
                Debug.Log("BGM ON");
            }

        });

        // ȿ���� ��ư�� ������ ��
        sfxBtn.onClick.AddListener(() =>
        {
            if (!isSFX)
            {
                isSFX = true;
                sfxBtn.GetComponent<Image>().color = Color.gray;
                Debug.Log("SFX OFF");
            }

            else
            {
                isSFX = false;
                sfxBtn.GetComponent<Image>().color = Color.white;
                Debug.Log("SFX ON");
            }
        });

        // ���� ��ư�� ������ ��
        vibrationBtn.onClick.AddListener(() =>
        {
            if (!isVirbration)
            {
                isVirbration = true;
                vibrationBtn.GetComponent<Image>().color = Color.gray;
                Debug.Log("vibration OFF");
            }

            else
            {
                isVirbration = false;
                vibrationBtn.GetComponent<Image>().color = Color.white;
                Debug.Log("vibration ON");
            }
        });

        // �˸� ��ư�� ������ ��
        notificationBtn.onClick.AddListener(() =>
        {
            if (!isNotification)
            {
                isNotification = true;
                notificationBtn.GetComponent<Image>().color = Color.gray;
                Debug.Log("Notification OFF");
            }

            else
            {
                isNotification = false;
                notificationBtn.GetComponent<Image>().color = Color.white;
                Debug.Log("Notification ON");
            }
        });
    }
}
