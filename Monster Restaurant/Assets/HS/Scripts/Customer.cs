using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public enum EcustomerType
{
    Alien,
    Hyena,
    Robot,
    GroupOrder
}


public class Customer : MonoBehaviour
{
    [SerializeField]
    private Transform[] SlowMovingPos, OrderPos;
    [SerializeField]
    private Transform FastMovingPos;
    [SerializeField]
    private Sprite[] GuestDefualts;
    [SerializeField, Tooltip("배경 위에 보이기 하기 위한")]
    private GameObject BackgroundCanvas;

    bool playerDetect = false;

    int curCustomerType;
    private void Start()
    {
        StartCoroutine(Moving());
    }

    /// <summary>
    /// 처음 등장부터 주문전까지 이동하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator Moving()
    {
        float delayTime = 0.5f;

        curCustomerType = Random.Range(0, System.Enum.GetValues(typeof(EcustomerType)).Length);

        gameObject.GetComponent<Image>().sprite = GuestDefualts[(int)(EcustomerType)curCustomerType];
        for (int i = 0; i < SlowMovingPos.Length; i++)
        {
            if (i != SlowMovingPos.Length - 1)
                transform.DOMove(SlowMovingPos[i].position, delayTime);
            else
                transform.DOMove(SlowMovingPos[i].position, 0.25f);


            yield return new WaitForSeconds(delayTime);
        }

        //빠르게 이동할때 가면 감지
        playerDetect = true;
        //빠르게 이동
        transform.DOMove(FastMovingPos.position, delayTime);
        gameObject.GetComponent<Image>().DOColor(new Color(1,1,1,0), delayTime);
        yield return new WaitForSeconds(1.5f);

        //주문 테이블쪽 이동
        gameObject.transform.parent = BackgroundCanvas.transform;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 1000);
        gameObject.GetComponent<Image>().sprite = GuestDefualts[(int)(EcustomerType)curCustomerType]; 
        transform.position = OrderPos[0].position;
        transform.DOMove(OrderPos[1].position, delayTime);
        gameObject.GetComponent<Image   >().DOColor(new Color(1, 1, 1, 1), delayTime);


        yield return new WaitForSeconds(delayTime);
        playerDetect = false;
    }
}
