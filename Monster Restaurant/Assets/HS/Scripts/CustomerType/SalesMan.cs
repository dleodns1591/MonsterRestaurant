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
            StartCoroutine(OrderManager.Instance.ExitAndComein());
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

        OrderManager.Instance.OrderTalk[0] = "살래? 말래? 난 이대운 병신이야 ㅋ";

        OrderManager.Instance.dialogNumber++;
        cook.text = "살래";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);

            GameManager.Instance.ShopAppearProd();
        });

        ask.text = "말래";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.OrderTalk[1] = "이걸 안사누...;;;;;";

            RefuseOrder();
        });
    }
}
