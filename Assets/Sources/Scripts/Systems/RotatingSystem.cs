using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class RotatingSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly EcsFilter<MovingComponent> movingFilter;
        readonly EcsFilter<MovingComponent, SlimeRotatingComponent> slimeFilter;

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

            foreach (var i in slimeFilter)
            {
                ref var moverCom = ref slimeFilter.Get1(i);
                ref var slimeRotatingCom = ref slimeFilter.Get2(i);


                if (Mathf.Abs(moverCom.Direction.x) > 0f)
                {
                    if(Mathf.Sign(moverCom.Direction.y) <= 0f)
                    {
                        if (Mathf.Sign(moverCom.Direction.x) > 0f && moverCom.IsLookingLeft)
                        {
                            slimeRotatingCom.Body.localScale = new Vector3(1f * Mathf.Abs(slimeRotatingCom.Body.localScale.x), slimeRotatingCom.Body.localScale.y, slimeRotatingCom.Body.localScale.z);
                           
                        }
                        else if (Mathf.Sign(moverCom.Direction.x) < 0f && !moverCom.IsLookingLeft)
                        {
                            slimeRotatingCom.Body.localScale = new Vector3(-1f * Mathf.Abs(slimeRotatingCom.Body.localScale.x), slimeRotatingCom.Body.localScale.y, slimeRotatingCom.Body.localScale.z);
                            
                        }

                        slimeRotatingCom.SpriteRenderer.color = Color.white;
                        slimeRotatingCom.RotatingPart.localScale = new Vector3(-1f * Mathf.Abs(slimeRotatingCom.RotatingPart.localScale.x), slimeRotatingCom.RotatingPart.localScale.y, slimeRotatingCom.RotatingPart.localScale.z);
                    }
                    else
                    {
                        if (Mathf.Sign(moverCom.Direction.x) > 0f && moverCom.IsLookingLeft)
                        {
                            slimeRotatingCom.Body.localScale = new Vector3(-1f * Mathf.Abs(slimeRotatingCom.Body.localScale.x), slimeRotatingCom.Body.localScale.y, slimeRotatingCom.Body.localScale.z);
                            
                        }
                        else if (Mathf.Sign(moverCom.Direction.x) < 0f && !moverCom.IsLookingLeft)
                        {
                            slimeRotatingCom.Body.localScale = new Vector3(1f * Mathf.Abs(slimeRotatingCom.Body.localScale.x), slimeRotatingCom.Body.localScale.y, slimeRotatingCom.Body.localScale.z);
                        }

                        slimeRotatingCom.SpriteRenderer.color = Color.black * 0.4f;
                        slimeRotatingCom.RotatingPart.localScale = new Vector3(1f * Mathf.Abs(slimeRotatingCom.RotatingPart.localScale.x), slimeRotatingCom.RotatingPart.localScale.y, slimeRotatingCom.RotatingPart.localScale.z);

                    }
                    moverCom.IsLookingLeft = !moverCom.IsLookingLeft;
                }

            }
        }
    }
}