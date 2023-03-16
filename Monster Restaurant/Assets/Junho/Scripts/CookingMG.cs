using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CookingMG : Singleton<CookingMG>
{
    private ECookingMainMaterial mainMaterial;
    public ECookingMainMaterial MyMainMaterial
    {
        get { return mainMaterial; }
        set 
        {
            mainMaterial = value;
            subMaterial = ECookingSubMaterial.END;
        }
    }
    private ECookingSubMaterial subMaterial;
    public ECookingSubMaterial MySubMaterial
    {
        get { return subMaterial; }
        set
        {
            subMaterial = value;
            mainMaterial = ECookingMainMaterial.END;
        }
    }
    public ECookingList myCookingList;

    public Dictionary<ECookingMainMaterial, int> mainPriceCount = new Dictionary<ECookingMainMaterial, int>();
    public Dictionary<ECookingSubMaterial, int> subPriceCount = new Dictionary<ECookingSubMaterial, int>();


    private void Start()
    {
        for (int i = 0; i < (int)ECookingMainMaterial.END; i++)
        {
            mainPriceCount.Add((ECookingMainMaterial)i,0);
        }
        for (int i = 0; i < (int)ECookingSubMaterial.END; i++)
        {
            subPriceCount.Add((ECookingSubMaterial)i,0);
        }
    }
}
