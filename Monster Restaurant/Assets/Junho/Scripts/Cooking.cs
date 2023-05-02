using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : Singleton<Cooking>
{
    public bool cookUp;

    public ESubMatarials myType;
    public Sprite[] sprites;
    public void CookingTypePush(ESubMatarials type, Sprite[] thisSprites)
    {
        myType = type;
        sprites = thisSprites;
    }
}
