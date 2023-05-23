using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : Singleton<Cooking>
{
    public bool cookUp;

    public ESubMatarials myType;
    public Sprite[] sprites;

    [SerializeField] private float[] SubMaterialsPriece;
    public float[] MainMaterialsPriece;
    public float materialPrice;
    
    public void CookingTypePush(ESubMatarials type, Sprite[] thisSprites)
    {
        myType = type;

        sprites = (type == ESubMatarials.NULL) ? null : thisSprites;

        materialPrice = SubMaterialsPriece[((int)type)];
    }
}
