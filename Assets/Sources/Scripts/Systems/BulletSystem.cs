using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class BulletSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly EcsFilter<BulletComponent, MovingComponent> filter;
        void IEcsRunSystem.Run () {
            foreach (var i in filter)
            {
                ref var com = ref filter.Get1(i);
                ref var moverCom = ref filter.Get2(i);
                if (com.CanFly)
                {
                    if (com.FlyingSpandedTime >= com.FlyTime)
                    {
                        if(com.DroppingSpandedTime >= com.ShootingStaticData.StopTime)
                        {
                            com.GameObject.SetActive(false);
                            com.CanFly = false;
                        }
                        else
                        {
                            com.DroppingSpandedTime += Time.deltaTime;
                            moverCom.Direction = Vector3.Lerp(-moverCom.Transform.right, Vector3.down, com.ShootingStaticData.BulletFlyCurve.Evaluate(Mathf.Clamp01(com.DroppingSpandedTime/ com.ShootingStaticData.StopTime)));
                            moverCom.Transform.localEulerAngles = new Vector3(0f,0f,Mathf.LerpAngle(moverCom.Transform.localEulerAngles.z, 90f, com.ShootingStaticData.BulletFlyCurve.Evaluate(Mathf.Clamp01(com.DroppingSpandedTime / com.ShootingStaticData.StopTime))));
                            moverCom.Direction = Vector3.Lerp(moverCom.Direction, Vector3.zero, com.ShootingStaticData.BulletFlyCurve.Evaluate(Mathf.Clamp01(com.DroppingSpandedTime / com.ShootingStaticData.StopTime)));
                        }
                    }
                    else
                    {
                        moverCom.Direction = -moverCom.Transform.right;
                        com.FlyingSpandedTime += Time.deltaTime;
                    }
                }
            }
        }
    }
}