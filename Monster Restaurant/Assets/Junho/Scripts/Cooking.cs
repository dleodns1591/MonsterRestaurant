using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : Singleton<Cooking>
{
    public bool cookUp;

    public Trash trash;

    public ESubMatarials myType;
    public Sprite[] sprites;
    public CookingStyleSprites[] styleSprites;

    [SerializeField] private float[] SubMaterialsPriece;
    public float[] MainMaterialsPriece;
    public float materialPrice;

    public CookingMachine cookingMachine;

    public GameObject KitchenRoom;

    public OrderSet AnswerOrder;

    public Transform foodPool;

    public CursorSet cursorSet;

    private void Start()
    {
        GameManager.Instance.ConditionSetting = (EMainMatarials main, List<ESubMatarials> subs, int count, ECookingStyle style, int dishCount) =>
        {
            AnswerOrder.main = main;
            AnswerOrder.sub = subs;
            AnswerOrder.count = count;
            AnswerOrder.style = style;
            AnswerOrder.dishCount = dishCount;
        };
    }

    public void CookingTypePush(ESubMatarials type, Sprite[] thisSprites, CookingStyleSprites[] styleSprite)
    {
        myType = type;

        sprites = (type == ESubMatarials.NULL) ? null : thisSprites;
        styleSprites = styleSprite;

        materialPrice = SubMaterialsPriece[((int)type)];
    }
}
