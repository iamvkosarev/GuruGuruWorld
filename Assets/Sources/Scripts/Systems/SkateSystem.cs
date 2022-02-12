using Leopotam.Ecs;
using Client;

sealed class SkateSystem : IEcsRunSystem
{
    // auto-injected fields.
    readonly EcsWorld world = null;
    readonly EcsFilter<TransportComponent, SkateComponent> filter = null;
    void IEcsRunSystem.Run()
    {
        foreach (var i in filter)
        {
            ref var transportCom = ref filter.Get1(i);
            ref var skateCom = ref filter.Get2(i);

            if (transportCom.TransportView.Direction.magnitude > 0.2f)
            {
                skateCom.Animator.SetFloat("X", transportCom.TransportView.Direction.x);
                skateCom.Animator.SetFloat("Y", transportCom.TransportView.Direction.y);
            }
        }
    }
}