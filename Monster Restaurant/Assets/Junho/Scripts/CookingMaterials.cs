using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum ESubMatarials
{
    AlienPlant,
    Battery,
    Bismuth,
    Bolt,
    Eyes,
    Fur,
    Jewelry,
    Money,
    Paper,
    Poop,
    Preservatives,
    Sticker,
    NULL
}

public class CookingMaterials : MonoBehaviour
{
    [SerializeField] private ESubMatarials myType;
    [SerializeField] private Sprite[] sprites;

    
    public void CookingSubMaterialPush()
    {
        Cooking.Instance.CookingTypePush(myType, sprites);
    }
}
