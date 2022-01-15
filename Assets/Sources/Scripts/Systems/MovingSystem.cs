using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class MovingSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly EcsFilter<MovingComponent> movingFilter;

        void IEcsRunSystem.Run () {
            foreach (var i in movingFilter)
            {
                ref var moverCom = ref movingFilter.Get1(i);
                if (moverCom.MovingStaticData.IsJumping)
                {
                    if (moverCom.JumpPauseSpandedTime > 0)
                    {
                        moverCom.JumpPauseSpandedTime -= Time.deltaTime;
                        moverCom.Speed = 0f;
                    }
                    else
                    {
                        if(moverCom.Direction.magnitude > 0.2f)
                        {
                            moverCom.WantJump = true;
                            moverCom.JumpDirection = moverCom.Direction;
                        }
                        if (moverCom.WantJump)
                        {
                            moverCom.Direction = moverCom.JumpDirection;
                            if (moverCom.Speed == 0)
                            {
                                moverCom.Speed = 0.001f;
                                moverCom.Animator.SetTrigger("Jump");
                            }
                            if (moverCom.JumpSpandedTime < moverCom.MovingStaticData.JumpTime)
                            {
                                var pos = moverCom.Body.localPosition;

                                if(moverCom.JumpSpandedTime > moverCom.MovingStaticData.SetSpeedTime && moverCom.JumpSpandedTime < moverCom.MovingStaticData.LoseSpeedTime)
                                {
                                    moverCom.Speed = moverCom.MovingStaticData.Speed * moverCom.IncreasingSpeedValue + 0.001f;
                                    moverCom.Body.localPosition = 
                                        new Vector3(pos.x, moverCom.MovingStaticData.JumpCurve.Evaluate(Mathf.Clamp01((moverCom.JumpSpandedTime - moverCom.MovingStaticData.SetSpeedTime )/ (moverCom.MovingStaticData.LoseSpeedTime - moverCom.MovingStaticData.SetSpeedTime))) * moverCom.MovingStaticData.JumpHight);
                                }
                                else if (moverCom.JumpSpandedTime >= moverCom.MovingStaticData.LoseSpeedTime)
                                {
                                    moverCom.Speed = 0.001f;

                                }
                                
                                moverCom.JumpSpandedTime += Time.deltaTime;
                            }
                            else
                            {
                                moverCom.Speed = 0f;
                                moverCom.WantJump = false;
                                moverCom.JumpPauseSpandedTime = moverCom.MovingStaticData.JumpPauseTime;
                                moverCom.JumpSpandedTime =0f;
                            }
                        }

                    }
                }
                else
                {
                    moverCom.Speed = moverCom.MovingStaticData.Speed * moverCom.IncreasingSpeedValue;
                }

                moverCom.Transform.position += moverCom.Direction.normalized * moverCom.Speed * Time.fixedDeltaTime;
            }
        }
    }
}