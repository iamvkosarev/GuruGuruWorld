using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class PlayerInputSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly EcsFilter<PlayerComponent, MovingComponent, TransportUserComponent> playerFilter;
        readonly EcsFilter<PlayerComponent, TransportUserComponent> playerUserTransportFilter;


        void IEcsRunSystem.Run () {
            foreach (var i in playerFilter)
            {
                ref var playerCom = ref playerFilter.Get1(i);
                ref var moverCom = ref playerFilter.Get2(i);
                moverCom.Direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


            }
            foreach (var i in playerUserTransportFilter)
            {
                if (Input.GetKey(KeyCode.X))
                {
                    ref var userCom = ref playerUserTransportFilter.Get2(i);
                    userCom.UseTranspor = true;
                }
                if (Input.GetKey(KeyCode.Q))
                {
                    ref var userCom = ref playerUserTransportFilter.Get2(i);
                    userCom.LeaveTranspor = true;
                }
            }
        }
    }
}