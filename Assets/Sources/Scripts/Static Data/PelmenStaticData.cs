using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PelmenFaceType
{
    Base,
    CatSmile,
    Loover,
    Ass,
    RockFace,
    FullToothSmile,
    Talking_1,
    Ñondemning_1,
    SadSimple,
    Talking_2,
    Disappointed,
    Condemning_2,
    Dio
}
public enum PelmenHatType
{
    Base,
    Cowboy,
    Devil,
    Emo,
    Xmas,
    Arrow,
    Random,
    VanGogh,
    Dio
}
[Serializable]
public class PelmenFaceData
{
    public Sprite Sprite;
    public PelmenFaceType PelmenFaceType;
    public Vector3 FaceLocalPos;

}
[Serializable]
public class PelmenHatData
{
    public Sprite Sprite;
    public PelmenHatType PelmenHatType;
    public Vector3 HatLocalPos;

}
[CreateAssetMenu(menuName = "MyProject/Game/PelmenStaticData")]
public class PelmenStaticData : ScriptableObject
{
    public PelmenFaceData[] PelmenFaces;
    public PelmenFaceData PelmenAss;
    public PelmenHatData[] PelmenHats;
    public Color[] colors;
}
