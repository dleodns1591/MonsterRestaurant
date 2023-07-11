using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class EndingManager : MonoBehaviour
{
    [Header("엔딩 관련")]
    [SerializeField] private Image EndingImg;
    [SerializeField] private Text EndingExplanTxt;
    [SerializeField] private EndingData EndingTypes;
    private GameObject EndingCanvas => EndingImg.transform.parent.gameObject;



    public void EndingProduction(EendingType endingType)
    {
        SoundManager.instance.PlaySoundClip("Ending_bgm", SoundType.BGM);
        StartCoroutine(EndingDelay(EndingTypes.endingData[(int)endingType].Speech, EndingTypes.endingData[(int)endingType].EndingSpr));

        IEnumerator EndingDelay(string speech, Sprite spr)
        {
            bool isEndLine = false;
            EndingCanvas.SetActive(true);
            FadeInOut.instance.FadeOut();
            EndingImg.sprite = spr;
            EndingImg.DOFade(1, FadeInOut.instance.fadeTime);
            yield return new WaitForSeconds(FadeInOut.instance.fadeTime + 1);

            StartCoroutine(Typing(speech));
            IEnumerator Typing(string str)
            {
                string[] line = str.Split('\n');
                var wait = new WaitForSeconds(0.05f);
                yield return wait;
                for (int i = 0; i < line.Length; i++)
                {
                    //print(line.Length);
                    EndingExplanTxt.text = "";
                    EndingExplanTxt.DOText(line[i], 0.05f * line[i].Length).OnComplete(() =>
                    {
                        isEndLine = true;
                    });
                    while (true)
                    {
                        yield return null;
                        if (Input.GetMouseButtonDown(0) && isEndLine)
                            break;
                    }
                    isEndLine = false;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene("Title");
                }
            }
        }
    }
}
