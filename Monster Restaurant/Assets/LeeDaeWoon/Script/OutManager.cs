using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class OutManager : MonoBehaviour
{
    [SerializeField] Button outBtn;
    [SerializeField] Button outYesBtn;
    [SerializeField] Button outNoBtn;
    [SerializeField] Image outFade;
    [SerializeField] CanvasGroup outWindow;

    bool isOut = false;
    bool isOutClick = false;


    void Start()
    {
        OutBtn();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isOutClick)
        {
            if (!isOut)
                OutSetting(0, 0.5f, true);
            else
                OutSetting(1, 0, false);
        }
    }

    void OutSetting(int time, float fadeCount, bool isOutCheck)
    {
        isOut = isOutCheck;
        outFade.DOFade(fadeCount, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
        outWindow.DOFade(fadeCount * 2, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
        Time.timeScale = time;
    }

    void OutBtn()
    {
        Image fade = FadeInOut.instance.GetComponent<Image>();

        outBtn.onClick.AddListener(() =>
        {
            if (!isOutClick)
            {
                if (!isOut)
                    OutSetting(0, 0.5f, true);
                else
                    OutSetting(1, 0, false);
            }
        });

        outYesBtn.onClick.AddListener(() =>
        {
            isOutClick = true;

            fade.DOFade(1, 0.5f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                Time.timeScale = 1;
                DOTween.KillAll();
                SceneManager.LoadScene(1);
            });
        });

        outNoBtn.onClick.AddListener(() =>
        {
            OutSetting(1, 0, false);
        });
    }
}
