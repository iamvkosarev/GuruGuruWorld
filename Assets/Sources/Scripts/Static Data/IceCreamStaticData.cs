using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyProject/Game/IceCreamStaticData")]
public class IceCreamStaticData : ScriptableObject
{
    public GameObject IceCreamPrefab;
    public float FallingSpeed;
    public float LifeTime;
    public int PoolCount;
}
