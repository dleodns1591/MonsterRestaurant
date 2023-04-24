using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Beggar : MonoBehaviour, I_CustomerType
{
    public void SpecialType(UIText cook, UIText ask, UIText speech)
    {
        
    }
    void Point0(UIText cook, UIText ask)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        Customer.OrderTalk[0] = "제발요..너무 배고파요..";
        cook.text = "잠시만요";
        cookBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "가..감사합니다..큭큭";
            Customer.SuccessPoint++;

            //요리

            //끝난 뒤 말
            //"큭큭..이 은혜는.. 꼭 갚겠습니다."
        });
        ask.text = "아잇... 나가세요";
        askBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "그렇게..깐깐하게 살다가 가게가 망해버릴거야!";

            //다음 손님
        });
    }

    void Point1(UIText cook, UIText ask)
    {
        Button cookBtn = cook.GetComponentInParent<Button>();
        Button askBtn = ask.GetComponentInParent<Button>();

        Customer.OrderTalk[0] = "안녕하세요, 또 저에요, 혹시.. (주문 내용)을 주실 수 있으실까요?";
        cook.text = "잠시만요";
        cookBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "큭큭..";
            Customer.SuccessPoint++;

            //요리

            //끝난 뒤 말
            //"다시 한 번 말하지만 이 은혜는 꼭 갚겠습니다.. 큭큭"
        });
        ask.text = "네?";
        askBtn.onClick.AddListener(() =>
        {
            Customer.OrderTalk[1] = "제발요.. 돈은 언젠간 드릴테니..";

            cook.text = "알겠습니다";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                Customer.OrderTalk[2] = "큭큭..";
                Customer.SuccessPoint++;

            });

            ask.text = "나가세요";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                Customer.OrderTalk[2] = "가게.. 망해버려라..";

                //다음 손님

            });
        });
    }

    void Point2(UIText cook, UIText ask)
    {

    }

    void Point3(UIText cook, UIText ask)
    {

    }

    void Point4(UIText cook, UIText ask)
    {

    }
}
