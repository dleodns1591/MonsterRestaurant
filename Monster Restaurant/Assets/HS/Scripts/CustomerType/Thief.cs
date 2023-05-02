using HS_Tree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thief : MonoBehaviour, I_CustomerType
{
    public void SpecialType(UIText cook, UIText ask)
    {
        if(Customer.isHoldingFlower == false)
        {
            Bloom(cook, ask);
        }
        else
        {
            NotBloom(cook, ask);
        }    
    }

    void Bloom(UIText cook, UIText ask)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        cook.text = "알겠습니다";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "알겠습니다";
            //요리
        });
        ask.text = "네?";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "네?";

            OrderManager.OrderTalk[1] = "왜.. 한 번에 못알아 들어? 너도 내가 만만해?";
            if (Customer.isBloom)
            {
                cook.text = "식인 식물을 보여준다.";
                ask.text = "죄송합니다.";

                cookBtn.onClick.RemoveAllListeners();
                cookBtn.onClick.AddListener(() =>
                {
                    OrderManager.AskTalk[1] = "식인 식물을 보여준다.";
                    OrderManager.OrderTalk[2] = "다..다음 부터 무..무시하지마..";

                    //다음 손님
                });
                askBtn.onClick.RemoveAllListeners();
                askBtn.onClick.AddListener(() =>
                {
                    OrderManager.AskTalk[1] = "죄송합니다.";
                    OrderManager.OrderTalk[2] = "진작 그럴 것이지.. 쳇..";

                    //현재 금액 1 /4 로
                    //다음 손님
                });
            }
            else
            {
                OrderManager.OrderTalk[1] = "왜.. 한 번에 못알아 들어? 너도 내가 만만해?";
                cook.transform.parent.gameObject.SetActive(false);
                ask.text = "죄송합니다.";
                askBtn.onClick.RemoveAllListeners();
                askBtn.onClick.AddListener(() =>
                {
                    OrderManager.AskTalk[1] = "죄송합니다.";
                    OrderManager.OrderTalk[2] = "진작 그럴 것이지.. 쳇..";

                    //현재 금액 1 /4 로
                    //다음 손님
                });
            }
        });
    }

    void NotBloom(UIText cook, UIText ask)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        OrderManager.OrderTalk[0] = "돈 내놔!";

        cook.text = "네?";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "네?";

            OrderManager.OrderTalk[1] = "내가 말해야 알아?";

            //현재 금액 1 / 3으로

            //다음 손님
        });
        ask.text = "잠..잠시만요..";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.AskTalk[0] = "잠..잠시만요..";

            OrderManager.OrderTalk[1] = "큭큭.. 아주 좋아";

            //현재 금액 1 / 5으로

            //다음 손님
        });
    }
}
