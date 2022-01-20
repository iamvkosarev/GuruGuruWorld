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
            var characterStaticData = staticData.CharactersStaticData.Butterfly;
            var characterView = characterStaticData.Prefab.Instantiate(creaturesParent);

            var newEntity = world.NewEntity();
            ref var moverCom = ref newEntity.Get<MovingComponent>();
            ref var npcCom = ref newEntity.Get<NPCComponent>();

            moverCom.Animator = characterView.Animator;
            moverCom.Transform = characterView.transform;
            moverCom.Body = characterView.Body;
            moverCom.IncreasingSpeedValue = 1f;
            moverCom.JumpPauseSpandedTime = UnityEngine.Random.Range(0f, 1f);
            moverCom.MovingStaticData = characterStaticData.MovingStaticData;
            moverCom.RotatingParts = characterView.RotatingParts;


            SetColor(ref characterView);
            characterView.transform.position = 20f * new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
            float targetPosLength = UnityEngine.Random.Range(moverCom.MovingStaticData.TargetPosLength.x, moverCom.MovingStaticData.TargetPosLength.y);
            Vector2 circlePos = UnityEngine.Random.insideUnitCircle * targetPosLength;
            npcCom.TargetPos = moverCom.Transform.position + new Vector3(circlePos.x, circlePos.y);
        }
        private void SpawnNPCRabbit()
        {
            var creatureStaticData = staticData.CharactersStaticData.Rabbit;
            var characterView = creatureStaticData.Prefab.Instantiate(creaturesParent);

            var newEntity = world.NewEntity();
            ref var moverCom = ref newEntity.Get<MovingComponent>();
            ref var npcCom = ref newEntity.Get<NPCComponent>();

            moverCom.Animator = characterView.Animator;
            moverCom.Transform = characterView.transform;
            moverCom.Body = characterView.Body;
            moverCom.IncreasingSpeedValue = 1f;
            moverCom.JumpPauseSpandedTime = UnityEngine.Random.Range(0f, 1f);
            moverCom.MovingStaticData = creatureStaticData.MovingStaticData;
            moverCom.RotatingParts = characterView.RotatingParts;


            characterView.transform.position = 20f * new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
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