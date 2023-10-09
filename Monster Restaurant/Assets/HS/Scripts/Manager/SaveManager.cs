using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveManager : MonoBehaviour
{
    public bool[] isEndingOpens = new bool[Enum.GetValues(typeof(EendingType)).Length];
    public bool isWormHoleFirstBuy;

    public bool isChallenge = false;

    public bool isEnglish = false;

    public static SaveManager Instance = null;
    void Awake()
    {
        SaveManager t;
        t = FindObjectOfType(typeof(SaveManager)) as SaveManager;
        if (null == Instance)
        {
            Instance = t;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
