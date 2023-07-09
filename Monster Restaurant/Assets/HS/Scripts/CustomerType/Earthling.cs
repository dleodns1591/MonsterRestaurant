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

        if (GameManager.Instance.Satisfaction >= 40)
        {
            OrderManager.Instance.EeventCustomerSetting((int)EeventCustomerType.Human);
            OrderManager.Instance.CustomerImg.sprite = OrderManager.Instance.EventGuestSuccess[(int)EeventCustomerType.Human];
            switch (OrderManager.Instance.Earthling_SuccessPoint - 1)
            {
                case 0:
                    return "감사합니다. 드디어 먹을 수 있는 가게를 찾았네요.";
                case 1:
                    return "이번에도 감사합니다...! 다음에 또 오겠습니다.";
                case 2:
                    return "이번에도 감사합니다...! 다음에 또 오겠습니다.";
                case 3:
                    return "이번에도 감사합니다...! 다음에 또 오겠습니다.";
                case 4:
                    return "역시, 계속 먹어보니 친근감이 드네요.";
                default:
                    return "제 판단은 틀리지 않았어요. 그 친근감은 당신이 지구인이기 때문에 느껴졌어요.\r\n같은 지구인이라 반가워서 그러는데 저와 함께 이 행성을 빠져나가실래요?";
            }
        }
        else
        {
            OrderManager.Instance.CustomerImg.sprite = OrderManager.Instance.EventGuestFails[(int)EeventCustomerType.Human];

            GameManager.Instance.isEarthlingRefuse = true;
            return "......죄송합니다, 다음 가게를 찾아야 하네..";
        }
    }


    public void SpecialType(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.StopOrderCoroutine();

        switch (OrderManager.Instance.Earthling_SuccessPoint)
        {
            case 0:
                Point0(cook, ask);
                break;
            case 1:
                Point1(cook, ask);
                break;
            case 2:
                Point2(cook, ask);
                break;
            case 3:
                Point3(cook, ask);
                break;
            case 4:
                Point4(cook, ask);
                break;
            case 5:
                Point5(cook, ask);
                break;
        }
    }

    void RefuseOrder()
    {
        StartCoroutine(RefuseOrderDelay());
        IEnumerator RefuseOrderDelay()
        {
            for (int i = 0; i < OrderManager.Instance.OrderTalk.Length; i++)
            {
                print(OrderManager.Instance.OrderTalk[i]);
            }

            GameManager.Instance.isEarthlingRefuse = true;
            OrderManager.Instance.isNext = true;
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(OrderManager.Instance.ExitAndComein(true));
            OrderManager.Instance.StopOrderCoroutine();

        }

    }

    void Point0(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "밥을 안 먹은 지 꽤 된 것 같은데 구워서 주실 수 있나요?";
        OrderManager.Instance.dialogNumber++;
        cook.text = "조금만 기다려 주세요.";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "조금만 기다려 주세요.";
            OrderManager.Instance.dialogNumber++;
            SucsessCook();
            List<ESubMatarials> subs = new List<ESubMatarials>
        {
            ESubMatarials.NULL
        };
            GameManager.Instance.ConditionSetting(EMainMatarials.Rice, subs,     0, ECookingStyle.Roast, 1);
        });
        ask.text = "저희가 메인 재료만 팔지 않아서 안 될 것 같습니다.";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "저희가 메인 재료만 팔지 않아서 안 될 것 같습니다.";
            OrderManager.Instance.dialogNumber++;
            OrderManager.Instance.OrderTalk[1] = "이 가게도 안 되는건가...";
            OrderManager.Instance.dialogNumber++;
            RefuseOrder();
        });
    }
    void Point1(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "안녕하세요. 혹시 이번에도 메인 재료만 시켜도 될까요..?";
        OrderManager.Instance.dialogNumber++;

        cook.text = "네";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "네";
            OrderManager.Instance.dialogNumber++;

            OrderManager.Instance.OrderTalk[1] = "그러면 혹시, 이번에는 고기를 구워서 주실 수 있나요?";
            OrderManager.Instance.dialogNumber++;

            OrderManager.Instance.isNext = true;

            cook.text = "네";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "네";
                OrderManager.Instance.dialogNumber++;
                SucsessCook();
                List<ESubMatarials> subs = new List<ESubMatarials>
                {
                    ESubMatarials.NULL
                };
                GameManager.Instance.ConditionSetting(EMainMatarials.Meat, subs, 0, ECookingStyle.Roast, 1);


                askBtn.gameObject.SetActive(false);
            });

            askBtn.onClick.RemoveAllListeners();
        });

        ask.text = "이번에는 힘들 것 같습니다.";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "이번에는 힘들 것 같습니다.";
            OrderManager.Instance.dialogNumber++;
            OrderManager.Instance.OrderTalk[1] = "......죄송합니다, 다음 가게를 찾아야 하네..";
            OrderManager.Instance.dialogNumber++;

            RefuseOrder();

        });
    }
    void Point2(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "안녕하세요.. 이번에는 파스타를 끓여서 주실 수 있나요?";
        OrderManager.Instance.dialogNumber++;
        cook.text = "네";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "네";
            OrderManager.Instance.dialogNumber++;
            SucsessCook();
            List<ESubMatarials> subs = new List<ESubMatarials>
        {
            ESubMatarials.NULL
        };
            GameManager.Instance.ConditionSetting(EMainMatarials.Noodle, subs, 0, ECookingStyle.Boil, 1);
        });

        ask.text = "이번에는 힘들 것 같습니다.";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "이번에는 힘들 것 같습니다.";
            OrderManager.Instance.dialogNumber++;
            OrderManager.Instance.OrderTalk[1] = "......죄송합니다, 다음 가게를 찾아야 하네..";
            OrderManager.Instance.dialogNumber++;

            RefuseOrder();

        });
    }
    void Point3(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "안녕하세요.. 이번에는 빵을 구워서 주실 수 있나요?";
        OrderManager.Instance.dialogNumber++;
        cook.text = "네";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "네";
            OrderManager.Instance.dialogNumber++;
            SucsessCook();
            List<ESubMatarials> subs = new List<ESubMatarials>
        {
            ESubMatarials.NULL
        };
            GameManager.Instance.ConditionSetting(EMainMatarials.Bread, subs, 0, ECookingStyle.Roast, 1);
        });
        ask.text = "이번에는 힘들 것 같습니다.";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "이번에는 힘들 것 같습니다.";
            OrderManager.Instance.dialogNumber++;
            OrderManager.Instance.OrderTalk[1] = "......죄송합니다, 다음 가게를 찾아야 하네..";
            OrderManager.Instance.dialogNumber++;

            RefuseOrder();

        });
    }
    void Point4(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "안녕하세요.. 이번에는 밥을 구워서 주실 수 있나요?";
        OrderManager.Instance.dialogNumber++;
        cook.text = "네";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "네";
            OrderManager.Instance.dialogNumber++;
            SucsessCook();
            List<ESubMatarials> subs = new List<ESubMatarials>
        {
            ESubMatarials.NULL
        };
            GameManager.Instance.ConditionSetting(EMainMatarials.Rice, subs, 0, ECookingStyle.Roast, 1);
        });
        ask.text = "이번에는 힘들 것 같습니다.";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "이번에는 힘들 것 같습니다.";
            OrderManager.Instance.dialogNumber++;
            OrderManager.Instance.OrderTalk[1] = "이 가게에 어딘가 친근감이 들었는데, 아니었네요....";
            OrderManager.Instance.dialogNumber++;

            RefuseOrder();

        });
    }
    void Point5(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "제 판단은 틀리지 않았어요. 그 친근감은 당신이 지구인이기 때문에 느껴졌어요. 같은 지구인이라 반가워서 그러는데 저와 함께 이 행성을 빠져나가실래요?";

        askBtn.GetComponent<Image>().enabled = false;
        askBtn.enabled = false;
        ask.text = "";
        cook.text = "알겠습니다";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.EndingProduction(EendingType.LookStar);
            GameManager.Instance.isEndingOpens[(int)EendingType.LookStar] = true;
            SaveManager.Instance.isEndingOpens[(int)EendingType.LookStar] = true;
            askBtn.GetComponent<Image>().enabled = true;
            askBtn.enabled = true;
            askBtn.gameObject.SetActive(false);
        });
    }
}

