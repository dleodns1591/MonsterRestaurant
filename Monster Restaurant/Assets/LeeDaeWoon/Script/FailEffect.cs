using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;

public class FailEffect : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField] PostProcessProfile postProcessProfile;
    Vignette vignette;

    void Start()
    {
        Effect(Color.red, 0.6f, 0.95f, 0.15f, false, true);
    }

    void Update()
    {

    }

    void OnApplicationQuit() // 게임이 종료 되기 전 실행되는 함수
    {
        Effect(Color.black, 0.2f, 0.2f, 1, true, false);
    }


    void Effect(Color color, float intensity, float smoothness, float roundness, bool isRounded, bool isShake)
    {
        if (postProcessProfile.TryGetSettings(out vignette))
        {
            vignette.color.value = color;
            vignette.intensity.value = intensity;
            vignette.smoothness.value = smoothness;
            vignette.roundness.value = roundness;
            vignette.rounded.value = isRounded;

            if (isShake)
            {
                mainCam.DOShakePosition(0.5f, 1).OnComplete(() =>
                {
                    mainCam.transform.position = new Vector3(0, 0, mainCam.transform.position.z);
                    Effect(Color.black, 0.2f, 0.2f, 1, true, false);
                });
            }
        }
    }
}
