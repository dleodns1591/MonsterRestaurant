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

    [SerializeField] Sprite bgmCancle;
    [SerializeField] Sprite sfxCancle;

    public GameObject amongSetting;
    public GameObject rightSetting;

    bool isBGM = false;
    bool isSFX = false;

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

    [SerializeField] Image backgroundCredit;
    [SerializeField] Sprite creditKO;
    [SerializeField] Sprite creditEN;
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

        // 크레딧 버튼을 눌렀을 시
        creditBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            creditGroup.alpha = 1;

            creditGroup.gameObject.SetActive(true);
            creditWindow.transform.DOLocalMoveY(0, 0.2f).SetEase(Ease.Linear);

            switch (LanguageManager.instance.languageNum)
            {
                case 0:
                    backgroundCredit.sprite = creditEN;
                    break;

                case 1:
                    backgroundCredit.sprite = creditKO;
                    break;
            }
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
            SaveManager.Instance.isEnglish = false;
        });

        englishBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            LanguageManager.instance.LanguageSetting(0);
            SaveManager.Instance.isEnglish = true;
        });

        backBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            amongSetting.transform.DOLocalMoveX(0, 0.2f).SetEase(Ease.Linear);
            rightSetting.transform.DOLocalMoveX(1050, 0.2f).SetEase(Ease.Linear);
        });
    }
}
