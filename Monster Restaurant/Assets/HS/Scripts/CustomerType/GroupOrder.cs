using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GroupOrder : MonoBehaviour, I_CustomerType
{
    public string SpecialAnswer()
    {
        if (OrderManager.Instance.isCookingSuccess)
        {
            return "������ ������ּż� �����մϴ�. ���̵��� ������ �ſ���";
            //�� ����
        }
        else
            return "�ʰ� �ֽø� ��ؿ�..! ���� �޽� ����� �� ó���ؼ� ����������, ���� ���帮�ڳ׿�";
    }

    public void SpecialType(TextMeshProUGUI cook, TextMeshProUGUI ask)
    {
        OrderManager.Instance.OrderTalk[0] = "�ȳ��ϼ���. �� �ǹ����� ����� ����� ���ϰ� �ֽ��ϴ�. ���� �޽� ��п� ������ ���ܼ� �׷��µ� n�� �ȿ� n����(�ֹ� ����)�� ����� �ֽ� �� �ֳ��� ?";

        Button cookBtn = cook.transform.parent.GetComponent<Button>();
        Button askBtn = ask.transform.parent.GetComponent<Button>();

        cook.text = "�˰ڽ��ϴ�";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            OrderManager.Instance.AskTalk[0] = "�˰ڽ��ϴ�";
            OrderManager.Instance.dialogNumber++;

            cookBtn.gameObject.SetActive(false);
            askBtn.gameObject.SetActive(false);

            //�丮
            GameManager.Instance.ReturnCook();

        });

        askBtn.gameObject.SetActive(false);
    }
}
