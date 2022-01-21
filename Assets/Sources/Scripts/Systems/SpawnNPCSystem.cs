using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Client {
    sealed class SpawnNPCSystem : IEcsInitSystem {
        readonly EcsWorld world = null;
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
            var characterStaticData = staticData.CharactersStaticData.Pelmen;
            var characterView = characterStaticData.Prefab.Instantiate(npcParent);
            var pelmenView = characterView.GetComponent<PelmenView>();

            var newEntity = world.NewEntity();
            ref var moverCom = ref newEntity.Get<MovingComponent>();
            ref var npcCom = ref newEntity.Get<NPCComponent>();

            moverCom.Animator = characterView.Animator;
            moverCom.Transform = characterView.transform;
            moverCom.MovingParts = characterView.MovingParts;
            moverCom.IncreasingSpeedValue = 1f;
            moverCom.JumpPauseSpandedTime = UnityEngine.Random.Range(0f, 1f);
            moverCom.MovingStaticData = characterStaticData.MovingStaticData;
            moverCom.RotatingParts = characterView.RotatingParts;


            world.AddPelmenCom(newEntity, pelmenView, true, PelmenHatType.Random, true);
            world.AddEaterCom(newEntity, moverCom.MovingParts[0], moverCom.RotatingParts[0]);


            ref var eaterCom = ref newEntity.Get<EaterComponent>();
            eaterCom.IceCreamTime = UnityEngine.Random.Range(4f, 8f);


            characterView.transform.position = characterStaticData.SpawnRadius * new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
            float targetPosLength = UnityEngine.Random.Range(moverCom.MovingStaticData.TargetPosLength.x, moverCom.MovingStaticData.TargetPosLength.y);
            Vector2 circlePos = UnityEngine.Random.insideUnitCircle * targetPosLength;
            npcCom.TargetPos = moverCom.Transform.position + new Vector3(circlePos.x, circlePos.y);
        }

        
    }
}