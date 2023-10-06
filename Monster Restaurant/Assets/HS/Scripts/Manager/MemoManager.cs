using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MemoManager : MonoBehaviour
{
    [Header("메모 관련")]
    [SerializeField] private RectTransform MemoPaper;
    [SerializeField] private Button OnButton, OffButton;
    [SerializeField] private TextMeshProUGUI[] MemoTexts;
    [SerializeField] private Image MemoPaperBackground;
    private readonly Vector2[] MemoOnTextSizes = { new Vector2(-72.51f, 80.92996f), new Vector2(-3, 6.999878f), new Vector2(-72.51f, -64.00003f), new Vector2(-3, -138), new Vector2(-72.51f, -204) };

    private OrderManager OM;

    private void Awake()
    {
        OM = OrderManager.Instance;
    }

    private void Start()
    {
        OnButton.onClick.AddListener(() => MemoOn());
        OffButton.onClick.AddListener(() => MemoOff());
        GameManager.Instance.ReturnOrder += () => MemoOff();
    }

    public void ResetMemo()
    {
        OM.dialogNumber = 0;

        for (int i = 0; i < MemoTexts.Length; i++)
        {
            MemoTexts[i].text = "";
        }
    }

    #region 버튼 이벤트 관련
    private void MemoOn()
    {
        MemoPaper.gameObject.SetActive(true);
        MemoPaperBackground.gameObject.SetActive(true);
        MemoPaperBackground.DOFade(163 / 255f, 0.5f);
        MemoPaper.DOSizeDelta(new Vector2(650, 549), 0.3f).SetEase(Ease.OutQuint);
        MemoPaper.DOAnchorPos(new Vector2(91, 0), 0.2f).SetEase(Ease.OutQuint).OnComplete(OnMemoTexts);
        void OnMemoTexts()
        {
            for (int i = 0; i < OM.dialogNumber; i++)
            {
                MemoTexts[i].gameObject.SetActive(true);
            }
        }
        int OrderCheck = 0;
        int AskCheck = 0;
        for (int i = 0; i < OM.dialogNumber; i++)
        {
            if (i % 2 != 0)
            {
                print("ConditionSetting");
                MemoTexts[i].text = OM.AskTalk[AskCheck];
                AskCheck++;
            }
            else
            {
                MemoTexts[i].text = OM.OrderTalk[OrderCheck];
                OrderCheck++;
            }
            MemoTexts[i].rectTransform.DOAnchorPos(MemoOnTextSizes[i], 0.3f).SetEase(Ease.OutQuint);
        }
    }
    private void MemoOff()
    {
        for (int i = 0; i < MemoTexts.Length; i++)
        {
            MemoTexts[i].gameObject.SetActive(false);
        }
        StartCoroutine(MemoTextOff());
        MemoPaper.DOSizeDelta(new Vector2(150, 120), 0.3f);
        MemoPaper.DOAnchorPos(new Vector2(-158, 0), 0.3f);

        MemoPaperBackground.DOColor(new Color(0, 0, 0, 0), 0.3f);

        IEnumerator MemoTextOff()
        {
            yield return new WaitForSeconds(0.3f);
            MemoPaper.gameObject.SetActive(false);
            MemoPaperBackground.gameObject.SetActive(false);
        }
    }
    #endregion
}
