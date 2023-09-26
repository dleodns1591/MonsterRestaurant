using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class FoodCleanTester : MonoBehaviour, I_CustomerType
{
    OrderManager OM;
    GameManager GM;
    OrderButtonObject BtnObjects;
    Button cookBtn;
    Button askBtn;
    TextMeshProUGUI cook, ask;

    public string SpecialAnswer()
    {
        return " ";
    }

    void RefuseOrder()
    {
        StartCoroutine(RefuseOrderDelay());
        IEnumerator RefuseOrderDelay()
        {
           // OM.StopOrderCoroutine();
            OM.isNext = true;
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            yield return new WaitForSeconds(2.5f);
            StartCoroutine(OM.ExitAndComein());
        }
    }

    void DrawResult(int SuccessRate)
    {
        int rand = 0;
        rand = Random.Range(1, 100);

        if (rand <= SuccessRate)
        {
            int orderTalkNum = 1;
            
            if (SuccessRate == 60)
                orderTalkNum++;

            OM.OrderTalk[orderTalkNum] = "역시 인기 있는 곳인 만큼 주방도 깨끗하네요.";

            RefuseOrder();
        }
        else
        {
            if(SuccessRate == 60)
            {
                OM.OrderTalk[1] = $"잠시만요.. 주방 구석에 우주벌레가 나왔네요. " +
                    $"식품위생법 제 3조 위생적 취급기준을 위반 하였기에 {(int)GameManager.Instance.Money / 4}원을 지불 하시면 됩니다.";

                OM.isNext = true;

                GameManager.Instance.Money -= GameManager.Instance.Money / 4;

                RefuseOrder();
                return;
            }

            OM.OrderTalk[1] = $"식품 위생법 제 3조 위생적 취급기준을 위반 하였기에 {(int)GM.Money / 4}의 벌금을 지불 하시면 됩니다.";

            OM.isNext = true;

            cook.text = "알겠습니다";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OM.OrderTalk[2] = "수고하세요.";
                GM.Money -= GM.Money / 4;

                RefuseOrder();

            });

            ask.text = "죄송하지만 못 드릴 것 같네요.";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OM.OrderTalk[2] = $".......제 314조 업무방해죄로 총 {(int)GM.Money / 3}원의 벌금을 지불 하시면 됩니다.";
                GM.Money -= GM.Money / 3;

                RefuseOrder();
            });
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

        OM.OrderTalk[0] = "식품위생관리 위원회에서 나왔습니다. 잠시 주방을 검사해도 되겠습니까?";
        
        cook.text = "당연하죠~";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            DrawResult(70);
        });

        ask.text = "아니요";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.OrderTalk[1] = " 이 가게가 불이익을 받으실 수 있습니다. 그래도 주방 검사를 거부하실 건가요?";
            OM.isNext = true;

            cook.text = "네";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OM.OrderTalk[2] = "알겠습니다.";

                GM.Money -= GM.Money / 2;

                RefuseOrder();
            });

            ask.text = "아니요. 검사 부탁드립니다.";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                DrawResult(60);
            });
        }); 
    }
}
