using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CustomerTimer : MonoBehaviour
{
    [SerializeField] Image timeFill;

    void Start()
    {

    }

    void Update()
    {

    }

    void Timer(int time)
    {
        DOTween.To(() => timeFill.fillAmount, x => timeFill.fillAmount = x, 0, time).SetEase(Ease.Linear).OnComplete(() =>
          {

          });
    }
}
