using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GroupOrder : MonoBehaviour, I_CustomerType
{
    public string SpecialAnswer()
    {
        if (OrderManager.Instance.isCookingSuccess)
        {
            return "빠르게 만들어주셔서 감사합니다. 아이들이 좋아할 거예요";
            //돈 증가
        }
        else
            return "늦게 주시면 어떡해요..! 현재 급식 배분을 잘 처리해서 다행이지만, 돈은 못드리겠네요";
    }

    public void SpecialType(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.OrderTalk[0] = "안녕하세요. 옆 건물에서 어린이집 교사로 일하고 있습니다. 현재 급식 배분에 문제가 생겨서 그러는데 n초 안에 n개의(주문 내용)을 만들어 주실 수 있나요 ?";

        Button cookBtn = cook.transform.parent.GetComponent<Button>();
        Button askBtn = ask.transform.parent.GetComponent<Button>();

        cook.text = "알겠습니다";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "알겠습니다";
            OrderManager.Instance.dialogNumber++;

            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);

            //요리
            GameManager.Instance.ReturnCook();

        });

        askBtn.gameObject.SetActive(false);
    }
}
