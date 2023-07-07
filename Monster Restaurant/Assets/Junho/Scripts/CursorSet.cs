using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CursorSet : MonoBehaviour
{
    [SerializeField] private Texture2D basicImage;
    [SerializeField] private Texture2D grapImage;

    [SerializeField] private Texture2D[] grapImages;

    public void GrapImageChange(EMainMatarials main)
    {
        grapImage = grapImages[((int)main)];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(grapImage, Vector2.zero, CursorMode.ForceSoftware);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(basicImage, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}
