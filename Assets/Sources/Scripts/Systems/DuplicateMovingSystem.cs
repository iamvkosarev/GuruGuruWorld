using Leopotam.Ecs;
using UnityEngine;
using Client;

sealed class DuplicateMovingSystem : IEcsRunSystem
{
    // auto-injected fields.
    readonly EcsWorld world = null;
    readonly EcsFilter<MovingComponent, DuplicateMovingComponent> filter = null;

    void IEcsRunSystem.Run()
    {
        foreach (var i in filter)
        {
            ref var com = ref filter.Get2(i);
            if (Vector3.Distance(com.DuplicateTransform.transform.localPosition, com.WhenTelportPoint) <= com.CheckRadius)
            {
                com.DuplicateTransform.transform.localPosition = com.WhereTelportLocalPoint;
            }
        }
    }
}