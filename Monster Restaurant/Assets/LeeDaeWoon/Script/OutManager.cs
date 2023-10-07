using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class OutManager : MonoBehaviour
{
    public static OutManager instance;
    void Awake() => instance = this;

    [SerializeField] Button outBtn;
    [SerializeField] Button outYesBtn;
    [SerializeField] Button outNoBtn;
    [SerializeField] Image outFade;
    [SerializeField] CanvasGroup outWindow;

    Image fade;
    public bool isOut = false;


    void Start()
    {
        fade = FadeInOut.instance.GetComponent<Image>();

        OutBtn();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOut)
            {
                fade.DOFade(0, 0).SetUpdate(true);
                OutSetting(0, 0, 0.3f, 0.5f, true, true);
            }
            else
            {
                fade.DOKill();
                OutSetting(1, 1050, 0, 0, false, false);
            }
        }
    }

    void OutSetting(int time, int windowPosY, float windowTime, float fadeCount, bool isOutCheck, bool isSetactive)
    {
        isOut = isOutCheck;
        Time.timeScale = time;
        outWindow.gameObject.SetActive(isSetactive);
        outFade.DOFade(fadeCount, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
        outWindow.DOFade(fadeCount * 2, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
        outWindow.transform.DOLocalMoveY(windowPosY, windowTime).SetEase(Ease.Linear).SetUpdate(true);
    }

    void OutBtn()
    {
        outBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);
            if (!isOut)
            {
                fade.DOFade(0, 0).SetUpdate(true);
                OutSetting(0, 0, 0.3f, 0.5f, true, true);
            }
            else
                OutSetting(1, 1050, 0, 0, false, false);
        });

        outYesBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);
            OutSetting(1, 1050, 0, 0, false, false);
        });

        outNoBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            fade.DOKill();

            fade.DOFade(1, 0.5f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                Time.timeScale = 1;
                DOTween.KillAll();
                SceneManager.LoadScene("SubMain");
            });
        });
    }
}
