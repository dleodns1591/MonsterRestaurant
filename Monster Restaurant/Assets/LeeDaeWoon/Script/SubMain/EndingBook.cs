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

            if (0 < endingCount)
            {
                --endingCount;
                AutoFlip.instnace.FlipLeftPage();
            }
        });

        endingRightBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("Button_SFX", SoundType.SFX);

            if (endingCount < (endingSprite.Count / 2) - 1)
            {
                ++endingCount;
                AutoFlip.instnace.FlipRightPage();
            }
        });
    }

    void EndingCheck()
    {
        endingSprite[1].isEndingcheck = SaveManager.Instance.isEndingOpens[(int)EendingType.Eating];
        endingSprite[2].isEndingcheck = SaveManager.Instance.isEndingOpens[(int)EendingType.Loser];
        endingSprite[3].isEndingcheck = SaveManager.Instance.isEndingOpens[(int)EendingType.Salve];
        endingSprite[4].isEndingcheck = SaveManager.Instance.isEndingOpens[(int)EendingType.Mine];
        endingSprite[5].isEndingcheck = SaveManager.Instance.isEndingOpens[(int)EendingType.WormHole];
        endingSprite[6].isEndingcheck = SaveManager.Instance.isEndingOpens[(int)EendingType.WormHole_SpaceAdventure];
        endingSprite[7].isEndingcheck = SaveManager.Instance.isEndingOpens[(int)EendingType.WormHole_FindHouse];
        endingSprite[8].isEndingcheck = SaveManager.Instance.isEndingOpens[(int)EendingType.Dragon];
        endingSprite[9].isEndingcheck = SaveManager.Instance.isEndingOpens[(int)EendingType.LookStar];


        for (int i = 1; i < endingSprite.Count; i++)
        {
            if(endingSprite[i].isEndingcheck)
                Book.instnace.bookPages[i] = endingSprite[i].endingsprite;
        }
    }
}
