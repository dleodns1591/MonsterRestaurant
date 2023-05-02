using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScrollMG : Singleton<MapScrollMG>
{
    [SerializeField] private RectTransform bg;
    [SerializeField] private float maxX;
    [SerializeField] private float minX;
    [SerializeField] private Vector3[] BgXPos;
    [SerializeField] private int myBgXPos;

    public IEnumerator MouseCheck()
    {
        while (true)
        {
            yield return null;

            if (Input.mousePosition.x > maxX) yield return MapMove(1);
            else if (Input.mousePosition.x < minX) yield return MapMove(-1);

            if (Input.GetMouseButtonUp(0))
            {
                print("End");
                break;
            }
        }
    }

    private IEnumerator MapMove(int num)
    {
        myBgXPos += num;

        if (myBgXPos <= 0) myBgXPos = 0;
        else if (myBgXPos >= BgXPos.Length - 1) myBgXPos = BgXPos.Length - 1;

        Vector3 startPos = bg.anchoredPosition;
        float t = 0;
        while (t < 0.5f)
        {
            yield return null;
            t += Time.deltaTime;

            bg.anchoredPosition = Vector3.Lerp(startPos, BgXPos[myBgXPos], t / 0.5f);
        }

        yield return new WaitForSeconds(1);
    }
}
