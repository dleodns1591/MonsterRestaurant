using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : Singleton<Cooking>
{
    public bool cookUp;

    public ESubMatarials myType;
    public Sprite[] sprites;
    public CookingStyleSprites[] styleSprites;

    [SerializeField] private float[] SubMaterialsPriece;
    public float[] MainMaterialsPriece;
    public float materialPrice;

    public CookingMachine cookingMachine;

    public void CookingTypePush(ESubMatarials type, Sprite[] thisSprites, CookingStyleSprites[] styleSprite)
    {
        myType = type;

        sprites = (type == ESubMatarials.NULL) ? null : thisSprites;
        styleSprites = styleSprite;

        materialPrice = SubMaterialsPriece[((int)type)];
    }
}
