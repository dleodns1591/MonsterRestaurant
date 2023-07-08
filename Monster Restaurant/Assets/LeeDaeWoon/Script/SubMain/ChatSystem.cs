using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

[System.Serializable]
class Chat
{
    public string chat;
    public int num = 0;
}

public class ChatSystem : MonoBehaviour
{
    [Header("대화창")]
    [SerializeField] int chatNum = 0;
    [SerializeField] List<Chat> chatList = new List<Chat>();

    [SerializeField] Text chatText;
    [SerializeField] Button chatBtn;
    [SerializeField] Button leftBtn;
    [SerializeField] Button rightBtn;

    [Header("엔딩")]
    [SerializeField] CanvasGroup endingGroup;
    [SerializeField] GameObject endingWindow;
    int endingCount = 0;

    [Header("설정")]
    [SerializeField] CanvasGroup settingGroup;
    [SerializeField] GameObject settingWidnow;

    void Start()
    {
        Btns();
    }

    void Update()
    {
        chatText.text = chatList[0].chat;
        chatNum = chatList[0].num;
    }

    void Btns()
    {
        chatBtn.onClick.AddListener(() =>
        {
            switch (chatNum)
            {
                case 0:
                    Application.Quit();
                    break;

                case 1:
                    SceneManager.LoadScene(2);
                    break;

                case 2:
                    endingCount = 0;
                    endingGroup.alpha = 1;

                    endingWindow.transform.DOLocalMoveY(0, 0.2f).SetEase(Ease.Linear);
                    endingGroup.gameObject.SetActive(true);
                    break;

                case 3:
                    settingGroup.alpha = 1;

                    settingGroup.gameObject.SetActive(true);
                    settingWidnow.transform.DOLocalMoveY(0, 0.2f).SetEase(Ease.Linear);
                    break;
            }
        });

        leftBtn.onClick.AddListener(() =>
        {
            int saveChat = chatList.Count;

            chatList.Insert(0, chatList[saveChat - 1]);
            chatList.RemoveAt(saveChat);
        });

        rightBtn.onClick.AddListener(() =>
        {
            chatList.Add(chatList[0]);
            chatList.RemoveAt(0);
        });
    }
}
