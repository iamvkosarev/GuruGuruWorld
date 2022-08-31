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
    Dio,
    Moustache_1,
    Tanjiro,
    Seva,
    Moustache_2,
    x_x,
    SickWithTongue,
    EvilMonster,
    Scared_1,
    Scared_2,
    Crying
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
    Dio,
    Brown_Hair_1,
    Witcher
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
    public PelmenFaceData[] PelmenSmallFaces;
    public PelmenFaceData PelmenSmallAss;
    public PelmenHatData[] PelmenSmallHats;
    public Color[] colors;
    public SetFaceProvider SetFaceProvider;
    public PelmenFaceType[] PlayerFaces;
}
