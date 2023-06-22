using UnityEngine;
using UnityEngine.UI;
public class SubMaterialImages : MonoBehaviour
{
    private Image img;
    private Sprite[] sprites;
    private CookingStyleSprites[] styleSprites;

    private int ran;

    void Start()
    {
        img = GetComponent<Image>();
        sprites = Cooking.Instance.sprites;
        styleSprites = Cooking.Instance.styleSprites;

        ran = Random.Range(0, sprites.Length);

        img.sprite = sprites[ran];
        img.rectTransform.sizeDelta = sprites[ran].rect.size;
    }

    public void ImageProcessing(ECookingStyle style)
    {
        img.sprite = styleSprites[((int)style)].sprites[ran];
    }
}
