using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyProject/Game/ShootingStaticData")]
public class ShootingStaticData : ScriptableObject
{
    public GameObject Bullet;
    public int Damage;
    public int bulletPoolSize = 3;
    public AnimationCurve BulletFlyCurve;
    public float BulletFlyMaxTime;
    public float StopTime;
    public VFXType ShotVFX;
    public VFXType TouchVFX;
}
