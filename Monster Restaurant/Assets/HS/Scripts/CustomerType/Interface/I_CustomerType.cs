using HS_Tree;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface I_CustomerType
{
    void SpecialType(TextMeshProUGUI cook, TextMeshProUGUI ask);

    string SpecialAnswer();
}
