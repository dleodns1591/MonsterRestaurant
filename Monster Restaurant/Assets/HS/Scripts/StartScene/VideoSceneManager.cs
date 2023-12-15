using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using DG.Tweening;
using TMPro;

public class VideoSceneManager : MonoBehaviour
{
    [TextArea, SerializeField]
    private string Speech, EnglishSpeech;
    [SerializeField] private Button MainButton;
    [SerializeField] private TextMeshProUGUI LanguageText;
    [SerializeField] private Image StoryImg;
    [SerializeField] private Text StoryExplanTxt;

    [SerializeField] private Button SkipButton;
    [SerializeField] private VideoClip VideoKorean, VideoEnglish;
    [SerializeField] private VideoPlayer videoPlayer;

    bool isEndLine = false;
    bool isFinal = false;
    bool isClick = false;
    private void Start()
    {
        PlayerPrefs.SetInt("FirstConnect", 0);

        if (PlayerPrefs.GetInt("FirstConnect") != 1)
        {
            StartCoroutine(StoryDelay());
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

    public void Selectlanguage(bool isEnglish)
    {
        if (isEnglish == false)
        {
            LanguageText.text = "   한국어 | 영어";
            LanguageManager.instance.LanguageSetting(1);
            SaveManager.Instance.isEnglish = false;
        }
        else
        {
            LanguageText.text = "Korean | English";
            LanguageManager.instance.LanguageSetting(0);
            SaveManager.Instance.isEnglish = true;
        }
    }

    public void NextStory()
    {
        if(isEndLine == true)
            isClick = true;


        if(isFinal == true)
        {
            if (SaveManager.Instance.isEnglish == false)
                videoPlayer.clip = VideoKorean;
            else
                videoPlayer.clip = VideoEnglish;

            videoPlayer.Play();
            StoryImg.gameObject.SetActive(false);
            StoryExplanTxt.gameObject.SetActive(false);
        }
    }

    IEnumerator StoryDelay()
    {
        isEndLine = false;
        FadeInOut.instance.Fade();
        StoryImg.DOFade(1, FadeInOut.instance.fadeTime);
        yield return new WaitForSeconds(FadeInOut.instance.fadeTime);

        StartCoroutine(Typing());
        IEnumerator Typing()
        {
            string[] line = Speech.Split('\n');
            string[] lineEnglish = EnglishSpeech.Split('\n');
            var wait = new WaitForSeconds(0.05f);
            yield return wait;
            for (int i = 0; i < line.Length; i++)
            {
                StoryExplanTxt.text = "";
                if (SaveManager.Instance.isEnglish == false)
                {
                    StoryExplanTxt.DOText(line[i], 0.05f * line[i].Length).OnComplete(() =>
                    {
                        StartCoroutine(CompleteDelay());
                    });
                }
                else
                {
                    StoryExplanTxt.DOText(lineEnglish[i], 0.05f * lineEnglish[i].Length).OnComplete(() =>
                    {
                        StartCoroutine(CompleteDelay());
                    });
                }
                while (isClick == false)
                {
                    yield return null;
                }
                    isClick = false;
                    isEndLine = false;
            }

            isFinal = true;
            IEnumerator CompleteDelay()
            {
                yield return new WaitForSeconds(0.8f);
                isEndLine = true;
            }
        }

    }
}
