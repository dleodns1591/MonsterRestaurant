using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
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
    [SerializeField] private Sprite[] pushMachineSprite;
    [SerializeField] private Image pushMachineImage;
    [SerializeField] private Animator railAnimation;
    [SerializeField] private Animator pushAnimation;

    public bool isCooking;

    public bool isCookingDrop;
    private bool isSelectCookingStyle;
    private ECookingStyle cookingStyle;
    public void CookDrop(CookingBoard cooking)
    {
        if (isCookingDrop == true) return;

        cook = cooking;
        cook.gameObject.transform.parent = transform;
        cook.transform.position = pos[0].position;

        isCookingDrop = true;
    }
    public void CookingStyleButton(int num)
    {
        if (isCooking == true || isCookingDrop == false) return;

        isSelectCookingStyle = true;
        cookingStyle = (ECookingStyle)num;

        cook.style = cookingStyle;

        StartCoroutine(Cooking());
    }

    private IEnumerator Cooking()
    {
        pushMachineImage.sprite = pushMachineSprite[((int)cookingStyle)];


        isCooking = true;
        railAnimation.SetTrigger("IsPlay");
        float t = 0;
        while (t < 0.5f)
        {
            yield return null;
            t+=Time.deltaTime;

            cook.gameObject.transform.position = Vector3.Lerp
                (pos[0].position, pos[1].position, t/0.5f);
        }

        pushAnimation.SetTrigger("IsPlay");

        yield return new WaitForSeconds(0.1f);
        cook.style = cookingStyle;
        cook.CookingComplete();
        yield return new WaitForSeconds(0.9f);

        pushAnimation.ResetTrigger("IsPlay");

        //animation Play

        t = 0;
        while (t < 0.5f)
        {
            yield return null;
            t += Time.deltaTime;

            cook.gameObject.transform.position = Vector3.Lerp
                (pos[1].position, pos[2].position, t / 0.5f);
        }

        railAnimation.ResetTrigger("IsPlay");


        isSelectCookingStyle = false;
        cook = null;
        isCookingDrop = false;
        cookingStyle = ECookingStyle.None;
    }



}
