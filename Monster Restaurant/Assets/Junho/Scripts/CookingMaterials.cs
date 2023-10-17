using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using DG.Tweening;

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

    private TextMeshProUGUI ExplanText;
    private Tween ColorDotween;
    private Tween PosDotween;
    private float ExplanTextFirstPosY;
    public void CookingSubMaterialPush()
    {
        if (ExplanText == null)
        {
            ExplanText = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            ExplanTextFirstPosY = ExplanText.gameObject.transform.position.y;
        }
        ExplanText.gameObject.SetActive(true);
        ColorDotween.Kill();
        PosDotween.Kill();
        ExplanText.color = new Color(1, 1, 1, 0);
        ExplanText.gameObject.transform.position = new Vector3(ExplanText.gameObject.transform.position.x, ExplanTextFirstPosY);


        ESubMatarials type = (Cooking.Instance.myType == myType) ? ESubMatarials.NULL: myType;

        ColorDotween = ExplanText.DOColor(new Color(1, 1, 1, 1), 0.5f);
        PosDotween = ExplanText.gameObject.transform.DOMoveY(ExplanText.gameObject.transform.position.y + 0.75f, 0.5f);

        Cooking.Instance.CookingTypePush(type, sprites, cookingStyleSprites);
    }
}
