using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] int fadeTime = 0;

    void Start()
    {
        Fade();
    }

    void Fade()
    {
        Image fade = GetComponent<Image>();

        fade.DOFade(1, 0);
        fade.DOFade(0, fadeTime).SetEase(Ease.Linear);
    }
}
