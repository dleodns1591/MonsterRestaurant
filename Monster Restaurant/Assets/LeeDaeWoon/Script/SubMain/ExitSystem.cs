using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitSystem : MonoBehaviour
{
    Button exitYesBtn;
    Button exitNoBtn;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void Btns()
    {
        // 나가기 버튼을 눌렀을 때
        exitYesBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        // 계속하기 버튼을 눌렀을 때
        exitNoBtn.onClick.AddListener(() =>
        {

        });
    }
}
