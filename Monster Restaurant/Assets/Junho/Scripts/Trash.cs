using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class Trash : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float startPos = -800;
    private float endPos = -223;

    public bool isEnter = false;

    private RectTransform my;

    private void Start()
    {
        my = GetComponent<RectTransform>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //요리 들었을때
        //if (Cooking.Instance.cookUp == false) return;

        isEnter = true;
        my.DOAnchorPosY(endPos,0.5f);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isEnter = false;
        my.DOAnchorPosY(startPos,0.5f);
    }

}
