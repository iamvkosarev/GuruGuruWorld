using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class BowSystem : IEcsRunSystem, IEcsInitSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly GameSceneDataView sceneData;
        readonly EcsFilter<BowComponent> filter = null;
        readonly EcsFilter<BulletComponent> bulletFilter = null;

        void IEcsInitSystem.Init()
        {
            foreach (var i in filter)
            {
                ref var com = ref filter.Get1(i);
                sceneData.PoolView.Create(com.ShootingStaticData.Bullet.gameObject.name, com.ShootingStaticData.Bullet, com.ShootingStaticData.bulletPoolSize);
                for (int j = 0;j < com.ShootingStaticData.bulletPoolSize; j++)
                {
                    if (sceneData.PoolView.Get(com.ShootingStaticData.Bullet.name, out var @object))
                    {
                        @object.gameObject.SetActive(true);
                    }
                }
            }
        }

        void IEcsRunSystem.Run () {
            foreach (var i in filter)
            {
                ref var com = ref filter.Get1(i);
                if (com.GunView.IsShooting)
                {
                    if(com.CurrentBullet == EcsEntity.Null)
                    {
                        foreach (var j in bulletFilter)
                        {
                            ref var bulletCom = ref bulletFilter.Get1(j);
                            Debug.Log($"{bulletCom.ShootingStaticData.Bullet.name} ==  {com.ShootingStaticData.Bullet.name} && {!bulletCom.GameObject.activeSelf}");
                            if(bulletCom.ShootingStaticData.Bullet.name == com.ShootingStaticData.Bullet.name && !bulletCom.GameObject.activeSelf)
                            {
                                bulletCom.GameObject.SetActive(true);
                                com.CurrentBullet = bulletFilter.GetEntity(j);
                                break;
                            }
                        }
                    }
                    if(com.CurrentBullet == EcsEntity.Null) { continue; }
                    if(com.spandedTime > com.timeForTensionStages)
                    {

                        com.currentTensionStages = Mathf.Min(com.currentTensionStages + 1, com.TensionStages.Length-1);


                        com.spandedTime = 0f;
                    }
                    else
                    {
                        com.spandedTime += Time.deltaTime;
                    }


                    ref var currentBulletCom = ref com.CurrentBullet.Get<BulletComponent>();
                    currentBulletCom.Transform.parent = com.TensionStages[com.currentTensionStages].PointForArrow;
                    currentBulletCom.Transform.localPosition = Vector3.zero;
                    currentBulletCom.Transform.localEulerAngles = Vector3.zero;

                    currentBulletCom.Transform.localScale = new Vector3(1f, 1f, 1f);

                }
                else
                {
                    com.spandedTime = 0f;
                    com.currentTensionStages = 0;
                    if (com.CurrentBullet != EcsEntity.Null)
                    {
                        ref var currentBullet = ref com.CurrentBullet.Get<BulletComponent>();
                        currentBullet.CanFly = true;
                        currentBullet.FlyingSpandedTime = 0f;
                        currentBullet.DroppingSpandedTime = 0f;
                        currentBullet.FlyTime = ((float)com.currentTensionStages+1)/(float)com.TensionStages.Length * com.ShootingStaticData.BulletFlyMaxTime;
                        currentBullet.Transform.parent = null;
                        com.CurrentBullet = EcsEntity.Null;
                    }

                }

                com.SpriteRenderer.sprite = com.TensionStages[com.currentTensionStages].Sprite;
            }
        }
    }
}