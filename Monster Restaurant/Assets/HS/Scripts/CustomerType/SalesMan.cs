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

        OrderManager.Instance.OrderTalk[0] = "�췡? ����? �� �̴�� �����̾� ��";

        OrderManager.Instance.dialogNumber++;
        cook.text = "�췡";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);

            GameManager.Instance.ShopAppearProd();
        });

        ask.text = "����";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.OrderTalk[1] = "�̰� �Ȼ紩...;;;;;";

            RefuseOrder();
        });
    }
}
