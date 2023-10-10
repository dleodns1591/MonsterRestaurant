using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Earthling : MonoBehaviour, I_CustomerType
{
    OrderManager OM;
    GameManager GM;
    SaveManager SM;
    OrderButtonObject BtnObjects;
    Button cookBtn;
    Button askBtn;
    TextMeshProUGUI cook, ask;
    List<ESubMatarials> subs;

    void SucsessCook()
    {
        OM.Earthling_SuccessPoint++;

        cookBtn.gameObject.SetActive(false);
        askBtn.gameObject.SetActive(false);

        //요리
        GM.ReturnCook();
    }

    void RefuseOrder()
    {
        StartCoroutine(RefuseOrderDelay());
        IEnumerator RefuseOrderDelay()
        {
            GM.isEarthlingRefuse = true;
            OM.isNext = true;
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(OM.ExitAndComein());

        }

    }

    public string SpecialAnswer()
    {
        OM.perfectMade = 1;

        if (GM.Satisfaction >= 40)
        {
            OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.Human);
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestSuccess[(int)EeventCustomerType.Human];
            if(SM.isEnglish == false)
            {
                switch (OM.Earthling_SuccessPoint - 1)
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
                switch (OM.Earthling_SuccessPoint - 1)
                {
                    case 0:
                        return "thank you I finally found a restaurant where I could eat.";
                    case 1:
                        return "Is it not even this store...";
                    case 2:
                        return "Is it not even this store...";
                    case 3:
                        return "Is it not even this store...";
                    case 4:
                        return "As expected, it feels familiar after eating it over and over again.";
                    default:
                        return "My judgment was not wrong. I felt that friendliness because you were an Earthling.\r\nI'm glad we're fellow Earthlings, but will you leave this planet with me?";
                }
            }
            
        }
        else
        {
            OM.customer.CustomerImg.sprite = OM.customer.EventGuestFails[(int)EeventCustomerType.Human];

            GameManager.Instance.isEarthlingRefuse = true;
            return "I felt somewhat familiar with this store, but it wasn't...";
        }
    }

    void FirstTalking(string speech)
    {
        OM.OrderTalk[0] = speech;
        OM.dialogNumber++;
    }

    /// <summary>
    /// 버튼에 이벤트 넣어주는 함수
    /// </summary>
    /// <param name="cookBtnText">1번째 버튼에 적힐 말</param>
    /// <param name="askBtnText">2번째 버튼에 적힐 말</param>
    /// <param name="RefuseText">2번째 버튼을 누르면 나오는 말</param>
    /// <param name="main">원하는 메인재료</param>
    /// <param name="style">원하는 쿠킹방식</param>
    void ButtonAssignment(string cookBtnText, string askBtnText, string RefuseText, EMainMatarials main, ECookingStyle style)
    {
        cook.text = cookBtnText;
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = cookBtnText;
            OM.dialogNumber++;
            SucsessCook();
            GM.ConditionSetting(main, subs, 0, style, 1);
        });
        ask.text = askBtnText;
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = askBtnText;
            OM.dialogNumber++;
            OM.OrderTalk[1] = RefuseText;
            OM.dialogNumber++;
            RefuseOrder();
        });
    }

    public void SpecialType()
    {
        BtnObjects = OrderButtonObject.Instance;
        OM = OrderManager.Instance;
        GM = GameManager.Instance;
        SM = SaveManager.Instance;
        cookBtn = BtnObjects.CookingBtn;
        askBtn = BtnObjects.ReAskBtn;
        cook = BtnObjects.BtnCookText;
        ask = BtnObjects.BtnAskText;

        OM.perfectMade = 100;

        subs = new List<ESubMatarials> { ESubMatarials.NULL };

        OM.StopOrderCoroutine();

        switch (OM.Earthling_SuccessPoint)
        {
            case 0:
                Point0();
                break;
            case 1:
                Point1();
                break;
            case 2:
                Point2();
                break;
            case 3:
                Point3();
                break;
            case 4:
                Point4();
                break;
            case 5:
                Point5();
                break;
        }
    }


    void Point0()
    {
        if(SM.isEnglish == false)
        {
            FirstTalking("밥을 안 먹은 지 꽤 된 것 같은데 구워서 주실 수 있나요?");

            ButtonAssignment("조금만 기다려 주세요.", "저희가 메인 재료만 팔지 않아서 안 될 것 같습니다.", "이 가게도 안 되는건가...", EMainMatarials.Rice, ECookingStyle.Roast);
        }
        else
        {
            FirstTalking("It seems like I haven't eaten in a while. Can you cook it for me?");

            ButtonAssignment("Please wait a minute.", "I don't think we can because we don't sell only the main ingredients. I'm sorry.", "Is it not even this store...", EMainMatarials.Rice, ECookingStyle.Roast);
        }
    }
    void Point1()
    {
        if (SM.isEnglish == false)
        {
            FirstTalking("안녕하세요. 이번에는 고기를 구워서 주실 수 있나요?");

            ButtonAssignment("네", "이번에는 힘들 것 같습니다.", "이 가게도 안 되는건가...", EMainMatarials.Meat, ECookingStyle.Roast);
        }
        else
        {
            FirstTalking("can you grill the meat and give it to me this time?");

            ButtonAssignment("All right.", "I think it's going to be difficult this time.", ".... I'm sorry, I need to find the next store..", EMainMatarials.Meat, ECookingStyle.Roast);
        }
    }
    void Point2()
    {
        if (SM.isEnglish == false)
        {
            FirstTalking("안녕하세요.. 이번에는 파스타를 끓여서 주실 수 있나요?");

            ButtonAssignment("네", "이번에는 힘들 것 같습니다.", "......죄송합니다, 다음 가게를 찾아야 하네..", EMainMatarials.Noodle, ECookingStyle.Boil);
        }
        else
        {
            FirstTalking("Hello.. Can you boil the pasta this time?");

            ButtonAssignment("All right.", "I think it's going to be difficult this time.", ".... I'm sorry, I need to find the next store..", EMainMatarials.Noodle, ECookingStyle.Boil);
        }
    }
    void Point3()
    {
        if (SM.isEnglish == false)
        {
            FirstTalking("안녕하세요.. 이번에는 빵을 구워서 주실 수 있나요?");

            ButtonAssignment("네", "이번에는 힘들 것 같습니다.", "......죄송합니다, 다음 가게를 찾아야 하네..", EMainMatarials.Bread, ECookingStyle.Roast);
        }
        else
        {
            FirstTalking("Hello.. Can you bake and give me bread this time?");

            ButtonAssignment("All right.", "I think it's going to be difficult this time.", ".... I'm sorry, I need to find the next store..", EMainMatarials.Bread, ECookingStyle.Roast);
        }
    }
    void Point4()
    {
        if (SM.isEnglish == false)
        {
            FirstTalking("안녕하세요.. 이번에는 밥을 구워서 주실 수 있나요?");

            ButtonAssignment("네", "이번에는 힘들 것 같습니다.", "이 가게에 어딘가 친근감이 들었는데, 아니었네요....", EMainMatarials.Rice, ECookingStyle.Roast);
        }
        else
        {
            FirstTalking("Hello.. Can you cook the rice this time?");

            ButtonAssignment("All right.", "I think it's going to be difficult this time.", "I felt somewhat familiar with this store, but it wasn't...", EMainMatarials.Rice, ECookingStyle.Roast);
        }
    }
    void Point5()
    {

        if (SM.isEnglish == false)
        {
            FirstTalking("제 판단은 틀리지 않았어요. 그 친근감은 당신이 지구인이기 때문에 느껴졌어요. 같은 지구인이라 반가워서 그러는데 저와 함께 이 행성을 빠져나가실래요?");

            OM.OrderTalk[0] = "제 판단은 틀리지 않았어요. 그 친근감은 당신이 지구인이기 때문에 느껴졌어요. 같은 지구인이라 반가워서 그러는데 저와 함께 이 행성을 빠져나가실래요?";
        }
        else
        {
            FirstTalking("My judgment was not wrong. I felt that friendliness because you were an Earthling. I'm glad we're fellow Earthlings, but will you leave this planet with me?");

            OM.OrderTalk[0] = "My judgment was not wrong. I felt that friendliness because you were an Earthling. I'm glad we're fellow Earthlings, but will you leave this planet with me?";
        }


        askBtn.GetComponent<Image>().enabled = false;
        askBtn.enabled = false;
        ask.text = "";

        if (SM.isEnglish == false)
            cook.text = "알겠습니다";
        else
            cook.text = "All right";

        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.endingManager.EndingProduction(EendingType.LookStar);
            GM.IsEndingOpens[(int)EendingType.LookStar] = true;
            askBtn.GetComponent<Image>().enabled = true;
            askBtn.enabled = true;
            askBtn.gameObject.SetActive(false);
        });
    }
}

