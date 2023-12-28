using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public class LanguageSetting : MonoBehaviour
{
    LanguageManager languageManager;

    void Start()
    {
        languageManager = LanguageManager.instance;
        LocalizationSettings.SelectedLocale = languageManager.selectedLanguage[languageManager.languageNum];
    }
}
