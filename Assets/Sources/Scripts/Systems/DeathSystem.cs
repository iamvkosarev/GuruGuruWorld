using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class DeathSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly EcsFilter<HealthComponent, OrganismComponent> filter = null;
        void IEcsRunSystem.Run () {
            foreach (var i in filter)
            {
                ref var healthCom = ref filter.Get1(i);
                ref var organismCom = ref filter.Get2(i);
                if(healthCom.Health<= 0 && !healthCom.IsImmortal && !healthCom.IsDead)
                {
                    healthCom.IsDead = true;
                    healthCom.GameObject.SetActive(false);
                    if(organismCom.OrganismStaticData.Name == "Pelmen")
                    {
                        world.SpawnVFXCom(VFXType.DeathPelmen, healthCom.HightTransform.position);
                    }
                }
            }
        }
    }
}