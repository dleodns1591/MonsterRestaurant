using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CookingBoard : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private Image mainMaterialImage;

    private Vector2[] mainMaterialImageSize =
    {
        new Vector2(300, 315), // �� ������
        new Vector2(340, 235), // ��� ������
        new Vector2(324, 295) // ��, �� ������
    };

    public EMainMatarials mainMaterial;
    public List<ESubMatarials> subMaterials = new List<ESubMatarials>();

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

    //public void OnDrag(PointerEventData eventData)
    //{
    //    if (isMainMaterialDrop == false || Cooking.Instance.myType == ESubMatarials.NULL) return;


    //    drawT += Time.deltaTime;
    //    if (drawCnt > drawT) return;
    //    GameObject sub = Instantiate(subM, transform).gameObject;
    //    Vector2 inputPos = Camera.main.ScreenToWorldPoint(eventData.position);
    //    sub.transform.position = inputPos;
    //    sub.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));

    //    drawT = 0;
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isMainMaterialDrop == false || Cooking.Instance.myType == ESubMatarials.NULL) return;

        GameObject sub = Instantiate(subM, transform).gameObject;
        Vector2 inputPos = Camera.main.ScreenToWorldPoint(eventData.position);
        sub.transform.position = inputPos;
        sub.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
        sub.GetComponent<Image>().raycastTarget = false;
        drawT = 0;
        subMaterials.Add(Cooking.Instance.myType);
    }
}
