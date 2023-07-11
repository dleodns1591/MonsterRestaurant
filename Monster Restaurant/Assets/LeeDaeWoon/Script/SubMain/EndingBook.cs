using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
class endingCheck
{
    public Sprite endingsprite;
    public bool isEndingcheck = false;
}

public class EndingBook : MonoBehaviour
{
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


    void Start()
    {
        EndingBtns();
    }

    void Update()
    {
        EndingCheck();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < endingSprite.Count; i++)
            {
                endingSprite[i].isEndingcheck = true;
            }
        }
    }

    void EndingBtns()
    {
        endingCancleBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            endingGroup.DOFade(0, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                endingWindow.transform.DOLocalMoveY(-1050, 0.5f).SetEase(Ease.OutBack);
                endingGroup.gameObject.SetActive(false);
            });
        });

        endingLeftBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);
            SoundManager.instance.PlaySoundClip("page-flip-3", SoundType.SFX);

            if (0 < endingCount)
            {
                --endingCount;
                AutoFlip.instnace.FlipLeftPage();
            }
        });

        endingRightBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);
            SoundManager.instance.PlaySoundClip("page-flip-3", SoundType.SFX);

            if (endingCount < (endingSprite.Count / 2))
            {
                ++endingCount;
                AutoFlip.instnace.FlipRightPage();
            }
        });
    }

    void EndingCheck()
    {
        SaveManager saveManager = SaveManager.Instance;

        //endingSprite[0].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.Eating];
        //endingSprite[1].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.Loser];
        //endingSprite[2].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.Salve];
        //endingSprite[3].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.Mine];
        //endingSprite[4].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.WormHole];
        //endingSprite[5].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.WormHole_SpaceAdventure];
        //endingSprite[6].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.WormHole_FindHouse];
        //endingSprite[7].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.Dragon];
        //endingSprite[8].isEndingcheck = saveManager.isEndingOpens[(int)EendingType.LookStar];


        for (int i = 0; i < endingSprite.Count; i++)
        {
            if (endingSprite[i].isEndingcheck)
            {
                if (i == 0)
                    Book.instnace.background = endingSprite[0].endingsprite;
                else
                    Book.instnace.bookPages[i-1] = endingSprite[i].endingsprite;
            }
        }
    }
}
