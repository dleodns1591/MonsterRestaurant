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
    [SerializeField] private Sprite[] WormHoleEndingImg;
    [SerializeField] private Text EndingExplanTxt;
    [SerializeField] private EndingData EndingTypes;
    private GameObject EndingCanvas => EndingImg.transform.parent.gameObject;



    public void EndingProduction(EendingType endingType)
    {
        SoundManager.instance.PlaySoundClip("Ending_bgm", SoundType.BGM);

        bool WormHole = false;
        bool WormHoleType = false;

        if (endingType == EendingType.WormHole_FindHouse || endingType == EendingType.WormHole_SpaceAdventure)
        {
            WormHole = true;
            if (endingType == EendingType.WormHole_SpaceAdventure) WormHoleType = true;
        }


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
                    EndingExplanTxt.text = "";
                    if (WormHole == true && i == 8) EndingImg.sprite = WormHoleEndingImg[Convert.ToInt32(WormHoleType)];
                    EndingExplanTxt.DOText(line[i], 0.05f * line[i].Length).OnComplete(() =>
                    {
                        StartCoroutine(CompleteDelay());
                    });
                    while (true)
                    {
                        yield return null;
                        if (Input.GetMouseButtonDown(0) && isEndLine)
                        {
                            isEndLine = false;
                            break;
                        }
                    }
                }
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene("Title");
                }


                IEnumerator CompleteDelay()
                {
                    yield return new WaitForSeconds(0.8f);
                    isEndLine = true;
                }
            }
        }
    }
}
