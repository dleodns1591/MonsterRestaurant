using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CuttingBoard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image board;
    private Vector3 startPos;
    [SerializeField] private Trash trash;
    [SerializeField] private CookingBoard food;
    [SerializeField] private CookingBoard NewFoodObj;
    [SerializeField] private Transform boardPool;
    [SerializeField] private Transform foodPool;


    private void Start()
    {
        board = GetComponent<Image>();
        startPos = board.GetComponent<RectTransform>().anchoredPosition;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (food.mainMaterial == EMainMatarials.NULL) return;
        StartCoroutine(BoardMove());
        transform.SetParent(foodPool);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (food.mainMaterial == EMainMatarials.NULL) return;
        transform.SetParent(boardPool);

        board.GetComponent<RectTransform>().anchoredPosition = startPos;

        if (trash.isEnter == true)
        {
            FoodDrop();
            trash.Exit();
        }
    }

    private void FoodDrop()
    {
        Destroy(food.gameObject);
        food = Instantiate(NewFoodObj, board.transform);
        food.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 80);
    }
    private IEnumerator BoardMove()
    {
        bool isEnterTrash = false;
        while (true)
        {
            yield return null;

            if (Input.GetMouseButtonUp(0)) break;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            board.rectTransform.position = new Vector3(mousePos.x, mousePos.y + 2, 0);
            Debug.Log(board.rectTransform.position);

            //쓰레기 위치 체크
            if ((mousePos.x > -3f || mousePos.x < 3f) && mousePos.y < -4.5f)
                isEnterTrash = true;
            else isEnterTrash = false;


            if (isEnterTrash == true) trash.Enter();
            else trash.Exit();

        }
    }
}
