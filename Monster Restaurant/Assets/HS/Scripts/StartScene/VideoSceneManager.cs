using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using DG.Tweening;

public class VideoSceneManager : MonoBehaviour
{
    [TextArea, SerializeField]
    private string Speech;
    [SerializeField] private Image StoryImg;
    [SerializeField] private Text StoryExplanTxt;

    [SerializeField] private Button SkipButton;
    [SerializeField] private VideoPlayer videoPlayer;

    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstConnect") != 1)
        {
            StartCoroutine(StoryDelay(Speech));
            videoPlayer.loopPointReached += OnVideoEnd;
            ButtonAddListener();
            PlayerPrefs.SetInt("FirstConnect", 1);
        }
        else SceneManager.LoadScene("Title");
    }

    void ButtonAddListener()
    {
        SkipButton.onClick.AddListener(() => { SceneManager.LoadScene("Title"); });
    }

    void OnVideoEnd(VideoPlayer videoPlayer)
    {
        SceneManager.LoadScene("Title");
    }

    IEnumerator StoryDelay(string speech)
    {
        bool isEndLine = false;
        FadeInOut.instance.Fade();
        StoryImg.DOFade(1, FadeInOut.instance.fadeTime);
        yield return new WaitForSeconds(FadeInOut.instance.fadeTime);

        StartCoroutine(Typing(speech));
        IEnumerator Typing(string str)
        {
            string[] line = str.Split('\n');
            var wait = new WaitForSeconds(0.05f);
            yield return wait;
            for (int i = 0; i < line.Length; i++)
            {
                StoryExplanTxt.text = "";
                StoryExplanTxt.DOText(line[i], 0.05f * line[i].Length).OnComplete(() =>
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
                videoPlayer.Play();
                StoryImg.gameObject.SetActive(false);
                StoryExplanTxt.gameObject.SetActive(false);
                SkipButton.gameObject.SetActive(true);
            }


            IEnumerator CompleteDelay()
            {
                yield return new WaitForSeconds(0.8f);
                isEndLine = true;
            }
        }

    }
}
