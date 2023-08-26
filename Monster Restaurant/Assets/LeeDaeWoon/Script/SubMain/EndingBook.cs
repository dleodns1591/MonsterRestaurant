using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
class endingCheck
{
    public Sprite endingspriteKO;
    public Sprite endingspriteENG;
    public bool isEndingcheck = false;
}

public class EndingBook : MonoBehaviour
{
    public static EndingBook instnace;
    void Awake() => instnace = this;

    [Header("¿£µù")]
    [SerializeField] CanvasGroup endingGroup;
    [SerializeField] GameObject endingWindow;
    [SerializeField] Animator endingAnimation;

    [SerializeField] Button endingCancleBtn;
    [SerializeField] Button endingLeftBtn;
    [SerializeField] Button endingRightBtn;
    [SerializeField] int endingCount = 0;

    [Space(10)]
    [SerializeField] List<endingCheck> endingSprite = new List<endingCheck>();

    public bool isEndingLeft = false;
    public bool isEndingRight = false;

    void Start()
    {
        EndingBtns();
    }

    void Update()
    {
        EndingCheck();
        EndingBtnCheck();
    }

    void EndingBtns()
    {
        endingCancleBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            endingGroup.DOFade(0, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                endingWindow.transform.DOLocalMoveY(-1050, 0.5f).SetEase(Ease.OutBack);
                //endingGroup.gameObject.SetActive(false);
            });
        });

        endingLeftBtn.onClick.AddListener(() =>
        {
            if (0 < endingCount && !isEndingLeft)
            {
                isEndingLeft = true;
                SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);
                SoundManager.instance.PlaySoundClip("page-flip-3", SoundType.SFX, 0.8f);

                --endingCount;
                AutoFlip.instnace.FlipLeftPage();
            }
        });

        endingRightBtn.onClick.AddListener(() =>
        {
            if (endingCount < (endingSprite.Count / 2) && !isEndingRight)
            {
                isEndingRight = true;
                SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);
                SoundManager.instance.PlaySoundClip("page-flip-3", SoundType.SFX, 0.8f);

                ++endingCount;
                AutoFlip.instnace.FlipRightPage();
            }
        });
    }

    void EndingBtnCheck()
    {
        if (0 < endingCount)
            endingLeftBtn.gameObject.SetActive(true);
        else
            endingLeftBtn.gameObject.SetActive(false);

        if (endingCount < (endingSprite.Count / 2))
            endingRightBtn.gameObject.SetActive(true);
        else
            endingRightBtn.gameObject.SetActive(false);

    }

    void EndingCheck()
    {
        SaveManager saveManager = SaveManager.Instance;

        endingSprite[0].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.Eating];
        endingSprite[1].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.Loser];
        endingSprite[2].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.Salve];
        endingSprite[3].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.Mine];
        endingSprite[4].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.WormHole];
        endingSprite[5].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.WormHole_SpaceAdventure];
        endingSprite[6].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.WormHole_FindHouse];
        endingSprite[7].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.Dragon];
        endingSprite[8].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.LookStar];

        for (int i = 0; i < endingSprite.Count; i++)
        {
            if (endingSprite[i].isEndingcheck)
            {
                switch (LanguageManager.Instance.languageNum)
                {
                    case 0:
                        if (i == 0)
                            Book.instnace.background = endingSprite[0].endingspriteENG;
                        else
                            Book.instnace.bookPages[i - 1] = endingSprite[i].endingspriteENG;
                        break;

                    case 1:
                        if (i == 0)
                            Book.instnace.background = endingSprite[0].endingspriteKO;
                        else
                            Book.instnace.bookPages[i - 1] = endingSprite[i].endingspriteKO;
                        break;
                }
            }
        }
    }
}
