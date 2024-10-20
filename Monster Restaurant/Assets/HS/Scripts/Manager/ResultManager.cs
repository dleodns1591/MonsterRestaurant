using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ResultManager : MonoBehaviour
{
    [Header("결과 창 관련")]
    [SerializeField] private GameObject RevenuePopup;
    [SerializeField] private GameObject ExplanTextKR, ExplanTextEng;
    [SerializeField] private Button NextButton;
    [SerializeField] private Text Principal, BasicRevenue, SalesRevenue, MarterialCost, TaxCost, SettlementCost, Total;
    [SerializeField] private Text DayText;
    [Header("도장 창 관련")]
    [SerializeField] private GameObject DailyPopup;
    [SerializeField] private GameObject[] Stamps;
    [Header("날짜 관련")]
    [SerializeField] private TextMeshProUGUI CurDayText;

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
            #region 텍스트 초기화
            BasicRevenue.text = "";
            SalesRevenue.text = "";
            MarterialCost.text = "";
            TaxCost.text = "";
            SettlementCost.text = "";
            Total.text = "";
            #endregion
            for (int i = 0; i < RevenuePopup.transform.childCount; i++)
            {
                RevenuePopup.transform.GetChild(i).gameObject.SetActive(true);
            }
            if (SaveManager.Instance.isEnglish == false)
                ExplanTextEng.SetActive(false);
            else
                ExplanTextKR.SetActive(false);

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
            NumberAnimation((int)Mathf.Ceil(GameManager.Instance.Money), 1.3f, Total);
            yield return new WaitForSeconds(2.0f);

            if (SaveManager.Instance.isEnglish == false)
                NextButton.gameObject.transform.GetChild(0).GetComponent<Text>().text = "다음";
            else
                NextButton.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Next";

            NextButton.gameObject.SetActive(true);
            NextButton.onClick.RemoveAllListeners();
            NextButton.onClick.AddListener(() =>
            {
                NextButton.gameObject.SetActive(false);
                RevenuePopup.GetComponent<RectTransform>().DOAnchorPosY(865, 1.5f).OnComplete(() =>
                {
                    for (int i = 0; i < RevenuePopup.transform.childCount; i++)
                    {
                        RevenuePopup.transform.GetChild(i).gameObject.SetActive(false);
                    }

                    StartCoroutine(Daily());

                    IEnumerator Daily()
                    {
                        DailyPopup.GetComponent<RectTransform>().DOAnchorPosY(0, 1.5f);
                        yield return new WaitForSeconds(1.5f);
                        //도장연출
                        Stamps[GM.Day - 1].gameObject.SetActive(true);
                        Stamps[GM.Day - 1].GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f);
                        Stamps[GM.Day - 1].GetComponent<RectTransform>().DOScale(new Vector3(1, 1), 0.65f);
                        yield return new WaitForSeconds(0.65f);

                        if (SaveManager.Instance.isEnglish == false)
                            NextButton.gameObject.transform.GetChild(0).GetComponent<Text>().text = "다음 일차로!";
                        else
                            NextButton.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Next Day!";

                        NextButton.gameObject.SetActive(true);
                        NextButton.onClick.RemoveAllListeners();
                        NextButton.onClick.AddListener(() =>
                        {
                            NextButton.gameObject.SetActive(false);
                            DailyPopup.GetComponent<RectTransform>().DOAnchorPosY(865, 1.5f).OnComplete(() =>
                            {
                                OM.TimeFill.fillAmount = 1;

                                StartCoroutine(DayProduction());
                                StartCoroutine(Reset());
                            });
                        });
                    }

                    IEnumerator DayProduction()
                    {
                        GM.Day++;
                        CurDayText.text = $"Day {GM.Day}";
                        GM.eventCheck.Check();
                        if (SaveManager.Instance.isEnglish == false)
                            DayText.text = $"{GM.Day}일차....!";
                        else
                            DayText.text = $"Day {GM.Day}....!";

                        DayText.DOFade(1, 1);
                        yield return new WaitForSeconds(1.5f);
                        FadeInOut.instance.LittleFade();
                        DayText.DOFade(0, FadeInOut.instance.fadeTime);
                        yield return new WaitForSeconds(FadeInOut.instance.fadeTime);
                    }
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
