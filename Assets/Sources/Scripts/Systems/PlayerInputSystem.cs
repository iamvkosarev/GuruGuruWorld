using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class PlayerInputSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly EcsFilter<PlayerComponent, MovingComponent> playerFilter;


        void IEcsRunSystem.Run () {
            foreach (var i in playerFilter)
            {
                ref var playerCom = ref playerFilter.Get1(i);
                ref var moverCom = ref playerFilter.Get2(i);
                moverCom.Direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


            }
        }
    }
}