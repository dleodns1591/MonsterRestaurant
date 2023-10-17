using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager instance = null;

    public int languageNum = 1;

    public void LanguageSetting(int index)
    {
        languageNum = index;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    void Update()
    {
        if (!SaveManager.Instance.isEnglish)
            languageNum = 1;
        else
            languageNum = 0;
    }
}
