using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class OrderMessageManager : MonoBehaviour
{
    [Header("손님의 대사 관련")]
    [SerializeField] private UIText OrderText;
    [SerializeField] private GameObject NameBallon;
    private Image SpeechBallon => OrderText.transform.parent.GetComponent<Image>();
    private Text NameBallonText => NameBallon.transform.GetComponentInChildren<Text>();

    private Tween TalkingDotween;

    public Action AfterOrder;



    /// <summary>
    /// 대사 말하는 연출이 끝난 뒤
    /// 실행시키고 싶은 함수가 있다면
    /// AfterOrder 액션에다 실행 해야할 함수를 넣은 뒤
    /// 실행하면 됨
    /// </summary>
    /// <param name="speech"></param>
    public void TalkingText(string speech)
    {
        OrderText.text = "";

        TalkingDotween = OrderText.DOText(speech, 0.05f * speech.Length).OnComplete(() =>
        {
            if(AfterOrder != null)
            {
                AfterOrder();
                AfterOrder = null;
            }
        });
    }

    public void BallonSetActive(bool isActive)
    {
        SpeechBallon.gameObject.SetActive(isActive);
        NameBallon.gameObject.SetActive(isActive);
    }

    public void NameBallonSetting(string name)
    {
        NameBallonText.text = name;
    }

    public void ResetText()
    {
        OrderText.text = "";
    }

    public void StopTalking()
    {
        if (TalkingDotween != null)
        {
            TalkingDotween.Kill();
        }
    }
}
