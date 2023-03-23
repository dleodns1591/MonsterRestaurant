using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] Button startBtn;

    [Header("ÄÁÅÙÃ÷")]
    [SerializeField] GameObject content;
    [SerializeField] Button contentBtn;
    bool isContent = false;

    [Header("¼³Á¤")]
    [SerializeField] CanvasGroup settingWindow;
    [SerializeField] Button settingBtn;
    [SerializeField] Button settingCancleBtn;

    void Start()
    {
        Btns();
    }

    void Update()
    {
        
    }

    void Btns()
    {
        // ½ÃÀÛ ¹öÆ°À» ´­·¶À» °æ¿ì
        startBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });

        // ÄÁÅÙÃ÷ ¹öÆ°À» ´­·¶À» °æ¿ì
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
    }
}
