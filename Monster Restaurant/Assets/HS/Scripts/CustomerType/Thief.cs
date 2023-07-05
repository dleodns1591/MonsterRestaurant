using HS_Tree;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Thief : MonoBehaviour, I_CustomerType
{
    public string SpecialAnswer()
    {
        if (OrderManager.Instance.isCookingSuccess)
        {
            return "쳇... 음식 덕분에 산 줄 알아라..";
        }
        else
            return "제대로 만들 것이지.. 쯧...";
    }

    public void SpecialType(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        if(OrderManager.Instance.isHoldingFlower == false)
        {
            NotBloom(cook, ask);
        }
        else
        {
            Bloom(cook, ask);
        }    
    }

    void Bloom(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        Button cookBtn = cook.transform.parent.GetComponent<Button>();
        Button askBtn = ask.transform.parent.GetComponent<Button>();


        cook.text = "알겠습니다";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "알겠습니다";

            //요리
            GameManager.Instance.ReturnCook();
        });
        ask.text = "네?";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "네?";

            OrderManager.Instance.OrderTalk[1] = "왜.. 한 번에 못알아 들어? 너도 내가 만만해?";
            OrderManager.Instance.isNext = true;
            if (OrderManager.Instance.isBloom)
            {
                cook.text = "식인 식물을 보여준다.";
                ask.text = "죄송합니다.";

                cookBtn.onClick.RemoveAllListeners();
                cookBtn.onClick.AddListener(() =>
                {
                    OrderManager.Instance.AskTalk[1] = "식인 식물을 보여준다.";
                    OrderManager.Instance.OrderTalk[2] = "다..다음 부터 무..무시하지마..";

                    OrderManager.Instance.isNext = true;

                    //다음 손님
                    OrderManager.Instance.ExitAndComein();
                });
                askBtn.onClick.RemoveAllListeners();
                askBtn.onClick.AddListener(() =>
                {
                    OrderManager.Instance.AskTalk[1] = "죄송합니다.";
                    OrderManager.Instance.OrderTalk[2] = "진작 그럴 것이지.. 쳇..";

                    OrderManager.Instance.isNext = true;

                    //현재 금액 1 /4 로
                    GameManager.Instance.Money = GameManager.Instance.Money / 4;
                    //다음 손님
                    OrderManager.Instance.ExitAndComein();
                });
            }
            else
            {
                OrderManager.Instance.OrderTalk[1] = "왜.. 한 번에 못알아 들어? 너도 내가 만만해?";
                cook.transform.parent.gameObject.SetActive(false);
                ask.text = "죄송합니다.";
                askBtn.onClick.RemoveAllListeners();
                askBtn.onClick.AddListener(() =>
                {
                    OrderManager.Instance.AskTalk[1] = "죄송합니다.";
                    OrderManager.Instance.OrderTalk[2] = "진작 그럴 것이지.. 쳇..";

                    askBtn.gameObject.SetActive(false);
                    OrderManager.Instance.isNext = true;

                    //현재 금액 1 /4 로
                    GameManager.Instance.Money = GameManager.Instance.Money / 4;
                    //다음 손님
                    OrderManager.Instance.ExitAndComein();
                });
            }
        });
    }

    void NotBloom(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        Button cookBtn = cook.transform.parent.GetComponent<Button>();
        Button askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "돈 내놔!";

        cook.text = "네?";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "네?";

            OrderManager.Instance.OrderTalk[1] = "내가 말해야 알아?";
            OrderManager.Instance.isNext = true;

            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            //현재 금액 1 /3 로
            GameManager.Instance.Money = GameManager.Instance.Money / 3;
            //다음 손님
            OrderManager.Instance.ExitAndComein();
        });
        ask.text = "잠..잠시만요..";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "잠..잠시만요..";

            OrderManager.Instance.OrderTalk[1] = "큭큭.. 아주 좋아";
            OrderManager.Instance.isNext = true;

            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            //현재 금액 1 /5 로
            GameManager.Instance.Money = GameManager.Instance.Money / 5;
            //다음 손님
            OrderManager.Instance.ExitAndComein();
        });
        OrderManager.Instance.OrderTalk[2] = "";
    }
}
