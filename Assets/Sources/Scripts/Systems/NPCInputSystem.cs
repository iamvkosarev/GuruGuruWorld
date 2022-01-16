using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

namespace Client {
    sealed class NPCInputSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly EcsFilter<NPCComponent, MovingComponent> npcFilter;

        void IEcsRunSystem.Run () {
            foreach (var i in npcFilter)
            {
                ref var npcCom = ref npcFilter.Get1(i);
                ref var moverCom = ref npcFilter.Get2(i);

                var directionVector = npcCom.TargetPos - moverCom.Transform.position;
                moverCom.Direction = (directionVector).normalized;

                if(directionVector.magnitude < 1.4f)
                {
                    moverCom.JumpPauseSpandedTime = UnityEngine.Random.Range(0f,1f);
                    float targetPosLength = UnityEngine.Random.Range(moverCom.MovingStaticData.TargetPosLength.x, moverCom.MovingStaticData.TargetPosLength.y);
                    Vector2 circlePos = UnityEngine.Random.insideUnitCircle * targetPosLength;
                    npcCom.TargetPos = moverCom.Transform.position + new Vector3(circlePos.x, circlePos.y   );

                }


            }
        }
    }
}