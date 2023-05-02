using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private Image TimeFill, CustomerImg;
    [SerializeField] private Text OrderText;
    [SerializeField] private RandomText RT;
    [SerializeField] private Sprite[] GuestDefualts;
    private Tween TextTween;
    private I_CustomerType CustomerType;

    public static Text MoneyText;
    public static string[] OrderTalk = new string[3];
    public static bool isNext;

    void SetCustomerType(EeventCustomerType type)
    {
        switch (type)
        {
            case EeventCustomerType.Human:
                break;
            case EeventCustomerType.Thief:
                break;
            case EeventCustomerType.Beggar:
                break;
            case EeventCustomerType.Rich:
                break;
            case EeventCustomerType.GroupOrder:
                break;
            case EeventCustomerType.SalesMan:
                break;
            case EeventCustomerType.FoodCleanTester:
                break;
            default:
                int randomType = UnityEngine.Random.Range(0, Enum.GetValues(typeof(EcustomerType)).Length);
                switch ((EcustomerType)randomType)
                {
                    case EcustomerType.Alien:
                        CustomerImg.sprite = GuestDefualts[randomType];
                        break;
                    case EcustomerType.Hyena:
                        CustomerImg.sprite = GuestDefualts[randomType];
                        break;
                    case EcustomerType.Robot:
                        CustomerImg.sprite = GuestDefualts[randomType];
                        break;
                    case EcustomerType.Dragon:
                        CustomerImg.sprite = GuestDefualts[randomType];
                        break;
                    case EcustomerType.Light:
                        CustomerImg.sprite = GuestDefualts[randomType];
                        break;
                    case EcustomerType.FSM:
                        CustomerImg.sprite = GuestDefualts[randomType];
                        break;
                    default:
                        break;
                }
                OrderTalk[0] = "���� ��� " + RT.FirstTexts[UnityEngine.Random.Range(0, 20)] + " ����� ��ŭ " + RT.MiddleTexts[UnityEngine.Random.Range(0, 20)] + " ������� " + RT.LastTexts[UnityEngine.Random.Range(0, 20)];
                OrderTalk[1] = "���� ��� ����� ��ŭ " + RT.FirstTexts[UnityEngine.Random.Range(0, 20)] + " ������� " + RT.LastTexts[UnityEngine.Random.Range(0, 20)];
                OrderTalk[2] = "!���� ���!�� !�����! !��ŭ! �־ !�������! ���ּ��� '^'..";
                break;
        }
    }

    void UpdateTime()
    {
        DOTween.To(() => TimeFill.fillAmount, x => TimeFill.fillAmount = x, 0, 100)
        .OnComplete(() =>
        {
            //�մ� ȭ���鼭 ������

            //���� �մ�
            StartCoroutine(Order());
        });
    }


    public IEnumerator Order()
    {
        for (int i = 0; i < OrderTalk.Length; i++)
        {
            if (OrderTalk[i].Equals(""))
            {
                continue;
            }
            
            while(!isNext)
            {
                yield return null;
            }
            TextTween.Kill();
            TextTween = OrderText.DOText(OrderTalk[i], 0.05f * OrderTalk[i].Length);
        }
    }
}
