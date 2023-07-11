using System;
using UnityEngine;

[Serializable]
public struct EndingContentInfo
{
    [TextArea]
    public string Speech;
    public Sprite EndingSpr;
}


[CreateAssetMenu(fileName = "Ending Data", menuName = "Scriptable Object/Ending Data")]
public class EndingData : ScriptableObject
{
    public EndingContentInfo[] endingData;
}
