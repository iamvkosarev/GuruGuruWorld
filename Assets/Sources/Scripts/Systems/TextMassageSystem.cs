using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class TextMassageSystem : IEcsRunSystem, IEcsInitSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly GameSceneDataView sceneData = null;
        readonly GameStaticData staticData = null;
        readonly EcsFilter<SpawnTextMassageComponent> spawnTextFilter;
        readonly EcsFilter<TextMassageComponent> textFilter;
        readonly EcsFilter<PlayerComponent, MovingComponent> playerFilter;

        void IEcsInitSystem.Init()
        {
            int spawnCount = 10;
            sceneData.PoolView.Create("Massage", staticData.TextStaticData.TextMassageProvider.gameObject, spawnCount);
            for (int i = 0; i < spawnCount; i++)
            {
                if(sceneData.PoolView.Get("Massage", out var @object))
                {
                    @object.gameObject.SetActive(true);
                }
            }
        }
        void IEcsRunSystem.Run()
        {
            foreach (var i in textFilter)
            {
                ref var com = ref textFilter.Get1(i);
                if(com.MoveSpandedTime >= com.MoveTime)
                {
                    com.TextFormParent.gameObject.SetActive(false);
                }
                else
                {
                    com.MoveSpandedTime += Time.deltaTime;
                    var procent = com.MoveSpandedTime / com.MoveTime;
                    com.Text.localPosition = Vector3.Lerp(Vector3.zero, com.MoveLocalPos, procent);
                    com.TextMeshPro.alpha = com.ChangeAlphaCurve.Evaluate(procent);
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
                foreach (var i in spawnTextFilter)
                {
                    ref var spawnTextCom = ref spawnTextFilter.Get1(i);
                    
                    if (spawnTextCom.StartPos.x >= left && spawnTextCom.StartPos.x <= right 
                        && spawnTextCom.StartPos.y >= down &&  spawnTextCom.StartPos.y <= up)
                    {/*
                        Debug.Log($"\t\t{up}\n" +
                           $"{left}\t\t\t\t{right}\n" +
                           $"\t\t{down}\n" +
                           $"need: {spawnTextCom.StartPos}");*/
                        foreach (var j in textFilter)
                        {
                            ref var textCom = ref textFilter.Get1(j);
                            if (!textCom.TextFormParent.gameObject.activeSelf)
                            {
                                textCom.TextMeshPro.text = spawnTextCom.Text;
                                textCom.TextMeshPro.color = spawnTextCom.Color;
                                textCom.MoveTime = spawnTextCom.MoveTime;
                                textCom.MoveSpandedTime = 0f;
                                textCom.MoveLocalPos = spawnTextCom.MoveLocalPos;
                                textCom.TextFormParent.position = spawnTextCom.StartPos;
                                textCom.TextFormParent.gameObject.SetActive(true);
                                spawnTextFilter.GetEntity(i).Destroy();
                                break;
                            }
                        }
                    }
                    else
                    {
                        spawnTextFilter.GetEntity(i).Destroy();
                    }
                }
                break;
            }
        }
    }
}