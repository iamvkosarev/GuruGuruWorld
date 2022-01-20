using System;
using UnityEngine;

[Serializable]
public struct MovingComponent
{
    public Transform Transform, Body;
    public Transform[] RotatingParts;
    public Vector3 Direction, JumpDirection;
    public Animator Animator;
    public bool WantJump, IsLookingLeft;
    public float JumpSpandedTime, JumpPauseSpandedTime, Speed, IncreasingSpeedValue;
    public MovingStaticData MovingStaticData;
}