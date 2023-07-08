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
    int endingCount = 0;

    [Space(10)]
    [SerializeField] List<endingCheck> endingSprite = new List<endingCheck>();


    void Start()
    {
        EndingBtns();
    }

    void Update()
    {
        EndingAnimation();
    }

    void EndingBtns()
    {
        endingCancleBtn.onClick.AddListener(() =>
        {
            endingGroup.DOFade(0, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                endingWindow.transform.DOLocalMoveY(-1050, 0.5f).SetEase(Ease.OutBack);
                endingGroup.gameObject.SetActive(false);
            });
        });

        endingLeftBtn.onClick.AddListener(() =>
        {
            if (0 < endingCount)
            {
                --endingCount;
                endingAnimation.SetBool("Left", true);
                AutoFlip.instnace.FlipLeftPage();
            }
        });

        endingRightBtn.onClick.AddListener(() =>
        {
            if (endingCount < endingSprite.Count)
            {
                ++endingCount;
                endingAnimation.SetBool("Right", true);
                AutoFlip.instnace.FlipRightPage();
            }
        });
    }

    void EndingAnimation()
    {
        if (endingAnimation.GetCurrentAnimatorStateInfo(0).IsName("Book_Left") && endingAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            endingAnimation.SetBool("Left", false);

        if (endingAnimation.GetCurrentAnimatorStateInfo(0).IsName("Book_Right") && endingAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            endingAnimation.SetBool("Right", false);
    }

    void EndingCheck()
    {
        endingSprite[1].isEndingcheck = GameManager.Instance.isEndingOpens[(int)EendingType.Eating];
        endingSprite[2].isEndingcheck = GameManager.Instance.isEndingOpens[(int)EendingType.Loser];
        endingSprite[3].isEndingcheck = GameManager.Instance.isEndingOpens[(int)EendingType.Salve];
        endingSprite[4].isEndingcheck = GameManager.Instance.isEndingOpens[(int)EendingType.Mine];
        endingSprite[5].isEndingcheck = GameManager.Instance.isEndingOpens[(int)EendingType.WormHole];
        endingSprite[6].isEndingcheck = GameManager.Instance.isEndingOpens[(int)EendingType.Eating];
        endingSprite[7].isEndingcheck = GameManager.Instance.isEndingOpens[(int)EendingType.Eating];
        endingSprite[8].isEndingcheck = GameManager.Instance.isEndingOpens[(int)EendingType.Eating];
        endingSprite[9].isEndingcheck = GameManager.Instance.isEndingOpens[(int)EendingType.];

    }
}
