using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class RotatingSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly EcsFilter<MovingComponent> movingFilter;

        void IEcsRunSystem.Run ()
        {
            foreach (var i in movingFilter)
            {
                ref var moverCom = ref movingFilter.Get1(i);


                if (Mathf.Abs(moverCom.Direction.x) > 0f)
                {
                    if(Mathf.Sign(moverCom.Direction.x) > 0f && moverCom.IsLookingLeft)
                    {
                        for (int j = 0; j < moverCom.RotatingParts.Length; j++)
                        {
                            moverCom.RotatingParts[j].localScale = new Vector3(1f * Mathf.Abs(moverCom.RotatingParts[j].localScale.x), moverCom.RotatingParts[j].localScale.y, moverCom.RotatingParts[j].localScale.z);
                        }
                    }
                    else if(Mathf.Sign(moverCom.Direction.x) < 0f && !moverCom.IsLookingLeft)
                    {
                        for (int j = 0; j < moverCom.RotatingParts.Length; j++)
                        {
                            moverCom.RotatingParts[j].localScale = new Vector3(-1f * Mathf.Abs(moverCom.RotatingParts[j].localScale.x), moverCom.RotatingParts[j].localScale.y, moverCom.RotatingParts[j].localScale.z);
                        }
                    }
                    moverCom.IsLookingLeft = !moverCom.IsLookingLeft;
                }

            }
        }
    }
}