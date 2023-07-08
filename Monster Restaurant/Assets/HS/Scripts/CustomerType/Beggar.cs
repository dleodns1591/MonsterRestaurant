using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Beggar : MonoBehaviour, I_CustomerType
{
    Button cookBtn;
    Button askBtn;

    public string SpecialAnswer()
    {
        switch (OrderManager.Instance.Beggar_SuccessPoint)
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
        OrderManager.Instance.StopOrderCoroutine();

        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        print(askBtn);
        switch (OrderManager.Instance.Beggar_SuccessPoint)
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

    void SucsessCook()
    {
        OrderManager.Instance.Beggar_SuccessPoint++;

        cookBtn.gameObject.SetActive(false);
        askBtn.gameObject.SetActive(false);

        OrderManager.Instance.isBeggar = true;
        //요리
        GameManager.Instance.ReturnCook();
    }
    void RefuseOrder()
    {
        StartCoroutine(RefuseOrderDelay());
        IEnumerator RefuseOrderDelay()
        {
            GameManager.Instance.isBeggarRefuse = true;
            OrderManager.Instance.isNext = true;
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(OrderManager.Instance.ExitAndComein());
        }

    }

    void Point0(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.OrderTalk[0] = "제발요..너무 배고파요..";
        OrderManager.Instance.dialogNumber++;

        cook.text = "잠시만요";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "잠시만요";
            OrderManager.Instance.dialogNumber++;

            SucsessCook();

            //끝난 뒤 말
            //"큭큭..이 은혜는.. 꼭 갚겠습니다."
        });
        ask.text = "아잇... 나가세요";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "아잇... 나가세요";
            OrderManager.Instance.dialogNumber++;
            OrderManager.Instance.OrderTalk[1] = "그렇게..깐깐하게 살다가 가게가 망해버릴 거야!";
            OrderManager.Instance.dialogNumber++;

            RefuseOrder();

        });
    }

    void Point1(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.OrderTalk[0] = "안녕하세요, 또 저에요, 혹시.. 한번 더 주실 수 있으실까요?";
        OrderManager.Instance.dialogNumber++;
        cook.text = "잠시만요";
        print(cookBtn);
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "잠시만요";
            OrderManager.Instance.dialogNumber++;

            SucsessCook();

            //끝난 뒤 말
            //"다시 한 번 말하지만 이 은혜는 꼭 갚겠습니다.. 큭큭"
        });
        ask.text = "네?";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "네?";
            OrderManager.Instance.dialogNumber++;

            OrderManager.Instance.OrderTalk[1] = "제발요.. 돈은 언젠간 드릴테니..";
            OrderManager.Instance.dialogNumber++;

            OrderManager.Instance.isNext = true;

            print(OrderManager.Instance.OrderTalk[1]);
            cook.text = "알겠습니다";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "알겠습니다";
                OrderManager.Instance.dialogNumber++;

                SucsessCook();

            });

            ask.text = "나가세요";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "나가세요";
                OrderManager.Instance.dialogNumber++;
                OrderManager.Instance.OrderTalk[2] = "가게.. 망해버려라..";
                OrderManager.Instance.dialogNumber++;

                RefuseOrder();
            });
        });
    }

    void Point2(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.OrderTalk[0] = "안녕하세요.. 사장님 오늘도 가능한가요..?";

        cook.text = "잠시만요";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "잠시만요";
            OrderManager.Instance.dialogNumber++;

            SucsessCook();

            //끝난 뒤 말
            //"큭큭 감사합니다!.."
        });

        ask.text = "나가세요";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "나가세요";
            OrderManager.Instance.dialogNumber++;

            OrderManager.Instance.OrderTalk[1] = "여태까지.. 무료로 음식을 만들어주셔서 감사합니다.. 사장님";
            OrderManager.Instance.dialogNumber++;

            RefuseOrder();

            GameManager.Instance.Money += 10;
            //거지는 없다 이제
        });
    }

    void Point3(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.OrderTalk[0] = "안녕하세요.. 사장님 늘 먹던걸로 주세요..";
        OrderManager.Instance.dialogNumber++;

        cook.text = "잠시만요";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "잠시만요";
            OrderManager.Instance.dialogNumber++;

            SucsessCook();

            //끝난 뒤 말
            //"감사합니다!.. 큭큭"
        });

        ask.text = "나가세요";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "나가세요";
            OrderManager.Instance.dialogNumber++;

            OrderManager.Instance.OrderTalk[1] = "여태까지.. 무료로 음식을 만들어주셔서 감사합니다.. 사장님";
            OrderManager.Instance.dialogNumber++;

            RefuseOrder();

            GameManager.Instance.Money += 20;
            //거지는 없다 이제
        });
    }

    void Point4(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.OrderTalk[0] = "사장님 아시죠..?";
        OrderManager.Instance.dialogNumber++;

        cook.text = "잠시만요";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "잠시만요";
            OrderManager.Instance.dialogNumber++;

            SucsessCook();

            //끝난 뒤 말
            //"감사합니다!.. 큭큭"
        });

        ask.text = "나가세요";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "나가세요";
            OrderManager.Instance.dialogNumber++;
            OrderManager.Instance.OrderTalk[1] = "여태까지.. 무료로 음식을 만들어주셔서 감사합니다.. 사장님";
            OrderManager.Instance.dialogNumber++;

            RefuseOrder();

            GameManager.Instance.Money += 30;
            //거지는 없다 이제
        });
    }

    void Point5(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.OrderTalk[0] = "쉬운일이 아니였을 텐데 음식을 매번 무료로 주셔서 감사합니다. \n마지막으로 늘 먹던걸로 음식 3개 가능할까요?";
        List<ESubMatarials> subs = new List<ESubMatarials>
        {
            ESubMatarials.NULL
        };
        OrderManager.Instance.dialogNumber++;

        askBtn.gameObject.SetActive(false);

        cook.text = "알겠습니다";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "알겠습니다";
            OrderManager.Instance.dialogNumber++;

            SucsessCook();
            GameManager.Instance.ConditionSetting(EMainMatarials.NULL, subs, 0, ECookingStyle.None, 3);

            GameManager.Instance.Money += 10000000000;
            //개천에서 용난다 엔딩 ON
            OrderManager.Instance.EndingProduction(EendingType.Dragon);
            GameManager.Instance[(int)EendingType.Dragon] = true;
        });
    }
}
