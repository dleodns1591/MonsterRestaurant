using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GroupOrder : MonoBehaviour, I_CustomerType
{
    public void SpecialType(UIText cook, UIText ask)
    {
        OrderManager.OrderTalk[0] = "�ȳ��ϼ���.�� �ǹ����� ����� ����� ���ϰ� �ֽ��ϴ�. ���� �޽� ��п� ������ ���ܼ� �׷��µ� n�� �ȿ� n����(�ֹ� ����)�� ����� �ֽ� �� �ֳ��� ?";

        Button cookBtn = cook.transform.parent.GetComponent<Button>();
        Button askBtn = ask.transform.parent.GetComponent<Button>();

        cook.text = "�˰ڽ��ϴ�";
        cookBtn.onClick.RemoveAllListeners();
        cookBtn.onClick.AddListener(() =>
        {
            //�丮


        });
    }
}
