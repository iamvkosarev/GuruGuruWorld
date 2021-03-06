using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "MyProject/Game/MovingStaticData")]
public class MovingStaticData : ScriptableObject
{
    public bool IsJumping = false;
    public float Speed = 0f;
    public Vector2 TargetPosLength;
    public float JumpHight = 0f;
    public float JumpTime = 0f;
    public Vector2 JumpPauseTime;
    public float SetSpeedTime = 0f;
    public float LoseSpeedTime = 0f;
    public AnimationCurve JumpCurve;
}
