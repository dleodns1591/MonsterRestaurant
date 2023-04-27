using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCustomer : MonoBehaviour, I_CustomerType
{
    public void SpecialType(UIText cook, UIText ask, UIText speech)
    {
        cook.text = "알겠습니다";
        ask.text = "네?";
    }
}
