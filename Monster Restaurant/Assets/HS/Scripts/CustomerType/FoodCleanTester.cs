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
    SaveManager SM;
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
    void RefuseOrder(int money)
    {
        StartCoroutine(RefuseOrderDelay());
        IEnumerator RefuseOrderDelay()
        {
            OM.isNext = true;
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            OM.directingManager.DirectingReverse(money);
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
            if (SM.isEnglish == false)
                OM.OrderTalk[orderTalkNum] = "역시 인기 있는 곳인 만큼 주방도 깨끗하네요.";
            else
                OM.OrderTalk[orderTalkNum] = "As expected, the kitchen is clean as it is a popular place.";

            RefuseOrder();
        }
        else
        {
            if(SuccessRate == 60)
            {
                if (SM.isEnglish == false)
                    OM.OrderTalk[1] = $"잠시만요.. 주방 구석에 우주벌레가 나왔네요. " +
                    $"식품위생법 제 3조 위생적 취급기준을 위반 하였기에 {(int)GameManager.Instance.Money / 4}원을 지불 하시면 됩니다.";

                else
                    OM.OrderTalk[1] = $"Wait, there's a space bug in the corner of the kitchen. " +
                    $"You can pay a fine of {(int)GameManager.Instance.Money / 4} for violating the sanitary handling standards of Article 3 of the Food Sanitation Act.";
                OM.isNext = true;

                RefuseOrder((int)GameManager.Instance.Money / 4);
                return;
            }
            if (SM.isEnglish == false)
                OM.OrderTalk[1] = $"식품 위생법 제 3조 위생적 취급기준을 위반 하였기에 {(int)GM.Money / 4}의 벌금을 지불 하시면 됩니다.";
            else
                OM.OrderTalk[1] = $"You can pay a fine of {(int)GameManager.Instance.Money / 4} for violating the sanitary handling standards of Article 3 of the Food Sanitation Act.";

            OM.isNext = true;

            if (SM.isEnglish == false)
                cook.text = "알겠습니다";
            else
                cook.text = "All right";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                if (SM.isEnglish == false)
                    OM.OrderTalk[2] = "수고하세요.";
                else
                    OM.OrderTalk[2] = "take care";

                RefuseOrder((int)GM.Money / 4);

            });

            if (SM.isEnglish == false)
                ask.text = "죄송하지만 못 드릴 것 같네요.";
            else
                ask.text = "I'm sorry, but I can't give it to you.";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                if (SM.isEnglish == false)
                    OM.OrderTalk[2] = $".......제 314조 업무방해죄로 총 {(int)GM.Money / 3}원의 벌금을 지불 하시면 됩니다.";
                else
                    OM.OrderTalk[2] = $"...... Article 314 You can pay a total fine of {(int)GM.Money / 3}won for obstruction of business.”";

                RefuseOrder((int)GM.Money / 3);
            });
        }
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

        OM.StopOrderCoroutine();

        if(SM.isEnglish == false)
        {
        OM.OrderTalk[0] = "식품위생관리 위원회에서 나왔습니다. 잠시 주방을 검사해도 되겠습니까?";

        cook.text = "당연하죠~";
        }
        else
        {
            OM.OrderTalk[0] = "It's from the Food Sanitation Commission. May I inspect the kitchen for a moment?";

            cook.text = "Of course";
        }

        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            DrawResult(70);
        });
        if (SM.isEnglish == false)
            ask.text = "아니요";
        else
            ask.text = "No";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            if (SM.isEnglish == false)
                OM.OrderTalk[1] = " 이 가게가 불이익을 받으실 수 있습니다. 그래도 주방 검사를 거부하실 건가요?";
            else
                OM.OrderTalk[1] = "This store can receive disadvantages.Would you like to refuse the kitchen examination?";


            OM.isNext = true;

            if (SM.isEnglish == false)
                cook.text = "네";
            else
                cook.text = "Yes";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                if (SM.isEnglish == false)
                    OM.OrderTalk[2] = "알겠습니다.";
                else
                    OM.OrderTalk[2] = "All Right";

                RefuseOrder((int)GM.Money / 2);
            });

            if (SM.isEnglish == false)
                ask.text = "아니요. 검사 부탁드립니다.";
            else
                ask.text = "No. Please inspect.";

            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                DrawResult(60);
            });
        }); 
    }
}
