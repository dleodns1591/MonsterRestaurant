using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScrollMG : Singleton<MapScrollMG>
{
    [SerializeField] private RectTransform bg;
    private Vector2 LeftUpPos = new Vector2(1820, 800);
    private Vector2 RightDownPos = new Vector2(100, 125);

    [SerializeField] private Vector3[] BgXPos;
    [SerializeField] private int myBgXPos;

    private Coroutine MouseCheckCoroutine;


    public void StartSet()
    {
        bg.anchoredPosition = BgXPos[0];
        myBgXPos = 0;

        if (MouseCheckCoroutine != null) StopCoroutine(MouseCheckCoroutine);
        MouseCheckCoroutine = StartCoroutine(MouseCheck());
    }

    public void StopMouseCheck()
    {
        bg.anchoredPosition = BgXPos[0];

        StopCoroutine(MouseCheckCoroutine);
    }

    public IEnumerator MouseCheck()
    {
        while (true)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0)) print(Input.mousePosition);


            bool isPosYCheck = (Input.mousePosition.y <= LeftUpPos.y && Input.mousePosition.y >= RightDownPos.y);

            if (isPosYCheck)
            {
                if (Input.mousePosition.x > LeftUpPos.x) yield return MapMove(1);
                else if (Input.mousePosition.x < RightDownPos.x) yield return MapMove(-1);
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
        while (t < 0.25f)
        {
            yield return null;
            t += Time.deltaTime;

            bg.anchoredPosition = Vector3.Lerp(startPos, BgXPos[myBgXPos], t / 0.25f);
        }

        yield return new WaitForSeconds(1);
    }
}
