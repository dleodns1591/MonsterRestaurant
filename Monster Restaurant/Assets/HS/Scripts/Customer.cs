using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using System;

public class Customer : MonoBehaviour
{
    [Header("손님 관련")]
    [SerializeField] public Image CustomerImg;
    [SerializeField] public Sprite[] GuestDefualts, EventGuestDefualts;
    [SerializeField] public Sprite[] GuestSuccess, EventGuestSuccess;
    [SerializeField] public Sprite[] GuestFails, EventGuestFails;

    [Header("등장 연출의 위치 관련")]
    [SerializeField] private Transform[] SlowMovingPos, OrderPos;
    [SerializeField] private Transform FastMovingPos, OriginalPos;


    [SerializeField, Tooltip("배경 위에 보이기 하기 위한")]
    private GameObject BackgroundCanvas;
    [SerializeField]
    private RectTransform MemoPaper;
    [SerializeField]
    private FadeInOut fadeInOut;
    [SerializeField] private GameObject Window;

    private string[] memo = new string[5];

    /// <summary>
    /// 처음 등장부터 주문전까지 이동하는 코루틴
    /// </summary>
    /// <returns></returns>
    public IEnumerator Moving()
    {
        yield return new WaitForSeconds(fadeInOut.fadeTime);

        float delayTime = 0.5f;

        Image CustomerImg = gameObject.GetComponent<Image>();

        //와리가리 움직임
        for (int i = 0; i < SlowMovingPos.Length; i++)
        {
            if (i != SlowMovingPos.Length - 1)
                transform.DOMove(SlowMovingPos[i].position, delayTime);
            else
                transform.DOMove(SlowMovingPos[i].position, 0.25f);

            yield return new WaitForSeconds(delayTime);
        }

        //빠르게 이동
        transform.DOMove(FastMovingPos.position, delayTime);

        yield return new WaitForSeconds(1.5f);

        Appear(CustomerImg, delayTime);

        yield return new WaitForSeconds(delayTime);
    }
    public IEnumerator MovingTime()
    {
        yield return new WaitForSeconds(fadeInOut.fadeTime);

        float delayTime = 0.5f;

        for (int i = 0; i < SlowMovingPos.Length; i++)
        {
            yield return new WaitForSeconds(delayTime);
        }

        yield return new WaitForSeconds(1.5f);
        yield return new WaitForSeconds(delayTime);
    }
    void Appear(Image CustomerImg, float delayTime)
    {
        gameObject.transform.parent = BackgroundCanvas.transform;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 1000);
        transform.position = OrderPos[0].position;
        transform.DOMove(OrderPos[1].position, delayTime);
        CustomerImg.DOColor(new Color(1, 1, 1, 1), delayTime);
    }

    public void Exit()
    {
        transform.DOMove(OrderPos[2].position, 0.5f).OnComplete(() =>
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(155, 221);
            transform.position = OriginalPos.position;
            gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 1);
            gameObject.transform.parent = Window.transform;
        });
    }
}
