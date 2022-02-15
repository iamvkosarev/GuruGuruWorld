using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyProject/Game/DamagerStaticData")]
public class DamagerStaticData : ScriptableObject
{
    public OrganismStaticData[] AvailableTargetsFromOrganisms;
    public VFXType VFXWhenDamage;
    public float DemagePause;
    public int Damage;
}
