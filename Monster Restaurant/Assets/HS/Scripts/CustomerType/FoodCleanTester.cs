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
                OM.OrderTalk[orderTalkNum] = "���� �α� �ִ� ���� ��ŭ �ֹ浵 �����ϳ׿�.";
            else
                OM.OrderTalk[orderTalkNum] = "As expected, the kitchen is clean as it is a popular place.";

            RefuseOrder();
        }
        else
        {
            if(SuccessRate == 60)
            {
                if (SM.isEnglish == false)
                    OM.OrderTalk[1] = $"��ø���.. �ֹ� ������ ���ֹ����� ���Գ׿�. " +
                    $"��ǰ������ �� 3�� ������ ��ޱ����� ���� �Ͽ��⿡ {(int)GameManager.Instance.Money / 4}���� ���� �Ͻø� �˴ϴ�.";

                else
                    OM.OrderTalk[1] = $"Wait, there's a space bug in the corner of the kitchen. " +
                    $"You can pay a fine of {(int)GameManager.Instance.Money / 4} for violating the sanitary handling standards of Article 3 of the Food Sanitation Act.";
                OM.isNext = true;

                RefuseOrder((int)GameManager.Instance.Money / 4);
                return;
            }
            if (SM.isEnglish == false)
                OM.OrderTalk[1] = $"��ǰ ������ �� 3�� ������ ��ޱ����� ���� �Ͽ��⿡ {(int)GM.Money / 4}�� ������ ���� �Ͻø� �˴ϴ�.";
            else
                OM.OrderTalk[1] = $"You can pay a fine of {(int)GameManager.Instance.Money / 4} for violating the sanitary handling standards of Article 3 of the Food Sanitation Act.";

            OM.isNext = true;

            if (SM.isEnglish == false)
                cook.text = "�˰ڽ��ϴ�";
            else
                cook.text = "All right";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                if (SM.isEnglish == false)
                    OM.OrderTalk[2] = "�����ϼ���.";
                else
                    OM.OrderTalk[2] = "take care";

                RefuseOrder((int)GM.Money / 4);

            });

            if (SM.isEnglish == false)
                ask.text = "�˼������� �� �帱 �� ���׿�.";
            else
                ask.text = "I'm sorry, but I can't give it to you.";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                if (SM.isEnglish == false)
                    OM.OrderTalk[2] = $".......�� 314�� ���������˷� �� {(int)GM.Money / 3}���� ������ ���� �Ͻø� �˴ϴ�.";
                else
                    OM.OrderTalk[2] = $"...... Article 314 You can pay a total fine of {(int)GM.Money / 3}won for obstruction of business.��";

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
        OM.OrderTalk[0] = "��ǰ�������� ����ȸ���� ���Խ��ϴ�. ��� �ֹ��� �˻��ص� �ǰڽ��ϱ�?";

        cook.text = "�翬����~";
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
            ask.text = "�ƴϿ�";
        else
            ask.text = "No";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            if (SM.isEnglish == false)
                OM.OrderTalk[1] = " �� ���԰� �������� ������ �� �ֽ��ϴ�. �׷��� �ֹ� �˻縦 �ź��Ͻ� �ǰ���?";
            else
                OM.OrderTalk[1] = "This store can receive disadvantages.Would you like to refuse the kitchen examination?";


            OM.isNext = true;

            if (SM.isEnglish == false)
                cook.text = "��";
            else
                cook.text = "Yes";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                if (SM.isEnglish == false)
                    OM.OrderTalk[2] = "�˰ڽ��ϴ�.";
                else
                    OM.OrderTalk[2] = "All Right";

                RefuseOrder((int)GM.Money / 2);
            });

            if (SM.isEnglish == false)
                ask.text = "�ƴϿ�. �˻� ��Ź�帳�ϴ�.";
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
