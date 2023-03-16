using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CookingMove : MonoBehaviour, IPointerDownHandler,IDragHandler
{
    [SerializeField] private GameObject moveUIs;
    [SerializeField] private float moveSpd;
    private Vector2 startPos;

    public void OnDrag(PointerEventData eventData)
    {
        var x = (eventData.position.x - startPos.x > 0) ? 1: -1;

        if ((moveUIs.transform.position.x >= 0 && x == 1 )|| (moveUIs.transform.position.x <= -16 && x == -1)) return;

        moveUIs.transform.Translate(Vector2.right * x * moveSpd * Time.deltaTime);
        startPos = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = eventData.position;
    }
   
}
