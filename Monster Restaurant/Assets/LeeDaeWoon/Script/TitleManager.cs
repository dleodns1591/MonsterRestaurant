using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] Button startBtn;

    [Header("컨텐츠")]
    [SerializeField] GameObject content;
    [SerializeField] Button contentBtn;
    bool isContent = false;

    [Header("설정")]
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
        // 시작 버튼을 눌렀을 경우
        startBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });

        // 컨텐츠 버튼을 눌렀을 경우
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

        // 배경음 버튼을 눌렀을 시
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

        // 효과음 버튼을 눌렀을 시
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

        // 진동 버튼을 눌렀을 시
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

        // 알림 버튼을 눌렀을 시
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
