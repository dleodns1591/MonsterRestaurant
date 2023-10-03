using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSceneManager : MonoBehaviour
{
    [SerializeField] private Button SkipButton;
    [SerializeField] private VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
        ButtonAddListener();
    }

    void ButtonAddListener()
    {
        SkipButton.onClick.AddListener(() => { SceneManager.LoadScene("Title"); });
    }    

    void OnVideoEnd(VideoPlayer videoPlayer)
    {
        SceneManager.LoadScene("Title");
    }
}
