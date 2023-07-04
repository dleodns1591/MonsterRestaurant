using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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

    public void SpecialType(UIText cook, UIText ask)
    {
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

            //요리
            GameManager.Instance.ReturnCook();
    }
    void RefuseOrder()
    {
        cookBtn.gameObject.SetActive(false);
        askBtn.gameObject.SetActive(false);
        StartCoroutine(OrderManager.Instance.ExitAndComein());
    }
    void ResetTalk()
    {
        for (int i = 0; i < OrderManager.Instance.OrderTalk.Length; i++)
        {
           // OrderManager.Instance.OrderTalk[i].
        }
    }

    void Point0(UIText cook, UIText ask)
    {
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "제발요..너무 배고파요..";

        cook.text = "잠시만요";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "잠시만요";

            SucsessCook();

            //끝난 뒤 말
            //"큭큭..이 은혜는.. 꼭 갚겠습니다."
        });
        ask.text = "아잇... 나가세요";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "아잇... 나가세요";

            RefuseOrder();

        });
    }

    void Point1(UIText cook, UIText ask)
    {
        OrderManager.Instance.OrderTalk[0] = "안녕하세요, 또 저에요, 혹시.. (주문 내용)을 주실 수 있으실까요?";
        cook.text = "잠시만요";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "잠시만요";

            SucsessCook();

            //끝난 뒤 말
            //"다시 한 번 말하지만 이 은혜는 꼭 갚겠습니다.. 큭큭"
        });
        ask.text = "네?";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "네?";

            OrderManager.Instance.OrderTalk[1] = "제발요.. 돈은 언젠간 드릴테니..";

            OrderManager.Instance.isNext = true;

            cook.text = "알겠습니다";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "알겠습니다";
                
                SucsessCook();

            });

            ask.text = "나가세요";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "나가세요";
                OrderManager.Instance.OrderTalk[2] = "가게.. 망해버려라..";

                RefuseOrder();
            });
        });
    }

    void Point2(UIText cook, UIText ask)
    {
        OrderManager.Instance.OrderTalk[0] = "안녕하세요.. 사장님 오늘도 가능한가요..?";

        cook.text = "잠시만요";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "잠시만요";
           
            SucsessCook();

            //끝난 뒤 말
            //"큭큭 감사합니다!.."
        });

        ask.text = "나가세요";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "나가세요";

            OrderManager.Instance.OrderTalk[1] = "여태까지.. 무료로 음식을 만들어주셔서 감사합니다.. 사장님";

            RefuseOrder();

            GameManager.Instance.Money += 10;
            //거지는 없다 이제
        });
    }

    void Point3(UIText cook, UIText ask)
    {
        OrderManager.Instance.OrderTalk[0] = "안녕하세요.. 사장님 늘 먹던걸로 주세요..";

        cook.text = "잠시만요";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "잠시만요";
            
            SucsessCook();

            //끝난 뒤 말
            //"감사합니다!.. 큭큭"
        });

        ask.text = "나가세요";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "나가세요";

            OrderManager.Instance.OrderTalk[1] = "여태까지.. 무료로 음식을 만들어주셔서 감사합니다.. 사장님";

            RefuseOrder();

            GameManager.Instance.Money += 20;
            //거지는 없다 이제
        });
    }

    void Point4(UIText cook, UIText ask)
    {
        OrderManager.Instance.OrderTalk[0] = "사장님 아시죠..?";

        cook.text = "잠시만요";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "잠시만요";
            
            SucsessCook();

            //끝난 뒤 말
            //"감사합니다!.. 큭큭"
        });

        ask.text = "나가세요";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "나가세요";
            OrderManager.Instance.OrderTalk[1] = "여태까지.. 무료로 음식을 만들어주셔서 감사합니다.. 사장님";

            RefuseOrder();

            GameManager.Instance.Money += 30;
            //거지는 없다 이제
        });
    }

    void Point5(UIText cook, UIText ask)
    {
        OrderManager.Instance.OrderTalk[0] = "쉬운일이 아니였을 텐데 음식을 매번 무료로 주셔서 감사합니다. \n마지막으로 늘 먹던걸로 음식 3개 가능할까요?";

        askBtn.gameObject.SetActive(false);

        cook.text = "알겠습니다";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "알겠습니다";

            SucsessCook();

            GameManager.Instance.Money += 10000000000;
            //개천에서 용난다 엔딩 ON
        });
    }
}
