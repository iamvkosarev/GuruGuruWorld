using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ClipType
{
    PickUpFood,
    PelmenStartJump,
    PelmenLandJump,
    Guru
}
[Serializable]
public class ClipData
{
    public ClipType ClipType;
    public AudioClip Clip;
}

[CreateAssetMenu(menuName = "MyProject/Game/AudioStaticData")]
public class AudioStaticData : ScriptableObject
{
    public ClipData[] ClipDatas;
}
