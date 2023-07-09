using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class FoodCleanTester : MonoBehaviour, I_CustomerType
{
    Button cookBtn;
    Button askBtn;
    public string SpecialAnswer()
    {
        return " ";
    }

    void RefuseOrder()
    {
        StartCoroutine(RefuseOrderDelay());
        IEnumerator RefuseOrderDelay()
        {
            OrderManager.Instance.isNext = true;
            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);
            yield return new WaitForSeconds(2.5f);
            StartCoroutine(OrderManager.Instance.ExitAndComein(true));
            OrderManager.Instance.StopOrderCoroutine();
        }

    }

    public void SpecialType(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "��ǰ�������� ����ȸ���� ���Խ��ϴ�. ��� �ֹ��� �˻��ص� �ǰڽ��ϱ�?";
        
        cook.text = "�翬����~";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "�翬����~";

                cookBtn.gameObject.SetActive(false);
                askBtn.gameObject.SetActive(false);
            
            int RandomNum = Random.Range(0, 100);
            if(RandomNum <= 70)
            {
                OrderManager.Instance.OrderTalk[1] = "���� �α� �ִ� ���� ��ŭ �ֹ浵 �����ϳ׿�.";

                RefuseOrder();
            }
            else
            {
                OrderManager.Instance.OrderTalk[1] = $"��ø���.. �ֹ� ������ ���ֹ����� ���Գ׿�. ��ǰ������ �� 3�� ������ ��ޱ����� ���� �Ͽ��⿡ {(int)GameManager.Instance.Money / 3}���� ���� �Ͻø� �˴ϴ�.";
                //��� ���
                GameManager.Instance.Money -= GameManager.Instance.Money / 3;
                
                RefuseOrder();
            }
        });

        ask.text = "�ƴϿ�";
        askBtn.onClick.RemoveAllListeners();
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "�ƴϿ�";

            OrderManager.Instance.OrderTalk[1] = " �� ���԰� �������� ������ �� �ֽ��ϴ�. �׷��� �ֹ� �˻縦 �ź��Ͻ� �ǰ���?";

            OrderManager.Instance.isNext = true;

            cook.text = "��";
            cookBtn.onClick.RemoveAllListeners();
            cookBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "��";
                OrderManager.Instance.OrderTalk[2] = "�˰ڽ��ϴ�.";

                //��� ���
                GameManager.Instance.Money -= GameManager.Instance.Money / 2;

                RefuseOrder();
            });

            ask.text = "�ƴϿ�.";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "�ƴϿ�.";

                OrderManager.Instance.isNext = true;

                int RandomNum = Random.Range(0, 100);
                if (RandomNum <= 60)
                {
                    OrderManager.Instance.OrderTalk[2] = "���� �α� �ִ� ���� ��ŭ �ֹ浵 �����ϳ׿�.";

                    RefuseOrder();
                }
                else
                {
                    OrderManager.Instance.OrderTalk[2] = $"��ø���.. �ֹ� ������ ���ֹ����� ���Գ׿�. ��ǰ������ �� 3�� ������ ��ޱ����� ���� �Ͽ��⿡ {(int)GameManager.Instance.Money / 3}���� ���� �Ͻø� �˴ϴ�.";
                    
                    //��� ���
                    GameManager.Instance.Money -= GameManager.Instance.Money / 3;

                    RefuseOrder();
                }
            });
        }); 
    }
}
