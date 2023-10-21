using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public static FadeInOut instance;
    void Awake() => instance = this;

    public float fadeTime = 0;

    void Start()
    {
        Fade();
    }

    public void Fade()
    {
        Image fade = GetComponent<Image>();

        fade.DOFade(1, 0);
        fade.DOFade(0, fadeTime).SetEase(Ease.Linear).SetUpdate(true);
    }
    public void LittleFade()
    {
        Image fade = GetComponent<Image>();

        fade.DOFade(0.75f, 0);
        fade.DOFade(0, fadeTime).SetEase(Ease.Linear).SetUpdate(true);
    }

    public void FadeOut()
    {
        Image fade = GetComponent<Image>();

        fade.DOFade(1, fadeTime).SetEase(Ease.Linear).SetUpdate(true);
    }

    public void LittleFadeOut()
    {
        Image fade = GetComponent<Image>();

        fade.DOFade(0.75f, fadeTime).SetEase(Ease.Linear).SetUpdate(true);
    }
    public void RevenueFadeOut()
    {
        Image fade = transform.GetChild(0).GetComponent<Image>();

        fade.DOFade(0, 0);
        fade.DOFade(1, fadeTime).SetEase(Ease.Linear).SetUpdate(true);
    }
}
