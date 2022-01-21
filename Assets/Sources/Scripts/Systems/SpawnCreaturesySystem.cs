using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Client {
    sealed class SpawnCreaturesySystem : IEcsInitSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly GameStaticData staticData = null;

        private Transform creaturesParent;
        void IEcsInitSystem.Init()
        {
            creaturesParent = new GameObject("Creatures").transform;
            for (int i = 0; i < 30; i++)
            {
                SpawnNPCButterfly();
            }
            for (int i = 0; i < 30; i++)
            {
                SpawnNPCRabbit();
            }
        }

        private void SpawnNPCButterfly()
        {
            var creatureStaticData = staticData.CharactersStaticData.Butterfly;
            var creatureView = creatureStaticData.Prefab.Instantiate(creaturesParent);

            var newEntity = world.NewEntity();
            ref var moverCom = ref newEntity.Get<MovingComponent>();
            ref var npcCom = ref newEntity.Get<NPCComponent>();

            moverCom.Animator = creatureView.Animator;
            moverCom.Transform = creatureView.transform;
            moverCom.MovingParts = creatureView.MovingParts;
            moverCom.IncreasingSpeedValue = 1f;
            moverCom.JumpPauseSpandedTime = UnityEngine.Random.Range(0f, 1f);
            moverCom.MovingStaticData = creatureStaticData.MovingStaticData;
            moverCom.RotatingParts = creatureView.RotatingParts;


            SetColor(ref creatureView);
            creatureView.transform.position = creatureStaticData.SpawnRadius * new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
            float targetPosLength = UnityEngine.Random.Range(moverCom.MovingStaticData.TargetPosLength.x, moverCom.MovingStaticData.TargetPosLength.y);
            Vector2 circlePos = UnityEngine.Random.insideUnitCircle * targetPosLength;
            npcCom.TargetPos = moverCom.Transform.position + new Vector3(circlePos.x, circlePos.y);
        }
        private void SpawnNPCRabbit()
        {
            var creatureStaticData = staticData.CharactersStaticData.Rabbit;
            var creatureView = creatureStaticData.Prefab.Instantiate(creaturesParent);

            var newEntity = world.NewEntity();
            ref var moverCom = ref newEntity.Get<MovingComponent>();
            ref var npcCom = ref newEntity.Get<NPCComponent>();

            moverCom.Animator = creatureView.Animator;
            moverCom.Transform = creatureView.transform;
            moverCom.MovingParts = creatureView.MovingParts;
            moverCom.IncreasingSpeedValue = 1f;
            moverCom.JumpPauseSpandedTime = UnityEngine.Random.Range(0f, 1f);
            moverCom.MovingStaticData = creatureStaticData.MovingStaticData;
            moverCom.RotatingParts = creatureView.RotatingParts;


            creatureView.transform.position = creatureStaticData.SpawnRadius * new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
            float targetPosLength = UnityEngine.Random.Range(moverCom.MovingStaticData.TargetPosLength.x, moverCom.MovingStaticData.TargetPosLength.y);
            Vector2 circlePos = UnityEngine.Random.insideUnitCircle * targetPosLength;
            npcCom.TargetPos = moverCom.Transform.position + new Vector3(circlePos.x, circlePos.y);
        }

        private void SetColor(ref CharacterView characterView)
        {
            float randomValue = UnityEngine.Random.Range(0f, 1f);
            if(randomValue <= 0.33f)
            {
                characterView.Animator.SetTrigger("Red");
            }
            else if(randomValue >= 0.66f)
            {

                characterView.Animator.SetTrigger("Blue");
            }
            else
            {
                characterView.Animator.SetTrigger("Yellow");

            }
        }
    }
}