using Cinemachine;
using Leopotam.Ecs;
using UnityEngine;

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
                var transposer = sceneData.CinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
                transposer.m_FollowOffset = com.Offset + new Vector3(0,0,-1f);
                filter.GetEntity(i).Destroy();
            }
        }
    }
}