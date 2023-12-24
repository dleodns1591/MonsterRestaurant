using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageSetting : MonoBehaviour
{
    LanguageManager languageManager;

    void Start()
    {
        languageManager = LanguageManager.instance;
    }

    void Update()
    {

    }
}
