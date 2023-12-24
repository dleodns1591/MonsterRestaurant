using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager instance = null;

    public int languageNum = 1;
    SaveManager saveManager;

    public void LanguageSetting(int index)
    {
        languageNum = index;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];

        switch (languageNum)
        {
            case 0:
                saveManager.isEnglish = true;
                break;

            case 1:
                saveManager.isEnglish = false;
                break;
        }
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

    void Start()
    {
        saveManager = SaveManager.Instance;
    }
}