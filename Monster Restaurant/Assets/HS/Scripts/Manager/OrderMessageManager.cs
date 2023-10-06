using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class OrderMessageManager : MonoBehaviour
{
    [Header("�մ��� ��� ����")]
    [SerializeField] private UIText OrderText;
    [SerializeField] private GameObject NameBallon;
    private Image SpeechBallon => OrderText.transform.parent.GetComponent<Image>();
    private Text NameBallonText => NameBallon.transform.GetComponentInChildren<Text>();

    private Tween TalkingDotween;

    public Action AfterOrder;



    /// <summary>
    /// ��� ���ϴ� ������ ���� ��
    /// �����Ű�� ���� �Լ��� �ִٸ�
    /// AfterOrder �׼ǿ��� ���� �ؾ��� �Լ��� ���� ��
    /// �����ϸ� ��
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
