using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum EFaceType
{
    Happy,
    Umm,
    Angry
}

public class SatisfactionManager : MonoBehaviour
{
    [Header("�丮 �� ������ ����")]
    [SerializeField] private Sprite[] FaceSprites;
    [SerializeField] private Image FaceImage;
    [SerializeField] private Text EmotionText;


    private OrderManager OM;
    private void Start()
    {
        OM = OrderManager.Instance;
    }

    public void LoopStart()
    {
        GameManager.Instance.Satisfaction = 100;
        EmotionText.text = "100%";
        FaceImage.sprite = FaceSprites[(int)EFaceType.Happy];
        StartCoroutine(SatisfactionUpdate());
    }

    public void LoopStop()
    {
        StopAllCoroutines();

        if (GameManager.Instance.Satisfaction <= 70)
            FaceImage.sprite = FaceSprites[(int)EFaceType.Umm];
        if (GameManager.Instance.Satisfaction <= 40)
            FaceImage.sprite = FaceSprites[(int)EFaceType.Angry];

        EmotionText.text = $"{GameManager.Instance.Satisfaction}%";
    }

    IEnumerator SatisfactionUpdate()
    {
        yield return new WaitForSeconds(1);

        while (true)
        {
            if (OM.ReQuestionCount != 0 && !OM.isBeggar)
            {
                GameManager.Instance.Satisfaction -= (OM.ReQuestionCount * 8);
                OM.ReQuestionCount = 0;
            }
            if (!GameManager.Instance.isGroupOrder)
            {
                if (GameManager.Instance.Satisfaction <= 70)
                    FaceImage.sprite = FaceSprites[(int)EFaceType.Umm];
                if (GameManager.Instance.Satisfaction <= 40)
                    FaceImage.sprite = FaceSprites[(int)EFaceType.Angry];
            }
            else
            {
                if (GameManager.Instance.Satisfaction <= 55)
                    FaceImage.sprite = FaceSprites[(int)EFaceType.Angry];
            }
            yield return new WaitForSeconds(1f);

            EmotionText.text = $"{GameManager.Instance.Satisfaction}%";
            GameManager.Instance.Satisfaction--;
            EmotionText.text = $"{GameManager.Instance.Satisfaction}%";
        }
    }
}