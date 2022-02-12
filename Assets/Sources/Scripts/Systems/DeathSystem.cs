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
                    world.SpawnTextMassageCom($"{organismCom.OrganismStaticData.Name} is Dead", Color.red, healthCom.HightTransform.position + healthCom.GetDamageOffcet, Vector3.up * 3f, 2f);
                }
            }
        }
    }
}