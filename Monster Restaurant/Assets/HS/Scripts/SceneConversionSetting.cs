using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneConversionSetting : MonoBehaviour
{
    GameManager GM;
    OrderManager OM;

    private void Start()
    {
        GM = GameManager.Instance;
        OM = OrderManager.Instance;

        CookToOrder();
        OrderToCook();
    }

    public void CookToOrder()
    {
        GM.ReturnOrder = () =>
        {
            OM.satisfactionManager.LoopStop();

            string[] line = OM.AnswerTalkTxt.text.Split('\n');

            string[,] SucsessTalk = new string[Enum.GetValues(typeof(EcustomerType)).Length, 3];
            string[,] FailTalk = new string[Enum.GetValues(typeof(EcustomerType)).Length, 3];


            int sucsessCnt = 0;
            int FailCnt = 0;

            string formerName = "";
            for (int i = 0; i < line.Length; i++)
            {
                string[] cell = line[i].Split('\t');

                if (formerName != cell[0])
                {
                    sucsessCnt = 0;
                    FailCnt = 0;
                }

                formerName = cell[0];
                if (cell[1] == "성공")
                {
                    if (SaveManager.Instance.isEnglish == false)
                        SucsessTalk[(int)OM.customerManager.NameToEnumReturn(cell[0]), sucsessCnt] = cell[2];
                    else
                        SucsessTalk[(int)OM.customerManager.NameToEnumReturn(cell[0]), sucsessCnt] = cell[3];

                    sucsessCnt++;
                }
                else if (cell[1] == "실패")
                {
                    if (SaveManager.Instance.isEnglish == false)
                        FailTalk[(int)OM.customerManager.NameToEnumReturn(cell[0]), FailCnt] = cell[2];
                    else
                        FailTalk[(int)OM.customerManager.NameToEnumReturn(cell[0]), FailCnt] = cell[3];

                    FailCnt++;
                }

            }

            if (!OM.isBeggar)
            {

                if (GM.Satisfaction >= 40)
                    OM.isCookingSuccess = true;
                else
                    OM.isCookingSuccess = false;

                //if 성공 실패
                if (OM.isCookingSuccess)
                {
                    OM.directingManager.Directing(50, 12);
                    OM.customer.CustomerImg.sprite = OM.customer.GuestSuccess[OM.NormalGuestType];
                    OM.AnswerTalk = SucsessTalk[OM.NormalGuestType, UnityEngine.Random.Range(0, 2)];
                }
                else
                {
                    OM.directingManager.Directing(20, 4);
                    int rand = UnityEngine.Random.Range(1, 5);
                    if (rand == 1)
                        GM.SettlementCost += 100;
                    OM.customer.CustomerImg.sprite = OM.customer.GuestFails[OM.NormalGuestType];
                    OM.AnswerTalk = FailTalk[OM.NormalGuestType, UnityEngine.Random.Range(0, 2)];
                }
            }
            else
            {
                OM.customer.CustomerImg.sprite = OM.customer.EventGuestSuccess[(int)EeventCustomerType.Beggar];
            }

            OM.isBeggar = false;
            GM.isGroupOrder = false;
            if (!(OM.CustomerType is NormalCustomer))
            {
                OM.customerManager.EeventCustomerSetting(GM.SpecialType);
                OM.AnswerTalk = OM.CustomerType.SpecialAnswer();
            }

            OM.CookingScene.transform.DOMoveY(-10, 1).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                OM.orderMessageManager.AfterOrder = () =>
                {
                    StartCoroutine(OM.ExitAndComein());
                };
                OM.orderMessageManager.TalkingText(OM.AnswerTalk);
            });
            OM.orderMessageManager.ResetText();
        };
    }

    public void OrderToCook()
    {
        GM.ReturnCook = () =>
        {
            MapScrollMG.Instance.StartSet();
            OrderSet order = GM.orderSets[GM.randomCustomerNum];
            GM.ConditionSetting(order.main, order.sub, order.count, order.style, order.dishCount);
            OM.satisfactionManager.LoopStart();
            OM.CookingScene.transform.DOMoveY(0, 1).SetEase(Ease.OutBounce).OnComplete(() =>
            {

            });
        };
    }
}
