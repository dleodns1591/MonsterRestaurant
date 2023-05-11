using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CuttingBoard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image board;
    private Vector2 startPos;
    [SerializeField] private Trash trash;
    [SerializeField] private CookingBoard food;
    [SerializeField] private CookingBoard NewFoodObj;
    [SerializeField] private Transform boardPool;

    private void Start()
    {
        board = GetComponent<Image>();
        startPos = board.GetComponent<RectTransform>().anchoredPosition;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(food.mainMaterial != EMainMatarials.NULL)
        StartCoroutine(BoardMove());
        transform.parent = trash.transform;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (food.mainMaterial == EMainMatarials.NULL) return;
        transform.parent = boardPool;

        board.GetComponent<RectTransform>().anchoredPosition = startPos;

        if(trash.isEnter == true)
        {
            FoodDrop();
        }
    }

    private void FoodDrop()
    {
        Destroy(food.gameObject);
        food = Instantiate(NewFoodObj,board.transform);
        food.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
    private IEnumerator BoardMove()
    {
        while (true)
        {
            yield return null;

            if (Input.GetMouseButtonUp(0)) break;
            
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            board.transform.position = mousePos;

        }
    }
}
