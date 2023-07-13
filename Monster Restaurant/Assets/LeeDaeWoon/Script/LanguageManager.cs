using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageManager : Singleton<LanguageManager>
{
    public int languageNum = 1;

    public void LanguageSetting(int index)
    {
        languageNum = index;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}
