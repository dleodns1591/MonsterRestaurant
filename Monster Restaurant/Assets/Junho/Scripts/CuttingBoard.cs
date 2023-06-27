using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CuttingBoard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GraphicRaycaster gr;
    private PointerEventData ped;
    [SerializeField] private GameObject canvas;

    private Image board;
    private Vector3 startPos;
    [SerializeField] private CookingBoard food;
    [SerializeField] private CookingBoard NewFoodObj;
    [SerializeField] private Transform boardPool;
    [SerializeField] private Transform foodPool;


    private void Start()
    {
        board = GetComponent<Image>();
        startPos = board.GetComponent<RectTransform>().anchoredPosition;
        gr = canvas.GetComponent<GraphicRaycaster>();
        ped = new PointerEventData(null);
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

        if (Cooking.Instance.trash.isEnter == true)
        {
            FoodDrop();
            Cooking.Instance.trash.Exit();
        }

        ped.position = eventData.position;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(ped, results);

        foreach (var item in results)
        {
            print(item.gameObject.tag);

            if (item.gameObject.tag == "CookingMachine")
            {
                item.gameObject.GetComponent<CookingMachine>().CookDrop(food);
                CreateFood();
            }
        }
    }
    private void CreateFood()
    {
        food = Instantiate(NewFoodObj, board.transform);
        food.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 80);
    }
    private void FoodDrop()
    {
        Destroy(food.gameObject);
        CreateFood();
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

            //쓰레기 위치 체크
            if ((mousePos.x > -2.5f && mousePos.x < 2.5f) && mousePos.y < -4.5f)
                isEnterTrash = true;
            else isEnterTrash = false;


            if (isEnterTrash == true) Cooking.Instance.trash.Enter();
            else Cooking.Instance.trash.Exit();

        }
    }
}
