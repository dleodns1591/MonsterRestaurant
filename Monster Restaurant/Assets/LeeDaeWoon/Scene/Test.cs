using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] Text text;

    void Start()
    {
        
    }

    void Update()
    {
        TestClick();
    }

    void TestClick()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            text.text = "잘가세요";
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            text.text = "안녕하세요";
        }
    }
}
