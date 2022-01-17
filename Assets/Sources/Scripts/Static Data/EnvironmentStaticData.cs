using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnvironmentData
{
    public GameObject Prefab;
    public Sprite[] GressSprites;
    public int SpawnCount = 20;
    public float SpawnRadius = 35f;

}

[CreateAssetMenu(menuName = "MyProject/Game/EnvironmentStaticData")]
public class EnvironmentStaticData : ScriptableObject
{
    public EnvironmentData[] EnvironmentDatas;
}
