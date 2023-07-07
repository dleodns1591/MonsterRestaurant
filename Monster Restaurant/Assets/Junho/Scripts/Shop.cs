using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public enum EItem
{
    ManEatingPlant,
    Wormhole,
    PlainVase1,
    PlainVase2,
    NULL
}

[System.Serializable]
public class Item
{
    public EItem type;
    public bool isBuy;
    public float price;
    public GameObject Obj;

    public GameObject soldOutImg;

    public Item()
    {
        type = EItem.NULL;
        isBuy = false;
        price = 0;
        Obj = null;
    }


    public void BuyItem()
    {
        isBuy = true;

        soldOutImg.SetActive(true);

        //event

        switch (type)
        {
            case EItem.Wormhole:
                // 새로운 엔딩 이벤트
                break;
            default:
                Obj.SetActive(true);
                break;
        }
    }
}

public class Shop : MonoBehaviour
{
    [SerializeField] private Image Desk;

    [SerializeField] private List<Item> items = new List<Item>();

    private int ManEatingPlantEvolutionaryNum;
    private int purchaseDay;
    [SerializeField] private int evolutionDay;

    [SerializeField] private Sprite[] ManEatingPlantEvolutionSprites;
    [SerializeField] private Image ManEatingPlantObj;
    [SerializeField] private Vector2[] ManEatingPlantSize;


    public void ShopOpen()
    {
        Desk.transform.DOMoveX(-17.75f, 1f).SetEase(Ease.InOutSine);
    }
    public void ShopClose()
    {
        Desk.transform.DOMoveX(0f, 1f).SetEase(Ease.InOutSine);
    }

    //식인 식물 진화 
    public void PurchaseDayCheck() 
    {
        if (ManEatingPlantEvolutionaryNum >= 2) return;

        if (items[0].isBuy)
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


    public void ItemBuyButton(int item)
    {
        if(GameManager.Instance.BuyCheck(items[item].price))
        {
            items[item].BuyItem();
        }
    }

}
