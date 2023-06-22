using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CookingBoard : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private Image mainMaterialImage;
    private Sprite[] styleImage;

    private Vector2[] mainMaterialImageSize =
    {
        new Vector2(300, 315), // 빵 사이즈
        new Vector2(340, 235), // 고기 사이즈
        new Vector2(324, 295) // 밥, 면 사이즈
    };

    public ECookingStyle style;
    public EMainMatarials mainMaterial;
    public List<SubMaterialImages> subMaterials = new List<SubMaterialImages>();
    
    [SerializeField] private Image subM;

    public bool isFinish;
    private bool isMainMaterialDrop;
    [SerializeField] private bool isEnterImage;

    public void ImageProcessing()
    {
        mainMaterialImage.sprite = styleImage[((int)style)];

        foreach (var item in subMaterials)
        {
            item.ImageProcessing(style);
        }
    }



    public void DropMainMaterial(EMainMatarials main, Sprite sprite, Sprite[] styleSprites)
    {
        float mainPrice = Cooking.Instance.MainMaterialsPriece[((int)main)];


        if (isMainMaterialDrop == true && GameManager.Instance.BuyCheck(mainPrice)) return;

        GameManager.Instance.Money -= mainPrice;

        isMainMaterialDrop = true;
        mainMaterial = main;

        mainMaterialImage.sprite = sprite;
        styleImage = styleSprites;

        Vector2 imageSize;
        switch (main)
        {
            case EMainMatarials.Bread:
                imageSize = mainMaterialImageSize[0];
                break;
            case EMainMatarials.Meat:
                imageSize = mainMaterialImageSize[1];
                break;
            default:
                imageSize = mainMaterialImageSize[2];
                break;
        }
        mainMaterialImage.rectTransform.sizeDelta = imageSize;
        mainMaterialImage.color = new Vector4(1, 1, 1, 1);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isFinish == false)
        {
            float price = Cooking.Instance.materialPrice;

            if ((isMainMaterialDrop == false || Cooking.Instance.myType == ESubMatarials.NULL)
                && GameManager.Instance.BuyCheck(price)) return;


            GameObject sub = Instantiate(subM, transform).gameObject;
            Vector2 inputPos = Camera.main.ScreenToWorldPoint(eventData.position);
            sub.transform.position = inputPos;
            sub.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
            sub.GetComponent<Image>().raycastTarget = false;

            subMaterials.Add(sub.GetComponent<SubMaterialImages>());
            GameManager.Instance.Money -= price;
        }
        else
        {

        }

    }
}
