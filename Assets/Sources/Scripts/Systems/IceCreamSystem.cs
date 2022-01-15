using Leopotam.Ecs;

namespace Client {
    sealed class IceCreamSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly GameStaticData staticData = null;
        readonly EcsFilter<MovingComponent, IceCreamerComponent> iceCreamerFilter = null;
        readonly EcsFilter<IceCreamComponent> iceCreamFilter = null;
        
        void IEcsRunSystem.Run () {
            foreach (var i in iceCreamerFilter)
            {

            }
        }
    }
}