using Client;
using Leopotam.Ecs;
using UnityEngine;

sealed class PlayerInputSystem : IEcsRunSystem
{
    // auto-injected fields.
    readonly EcsWorld world = null;
    readonly EcsFilter<PlayerComponent, MovingComponent> playerFilter;
    readonly EcsFilter<PlayerComponent, TransportUserComponent> playerUserTransportFilter;
    readonly EcsFilter<PlayerComponent, GunUserComponent> playerGunUserFilter;

    private bool SwitchOffText;

    void IEcsRunSystem.Run()
    {
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
        foreach (var i in playerGunUserFilter)
        {
            if (Input.GetKey(KeyCode.X))
            {
                ref var userCom = ref playerGunUserFilter.Get2(i);
                userCom.UseGun = true;
            }
            if (Input.GetKey(KeyCode.E))
            {
                ref var userCom = ref playerGunUserFilter.Get2(i);
                userCom.DropGun = true;
            }
        }
    }
}