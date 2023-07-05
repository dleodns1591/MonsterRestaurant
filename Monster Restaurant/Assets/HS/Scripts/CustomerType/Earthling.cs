using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Earthling : MonoBehaviour, I_CustomerType
{
    Button cookBtn;
    Button askBtn;

    void SucsessCook()
    {
        OrderManager.Instance.Earthling_SuccessPoint++;

        cookBtn.gameObject.SetActive(false);
        askBtn.gameObject.SetActive(false);

        //요리
        GameManager.Instance.ReturnCook();
    }

    public string SpecialAnswer()
    {
        switch (OrderManager.Instance.Earthling_SuccessPoint)
        {
            case 0:
                return "큭큭..이 은혜는.. 꼭 갚겠습니다.";
            case 1:
                return "다시 한 번 말하지만 이 은혜는 꼭 갚겠습니다.. 큭큭";
            case 2:
                return "큭큭 감사합니다!..";
            case 3:
                return "감사합니다!.. 큭큭";
            case 4:
                return "감사합니다!.. 큭큭";
            default:
                return " ";
        }
    }
 

    public void SpecialType(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        
    }

    void Point0(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "밥을 안 먹은 지 꽤 된 것 같은데 구워서 주실 수 있나요?";

        cook.text = "조금만 기다려 주세요.";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "조금만 기다려 주세요.";

            SucsessCook();

            //끝난 뒤 말
            //"큭큭..이 은혜는.. 꼭 갚겠습니다."
        });
        ask.text = "아잇... 나가세요";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "아잇... 나가세요";

            //RefuseOrder();

        });
    }
}

