using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] Button startBtn;

    void Start()
    {
        SoundManager.instance.PlaySoundClip("Title_bgm", SoundType.BGM);

        StartBtn();
    }
    
    void StartBtn()
    {

        startBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            DOTween.KillAll();
            SceneManager.LoadScene(1);
        });
    }
}
