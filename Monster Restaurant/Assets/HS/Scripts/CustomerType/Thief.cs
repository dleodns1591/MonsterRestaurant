using HS_Tree;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Thief : MonoBehaviour, I_CustomerType
{
    Button cookBtn;
    Button askBtn;
    int rand;
    public string SpecialAnswer()
    {
        if (OrderManager.Instance.isCookingSuccess)
        {
            return "쳇... 음식 덕분에 산 줄 알아라..";
        }
        else
        {
            GameManager.Instance.Money -= 10;
            GameManager.Instance.SalesRevenue -= 10;
            return "제대로 만들 것이지.. 쯧...";
        }
    }

    public void SpecialType(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.StopOrderCoroutine();
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();
        rand = UnityEngine.Random.Range(0, OrderManager.Instance.OrderTalkTxt.text.Split('\n').Length);

        if (GameManager.Instance.shop.isFinalEvolution == false)
        {
            NotBloom(cook, ask);
        }
        else
        {
            Bloom(cook, ask);
        }
    }

    void RefuseOrder()
    {
        StartCoroutine(RefuseOrderDelay());
        IEnumerator RefuseOrderDelay()
        {
            OrderManager.Instance.StopOrderCoroutine();
            OrderManager.Instance.isNext = true;
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(OrderManager.Instance.ExitAndComein(true));
        }

    }
    void SucsessCook()
    {
        cookBtn.gameObject.SetActive(false);
        askBtn.gameObject.SetActive(false);
        
        //요리
        GameManager.Instance.ReturnCook();
            GameManager.Instance.ConditionSetting(GameManager.Instance.orderSets[rand].main, GameManager.Instance.orderSets[rand].sub, GameManager.Instance.orderSets[rand].count, GameManager.Instance.orderSets[rand].style, GameManager.Instance.orderSets[rand].dishCount);
    }

    void Bloom(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.OrderTalk[0] = OrderManager.Instance.RandomOrderSpeech(0)[rand];

        OrderManager.Instance.dialogNumber++;

        cook.text = "알겠습니다";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "알겠습니다";
            OrderManager.Instance.dialogNumber++;

            //요리
            SucsessCook();
        });
        ask.text = "네?";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "네?";

            OrderManager.Instance.OrderTalk[1] = "왜.. 한 번에 못알아 들어? 너도 내가 만만해?";
            OrderManager.Instance.isNext = true;

            if (GameManager.Instance.shop.isFinalEvolution == false)
            {
                cookBtn.gameObject.SetActive(false);
                cookBtn.onClick.RemoveAllListeners();
            }
            else
            {
                cook.text = "식인 식물을 보여준다.";
                cookBtn.onClick.RemoveAllListeners();
                cookBtn.onClick.AddListener(() =>
                {
                    OrderManager.Instance.AskTalk[1] = "식인 식물을 보여준다.";
                    OrderManager.Instance.OrderTalk[2] = "다..다음 부터 무..무시하지마..";

                    RefuseOrder();
                });
            }

            ask.text = "죄송합니다.";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "죄송합니다.";
                OrderManager.Instance.OrderTalk[2] = "진작 그럴 것이지.. 쳇..";

                GameManager.Instance.Money = GameManager.Instance.Money / 4;
                RefuseOrder();
            });
        });
    }

    void NotBloom(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        Button cookBtn = cook.transform.parent.GetComponent<Button>();
        Button askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "돈 내놔!";

        cook.text = "네?";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            print("asd");
            OrderManager.Instance.AskTalk[0] = "네?";

            OrderManager.Instance.OrderTalk[1] = "내가 말해야 알아?";
            GameManager.Instance.Money = GameManager.Instance.Money / 3;
            RefuseOrder();
        });
        ask.text = "잠..잠시만요..";

        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "잠..잠시만요..";

            OrderManager.Instance.OrderTalk[1] = "큭큭.. 아주 좋아";
            GameManager.Instance.Money = GameManager.Instance.Money / 5;
            RefuseOrder();
        });
    }
}
