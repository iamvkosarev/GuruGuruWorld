using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PelmenFaceType
{
    Base,
    CatSmile,
    Cool,
    Loover,
    Ass,
    RockFace,
    FullToothSmile
}
public enum PelmenHatType
{
    Base,
    Cowboy,
    Devil,
    Papadur,
    Rock,
    Arrow,
    Random
}
[Serializable]
public class PelmenFaceData
{
    public Sprite Sprite;
    public PelmenFaceType PelmenFaceType;

}
[Serializable]
public class PelmenHatData
{
    public Sprite Sprite;
    public PelmenHatType PelmenHatType;

}
[CreateAssetMenu(menuName = "MyProject/Game/PelmenStaticData")]
public class PelmenStaticData : ScriptableObject
{
    public PelmenFaceData[] PelmenFaces;
    public PelmenHatData[] PelmenHats;
    public Color[] colors;
}
