using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] Button startBtn;

    [Header("����")]
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
        // ���� ��ư�� ������ ���
        startBtn.onClick.AddListener(() =>
        {
            // �ΰ��� ������ �Ѿ��
        });

        // ���� ��ư�� ������ ���
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
