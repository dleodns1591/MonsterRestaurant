using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] Button startBtn;

    void Start()
    {
        StartBtn();
    }
    
    void StartBtn()
    {
        startBtn.onClick.AddListener(() =>
        {
            DOTween.KillAll();
            SceneManager.LoadScene(1);
        });
    }
}
