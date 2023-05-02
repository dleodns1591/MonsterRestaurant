using UnityEngine;
using UnityEngine.UI;
public class SubMaterialImages : MonoBehaviour
{
    private Image img;
    private Sprite[] sprites;

    void Start()
    {
        img = GetComponent<Image>();
        sprites = Cooking.Instance.sprites;

        int ran = Random.Range(0, sprites.Length);

        img.sprite = sprites[ran];
        img.rectTransform.sizeDelta = sprites[ran].rect.size;
    }
}
