using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyProject/Game/IceCreamStaticData")]
public class IceCreamStaticData : ScriptableObject
{
    public GameObject IceCreamPrefab;
    public AnimationCurve SwitchingCurve;
    public float FallingSpeed;
    public float SwitchingTime = 1f;
    public float LifeTime;
    public int PoolSize;
}
