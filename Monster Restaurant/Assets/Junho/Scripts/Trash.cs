using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class Trash : MonoBehaviour
{
    private float startPos = -800;
    private float endPos = -223;

    public bool isEnter = false;

    private RectTransform my;

    private void Start()
    {
        my = GetComponent<RectTransform>();
    }

    public void Enter()
    {
        isEnter = true;
        my.DOAnchorPosY(endPos, 0.5f);
    }
    public void Exit()
    {
        isEnter = false;
        my.DOAnchorPosY(startPos, 0.5f);
    }
}
