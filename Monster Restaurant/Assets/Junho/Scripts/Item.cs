using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public enum EItem
{
    ManEatingPlant,
    WormHole,
    PlainVase1,
    PlainVase2,
    NULL
}
public class Item : MonoBehaviour, IPointerDownHandler
{
    public EItem type;
    public bool isBuy;
    [SerializeField] private float price;
    [SerializeField] private TextMeshProUGUI priceTxt;
    [SerializeField] private GameObject Obj;
    [SerializeField] private GameObject ObjImage;
    [SerializeField] private string explanation;
    [SerializeField] private GameObject soldOutImg;
    public Item()
    {
        type = EItem.NULL;
        isBuy = false;
        price = 0;
        Obj = null;
        explanation = null;
    }

    private IEnumerator Start()
    {
        priceTxt.text = price.ToString() + "$";

        yield return new WaitForSeconds(10f);
    }

    public void BuyItem()
    {
        isBuy = true;

        soldOutImg.SetActive(true);
        ObjImage.SetActive(false);
        //event
        GameManager.Instance.BuyTalking();
        switch (type)
        {
            case EItem.WormHole:
                GameManager.Instance.WormHoleDraw();
                //
                break;
            default:
                Obj.SetActive(true);
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            if (GameManager.Instance.BuyCheck(price) == true)
            {
                BuyItem();
            }
        }
        else if (Input.GetMouseButton(1))
        {
            print("Right");

            if (GameManager.Instance.shop.ReturnScript != null) StopCoroutine(GameManager.Instance.shop.ReturnScript);
           GameManager.Instance.shop.ReturnScript = StartCoroutine(ReturnExplanation());
        }

    }

    private IEnumerator ReturnExplanation()
    {
        print("ex");

        // 설명 대사 

        yield return new WaitForSeconds(3f);

        // 원래 대사
    }
}