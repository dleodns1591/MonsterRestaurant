using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public enum ECookingList
{
    Boil,
    RadioactiveFry,
    Oven,
    Raw,
    END
}
public enum ECookingMainMaterial
{
    Noodle,
    Rice,
    Bread,
    Meat,
    Fish,
    END

}
public enum ECookingSubMaterial
{
    Volt,
    Jewel,
    Paper,
    ElectricWire,
    Bismuth,
    Eraser,
    Grass,
    Poop,
    Eyeballs,
    Battery,
    Hairball,
    END
}


public class Ingredients : MonoBehaviour
{
    private CookingMG cooking;
    [SerializeField] private Button[] cookingListBtns;
    [SerializeField] private Button[] mainMaterialBtns;
    [SerializeField] private Button[] subMaterialBtns;

    private void Start()
    {
        cooking = GameObject.Find("CookingMG").GetComponent<CookingMG>();
        BtnSET();
    }

    private void BtnSET()
    {
        for (int i = 0; i < ((int)ECookingList.END -1); i++)
        {
            ECookingList list = (ECookingList)i;
            cookingListBtns[i].onClick.AddListener(() => {

                cooking.myCookingList = list;
                print(list);
            });
        }

        for (int i = 0; i < ((int)ECookingMainMaterial.END -1); i++)
        {
            ECookingMainMaterial list = (ECookingMainMaterial)i;
            mainMaterialBtns[i].onClick.AddListener(() => {

                cooking.MyMainMaterial = list;

            });
        }

        for (int i = 0; i < ((int)ECookingSubMaterial.END -1); i++)
        {
            ECookingSubMaterial list = (ECookingSubMaterial)i;
            subMaterialBtns[i].onClick.AddListener(() => {

                cooking.MySubMaterial = list;
            });
        }

    }

}
