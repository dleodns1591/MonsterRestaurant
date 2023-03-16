using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FoodSubMaterials : MonoBehaviour
{
    [SerializeField] private Sprite[] materials;
    void Start()
    {
        transform.GetComponent<Image>().sprite = materials[(int)CookingMG.Instance.MySubMaterial];
    }
}
