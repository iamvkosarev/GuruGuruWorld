using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "MyProject/Game/WalkerToTargetStaticData")]
public class WalkerToTargetStaticData : ScriptableObject
{
    public OrganismStaticData[] AvailableTargetsFromOrganisms;
    public float ClosestDistance = 2f;
}