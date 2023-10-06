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
    //���� �Ĺ� ��ȭüũ (���� ���������� ȣ��)
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

    #region ���� ���� & ��ư ��ȣ�ۿ� ���� �ڵ�
    private void ShopProduction()
    {
        StopBuyBtn.onClick.AddListener(() =>
        {
            ShopCloseBtn();
            StartCoroutine(RefuseOrderDelay());
            IEnumerator RefuseOrderDelay()
            {
                OM.OrderTalk[1] = "�̿��� �ּż� �����մϴ�.";
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
            string[] speechs = new string[3] { "Ź���� �����̽ó׿�.", "����.. ���� ���� �����ó׿�.", "������ �ּż� �����մϴ�." };

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
                OM.SpeakOrder("������ ��ô� ��ǰ �����ø� �������ּ���.");
                yield return new WaitForSeconds("������ ��ô� ��ǰ �����ø� �������ּ���.".Length * 0.05f);
                MouseGuide.SetActive(true);
            }

        };
    }

    #endregion
}
