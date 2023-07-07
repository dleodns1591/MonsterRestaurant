using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;


public class Shop : MonoBehaviour
{
    [SerializeField] private Image Desk;

    [SerializeField] private Item ManEatingPlant;

    private int ManEatingPlantEvolutionaryNum;
    private int purchaseDay;
    [SerializeField] private int evolutionDay;

    [SerializeField] private Sprite[] ManEatingPlantEvolutionSprites;
    [SerializeField] private Image ManEatingPlantObj;
    [SerializeField] private Vector2[] ManEatingPlantSize;

    public Action ShopOpen;

    public Coroutine ReturnScript;

    private void Start()
    {
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
        if (ManEatingPlantEvolutionaryNum >= 2) return;

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
}
