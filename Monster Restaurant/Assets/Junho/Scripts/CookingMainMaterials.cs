using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum EMainMatarials
{
    Bread,
    Meat,
    Noodle,
    Rice,
    NULL
}
public class CookingMainMaterials : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private EMainMatarials myMainType;
    [SerializeField] private Sprite mySprite;
    private GraphicRaycaster gr;
    private PointerEventData ped;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Sprite[] styleSprites;

    private void Start()
    {
        gr = canvas.GetComponent<GraphicRaycaster>();
        ped = new PointerEventData(null);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Cooking.Instance.myType = ESubMatarials.NULL;
        Cooking.Instance.cursorSet.GrapImageChange(myMainType);
    }

    // 마우스 땟을때 MixedBaord 에 닿았는지 체크
    public void OnPointerUp(PointerEventData eventData)
    {
        Cooking.Instance.cursorSet.GrapImageChange(EMainMatarials.NULL);


        ped.position = eventData.position;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(ped, results);

        foreach (var item in results)
        {
            if(item.gameObject.tag == "MixedBoard")
            {
                item.gameObject.GetComponent<CookingBoard>().DropMainMaterial(myMainType, mySprite, styleSprites);
            }
        }
    }
}
