using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GroupOrder : MonoBehaviour, I_CustomerType
{
    Button cookBtn;
    Button askBtn;
    int rand;
    public string SpecialAnswer()
    {
        if (OrderManager.Instance.isCookingSuccess)
        {
            GameManager.Instance.Money += 700;
            return "빠르게 만들어주셔서 감사합니다. 아이들이 좋아할 거예요";
        }
        else
        {
            GameManager.Instance.Money -= 20;
            return "늦게 주시면 어떡해요..! 현재 급식 배분을 잘 처리해서 다행이지만, 돈은 못드리겠네요";
        }
    }

    string eMain(EMainMatarials cell)
    {
        switch (cell)
        {
            case EMainMatarials.Noodle:
                return "파스타";
            case EMainMatarials.Rice:
                return "밥";
            case EMainMatarials.Bread:
                return "식빵";
            case EMainMatarials.Meat:
                return "고기";
            default:
                return "";
        }
    }
    void SucsessCook()
    {
        cookBtn.gameObject.SetActive(false);
        askBtn.gameObject.SetActive(false);

        StartCoroutine(Delay());
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(1.5f);
            askBtn.gameObject.SetActive(true);
            askBtn.GetComponent<Image>().enabled = true;
        }

        //요리
        GameManager.Instance.ReturnCook();

        List<ESubMatarials> subs = new List<ESubMatarials>
        {
            ESubMatarials.NULL
        };
        GameManager.Instance.ConditionSetting((EMainMatarials)rand, subs, 0, ECookingStyle.Roast, 10);
    }

    public void SpecialType(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        rand = UnityEngine.Random.Range(0, 3);

        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();
        
        OrderManager.Instance.OrderTalk[0] = $"안녕하세요. 옆 건물에서 어린이집 교사로 일하고 있습니다. 현재 급식 배분에 문제가 생겨서 그러는데 30초 안에 10개의 구운 {eMain((EMainMatarials)rand)}을 만들어 주실 수 있나요 ?";
        OrderManager.Instance.dialogNumber++;

        cook.text = "알겠습니다";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "알겠습니다";
            OrderManager.Instance.dialogNumber++;

            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);

            //요리
            SucsessCook();
        });


        askBtn.onClick.RemoveAllListeners();
        ask.text = "";
        askBtn.GetComponent<Image>().enabled = false;
    }
}
