using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GroupOrder : MonoBehaviour, I_CustomerType
{
    public void SpecialType(UIText cook, UIText ask)
    {
        OrderManager.OrderTalk[0] = "안녕하세요.옆 건물에서 어린이집 교사로 일하고 있습니다. 현재 급식 배분에 문제가 생겨서 그러는데 n초 안에 n개의(주문 내용)을 만들어 주실 수 있나요 ?";

        Button cookBtn = cook.transform.parent.GetComponent<Button>();
        Button askBtn = ask.transform.parent.GetComponent<Button>();

        cook.text = "알겠습니다";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            //요리


        });
    }
}
