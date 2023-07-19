using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

[System.Serializable]
class Chat
{
    public int num = 0;
    public string koChat;
    public string enChat;
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
        SoundManager.instance.PlaySoundClip("Title_bgm", SoundType.BGM);
        Btns();
    }

    void Update()
    {
        ChatSetting();
    }

    void ChatSetting()
    {
        chatNum = chatList[0].num;

        switch (LanguageManager.Instance.languageNum)
        {
            case 0:
                chatText.text = chatList[0].enChat;
                break;

            case 1:
                chatText.text = chatList[0].koChat;
                break;
        }
    }

    void Btns()
    {
        chatBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

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

                    SettingSystem.instance.amongSetting.transform.DOLocalMoveX(0, 0).SetEase(Ease.Linear);
                    SettingSystem.instance.rightSetting.transform.DOLocalMoveX(1050, 0).SetEase(Ease.Linear);

                    settingGroup.gameObject.SetActive(true);
                    settingWidnow.transform.DOLocalMoveY(0, 0.2f).SetEase(Ease.Linear);
                    break;
            }
        });
        
        leftBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            int saveChat = chatList.Count;

            chatList.Insert(0, chatList[saveChat - 1]);
            chatList.RemoveAt(saveChat);
        });

        rightBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            chatList.Add(chatList[0]);
            chatList.RemoveAt(0);
        });
    }
}
