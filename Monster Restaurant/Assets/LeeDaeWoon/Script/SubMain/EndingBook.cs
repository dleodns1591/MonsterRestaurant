using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class endingCheck
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
    public GameObject endingWindow;
    [SerializeField] Animator endingAnimation;

    [SerializeField] Button endingCancleBtn;
    public Button endingLeftBtn;
    [SerializeField] Button endingRightBtn;
    public int endingCount = 0;

    [Space(10)]
    [SerializeField] List<endingCheck> endingSprite = new List<endingCheck>();

    public bool isEndingClick = false;
    public bool isEndingLeft = false;
    public bool isEndingRight = false;
    public bool isEndingCancle = false;

    void Start()
    {
        EndingBtns();
    }

    void Update()
    {
        StartCoroutine(EndingCheck());
        EndingBtnCheck();
    }

    void EndingBtns()
    {
        endingCancleBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            endingGroup.DOFade(0, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                isEndingCancle = true;
                isEndingClick = false;
                endingWindow.transform.DOLocalMoveY(-1050, 0.5f).SetEase(Ease.OutBack);
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
        if (1 < endingCount)
            endingLeftBtn.gameObject.SetActive(true);
        else
            endingLeftBtn.gameObject.SetActive(false);

        if (endingCount < (endingSprite.Count / 2))
            endingRightBtn.gameObject.SetActive(true);
        else
            endingRightBtn.gameObject.SetActive(false);
    }

    public void EndingPageRight()
    {
        if (!isEndingClick)
        {
            isEndingClick = true;

            AutoFlip.instnace.PageFlipTime = 0.01f;
            AutoFlip.instnace.AnimationFramesCount = 2;

            isEndingRight = true;

            ++endingCount;
            AutoFlip.instnace.FlipRightPage();
        }

        else
        {
            AutoFlip.instnace.PageFlipTime = 0.4f;
            AutoFlip.instnace.AnimationFramesCount = 40;
        }
    }

    IEnumerator EndingCheck()
    {
        SaveManager saveManager = SaveManager.Instance;

        endingSprite[2].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.Eating];
        endingSprite[3].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.Loser];
        endingSprite[4].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.Salve];
        endingSprite[5].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.Mine];
        endingSprite[6].isEndingcheck = saveManager.isWormHoleFirstBuy;
        endingSprite[7].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.WormHole_SpaceAdventure];
        endingSprite[8].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.WormHole_FindHouse];
        endingSprite[9].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.Dragon];
        endingSprite[10].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.LookStar];

        for (int i = 0; i < endingSprite.Count; i++)
        {
            if (endingSprite[i].isEndingcheck)
            {
                switch (LanguageManager.instance.languageNum)
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

        if (isEndingCancle)
        {
            isEndingCancle = false;

            int count = endingCount;
            AutoFlip.instnace.AnimationFramesCount = 5;
            AutoFlip.instnace.PageFlipTime = 0.01f;

            for (int i = 0; i < count; i++)
            {
                endingCount--;
                AutoFlip.instnace.FlipLeftPage();

                yield return new WaitForSeconds(0.2f);
            }

            if (endingCount == 0)
            {
                Book.instnace.currentPage = 0;
                AutoFlip.instnace.TimeBetweenPages = 0;
                AutoFlip.instnace.PageFlipTime = 0.4f;
                AutoFlip.instnace.AnimationFramesCount = 40;
            }
        }
    }
}
