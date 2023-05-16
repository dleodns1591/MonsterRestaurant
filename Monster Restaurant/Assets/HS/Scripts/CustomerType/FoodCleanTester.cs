using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FoodCleanTester : MonoBehaviour, I_CustomerType
{
    public void SpecialType(UIText cook, UIText ask)
    {
        OrderManager.Instance.OrderTalk[0] = "식품위생관리 위원회에서 나왔습니다. 잠시 주방을 검사해도 되겠습니까?";

        Button cookBtn = cook.transform.parent.GetComponent<Button>();
        Button askBtn = ask.transform.parent.GetComponent<Button>();

        cook.text = "당연하죠~";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "당연하죠~";

                cookBtn.gameObject.SetActive(false);
                askBtn.gameObject.SetActive(false);
            OrderManager.Instance.OrderTalk[1] = "흐음..";

            OrderManager.Instance.isNext = true;

            int RandomNum = Random.Range(0, 100);
            if(RandomNum <= 30)
            {
                OrderManager.Instance.OrderTalk[2] = "역시 인기 있는 곳인 만큼 주방도 깨끗하네요.";

                print("역시 인기 있는 곳인 만큼 주방도 깨끗하네요.");
                OrderManager.Instance.isNext = true;

                //다음 손님
            }
            else
            {
                OrderManager.Instance.OrderTalk[2] = "잠시만요.. 주방 구석에 우주벌레가 나왔네요. 식품위생법 제 3조 위생적 취급기준을 위반 하였기에 (현재 플레이어가 가진 소지금 / 3)을 지불 하시면 됩니다.";
                //골드 깎기
                print("잠시만요.. 주방 구석에 우");
                OrderManager.Instance.isNext = true;

                cookBtn.gameObject.SetActive(false);
                askBtn.gameObject.SetActive(false);
                //다음 손님
            }
        });

        ask.text = "아니요";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "아니요";

            OrderManager.Instance.OrderTalk[1] = " 이 가게가 불이익을 받으실 수 있습니다. 그래도 주방 검사를 거부하실 건가요?";

            OrderManager.Instance.isNext = true;

            cook.text = "네";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "네";
                OrderManager.Instance.OrderTalk[2] = "알겠습니다.";

                OrderManager.Instance.isNext = true;

                cookBtn.gameObject.SetActive(false);
                askBtn.gameObject.SetActive(false);

                //골드 1 / 2 깎기
            });

            ask.text = "아니요.";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "아니요.";

                cookBtn.gameObject.SetActive(false);
                askBtn.gameObject.SetActive(false);

                OrderManager.Instance.isNext = true;

                int RandomNum = Random.Range(0, 100);
                if (RandomNum <= 60)
                {
                    OrderManager.Instance.OrderTalk[2] = "역시 인기 있는 곳인 만큼 주방도 깨끗하네요.";

                    print("역시 좋아");
                    OrderManager.Instance.isNext = true;

                    cookBtn.gameObject.SetActive(false);
                    askBtn.gameObject.SetActive(false);
                    //다음 손님
                }
                else
                {
                    OrderManager.Instance.OrderTalk[2] = "잠시만요.. 주방 구석에 우주벌레가 나왔네요. 식품위생법 제 3조 위생적 취급기준을 위반 하였기에 (현재 플레이어가 가진 소지금 / 3)을 지불 하시면 됩니다.";
                    //골드 깎기

                    OrderManager.Instance.isNext = true;
                    
                    print("벌레쉨");

                    cookBtn.gameObject.SetActive(false);
                    askBtn.gameObject.SetActive(false);
                    //다음 손님
                }
            });
        }); 
    }
}
