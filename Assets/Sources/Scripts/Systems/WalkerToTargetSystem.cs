using Leopotam.Ecs;
using System.Linq;
using UnityEngine;
using Client;

sealed class WalkerToTargetSystem : IEcsRunSystem
{
    // auto-injected fields.
    readonly EcsWorld world = null;
    readonly EcsFilter<WalkerToTargetComponent, MovingComponent, OrganismComponent> filter = null;
    readonly EcsFilter<OrganismComponent> organismFilter = null;
    void IEcsRunSystem.Run()
    {
        foreach (var i in filter)
        {
            ref var com = ref filter.Get1(i);
            ref var moving = ref filter.Get2(i);
            ref var organism = ref filter.Get3(i);
            ref var entity = ref filter.GetEntity(i);
            if (com.Target == null)
            {
                // Find Target
                int indexOfClosest = -1;
                float closestDistance = Mathf.Infinity;
                foreach (var j in organismFilter)
                {
                    ref var otherOrganism = ref organismFilter.Get1(j);
                    ref var otherEntity = ref organismFilter.GetEntity(j);
                    if (entity == otherEntity) { continue; }
                    if (!com.WalkerToTargetStaticData.AvailableTargetsFromOrganisms.Contains(otherOrganism.OrganismStaticData)) { continue; }
                    var dist = Vector3.Distance(organism.Transform.position, otherOrganism.Transform.position);
                    if (dist < closestDistance)
                    {
                        closestDistance = dist;
                        indexOfClosest = j;
                    }
                }
                if (indexOfClosest != -1)
                {
                    com.Target = organismFilter.Get1(indexOfClosest).Transform;
                }
            }
            else
            {
                if (!com.Target.gameObject.activeSelf)
                {
                    com.Target = null;
                }
            }
            if (com.Target != null)
            {
                var vector = com.Target.position - organism.Transform.position;
                if (vector.magnitude < com.WalkerToTargetStaticData.ClosestDistance)
                {

                    moving.Direction = Vector3.zero;
                }
                else
                {

                    moving.Direction = vector.normalized;
                }
            }
        }
    }
}