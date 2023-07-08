using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveManager : MonoBehaviour
{
    public bool[] isEndingOpens = new bool[Enum.GetValues(typeof(EendingType)).Length];

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

    private void Start()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        isEndingOpens = GameManager.Instance.isEndingOpens;
    }
}
