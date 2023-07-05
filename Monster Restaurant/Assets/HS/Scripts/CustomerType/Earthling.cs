using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Earthling : MonoBehaviour, I_CustomerType
{
    Button cookBtn;
    Button askBtn;

    void SucsessCook()
    {
        OrderManager.Instance.Earthling_SuccessPoint++;

        cookBtn.gameObject.SetActive(false);
        askBtn.gameObject.SetActive(false);

        //�丮
        GameManager.Instance.ReturnCook();
    }

    public string SpecialAnswer()
    {
        switch (OrderManager.Instance.Earthling_SuccessPoint)
        {
            case 0:
                return "ŪŪ..�� ������.. �� ���ڽ��ϴ�.";
            case 1:
                return "�ٽ� �� �� �������� �� ������ �� ���ڽ��ϴ�.. ŪŪ";
            case 2:
                return "ŪŪ �����մϴ�!..";
            case 3:
                return "�����մϴ�!.. ŪŪ";
            case 4:
                return "�����մϴ�!.. ŪŪ";
            default:
                return " ";
        }
    }
 

    public void SpecialType(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        
    }

    void Point0(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        cookBtn = cook.transform.parent.GetComponent<Button>();
        askBtn = ask.transform.parent.GetComponent<Button>();

        OrderManager.Instance.OrderTalk[0] = "���� �� ���� �� �� �� �� ������ ������ �ֽ� �� �ֳ���?";

        cook.text = "���ݸ� ��ٷ� �ּ���.";
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "���ݸ� ��ٷ� �ּ���.";

            SucsessCook();

            //���� �� ��
            //"ŪŪ..�� ������.. �� ���ڽ��ϴ�."
        });
        ask.text = "����... ��������";
        askBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "����... ��������";

            //RefuseOrder();

        });
    }
}

