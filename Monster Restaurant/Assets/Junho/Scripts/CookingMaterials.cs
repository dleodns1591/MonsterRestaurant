using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public struct CookingStyleSprites
{
    public Sprite[] sprites;
}
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
    [SerializeField] private CookingStyleSprites[] cookingStyleSprites;

 


    public void CookingSubMaterialPush()
    {

        ESubMatarials type = (Cooking.Instance.myType == myType) ? ESubMatarials.NULL: myType;
            
        Cooking.Instance.CookingTypePush(type, sprites, cookingStyleSprites);
    }

    
}
