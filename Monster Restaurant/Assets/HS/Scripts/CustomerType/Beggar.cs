using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Beggar : MonoBehaviour, I_CustomerType
{
    OrderManager OM;
    GameManager GM;
    OrderButtonObject BtnObjects;
    Button cookBtn;
    Button askBtn;
    TextMeshProUGUI cook, ask;

    public string SpecialAnswer()
    {
        OM.customerManager.EeventCustomerSetting((int)EeventCustomerType.Beggar);
        OM.customer.CustomerImg.sprite = OM.customer.EventGuestSuccess[(int)EeventCustomerType.Beggar];
        switch (OM.Beggar_SuccessPoint)
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

    public void SpecialType()
    {
        BtnObjects = OrderButtonObject.Instance;
        OM = OrderManager.Instance;
        GM = GameManager.Instance;

        cookBtn = BtnObjects.CookingBtn;
        askBtn = BtnObjects.ReAskBtn;
        cook = BtnObjects.BtnCookText;
        ask = BtnObjects.BtnAskText;

        OM.StopOrderCoroutine();

        switch (OM.Beggar_SuccessPoint)
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

    void SucsessCook()
    {
        OM.Beggar_SuccessPoint++;

        cookBtn.gameObject.SetActive(false);
        askBtn.gameObject.SetActive(false);

        OM.isBeggar = true;
        //요리
        GM.ReturnCook();
        List<ESubMatarials> subs = new List<ESubMatarials>
        {
            ESubMatarials.NULL
        };
        GM.ConditionSetting(EMainMatarials.NULL, subs, 0, ECookingStyle.None, 1);
    }
    void RefuseOrder()
    {
        StartCoroutine(RefuseOrderDelay());
        IEnumerator RefuseOrderDelay()
        {
            GM.isBeggarRefuse = true;
            OM.StopOrderCoroutine();
            OM.isNext = true;
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(OM.ExitAndComein());
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
    void ButtonAssignment(string cookBtnText, string askBtnText, string RefuseText)
    {
        cook.text = cookBtnText;
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = cookBtnText;
            OM.dialogNumber++;

            SucsessCook();
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


            switch (OM.Beggar_SuccessPoint)
            {
                case 2:
                    GM.Money += 10;
                    break;
                case 3:
                    GM.Money += 20;
                    break;
                case 4:
                    GM.Money += 30;
                    break;
                case 5:
                    List<ESubMatarials> subs = new List<ESubMatarials> { ESubMatarials.NULL };

                    OM.dialogNumber++;

                    askBtn.gameObject.SetActive(false);

                    cook.text = "알겠습니다";
                    cookBtn.onClick.RemoveAllListeners();
                    cookBtn.onClick.AddListener(() =>
                    {
                        OM.AskTalk[0] = "알겠습니다";
                        OM.dialogNumber++;

                        SucsessCook();
                        GM.ConditionSetting(EMainMatarials.NULL, subs, 0, ECookingStyle.None, 3);

                        GM.Money += 10000000000;
                        //개천에서 용난다 엔딩 ON
                        OM.endingManager.EndingProduction(EendingType.Dragon);
                        GM.IsEndingOpens[(int)EendingType.Dragon] = true;
                    });
                    break;
            }
        });
    }

    void Point0()
    {
        FirstTalking("제발요..너무 배고파요..");

        ButtonAssignment("잠시만요", "아잇... 나가세요", "그렇게..깐깐하게 살다가 가게가 망해버릴 거야!");
    }

    void Point1()
    {
        FirstTalking("안녕하세요, 또 저에요, 혹시.. 한번 더 주실 수 있으실까요?");

        cook.text = "잠시만요";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "잠시만요";
            OM.dialogNumber++;

            SucsessCook();
        });
        ask.text = "네?";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.AskTalk[0] = "네?";
            OM.dialogNumber++;

            OM.OrderTalk[1] = "제발요.. 돈은 언젠간 드릴테니..";
            OM.dialogNumber++;

            OM.isNext = true;

            cook.text = "알겠습니다";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OM.AskTalk[1] = "알겠습니다";
                OM.dialogNumber++;

                SucsessCook();

            });

            ask.text = "나가세요";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OM.AskTalk[1] = "나가세요";
                OM.dialogNumber++;
                OM.OrderTalk[2] = "가게.. 망해버려라..";
                OM.dialogNumber++;

                RefuseOrder();
            });
        });
    }

    void Point2()
    {
        FirstTalking("안녕하세요.. 사장님 오늘도 가능한가요..?");

        ButtonAssignment("잠시만요", "나가세요", "여태까지.. 무료로 음식을 만들어주셔서 감사합니다.. 사장님");
    }

    void Point3()
    {
        FirstTalking("안녕하세요.. 사장님 늘 먹던걸로 주세요..");

        ButtonAssignment("잠시만요", "나가세요", "여태까지.. 무료로 음식을 만들어주셔서 감사합니다.. 사장님");
    }

    void Point4()
    {
        FirstTalking("사장님 아시죠..?");

        ButtonAssignment("잠시만요", "나가세요", "여태까지.. 무료로 음식을 만들어주셔서 감사합니다.. 사장님");
    }

    void Point5()
    {
        FirstTalking("쉬운일이 아니였을 텐데 음식을 매번 무료로 주셔서 감사합니다. \n마지막으로 늘 먹던걸로 음식 3개 가능할까요?");

        ButtonAssignment("", "나가세요", "");
    }
}
