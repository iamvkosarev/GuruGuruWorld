using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyProject/Game/StaticData")]
public class GameStaticData : ScriptableObject
{
    public AudioStaticData AudioStaticData;
    public VFXStaticData VFXStaticData;
    public MovementStaticData MovementStaticData;
    public CharactersStaticData CharactersStaticData;
    public PelmenStaticData PelmenStaticData;
    public EnvironmentStaticData EnvironmentStaticData;
    public IceCreamStaticData IceCreamStaticData;
    public PositionRenserSorterStaticData PositionRenserSorterStaticData;
    public TextStaticData TextStaticData;
    public OptimizationStaticData OptimizationStaticData;
}
