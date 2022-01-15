using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Client {
    sealed class SpawnNPCSystem : IEcsInitSystem {
        readonly EcsWorld world = null;
        readonly GameSceneDataView sceneData = null;
        readonly GameShareDataView shareData = null;
        readonly GameStaticData staticData = null;

        public void Init()
        {
            for (int i = 0; i < 30; i++)
            {
                SpawnNPCPelmen();
            }
        }

        private void SpawnNPCPelmen()
        {
            var characterStaticData = staticData.CharactersStaticData.Player;
            var characterView = characterStaticData.Prefab.Instantiate(null);

            var newEntity = world.NewEntity();
            ref var moverCom = ref newEntity.Get<MovingComponent>();
            ref var pelmenCom = ref newEntity.Get<PelmenComponent>();
            ref var npcCom = ref newEntity.Get<NPCComponent>();

            moverCom.Animator = characterView.Animator;
            moverCom.Transform = characterView.transform;
            moverCom.Body = characterView.Body;
            moverCom.IncreasingSpeedValue = 1f;
            moverCom.JumpPauseSpandedTime = UnityEngine.Random.Range(0f, 1f);
            moverCom.MovingStaticData = characterStaticData.MovingStaticData;

            var pelmenView = characterView.GetComponent<PelmenView>();
            SelectHat(ref pelmenView);
            moverCom.RotatingParts = characterView.RotatingParts;

            pelmenCom.Face = pelmenView.Face;


            characterView.transform.position = 20f * new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
            Vector2 circlePos = UnityEngine.Random.insideUnitCircle * 15f;
            npcCom.TargetPos = moverCom.Transform.position + new Vector3(circlePos.x, circlePos.y);
        }

        private void SelectHat(ref PelmenView pelmenView)
        {
            int indexHat = UnityEngine.Random.Range(0, staticData.PelmenStaticData.PelmenHats.Length);
            var hatData = staticData.PelmenStaticData.PelmenHats[indexHat];
            if (hatData.PelmenHatType == PelmenHatType.Base)
            {
                pelmenView.Hat.enabled = false;
            }
            else
            {
                pelmenView.Hat.enabled = true;
                pelmenView.Hat.sprite = hatData.Sprite;

            }
        }
    }
}