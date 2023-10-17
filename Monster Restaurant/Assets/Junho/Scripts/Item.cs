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
    [SerializeField, TextArea] private string explanation;
    [SerializeField, TextArea] private string explanationEnglish;
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
            if(GameManager.Instance.Money > price)
            {
                BuyItem();
                SoundManager.instance.PlaySoundClip("BuySFX", SoundType.SFX);
            }
            else
            {
                if (SaveManager.Instance.isEnglish == false)
                    OrderManager.Instance.SpeakOrder("구매할 수 있는 돈이 충분하지 않습니다.");
                else
                    OrderManager.Instance.SpeakOrder("Don't have enough money to buy it.");
            }
        }
        else if (Input.GetMouseButton(1))
        {
            OrderManager.Instance.orderMessageManager.ResetText();
            if (SaveManager.Instance.isEnglish == false)
                OrderManager.Instance.SpeakOrder(explanation);
            else
                OrderManager.Instance.SpeakOrder(explanationEnglish);
        }

    }
}