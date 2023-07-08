using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using DG.Tweening;

public class SettingSystem : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] CanvasGroup settingGroup;
    [SerializeField] GameObject settingWidnow;
    [SerializeField] Button settingCancleBtn;
    [SerializeField] Button bgmBtn;
    [SerializeField] Button sfxBtn;
    [SerializeField] Button vibrationBtn;
    [SerializeField] Button notificationBtn;

    [SerializeField] Sprite bgmCancle;
    [SerializeField] Sprite sfxCancle;
    [SerializeField] Sprite vibarationCancle;
    [SerializeField] Sprite notificationCancle;

    bool isBGM = false;
    bool isSFX = false;
    bool isVirbration = false;
    bool isNotification = false;

    [Header("언어")]
    [SerializeField] CanvasGroup languageWindow;
    [SerializeField] Button languageBtn;
    [SerializeField] Button languageCancleBtn;
    [SerializeField] Button englishBtn;
    [SerializeField] Button koreaBtn;

    bool isLanguage = true;

    void Start()
    {
        SettingBtns();
        LanguageBtns();
    }

    void Update()
    {

    }

    void LanguageSetting(int index) =>
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];

    void SettingBtns()
    {
        Sprite bgmBasic = bgmBtn.GetComponent<Image>().sprite;
        Sprite sfxBasic = sfxBtn.GetComponent<Image>().sprite;
        Sprite vibrationBasic = vibrationBtn.GetComponent<Image>().sprite;
        Sprite notificationBasic = notificationBtn.GetComponent<Image>().sprite;

        settingCancleBtn.onClick.AddListener(() =>
        {
            settingGroup.DOFade(0, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                settingWidnow.transform.DOLocalMoveY(1050, 0).SetEase(Ease.Linear);
                settingGroup.gameObject.SetActive(false);
            });
        });

        // 배경음 버튼을 눌렀을 시
        bgmBtn.onClick.AddListener(() =>
        {
            if (!isBGM)
            {
                isBGM = true;
                bgmBtn.GetComponent<Image>().sprite = bgmCancle;
            }

            else
            {
                isBGM = false;
                bgmBtn.GetComponent<Image>().sprite = bgmBasic;
            }

        });

        // 효과음 버튼을 눌렀을 시
        sfxBtn.onClick.AddListener(() =>
        {

            if (!isSFX)
            {
                isSFX = true;
                sfxBtn.GetComponent<Image>().sprite = sfxCancle;
            }

            else
            {
                isSFX = false;
                sfxBtn.GetComponent<Image>().sprite = sfxBasic;
            }
        });

        // 진동 버튼을 눌렀을 시
        vibrationBtn.onClick.AddListener(() =>
        {

            if (!isVirbration)
            {
                isVirbration = true;
                vibrationBtn.GetComponent<Image>().sprite = vibarationCancle;
            }

            else
            {
                isVirbration = false;
                vibrationBtn.GetComponent<Image>().sprite = vibrationBasic;
            }
        });

        // 알림 버튼을 눌렀을 시
        notificationBtn.onClick.AddListener(() =>
        {

            if (!isNotification)
            {
                isNotification = true;
                notificationBtn.GetComponent<Image>().sprite = notificationCancle;
            }

            else
            {
                isNotification = false;
                notificationBtn.GetComponent<Image>().sprite = notificationBasic;
            }
        });
    }

    void LanguageBtns()
    {
        koreaBtn.GetComponent<Image>().color = Color.gray;

        languageBtn.onClick.AddListener(() =>
        {
            languageWindow.alpha = 1;
            languageWindow.gameObject.SetActive(true);
        });

        languageCancleBtn.onClick.AddListener(() =>
        {
            languageWindow.DOFade(0, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                languageWindow.gameObject.SetActive(false);
            });
        });

        englishBtn.onClick.AddListener(() =>
        {
            if (isLanguage)
            {
                isLanguage = false;
                englishBtn.GetComponent<Image>().color = Color.gray;
                koreaBtn.GetComponent<Image>().color = Color.white;
                LanguageSetting(0);
            }
        });

        koreaBtn.onClick.AddListener(() =>
        {
            if (!isLanguage)
            {
                isLanguage = true;
                koreaBtn.GetComponent<Image>().color = Color.gray;
                englishBtn.GetComponent<Image>().color = Color.white;
                LanguageSetting(1);
            }
        });
    }
}
