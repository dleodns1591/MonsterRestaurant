using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CookingBoard : MonoBehaviour, IDragHandler
{
    [SerializeField] private Image mainMaterialImage;

    private Vector2[] mainMaterialImageSize =
    {
        new Vector2(300, 315), // 빵 사이즈
        new Vector2(340, 235), // 고기 사이즈
        new Vector2(324, 295) // 밥, 면 사이즈
    };

    public EMainMatarials mainMaterial;
    public ESubMatarials[] subMaterials;

    [SerializeField] private Image subM;

    private bool isMainMaterialDrop;
    [SerializeField] private bool isEnterImage;

    [SerializeField] private float drawCnt;
    [SerializeField] private float drawT;

    public void DropMainMaterial(EMainMatarials main, Sprite sprite)
    {
        if (isMainMaterialDrop == true) return;

        isMainMaterialDrop = true;
        mainMaterial = main;

        mainMaterialImage.sprite = sprite;

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

    public void OnDrag(PointerEventData eventData)
    {
        if (isMainMaterialDrop == false || Cooking.Instance.myType == ESubMatarials.NULL) return;


        drawT += Time.deltaTime;
        if (drawCnt > drawT) return;
        GameObject sub = Instantiate(subM, transform).gameObject;
        Vector2 inputPos = Camera.main.ScreenToWorldPoint(eventData.position);
        sub.transform.position = inputPos;

        drawT = 0;
    }
}
