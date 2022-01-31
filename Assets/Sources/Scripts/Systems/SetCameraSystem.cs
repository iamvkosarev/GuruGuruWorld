using Leopotam.Ecs;

namespace Client {
    sealed class SetCameraSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly GameSceneDataView sceneData;
        readonly EcsFilter<SetCameraComponent> filter = null;
        void IEcsRunSystem.Run () {
            foreach (var i in filter)
            {
                ref var com = ref filter.Get1(i);
                sceneData.CinemachineVirtualCamera.Follow = com.Target;
                filter.GetEntity(i).Destroy();
            }
        }
    }
}