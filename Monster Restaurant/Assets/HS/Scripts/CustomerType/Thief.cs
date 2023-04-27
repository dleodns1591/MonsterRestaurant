using HS_Tree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EThiefSpeechCase
{
    NormalOrder,


}

public enum EThiefSelectCase
{
    Okey,
    ReOrder,
    What,
    Wait,
    Show,
    Sorry
}

public class Thief : MonoBehaviour, I_CustomerType
{
    public void SpecialType(UIText cook, UIText ask, UIText speech)
    {
        if(Customer.isHoldingFlower == false)
        {
            Bloom(cook, ask, speech);
        }
        else
        {
            NotBloom(cook, ask, speech);
        }    
    }

    void Bloom(UIText cook, UIText ask, UIText speech)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        cook.text = "알겠습니다";
        cookBtn.onClick.AddListener(() =>
        {
            //요리
        });
        ask.text = "네?";
        askBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "왜.. 한 번에 못알아 들어? 너도 내가 만만해?";
            if (Customer.isBloom)
            {
                cook.text = "식인 식물을 보여준다.";
                ask.text = "죄송합니다.";

                cookBtn.onClick.RemoveAllListeners();
                cookBtn.onClick.AddListener(() =>
                {
                    Customer.OrderTalk[2] = "다..다음 부터 무..무시하지마..";

                    //다음 손님
                });
                askBtn.onClick.RemoveAllListeners();
                askBtn.onClick.AddListener(() =>
                {
                    Customer.OrderTalk[2] = "진작 그럴 것이지.. 쳇..";

                    //현재 금액 1 /4 로
                    //다음 손님
                });
            }
            else
            {
                Customer.OrderTalk[1] = "왜.. 한 번에 못알아 들어? 너도 내가 만만해?";
                cook.transform.parent.gameObject.SetActive(false);
                ask.text = "죄송합니다.";
                askBtn.onClick.RemoveAllListeners();
                askBtn.onClick.AddListener(() =>
                {
                    Customer.OrderTalk[2] = "진작 그럴 것이지.. 쳇..";

                    //현재 금액 1 /4 로
                    //다음 손님
                });
            }
        });
    }

    void NotBloom(UIText cook, UIText ask, UIText speech)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        Customer.OrderTalk[0] = "돈 내놔!";

        cook.text = "네?";
        cookBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "내가 말해야 알아?";

            //현재 금액 1 / 3으로

            //다음 손님
        });
        ask.text = "잠..잠시만요..";
        askBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "큭큭.. 아주 좋아";

            //현재 금액 1 / 5으로

            //다음 손님
        });
    }
}
