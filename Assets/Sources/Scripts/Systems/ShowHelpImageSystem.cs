using Leopotam.Ecs;
using Client;
using UnityEngine;

sealed class ShowHelpImageSystem : IEcsRunSystem
{
    // auto-injected fields.
    readonly EcsWorld world = null;
    readonly EcsFilter<ShowHelpImageComponent> filter = null;
    readonly EcsFilter<PlayerComponent, MovingComponent> playerFilter = null;
    void IEcsRunSystem.Run()
    {
        foreach (var i in filter)
        {
            ref var com = ref filter.Get1(i);
            foreach (var j in playerFilter)
            {
                ref var playerMover = ref playerFilter.Get2(j);
                var distanceX = Mathf.Abs(com.CheckPoint.position.x - playerMover.Transform.position.x);
                var distanceY = Mathf.Abs(com.CheckPoint.position.y - playerMover.Transform.position.y);

                if (distanceX < com.CheckPlayerSize.x / 2f && distanceY < com.CheckPlayerSize.y / 2f && !com.UsingInterface.IsUsing)
                {
                    if (!com.ShowingGameObject.activeSelf)
                    {
                        com.ShowingGameObject.SetActive(true);
                    }

                }
                else
                {
                    if (com.ShowingGameObject.activeSelf)
                    {
                        com.ShowingGameObject.SetActive(false);
                    }

                }
                break;

            }
        }
    }
}