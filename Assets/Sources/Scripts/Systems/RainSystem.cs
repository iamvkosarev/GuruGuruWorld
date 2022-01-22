using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Client {
    sealed class RainSystem : IEcsRunSystem,IEcsInitSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly GameStaticData staticData = null;
        readonly EcsFilter<RainComponent, MovingComponent> rainFilter = null;
        readonly EcsFilter<PlayerComponent, MovingComponent> playerFilter;

        private List<GameObject> pool = new List<GameObject>();
        private float spawnTimer = 0f;

        void IEcsInitSystem.Init()
        {
            Transform parent = new GameObject("Rain").transform;
            for (int i = 0; i < staticData.EnvironmentStaticData.RainPoolSize; i++)
            {
                var rain = staticData.EnvironmentStaticData.Rain.Instantiate(parent);
                rain.gameObject.SetActive(false);
                pool.Add(rain);
            }
        }
        void IEcsRunSystem.Run () {
            spawnTimer -= Time.deltaTime;
            if(spawnTimer <= 0)
            {
                spawnTimer = staticData.EnvironmentStaticData.RainSpawnTimerMax;
                if (GetRain(out GameObject rain))
                {
                    rain.transform.position = playerFilter.Get2(0).Transform.position + staticData.EnvironmentStaticData.RainOffsetFromPlayer + new Vector3(UnityEngine.Random.Range( -1f,1f)* staticData.EnvironmentStaticData.RainSpawnZoneSize.x, 0f);
                    rain.SetActive(true);
                }
            }
            foreach (var i in rainFilter)
            {
                ref var rainCom = ref rainFilter.Get1(i);
                ref var moverCom = ref rainFilter.Get2(i);
                if (rainCom.GameObject.activeSelf)
                {
                    rainCom.MoveTimer -= Time.deltaTime;
                    if(rainCom.MoveTimer <= 0)
                    {
                        if(rainCom.SwitchOffTimer == rainCom.SwitchOffMaxTimer)
                        {
                            rainCom.Animator.SetTrigger("Drop");
                            moverCom.IncreasingSpeedValue = 0f;
                        }
                        rainCom.SwitchOffTimer -= Time.deltaTime;
                        if(rainCom.SwitchOffTimer <= 0)
                        {
                            rainCom.GameObject.SetActive(false);
                            rainCom.MoveTimer = UnityEngine.Random.Range(rainCom.MoveTimerMax.x, rainCom.MoveTimerMax.y);
                            rainCom.SwitchOffTimer = rainCom.SwitchOffMaxTimer;
                            rainCom.Animator.SetTrigger("Start");
                            moverCom.IncreasingSpeedValue = UnityEngine.Random.Range(0.5f,1f);
                        }
                    }
                }
            }
        }

        private bool GetRain(out GameObject rainOut)
        {
            rainOut = null;
            foreach (var rain in pool)
            {
                if (!rain.activeSelf)
                {
                    rainOut = rain;
                    return true;
                }
            }
            return false;
        }
    }
}