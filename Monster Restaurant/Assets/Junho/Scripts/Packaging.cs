using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Packaging : MonoBehaviour
{
    [SerializeField] private RectTransform up;

    [SerializeField] private GameObject[] stickers;


    public IEnumerator CheckPack(GameObject cook)
    {
        cook.transform.parent = transform;
        cook.transform.SetAsFirstSibling();
        cook.transform.localPosition = transform.position;


        up.transform.DOLocalMove(transform.position, 1);

        yield return new WaitForSeconds(1f);

        yield return Pack();
       
        //GameManager.Instance.orderSets[0].
        // 음식 제출

        // 체크 

            

    }
    private IEnumerator Pack()
    {
        int ran = Random.Range(0, stickers.Length);
        print(ran);
        stickers[ran].SetActive(true);

        yield return new WaitForSeconds(0.5f);

        transform.DOLocalMoveY(800, 1);
    }


}
