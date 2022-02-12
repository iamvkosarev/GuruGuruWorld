using System;
using UnityEngine;

[Serializable]
public struct MovingComponent
{
    public Transform Transform;
    public Transform[] MovingParts;
    public Transform[] RotatingParts;
    public Vector3 Direction, JumpDirection;
    public Animator Animator;
    public bool IsLookingLeft;
    [NonSerialized] public bool WantJump;
    [NonSerialized] public float JumpSpandedTime, JumpPauseSpandedTime, Speed;
    [NonSerialized] public float IncreasingSpeedValue;
    public MovingStaticData MovingStaticData;
}