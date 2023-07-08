using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetResolution();
    }

    void SetResolution()
    {
        int width = 1920;
        int Height = 1080;

        Screen.SetResolution(width, Height, true);
    }
}
