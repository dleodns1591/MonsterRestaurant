using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectingManager : MonoBehaviour
{
    [SerializeField] private GameObject SummonedObject;

    const float SpawnX = 5.76f, SpawnY = 0.59f;
    const float TargetX = 4.298f, TargetY = 4.245f;
    public void Directing(int money)
    {
        StartCoroutine(DirectingGoldCor(money));
    }

    IEnumerator DirectingGoldCor(int money)
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < 12; i++)
        {
            GameObject DirectingObj = Instantiate(SummonedObject, new Vector3(SpawnX, SpawnY), new Quaternion());

            Vector2 RandomPos = new Vector2(DirectingObj.transform.localPosition.x + Random.Range(-3f, 3f),
                                                DirectingObj.transform.localPosition.y + Random.Range(-2f, -4f));
            DirectingObj.transform.DOLocalMove(RandomPos, 0.7f);
            StartCoroutine(Directing2(DirectingObj, money));
        }
        IEnumerator Directing2(GameObject obj, int money)
        {
            yield return new WaitForSeconds(0.7f);
            obj.transform.DOMove(new Vector2(TargetX, TargetY), 0.5f);
            yield return new WaitForSeconds(0.5f);
            Destroy(obj);

            GameManager.Instance.Money += money;
            GameManager.Instance.SalesRevenue += money;
        }

    }
}
