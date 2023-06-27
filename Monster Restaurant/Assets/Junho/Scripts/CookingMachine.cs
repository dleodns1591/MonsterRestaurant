using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECookingStyle
{
    Fry,
    Boil,
    Roast,
    None
}


public class CookingMachine : MonoBehaviour
{
    [SerializeField] private CookingBoard cook;
    [SerializeField] private Transform[] pos;
    private bool isCooking;
    private bool isSelectCookingStyle;
    private ECookingStyle cookingStyle;
    public void CookDrop(CookingBoard cooking)
    {
        cook = cooking;
        cook.gameObject.transform.parent = transform;
        cook.transform.position = pos[0].position;
    }
    public void CookingStyleButton(int num)
    {
        if (isCooking == true) return;

        isSelectCookingStyle = true;
        cookingStyle = (ECookingStyle)num;

        StartCoroutine(Cooking());
    }

    private IEnumerator Cooking()
    {
        isCooking = true;

        float t = 0;
        while (t < 0.5f)
        {
            yield return null;
            t+=Time.deltaTime;

            cook.gameObject.transform.position = Vector3.Lerp
                (pos[0].position, pos[1].position, t/0.5f);
        }

        yield return new WaitForSeconds(1f);

        cook.style = cookingStyle;
        //animation Play

        t = 0;
        while (t < 0.5f)
        {
            yield return null;
            t += Time.deltaTime;

            cook.gameObject.transform.position = Vector3.Lerp
                (pos[1].position, pos[2].position, t / 0.5f);
        }

        isSelectCookingStyle = false;
        cook = null;
        isCooking = false;
        cookingStyle = ECookingStyle.None;
    }



}
