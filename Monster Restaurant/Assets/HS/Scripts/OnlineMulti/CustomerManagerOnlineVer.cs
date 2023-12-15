using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomerManagerOnlineVer : MonoBehaviour
{
    OrderManager OM;
    GameManager GM;
    Customer customer;

    [SerializeField] private Camera MainCam;
    [SerializeField] private PostProcessProfile PostProcessProfile;
    Vignette vignette;

    private void Awake()
    {
        OM = OrderManager.Instance;
        GM = GameManager.Instance;
        customer = OM.customer;
    }

    private void Start()
    {
        GM.PvpFailEffect = Effect;
    }

    void Effect(Color color, float intensity, float smoothness, float roundness, bool isRounded, bool isShake)
    {
        if (PostProcessProfile.TryGetSettings(out vignette))
        {
            vignette.color.value = color;
            vignette.intensity.value = intensity;
            vignette.smoothness.value = smoothness;
            vignette.roundness.value = roundness;
            vignette.rounded.value = isRounded;

            if (isShake)
            {
                MainCam.DOShakePosition(0.5f, 1).OnComplete(() =>
                {
                    MainCam.transform.position = new Vector3(0, 0, MainCam.transform.position.z);
                    Effect(Color.black, 0.2f, 0.2f, 1, true, false);
                });
            }
        }
    }

    public void EndBtnsSetActive(bool active)
    {
        if (SaveManager.Instance.isEnglish == false)
            OrderButtonObject.Instance.BtnCookText.text = "계속하기";
        else
            OrderButtonObject.Instance.BtnCookText.text = "Continue";

        OrderButtonObject.Instance.CookingBtn.onClick.RemoveAllListeners();
        OrderButtonObject.Instance.CookingBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("OnlinePvp");
        });

        if (SaveManager.Instance.isEnglish == false)
            OrderButtonObject.Instance.BtnAskText.text = "돌아가기";
        else
            OrderButtonObject.Instance.BtnAskText.text = "Go Back";
        OrderButtonObject.Instance.ReAskBtn.onClick.RemoveAllListeners();
        OrderButtonObject.Instance.ReAskBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Title");
        });

        OM.orderButtonManager.ButtonSetActive(active);
    }

    public void SetCustomerType()
    {
        OM.CustomerType = gameObject.AddComponent<OnlinePvp>();
        EeventCustomerSetting((int)EeventCustomerType.GroupOrder);

        GM.Money = 100000;

        OM.CustomerType.SpecialType();
        return;
    }
    public void EeventCustomerSetting(int type)
    {
        customer.CustomerImg.sprite = customer.EventGuestDefualts[type];
        OM.orderMessageManager.NameBallonSetting(NameKoreanReturn("GroupOrder"));
    }
    string NameKoreanReturn(string name)
    {
        if (SaveManager.Instance.isEnglish == false) return "플로리안";
        else return name;
    }
}

