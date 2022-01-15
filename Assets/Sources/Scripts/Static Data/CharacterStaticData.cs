using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyProject/Game/CharacterStaticData")]
public class CharacterStaticData : ScriptableObject
{
    public CharacterView Prefab;
    public MovingStaticData MovingStaticData;
}
