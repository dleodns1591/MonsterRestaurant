using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSetting : MonoBehaviour
{
    GameManager GM;
    OrderManager OM;

    private void Awake()
    {
        GM = GameManager.Instance;
        OM = OrderManager.Instance;
    }

    public void RandomOrderMaterial()
    {
        EMainMatarials eMain(string cell)
        {
            switch (cell)
            {
                case "��":
                    return EMainMatarials.Noodle;
                case "��":
                    return EMainMatarials.Rice;
                case "��":
                    return EMainMatarials.Bread;
                case "���":
                    return EMainMatarials.Meat;
                default:
                    return EMainMatarials.NULL;
            }
        }
        ESubMatarials eSub(string cell)
        {
            switch (cell)
            {
                case "��ƼĿ":
                    return ESubMatarials.Sticker;
                case "��":
                    return ESubMatarials.Poop;
                case "��Ʈ":
                    return ESubMatarials.Bolt;
                case "�����":
                    return ESubMatarials.Preservatives;
                case "����":
                    return ESubMatarials.Paper;
                case "��":
                    return ESubMatarials.Money;
                case "����":
                    return ESubMatarials.Jewelry;
                case "����":
                    return ESubMatarials.Eyes;
                case "������":
                    return ESubMatarials.Battery;
                case "�� ��ġ":
                    return ESubMatarials.Fur;
                case "�񽺹�Ʈ":
                    return ESubMatarials.Bismuth;
                case "�ܰ� Ǯ":
                    return ESubMatarials.AlienPlant;
                default:
                    return ESubMatarials.NULL;

            }
        }
        ECookingStyle eStyle(string cell)
        {
            switch (cell)
            {
                case "����":
                    return ECookingStyle.Boil;
                case "Ƣ���":
                    return ECookingStyle.Fry;
                case "����":
                    return ECookingStyle.Roast;
                default:
                    return ECookingStyle.None;
            }
        }

        string[] line = OM.OrderTalkTxt.text.Split('\n');

        GM.orderSets = new OrderSet[line.Length];
        for (int i = 1; i < line.Length; i++)
        {
            string[] cell = line[i].Split('\t');

            GM.orderSets[i].main = eMain(cell[0]);
            GM.orderSets[i].sub = new List<ESubMatarials>
            {
                eSub(cell[1]),
                eSub(cell[2]),
                eSub(cell[3])
            };
            GM.orderSets[i].style = eStyle(cell[4]);
            GM.orderSets[i].count = int.Parse(cell[5]);
            GM.orderSets[i].dishCount = int.Parse(cell[6]);
        }
    }
}
