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
        // ������ ��ư�� ������ ��
        exitYesBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        // ����ϱ� ��ư�� ������ ��
        exitNoBtn.onClick.AddListener(() =>
        {

        });
    }
}
