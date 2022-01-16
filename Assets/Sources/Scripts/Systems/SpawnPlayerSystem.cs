using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

namespace Client {
    sealed class SpawnPlayerSystem : IEcsInitSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly GameSceneDataView sceneData = null;
        readonly GameShareDataView shareData = null;
        readonly GameStaticData staticData = null;

        public void Init () {

            var characterStaticData = staticData.CharactersStaticData.Pelmen;
            var characterView = characterStaticData.Prefab.Instantiate(null);

            var newEntity = world.NewEntity();
            ref var moverCom = ref newEntity.Get<MovingComponent>();
            ref var playerCom = ref newEntity.Get<PlayerComponent>();

            sceneData.CinemachineVirtualCamera.Follow = characterView.transform;
            moverCom.Animator = characterView.Animator;
            moverCom.Transform = characterView.transform;
            moverCom.Body = characterView.Body;
            moverCom.IncreasingSpeedValue = 1f;
            moverCom.MovingStaticData = characterStaticData.MovingStaticData;
            moverCom.RotatingParts = characterView.RotatingParts;

            var pelmenView = characterView.GetComponent<PelmenView>();
            moverCom.RotatingParts = characterView.RotatingParts;

            world.AddPelmenCom(newEntity, pelmenView, true, PelmenHatType.Cowboy,false);
            world.AddEaterCom(newEntity, moverCom.Body, moverCom.RotatingParts[0]);


            ref var eaterCom = ref newEntity.Get<EaterComponent>();
            eaterCom.IceCreamTime = UnityEngine.Random.Range(4f, 8f);
        }
    }
}