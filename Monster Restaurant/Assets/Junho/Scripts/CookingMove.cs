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

        startPos = eventData.position;
        moveUIs.transform.Translate(Vector2.right * x * moveSpd * Time.deltaTime);
        if (moveUIs.transform.position.x >= 0) moveUIs.transform.position = Vector2.zero;
        else if (moveUIs.transform.position.x <= -16) moveUIs.transform.position = Vector2.right * -16;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("dd");
        startPos = eventData.position;
    }
   
}
