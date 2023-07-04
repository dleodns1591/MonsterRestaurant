using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Packaging : MonoBehaviour
{
    [SerializeField] private RectTransform up;

    [SerializeField] private GameObject[] stickers;

    readonly Vector3 startUnder = new Vector3(1722, -8, 0);
    readonly Vector3 startUp = new Vector3(200, 230, 0);

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
        GameManager.Instance.ReturnOrder();
        // 체크 

        StartSet();

    }
    private IEnumerator Pack()
    {
        int ran = Random.Range(0, stickers.Length);
        print(ran);
        stickers[ran].SetActive(true);

        yield return new WaitForSeconds(0.5f);

        transform.DOLocalMoveY(800, 1);
        yield return new WaitForSeconds(1f);

    }

    private void StartSet()
    {
        foreach (var item in stickers)
        {
            item.SetActive(false);
        }

        transform.localPosition = startUnder;
        up.localPosition = startUp;

        Destroy(transform.GetChild(0).gameObject);
    }
}
