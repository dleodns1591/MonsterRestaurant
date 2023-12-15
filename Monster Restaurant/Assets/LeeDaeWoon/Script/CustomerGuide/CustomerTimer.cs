using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CustomerTimer : MonoBehaviour
{
    [SerializeField] Image timeFill;

    GameManager GM;
    OrderManager OM;
    OrderButtonObject BtnObjects;
    Button cookBtn;
    Button askBtn;
    Tween TimerTween;

    private void Start()
    {
        GM = GameManager.Instance;
        OM = OrderManager.Instance;
        BtnObjects = OrderButtonObject.Instance;
        cookBtn = BtnObjects.CookingBtn;
        askBtn = BtnObjects.ReAskBtn;
    }

    void OnEnable()
    {
        Timer(15);
    }

    private void OnDisable()
    {
        TimerTween.Kill();
    }

    void Timer(int time)
    {
        timeFill.fillAmount = 1;

        TimerTween = DOTween.To(() => timeFill.fillAmount, x => timeFill.fillAmount = x, 0, time).SetEase(Ease.Linear).OnComplete(() =>
          {
              cookBtn.gameObject.SetActive(false);
              askBtn.gameObject.SetActive(false);

              GM.ReturnCook();
              GM.Satisfaction -= 20;
              OM.satisfactionManager.EmotionTextUpdate();

              this.gameObject.SetActive(false);
          });
    }
}
