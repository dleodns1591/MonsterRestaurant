using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomerGuideMouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Image customerGuide;

    void Start()
    {
        customerGuide = GetComponent<Image>();
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        customerGuide.rectTransform.sizeDelta = new Vector2(400, 369);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        customerGuide.rectTransform.sizeDelta = new Vector2(404, 357);
    }
}
