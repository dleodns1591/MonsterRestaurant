using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cooking : MonoBehaviour,IPointerDownHandler
{
    private CookingMG cookingMG;
    private bool isMainMaterial = false;

    [SerializeField] private GameObject mainMaterial;
    [SerializeField] private Sprite[] mainMaterialsImage;
    [SerializeField] private GameObject subObj;
    [SerializeField] private FoodSubMaterials subMaterial;

    private void Start()
    {
        cookingMG = CookingMG.Instance;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isMainMaterial == false)
        {
            if (cookingMG.MyMainMaterial == ECookingMainMaterial.END) return;

            isMainMaterial = true;
            mainMaterial.SetActive(isMainMaterial);
            mainMaterial.GetComponent<Image>().sprite = mainMaterialsImage[(int)cookingMG.MyMainMaterial];
            
            if(cookingMG.mainPriceCount.TryGetValue(cookingMG.MyMainMaterial, out int count)) 
            {
                cookingMG.mainPriceCount[cookingMG.MyMainMaterial] = ++count;
            }
        }
        else
        {
            if (cookingMG.MySubMaterial == ECookingSubMaterial.END) return;
           
            GameObject sub = Instantiate(subMaterial, subObj.transform).gameObject;
            Vector2 InputPos = Camera.main.ScreenToWorldPoint(eventData.position);
            sub.transform.position = InputPos;

            if (cookingMG.subPriceCount.TryGetValue(cookingMG.MySubMaterial, out int count))
            {
                cookingMG.subPriceCount[cookingMG.MySubMaterial] = ++count;
            }
        }
    }
}
