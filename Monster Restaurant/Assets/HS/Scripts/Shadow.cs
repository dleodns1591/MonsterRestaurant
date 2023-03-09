using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Shadow : MonoBehaviour
{
    [SerializeField]
    private Transform[] SlowMovingPos, OrderPos;
    [SerializeField]
    private Transform FastMovingPos;
    [SerializeField]
    private Sprite GuestSpr;

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
        gameObject.GetComponent<SpriteRenderer>().DOColor(new Color(1,1,1,0), delayTime);
        yield return new WaitForSeconds(1.5f);

        //�ֹ� ���̺��� �̵�
        gameObject.GetComponent<SpriteRenderer>().sprite = GuestSpr; 
        transform.position = OrderPos[0].position;
        transform.DOMove(OrderPos[1].position, delayTime);
        gameObject.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), delayTime);


        yield return new WaitForSeconds(delayTime);
        playerDetect = false;
    }
}
