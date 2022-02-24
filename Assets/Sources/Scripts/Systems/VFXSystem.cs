using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class VFXSystem : IEcsRunSystem, IEcsInitSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly GameSceneDataView sceneData = null;
        readonly GameStaticData staticData = null;
        readonly EcsFilter<SpawnVFXComponent> spawnVFXFilter;
        readonly EcsFilter<VFXComponent> vfxFilter;
        readonly EcsFilter<PlayerComponent, MovingComponent> playerFilter;

        void IEcsInitSystem.Init()
        {
            int spawnCount = 3;
            foreach (var item in staticData.VFXStaticData.VFX)
            {
                sceneData.PoolView.Create(item.name, item, spawnCount);
                for (int j = 0; j < spawnCount; j++)
                {
                    if (sceneData.PoolView.Get(item.name, out var @object))
                    {
                        @object.gameObject.SetActive(true);
                    }
                }
            }
        }
        void IEcsRunSystem.Run () {
            foreach (var i in vfxFilter)
            {
                ref var com = ref vfxFilter.Get1(i);
                if (com.PlaySpandedTime >= com.PlayTime)
                {
                    com.GameObject.SetActive(false);
                }
                else
                {
                    com.PlaySpandedTime += Time.deltaTime;
                }
            }

            foreach (var m in playerFilter)
            {
                ref var playerMoverCom = ref playerFilter.Get2(m);
                var playerPos = playerMoverCom.Transform.position;
                var left = playerPos.x - staticData.OptimizationStaticData.CheckPlayerZoneSize.x;
                var right = playerPos.x + staticData.OptimizationStaticData.CheckPlayerZoneSize.x;
                var up = playerPos.y + staticData.OptimizationStaticData.CheckPlayerZoneSize.y;
                var down = playerPos.y - staticData.OptimizationStaticData.CheckPlayerZoneSize.y;
                foreach (var i in spawnVFXFilter)
                {
                    ref var spawnVFXCom = ref spawnVFXFilter.Get1(i);

                    if (spawnVFXCom.Position.x >= left && spawnVFXCom.Position.x <= right
                        && spawnVFXCom.Position.y >= down && spawnVFXCom.Position.y <= up)
                    {
                        foreach (var j in vfxFilter)
                        {
                            ref var vfxCom = ref vfxFilter.Get1(j);
                            if (!vfxCom.GameObject.activeSelf && vfxCom.VFXType == spawnVFXCom.VFXType)
                            {
                                Debug.Log("Was Spawned");
                                vfxCom.PlaySpandedTime = 0f;
                                vfxCom.Transform.position = spawnVFXCom.Position;
                                vfxCom.GameObject.SetActive(true);
                                if (spawnVFXCom.SetColor)
                                {
                                    vfxCom.SpriteRenderer.color = spawnVFXCom.Color;
                                }
                                spawnVFXFilter.GetEntity(i).Destroy();
                                break;
                            }
                        }
                    }
                    else
                    {
                        spawnVFXFilter.GetEntity(i).Destroy();
                    }
                }
                break;
            }
        }
    }
}