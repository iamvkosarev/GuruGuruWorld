using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyProject/Game/EnvironmentStaticData")]
public class EnvironmentStaticData : ScriptableObject
{
    public GameObject Tree;
    public int TreeCount = 20;
    public float SpawnTreeRadius = 35f;
}
