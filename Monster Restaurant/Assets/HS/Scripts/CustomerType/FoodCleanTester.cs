using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FoodCleanTester : MonoBehaviour, I_CustomerType
{
    public string SpecialAnswer()
    {
        return " ";
    }

    public void SpecialType(UIText cook, UIText ask)
    {
        OrderManager.Instance.OrderTalk[0] = "��ǰ�������� ����ȸ���� ���Խ��ϴ�. ��� �ֹ��� �˻��ص� �ǰڽ��ϱ�?";

        Button cookBtn = cook.transform.parent.GetComponent<Button>();
        Button askBtn = ask.transform.parent.GetComponent<Button>();

        cook.text = "�翬����~";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "�翬����~";

                cookBtn.gameObject.SetActive(false);
                askBtn.gameObject.SetActive(false);
            OrderManager.Instance.OrderTalk[1] = "����..";

            OrderManager.Instance.isNext = true;

            int RandomNum = Random.Range(0, 100);
            if(RandomNum <= 30)
            {
                OrderManager.Instance.OrderTalk[2] = "���� �α� �ִ� ���� ��ŭ �ֹ浵 �����ϳ׿�.";

                OrderManager.Instance.isNext = true;

                //���� �մ�
                OrderManager.Instance.ExitAndComein();
            }
            else
            {
                OrderManager.Instance.OrderTalk[2] = $"��ø���.. �ֹ� ������ ���ֹ����� ���Գ׿�. ��ǰ������ �� 3�� ������ ��ޱ����� ���� �Ͽ��⿡ {GameManager.Instance.Money / 3}�� ���� �Ͻø� �˴ϴ�.";
                //��� ���
                GameManager.Instance.Money -= GameManager.Instance.Money / 3;
                OrderManager.Instance.isNext = true;

                cookBtn.gameObject.SetActive(false);
                askBtn.gameObject.SetActive(false);
                //���� �մ�
                OrderManager.Instance.ExitAndComein();
            }
        });

        ask.text = "�ƴϿ�";
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

                OrderManager.Instance.isNext = true;

                cookBtn.gameObject.SetActive(false);
                askBtn.gameObject.SetActive(false);
            });

            ask.text = "�ƴϿ�.";
            askBtn.onClick.RemoveAllListeners();
            askBtn.onClick.AddListener(() =>
            {
                OrderManager.Instance.AskTalk[1] = "�ƴϿ�.";

                cookBtn.gameObject.SetActive(false);
                askBtn.gameObject.SetActive(false);

                OrderManager.Instance.isNext = true;

                int RandomNum = Random.Range(0, 100);
                if (RandomNum <= 60)
                {
                    OrderManager.Instance.OrderTalk[2] = "���� �α� �ִ� ���� ��ŭ �ֹ浵 �����ϳ׿�.";

                    print("���� ����");
                    OrderManager.Instance.isNext = true;

                    cookBtn.gameObject.SetActive(false);
                    askBtn.gameObject.SetActive(false);

                    //���� �մ�
                    OrderManager.Instance.ExitAndComein();
                }
                else
                {
                    OrderManager.Instance.OrderTalk[2] = $"��ø���.. �ֹ� ������ ���ֹ����� ���Գ׿�. ��ǰ������ �� 3�� ������ ��ޱ����� ���� �Ͽ��⿡ {GameManager.Instance.Money / 3}�� ���� �Ͻø� �˴ϴ�.";
                    
                    //��� ���
                    GameManager.Instance.Money -= GameManager.Instance.Money / 3;

                    OrderManager.Instance.isNext = true;
                    
                    cookBtn.gameObject.SetActive(false);
                    askBtn.gameObject.SetActive(false);

                    //���� �մ�
                    OrderManager.Instance.ExitAndComein();
                }
            });
        }); 
    }
}
