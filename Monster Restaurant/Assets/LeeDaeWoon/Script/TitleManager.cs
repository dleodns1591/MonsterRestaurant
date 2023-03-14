using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] Button startBtn;

    [Header("설정")]
    [SerializeField] GameObject settingIn;
    [SerializeField] Button settingBtn;
    bool isSetting = false;

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

        // 설정 버튼을 눌렀을 경우
        settingBtn.onClick.AddListener(() =>
        {
            if(!isSetting)
            {
                isSetting = true;
                for (int i = 1; i < 5; i++)
                    settingIn.transform.GetChild(i -1).DOLocalMoveY(i * -200, 0.8f).SetEase(Ease.OutBack);
            }

            else
            {
                isSetting = false;
                for (int i = 1; i < 5; i++)
                    settingIn.transform.GetChild(i - 1).DOLocalMoveY(0, 0.8f).SetEase(Ease.InBack);
            }
        });
    }
}
