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

            OM.OrderTalk[orderTalkNum] = "���� �α� �ִ� ���� ��ŭ �ֹ浵 �����ϳ׿�.";

            RefuseOrder();
        }
        else
        {
            if(SuccessRate == 60)
            {
                OM.OrderTalk[1] = $"��ø���.. �ֹ� ������ ���ֹ����� ���Գ׿�. " +
                    $"��ǰ������ �� 3�� ������ ��ޱ����� ���� �Ͽ��⿡ {(int)GameManager.Instance.Money / 4}���� ���� �Ͻø� �˴ϴ�.";

                OM.isNext = true;

                GameManager.Instance.Money -= GameManager.Instance.Money / 4;

                RefuseOrder();
                return;
            }

            OM.OrderTalk[1] = $"��ǰ ������ �� 3�� ������ ��ޱ����� ���� �Ͽ��⿡ {(int)GM.Money / 4}�� ������ ���� �Ͻø� �˴ϴ�.";

            OM.isNext = true;

            cook.text = "�˰ڽ��ϴ�";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OM.OrderTalk[2] = "�����ϼ���.";
                GM.Money -= GM.Money / 4;

                RefuseOrder();

            });

            ask.text = "�˼������� �� �帱 �� ���׿�.";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OM.OrderTalk[2] = $".......�� 314�� ���������˷� �� {(int)GM.Money / 3}���� ������ ���� �Ͻø� �˴ϴ�.";
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

        OM.OrderTalk[0] = "��ǰ�������� ����ȸ���� ���Խ��ϴ�. ��� �ֹ��� �˻��ص� �ǰڽ��ϱ�?";
        
        cook.text = "�翬����~";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            DrawResult(70);
        });

        ask.text = "�ƴϿ�";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OM.OrderTalk[1] = " �� ���԰� �������� ������ �� �ֽ��ϴ�. �׷��� �ֹ� �˻縦 �ź��Ͻ� �ǰ���?";
            OM.isNext = true;

            cook.text = "��";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OM.OrderTalk[2] = "�˰ڽ��ϴ�.";

                GM.Money -= GM.Money / 2;

                RefuseOrder();
            });

            ask.text = "�ƴϿ�. �˻� ��Ź�帳�ϴ�.";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                DrawResult(60);
            });
        }); 
    }
}
