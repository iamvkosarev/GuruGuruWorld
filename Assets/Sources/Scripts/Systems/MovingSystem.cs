using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class MovingSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly EcsFilter<MovingComponent> movingFilter;
        readonly EcsFilter<PlayerComponent, MovingComponent> playerFilter;

        void IEcsRunSystem.Run () {

            MovingComponent playerMoverCom = new MovingComponent();
            foreach (var i in playerFilter)
            {
                playerMoverCom = playerFilter.Get2(i);
            }

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
                        bool wantJump = moverCom.WantJump;
                        if (moverCom.Direction.magnitude > 0.2f)
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
                            }
                            if (wantJump != moverCom.WantJump)
                            {
                                moverCom.Animator.SetTrigger("Jump");
                                if (moverCom.Transform == playerMoverCom.Transform)
                                {
                                    world.SpawnSoundCom(ClipType.PelmenStartJump);
                                }

                            }
                            if (moverCom.JumpSpandedTime < moverCom.MovingStaticData.JumpTime)
                            {

                                if (moverCom.JumpSpandedTime > moverCom.MovingStaticData.SetSpeedTime && moverCom.JumpSpandedTime < moverCom.MovingStaticData.LoseSpeedTime)
                                {
                                    moverCom.Speed = moverCom.MovingStaticData.Speed * moverCom.IncreasingSpeedValue + 0.001f;
                                    foreach (var movingPart in moverCom.MovingParts)
                                    {

                                        var pos = movingPart.localPosition;
                                        movingPart.localPosition =
                                            new Vector3(pos.x, moverCom.MovingStaticData.JumpCurve.Evaluate(Mathf.Clamp01((moverCom.JumpSpandedTime - moverCom.MovingStaticData.SetSpeedTime) / (moverCom.MovingStaticData.LoseSpeedTime - moverCom.MovingStaticData.SetSpeedTime))) * moverCom.MovingStaticData.JumpHight);
                                    }
                                }
                                else if (moverCom.JumpSpandedTime >= moverCom.MovingStaticData.LoseSpeedTime)
                                {
                                    if (moverCom.Speed > 0.001f)
                                    {

                                        moverCom.Speed = 0.001f;
                                        if (moverCom.Transform == playerMoverCom.Transform)
                                        {
                                            world.SpawnSoundCom(ClipType.PelmenLandJump);
                                        }
                                    }

                                }

                                moverCom.JumpSpandedTime += Time.deltaTime;
                            }
                            else
                            {
                                moverCom.Speed = 0f;
                                moverCom.WantJump = false;
                                moverCom.JumpPauseSpandedTime = UnityEngine.Random.Range(moverCom.MovingStaticData.JumpPauseTime.x,
                                    moverCom.MovingStaticData.JumpPauseTime.y);
                                moverCom.JumpSpandedTime = 0f;
                                moverCom.Animator.SetTrigger("Idle");
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