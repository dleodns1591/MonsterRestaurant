using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class OrderButtonManager : MonoBehaviour
{
    public void ResetButttonText()
    {
        OrderButtonObject.Instance.BtnCookText.text = "";
        OrderButtonObject.Instance.BtnAskText.text = "";
    }
    public void ButtonSetActive(bool isActive)
    {
        OrderButtonObject.Instance.CookingBtn.gameObject.SetActive(isActive);
        OrderButtonObject.Instance.ReAskBtn.gameObject.SetActive(isActive);
    }
}
