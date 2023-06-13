using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
public class Packaging : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform up;

    [SerializeField] private GameObject[] stickers;

    private bool isEnter;

    private IEnumerator CheckPack()
    {
        yield return Pack();

        // 음식 제출
    }
    private IEnumerator Pack()
    {
        int ran = Random.Range(0, stickers.Length);
        print(ran);
        stickers[ran].SetActive(true);

        yield return new WaitForSeconds(0.5f);

        transform.DOMoveY(7.5f, 1);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isEnter = true;
        print("enter");

        checkDrop = StartCoroutine(CheckDrop());
    }

    Coroutine checkDrop;
    IEnumerator CheckDrop()
    {
        while (true)
        {
            yield return null;

            if (Input.GetMouseButtonUp(0) && isEnter== true)
            {
                print("Check");

                up.transform.DOMove(transform.position, 1).OnComplete(() =>
                {
                    StartCoroutine(CheckPack());
                });

                break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isEnter = false;
        print("Exit");
        StopCoroutine(checkDrop);
    }

}
