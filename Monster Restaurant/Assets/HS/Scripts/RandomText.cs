using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomTexts", menuName = "ScriptableObject/RandomTexts")]
public class RandomText : ScriptableObject
{
    public string[] FirstTexts;
    public string[] MiddleTexts;
    public string[] LastTexts;
}
