using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingSystem : MonoBehaviour
{
    public static SettingSystem instance;
    void Awake() => instance = this;

    [Header("설정")]
    [SerializeField] CanvasGroup settingGroup;
    [SerializeField] GameObject settingWidnow;
    [SerializeField] Button settingCancleBtn;
    [SerializeField] Button bgmBtn;
    [SerializeField] Button sfxBtn;
    //[SerializeField] Button vibrationBtn;
    //[SerializeField] Button notificationBtn;

    [SerializeField] Sprite bgmCancle;
    [SerializeField] Sprite sfxCancle;
    //[SerializeField] Sprite vibarationCancle;
    //[SerializeField] Sprite notificationCancle;

    public GameObject amongSetting;
    public GameObject rightSetting;

    bool isBGM = false;
    bool isSFX = false;
    //bool isVirbration = false;
    //bool isNotification = false;

    [Header("언어")]
    [SerializeField] Button languageBtn;
    [SerializeField] Button englishBtn;
    [SerializeField] Button koreaBtn;
    [SerializeField] Button backBtn;

    bool isLanguage = true;

    [Header("크레딧")]
    [SerializeField] Button creditBtn;
    [SerializeField] Button creditCancleBtn;
    [SerializeField] CanvasGroup creditGroup;
    [SerializeField] GameObject creditWindow;

    void Start()
    {
        SettingBtns();
        LanguageBtn();
    }

    void Update()
    {

    }

    void SettingBtns()
    {
        Sprite bgmBasic = bgmBtn.GetComponent<Image>().sprite;
        Sprite sfxBasic = sfxBtn.GetComponent<Image>().sprite;
        //Sprite vibrationBasic = vibrationBtn.GetComponent<Image>().sprite;
        //Sprite notificationBasic = notificationBtn.GetComponent<Image>().sprite;

        settingCancleBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            settingGroup.DOFade(0, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                settingWidnow.transform.DOLocalMoveY(1050, 0).SetEase(Ease.Linear);
                settingGroup.gameObject.SetActive(false);
            });
        });

        // 배경음 버튼을 눌렀을 시
        bgmBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            if (SoundManager.instance.isBGMCheck)
            {
                SoundManager.instance.isBGMCheck = false;
                bgmBtn.GetComponent<Image>().sprite = bgmCancle;
            }

            else
            {
                SoundManager.instance.PlaySoundClip("Title_bgm", SoundType.BGM);

                SoundManager.instance.isBGMCheck = true;
                bgmBtn.GetComponent<Image>().sprite = bgmBasic;
            }

        });

        // 효과음 버튼을 눌렀을 시
        sfxBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            if (SoundManager.instance.isSFXCheck)
            {
                SoundManager.instance.isSFXCheck = false;
                sfxBtn.GetComponent<Image>().sprite = sfxCancle;
            }

            else
            {
                SoundManager.instance.isSFXCheck = true;
                sfxBtn.GetComponent<Image>().sprite = sfxBasic;
            }
        });

        // 진동 버튼을 눌렀을 시
        //vibrationBtn.onClick.AddListener(() =>
        //{
        //    SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

        //    if (!isVirbration)
        //    {
        //        isVirbration = true;
        //        vibrationBtn.GetComponent<Image>().sprite = vibarationCancle;
        //    }

        //    else
        //    {
        //        isVirbration = false;
        //        vibrationBtn.GetComponent<Image>().sprite = vibrationBasic;
        //    }
        //});

        // 알림 버튼을 눌렀을 시
        //notificationBtn.onClick.AddListener(() =>
        //{
        //    SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

        //    if (!isNotification)
        //    {
        //        isNotification = true;
        //        notificationBtn.GetComponent<Image>().sprite = notificationCancle;
        //    }

        //    else
        //    {
        //        isNotification = false;
        //        notificationBtn.GetComponent<Image>().sprite = notificationBasic;
        //    }
        //});

        // 크레딧 버튼을 눌렀을 시
        creditBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            creditGroup.alpha = 1;

            creditGroup.gameObject.SetActive(true);
            creditWindow.transform.DOLocalMoveY(0, 0.2f).SetEase(Ease.Linear);
        });

        // 크레딧 취소 버튼을 눌렀을 시
        creditCancleBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            creditGroup.DOFade(0, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                creditWindow.transform.DOLocalMoveY(1050, 0).SetEase(Ease.Linear);
                creditGroup.gameObject.SetActive(false);
            });
        });
    }

    void LanguageBtn()
    {
        languageBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            amongSetting.transform.DOLocalMoveX(-1050, 0.2f).SetEase(Ease.Linear);
            rightSetting.transform.DOLocalMoveX(0, 0.2f).SetEase(Ease.Linear);
        });

        koreaBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            LanguageManager.instance.LanguageSetting(1);
        });

        englishBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            LanguageManager.instance.LanguageSetting(0);
        });

        backBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            amongSetting.transform.DOLocalMoveX(0, 0.2f).SetEase(Ease.Linear);
            rightSetting.transform.DOLocalMoveX(1050, 0.2f).SetEase(Ease.Linear);
        });
    }
}
