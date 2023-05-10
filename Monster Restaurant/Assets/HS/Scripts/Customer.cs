using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using HS_Tree;
using System;


namespace HS_Tree
{
    public class TreeNode<T>
    {
        private List<TreeNode<T>> children = new List<TreeNode<T>>();

        public T Value { get; set; }

        public TreeNode(T value)
        {
            Value = value;
        }

        public List<TreeNode<T>> Children
        {
            get { return children; }
        }

        public void AddChild(TreeNode<T> node)
        {
            children.Add(node);
        }
    }

}


public class Customer : MonoBehaviour
{
    [SerializeField]
    private Transform[] SlowMovingPos, OrderPos;
    [SerializeField]
    private Transform FastMovingPos;
    [SerializeField]
    private Sprite[] GuestDefualts, EventGuestDefualts;
    [SerializeField, Tooltip("배경 위에 보이기 하기 위한")]
    private GameObject BackgroundCanvas;
    [SerializeField]
    private RectTransform MemoPaper;
    [SerializeField]
    private FadeInOut fadeInOut;
    [SerializeField]
    private UIText[] MemoTexts;
    [SerializeField]
    private Image MemoPaperBackground;

    private readonly Vector2[] MemoOnTextSizes = { new Vector2(-72.51f, 80.92996f), new Vector2(-3, 6.999878f), new Vector2(-72.51f, -64.00003f), new Vector2(-3, -138), new Vector2(-72.51f, -204) };
    private readonly Vector2[] MemoOffTextSizes = { new Vector2(132, -9), new Vector2(-135, 3), new Vector2(130, -36), new Vector2(-145, -29), new Vector2(131, -65) };
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

        //주문 테이블쪽 이동
        gameObject.transform.parent = BackgroundCanvas.transform;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 1000);
        transform.position = OrderPos[0].position;
        transform.DOMove(OrderPos[1].position, delayTime);
        CustomerImg.DOColor(new Color(1, 1, 1, 1), delayTime);

        yield return new WaitForSeconds(delayTime);
    }

    public void MemoOn()
    {
        MemoPaper.gameObject.SetActive(true);
        MemoPaperBackground.gameObject.SetActive(true);
        MemoPaperBackground.DOFade(163 / 255f, 0.5f);
        MemoPaper.DOSizeDelta(new Vector2(650, 549), 0.3f).SetEase(Ease.OutQuint);
        MemoPaper.DOAnchorPos(new Vector2(-242.47f, 0), 0.3f).SetEase(Ease.OutQuint);
        for (int i = 0; i < MemoTexts.Length; i++)
        {
            MemoTexts[i].gameObject.SetActive(true);
            MemoTexts[i].text = memo[i];
            MemoTexts[i].rectTransform.DOAnchorPos(MemoOnTextSizes[i], 0.3f).SetEase(Ease.OutQuint);
        }
    }
    public void MemoOff()
    {
        for (int i = 0; i < MemoTexts.Length; i++)
        {
            MemoTexts[i].text = memo[i];

            MemoTexts[i].rectTransform.DOAnchorPos(MemoOffTextSizes[i], 0.3f).SetEase(Ease.OutQuint);
        }
        StartCoroutine(MemoTextOff());
        MemoPaper.DOSizeDelta(new Vector2(150, 120), 0.3f);
        MemoPaper.DOAnchorPos(new Vector2(-492.47f, 0), 0.3f);

        MemoPaperBackground.DOColor(new Color(0, 0, 0, 0), 0.3f);

        IEnumerator MemoTextOff()
        {
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < MemoTexts.Length; i++)
            {
                MemoTexts[i].gameObject.SetActive(false);
            }
            MemoPaper.gameObject.SetActive(false);
            MemoPaperBackground.gameObject.SetActive(false);
        }
    }
}
