using Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

public class BulletProvider : MonoProvider<BulletComponent>
{
    private void Awake()
    {
        value.CanFly = true;
        value.DroppingSpandedTime = value.ShootingStaticData.StopTime;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + Vector3.up * value.Hight, 0.5f);
    }
}
