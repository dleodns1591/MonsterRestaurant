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
    private Sprite[] GuestDefults;
    [SerializeField, Tooltip("��� ���� ���̱� �ϱ� ����")]
    private GameObject BackgroundCanvas;

    bool playerDetect = false;
    private void Start()
    {
        StartCoroutine(Moving());
    }

    /// <summary>
    /// ó�� ������� �ֹ������� �̵��ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator Moving()
    {
        float delayTime = 0.5f;

        for (int i = 0; i < SlowMovingPos.Length; i++)
        {
            if (i != SlowMovingPos.Length - 1)
                transform.DOMove(SlowMovingPos[i].position, delayTime);
            else
                transform.DOMove(SlowMovingPos[i].position, 0.25f);


            yield return new WaitForSeconds(delayTime);
        }

        //������ �̵��Ҷ� ���� ����
        playerDetect = true;
        //������ �̵�
        transform.DOMove(FastMovingPos.position, delayTime);
        gameObject.GetComponent<Image>().DOColor(new Color(1,1,1,0), delayTime);
        yield return new WaitForSeconds(1.5f);

        //�ֹ� ���̺��� �̵�
        gameObject.transform.parent = BackgroundCanvas.transform;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 1000);
        gameObject.GetComponent<Image>().sprite = GuestDefults[(int)EcustomerType.Alien]; 
        transform.position = OrderPos[0].position;
        transform.DOMove(OrderPos[1].position, delayTime);
        gameObject.GetComponent<Image   >().DOColor(new Color(1, 1, 1, 1), delayTime);


        yield return new WaitForSeconds(delayTime);
        playerDetect = false;
    }
}
