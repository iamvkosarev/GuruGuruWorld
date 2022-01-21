using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyProject/Game/PositionRenserSorterStaticData")]
public class PositionRenserSorterStaticData : ScriptableObject
{
    public float TimerMax = 0.1f;
    public int SortingOrderBase = 5000;
}
