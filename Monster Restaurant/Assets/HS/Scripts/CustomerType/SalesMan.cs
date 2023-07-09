using HS_Tree;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SalesMan : MonoBehaviour, I_CustomerType
{
    Button cookBtn;
    Button askBtn;

    void RefuseOrder()
    {
        StartCoroutine(RefuseOrderDelay());
        IEnumerator RefuseOrderDelay()
        {
            OrderManager.Instance.isNext = true;
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(OrderManager.Instance.ExitAndComein(true));
            OrderManager.Instance.StopOrderCoroutine();
        }
    }

    public string SpecialAnswer()
    {
        return " ";
    }

    public void SpecialType(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.StopOrderCoroutine();
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "안녕하세요. 몬스터 아웃핏터스 회사의 영업사원을 맡게 된 리시드입니다. 제가 아주 좋은 물건\r\n들을 가지고 왔는데.. 한 번 확인해 보시겠습니까?";

        OrderManager.Instance.dialogNumber++;
        cook.text = "확인하겠습니다.";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);

            GameManager.Instance.ShopAppearProd();
        });

        ask.text = "나가주세요.";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.OrderTalk[1] = "이런.. 실례를 범했군요..";

            RefuseOrder();
        });
    }
}
