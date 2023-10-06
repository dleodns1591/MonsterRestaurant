using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class Shop : MonoBehaviour
{
    [SerializeField] private Image Desk;

    public Item ManEatingPlant;

    private int ManEatingPlantEvolutionaryNum;
    private int purchaseDay;
    [SerializeField] private int evolutionDay;

    [SerializeField] private Sprite[] ManEatingPlantEvolutionSprites;
    [SerializeField] private Image ManEatingPlantObj;
    [SerializeField] private Vector2[] ManEatingPlantSize;

    public Button StopBuyBtn;
    public GameObject MouseGuide;

    public Action ShopOpen;

    public Coroutine ReturnScript;

    public bool isFinalEvolution;


    private OrderManager OM;
    private GameManager GM;
    private SaveManager SM;
    private Coroutine BuyTextCoroutine;
    private void Start()
    {
        OM = OrderManager.Instance;
        GM = GameManager.Instance;
        SM = SaveManager.Instance;

        ShopProduction();
        ShopOpen = () =>
        {
            Desk.transform.DOMoveX(-17.75f, 1f).SetEase(Ease.InOutSine);
        };
    }
    public void ShopCloseBtn()
    {
        Desk.transform.DOMoveX(0f, 1f).SetEase(Ease.InOutSine);
    }
    //식인 식물 진화체크 (일차 지날때마다 호출)
    public void PurchaseDayCheck() 
    {
        if (ManEatingPlantEvolutionaryNum >= 2)
        {
            isFinalEvolution = true; 
            return;
        }


        if (ManEatingPlant.isBuy)
        {
            purchaseDay++;

            if (purchaseDay >= evolutionDay)
            {
                purchaseDay = 0;

                ManEatingPlantEvolutionaryNum++;

                ManEatingPlantObj.rectTransform.sizeDelta = ManEatingPlantSize[ManEatingPlantEvolutionaryNum];

                ManEatingPlantObj.sprite = ManEatingPlantEvolutionSprites[ManEatingPlantEvolutionaryNum];
            }
        }
    }

    #region 상점 연출 & 버튼 상호작용 관련 코드
    private void ShopProduction()
    {
        StopBuyBtn.onClick.AddListener(() =>
        {
            ShopCloseBtn();
            StartCoroutine(RefuseOrderDelay());
            IEnumerator RefuseOrderDelay()
            {
                OM.OrderTalk[1] = "이용해 주셔서 감사합니다.";
                OM.isNext = true;
                OM.orderButtonManager.ButtonSetActive(false);
                MouseGuide.SetActive(false);
                StopBuyBtn.gameObject.SetActive(false);
                yield return new WaitForSeconds(1.5f);
                StartCoroutine(OM.ExitAndComein());
            }
        });

        GameManager.Instance.WormHoleDraw = () =>
        {
            int rand = UnityEngine.Random.Range(1, 10);
            SM.isWormHoleFirstBuy = true;
            if (rand >= 7)
            {

                OM.endingManager.EndingProduction(EendingType.WormHole_FindHouse);
                GM.IsEndingOpens[(int)EendingType.WormHole_FindHouse] = true;
            }
            else
            {

                OM.endingManager.EndingProduction(EendingType.WormHole_SpaceAdventure);
                GM.IsEndingOpens[(int)EendingType.WormHole_SpaceAdventure] = true;
            }
        };

        GameManager.Instance.BuyTalking = () =>
        {
            int rand = UnityEngine.Random.Range(0, 2);
            string[] speechs = new string[3] { "탁월한 선택이시네요.", "역시.. 보는 눈이 있으시네요.", "구매해 주셔서 감사합니다." };

            OM.SpeakOrder(speechs[rand]);

            if (BuyTextCoroutine != null)
            {
                StopCoroutine(BuyTextCoroutine);
                OM.orderMessageManager.ResetText();

            }

            BuyTextCoroutine = StartCoroutine(Delay());
            IEnumerator Delay()
            {
                yield return new WaitForSeconds(3 + (speechs[rand].Length * 0.05f));

                OM.orderMessageManager.ResetText();

            }
        };

        GM.ShopAppearProd = () =>
        {
            StartCoroutine(Delay());
            IEnumerator Delay()
            {
                FadeInOut.instance.FadeOut();
                yield return new WaitForSeconds(FadeInOut.instance.fadeTime);
                ShopOpen();
                FadeInOut.instance.Fade();
                yield return new WaitForSeconds(FadeInOut.instance.fadeTime);
                StopBuyBtn.gameObject.SetActive(true);
                OM.orderMessageManager.ResetText();
                OM.SpeakOrder("마음에 드시는 제품 있으시면 구매해주세요.");
                yield return new WaitForSeconds("마음에 드시는 제품 있으시면 구매해주세요.".Length * 0.05f);
                MouseGuide.SetActive(true);
            }

        };
    }

    #endregion
}
