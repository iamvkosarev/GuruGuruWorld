using Client;
using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameEcsExtensions
{

    public static T Instantiate<T>(this T item, Transform parent = null, Vector3 pos = default, Quaternion rot = default) where T : UnityEngine.Object
    {
        return UnityEngine.Object.Instantiate(item, pos, rot, parent);
    }

    public static void SpawnVFX(this EcsWorld world, Vector3 pos, float fallingHight)
    {
        var entity = world.NewEntity();
        ref var iceCreamCom = ref entity.Get<IceCreamComponent>();
        iceCreamCom.FallingHight = fallingHight;
    }
}
