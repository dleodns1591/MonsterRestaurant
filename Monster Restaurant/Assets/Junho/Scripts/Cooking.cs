using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cooking : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private CookingMG cookingMG;
    private bool isMainMaterial = false;

    [SerializeField] private GameObject mainMaterial;
    [SerializeField] private Sprite[] mainMaterialsImage;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(isMainMaterial == false)
        {
            isMainMaterial = true;
            mainMaterial.SetActive(isMainMaterial);
            mainMaterial.GetComponent<Image>().sprite = mainMaterialsImage[((int)cookingMG.myMainMaterial)];
        }
    }
}
