using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultManager : MonoBehaviour
{
    [Header("결과 창 관련")]
    [SerializeField] private GameObject RevenuePopup;
    [SerializeField] private Button NextButton;
    [SerializeField] private Text Principal, BasicRevenue, SalesRevenue, MarterialCost, TaxCost, SettlementCost, Total;
    [SerializeField] private Text DayText;

    private int EndingDate = 20;

    OrderManager OM;
    GameManager GM;

    private void Start()
    {
        OM = OrderManager.Instance;
        GM = GameManager.Instance;
    }

    public void DayEnd()
    {
        if (GM.Day >= EndingDate)
        {
            if (GM.Money < 2500)
            {

                OM.endingManager.EndingProduction(EendingType.Loser);
                GM.IsEndingOpens[(int)EendingType.Loser] = true;
            }
            else if (GM.Money < 5000)
            {

                OM.endingManager.EndingProduction(EendingType.Salve);
                GM.IsEndingOpens[(int)EendingType.Salve] = true;
            }
            else
            {

                OM.endingManager.EndingProduction(EendingType.Mine);
                GM.IsEndingOpens[(int)EendingType.Mine] = true;
            }

            return;
        }
        FadeInOut.instance.LittleFadeOut();
        FadeInOut.instance.RevenueFadeOut();

        GM.shop.PurchaseDayCheck();
        GM.Money += 200;
        GM.TaxCost = GM.SalesRevenue / 10;
        GM.Money -= GM.TaxCost;
        GM.Money -= GM.SettlementCost;
        StartCoroutine(NumberAni());
        IEnumerator NumberAni()
        {
            yield return new WaitForSeconds(FadeInOut.instance.fadeTime);
            BasicRevenue.text = "";
            SalesRevenue.text = "";
            MarterialCost.text = "";
            TaxCost.text = "";
            SettlementCost.text = "";
            Total.text = "";
            for (int i = 0; i < RevenuePopup.transform.childCount; i++)
            {
                RevenuePopup.transform.GetChild(i).gameObject.SetActive(true);
            }
            NextButton.gameObject.SetActive(false);

            NumberAnimation(OM.firstMoney, 1.3f, Principal);
            yield return new WaitForSeconds(1.5f);
            NumberAnimation(GameManager.Instance.BasicRevenue, 1.3f, BasicRevenue);
            yield return new WaitForSeconds(1.5f);
            NumberAnimation(GameManager.Instance.SalesRevenue, 1.3f, SalesRevenue);
            yield return new WaitForSeconds(1.5f);
            NumberAnimation((int)GameManager.Instance.MarterialCost, 1.3f, MarterialCost);
            yield return new WaitForSeconds(1.5f);
            NumberAnimation(GameManager.Instance.TaxCost, 1.3f, TaxCost);
            yield return new WaitForSeconds(1.5f);
            NumberAnimation(GameManager.Instance.SettlementCost, 1.3f, SettlementCost);
            yield return new WaitForSeconds(1.5f);
            NumberAnimation((int)GameManager.Instance.Money, 1.3f, Total);
            yield return new WaitForSeconds(2.0f);
            NextButton.gameObject.SetActive(true);
            NextButton.onClick.RemoveAllListeners();
            NextButton.onClick.AddListener(() =>
            {
                NextButton.gameObject.SetActive(false);
                RevenuePopup.GetComponent<RectTransform>().DOAnchorPosY(865, 2.5f).OnComplete(() =>
                {
                    for (int i = 0; i < RevenuePopup.transform.childCount; i++)
                    {
                        RevenuePopup.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    OM.TimeFill.fillAmount = 1;

                    IEnumerator DayProduction()
                    {
                        GM.Day++;
                        GM.eventCheck.Check();
                        DayText.text = $"{GM.Day}일차....!";
                        DayText.DOFade(1, 1);
                        yield return new WaitForSeconds(1.5f);
                        FadeInOut.instance.LittleFade();
                        DayText.DOFade(0, FadeInOut.instance.fadeTime);
                        yield return new WaitForSeconds(FadeInOut.instance.fadeTime);
                    }
                    StartCoroutine(DayProduction());
                    StartCoroutine(Reset());
                });
            });
            IEnumerator Reset()
            {
                yield return new WaitForSeconds(FadeInOut.instance.fadeTime);
                RevenuePopup.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                RevenuePopup.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                GM.dayEndCheck = false;
                OM.OrderLoop();
            }
        }
    }

    /// <summary>
    /// 숫자 오르는 애니메이션
    /// </summary>
    /// <param name="targetNumber">원하는 수치</param>
    /// <param name="animationDuration">원하는 속도</param>
    /// <param name="numberText">적용될 텍스트 UI</param>
    void NumberAnimation(int targetNumber, float animationDuration, Text numberText)
    {
        int currentNumber = 0;
        DOTween.To(() => currentNumber, x => currentNumber = x, targetNumber, animationDuration)
            .SetEase(Ease.Linear)
            .OnUpdate(() => numberText.text = currentNumber.ToString());
    }
}
