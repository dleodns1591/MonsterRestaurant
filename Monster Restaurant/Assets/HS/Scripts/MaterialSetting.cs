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
                case "¸é":
                    return EMainMatarials.Noodle;
                case "¹ä":
                    return EMainMatarials.Rice;
                case "»§":
                    return EMainMatarials.Bread;
                case "°í±â":
                    return EMainMatarials.Meat;
                default:
                    return EMainMatarials.NULL;
            }
        }
        ESubMatarials eSub(string cell)
        {
            switch (cell)
            {
                case "½ºÆ¼Ä¿":
                    return ESubMatarials.Sticker;
                case "¶Ë":
                    return ESubMatarials.Poop;
                case "³ÊÆ®":
                    return ESubMatarials.Bolt;
                case "¹æºÎÁ¦":
                    return ESubMatarials.Preservatives;
                case "Á¾ÀÌ":
                    return ESubMatarials.Paper;
                case "µ·":
                    return ESubMatarials.Money;
                case "º¸¼®":
                    return ESubMatarials.Jewelry;
                case "´«¾Ë":
                    return ESubMatarials.Eyes;
                case "°ÇÀüÁö":
                    return ESubMatarials.Battery;
                case "ÅÐ ¹¶Ä¡":
                    return ESubMatarials.Fur;
                case "ºñ½º¹«Æ®":
                    return ESubMatarials.Bismuth;
                case "¿Ü°è Ç®":
                    return ESubMatarials.AlienPlant;
                default:
                    return ESubMatarials.NULL;

            }
        }
        ECookingStyle eStyle(string cell)
        {
            switch (cell)
            {
                case "²ú±â":
                    return ECookingStyle.Boil;
                case "Æ¢±â±â":
                    return ECookingStyle.Fry;
                case "±Á±â":
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
