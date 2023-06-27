using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputCheck : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GraphicRaycaster gr;
    private PointerEventData ped;
    [SerializeField] private GameObject canvas;

    private void Start()
    {
        gr = canvas.GetComponent<GraphicRaycaster>();
        ped = new PointerEventData(null);
    }

    private void Update()
    {
        //ped.position = Input.mousePosition;
        //List<RaycastResult> results = new List<RaycastResult>();
        //gr.Raycast(ped, results);

        //foreach (var item in results)
        //{
        //        print(item.gameObject.tag);
        //    if (item.gameObject.tag == "MixedBoard")
        //    {
        //    }
        //}
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    // 마우스 땟을때 MixedBaord 에 닿았는지 체크
    public void OnPointerUp(PointerEventData eventData)
    {
        ped.position = eventData.position;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(ped, results);

        foreach (var item in results)
        {
            print(item.gameObject.tag);

            if (item.gameObject.tag == "MixedBoard")
            {
            }
        }
    }
}
