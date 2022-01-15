using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Client {
    sealed class SpawnNPCSystem : IEcsInitSystem {
        readonly EcsWorld world = null;
        readonly GameSceneDataView sceneData = null;
        readonly GameShareDataView shareData = null;
        readonly GameStaticData staticData = null;

        private Transform npcParent;
        public void Init()
        {
            npcParent = new GameObject("NPC").transform;
            for (int i = 0; i < 30; i++)
            {
                SpawnNPCPelmen();
            }
        }

        private void SpawnNPCPelmen()
        {
            var characterStaticData = staticData.CharactersStaticData.Player;
            var characterView = characterStaticData.Prefab.Instantiate(npcParent);
            var pelmenView = characterView.GetComponent<PelmenView>();

            var newEntity = world.NewEntity();
            ref var moverCom = ref newEntity.Get<MovingComponent>();
            ref var npcCom = ref newEntity.Get<NPCComponent>();

            moverCom.Animator = characterView.Animator;
            moverCom.Transform = characterView.transform;
            moverCom.Body = characterView.Body;
            moverCom.IncreasingSpeedValue = 1f;
            moverCom.JumpPauseSpandedTime = UnityEngine.Random.Range(0f, 1f);
            moverCom.MovingStaticData = characterStaticData.MovingStaticData;


            world.AddPelmenCom(newEntity, pelmenView, true, PelmenHatType.Random, true);
            world.AddEaterCom(newEntity, moverCom.Body, moverCom.RotatingParts[0]);


            ref var eaterCom = ref newEntity.Get<EaterComponent>();
            eaterCom.iceCreamTime = UnityEngine.Random.Range(0f, 3f);


            characterView.transform.position = 20f * new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
            Vector2 circlePos = UnityEngine.Random.insideUnitCircle * 15f;
            npcCom.TargetPos = moverCom.Transform.position + new Vector3(circlePos.x, circlePos.y);
        }

        
    }
}