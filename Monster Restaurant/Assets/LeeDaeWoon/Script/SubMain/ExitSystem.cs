using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExitSystem : MonoBehaviour
{
    [SerializeField] Button exitYesBtn;
    [SerializeField] Button exitNoBtn;

    [SerializeField] CanvasGroup exitGroup;
    [SerializeField] GameObject exitWindow;

    void Start()
    {
        Btns();
    }

    void Update()
    {
        
    }

    void Btns()
    {
        // 나가기 버튼을 눌렀을 때
        exitYesBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        // 계속하기 버튼을 눌렀을 때
        exitNoBtn.onClick.AddListener(() =>
        {
            exitGroup.DOFade(0, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                exitWindow.transform.DOLocalMoveY(1050, 0).SetEase(Ease.Linear);
                exitGroup.gameObject.SetActive(false);
            });
        });
    }
}
