using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Client;

sealed class DamageSystem : IEcsRunSystem
{
    // auto-injected fields.
    readonly EcsWorld world = null;
    readonly EcsFilter<DamageAdderComponent> filter;
    readonly EcsFilter<HealthComponent, OrganismComponent> organismsFilter;


    void IEcsRunSystem.Run()
    {
        foreach (var i in filter)
        {
            ref var damageAdderCom = ref filter.Get1(i);

            if (!damageAdderCom.GameObject.activeSelf) { continue; }
            if(damageAdderCom.DamagePause > 0)
            {
                damageAdderCom.DamagePause -= Time.deltaTime;
                continue;
            }

            foreach (var damageCheckerEllipse in damageAdderCom.DamageCheckerEllipses)
            {

                foreach (var j in organismsFilter)
                {

                    ref var healthCom = ref organismsFilter.Get1(j);
                    ref var organismCom = ref organismsFilter.Get2(j);

                    if (!damageAdderCom.DamagerStaticData.AvailableTargetsFromOrganisms.Contains(organismCom.OrganismStaticData))
                    {
                        continue;
                    }
                    foreach (var healthDamageCheckerEllipse in healthCom.DamageCheckerEllipses)
                    {

                        var radiusFromDamager = GetEllipsRadius(damageAdderCom.HightTransform.position + damageCheckerEllipse.LocalPos,
                                                                healthCom.HightTransform.position + healthDamageCheckerEllipse.LocalPos,
                                                                damageCheckerEllipse.A, damageCheckerEllipse.B, out float dist_1);

                        var radiusFromOrganism = GetEllipsRadius(healthCom.HightTransform.position + healthDamageCheckerEllipse.LocalPos,
                                                                damageAdderCom.HightTransform.position + damageCheckerEllipse.LocalPos,
                                                                healthDamageCheckerEllipse.A, healthDamageCheckerEllipse.B, out float dist_2);
                        if (radiusFromDamager + radiusFromOrganism >= dist_1 && healthCom.Health > 0 && !healthCom.IsImmortal)
                        {
                            damageAdderCom.DamagePause = damageAdderCom.DamagerStaticData.DemagePause;
                            healthCom.Health -= damageAdderCom.DamagerStaticData.Damage;
                            if(damageAdderCom.DamagerStaticData.VFXWhenDamage != VFXType.Default)
                            {
                                world.SpawnVFXCom(damageAdderCom.DamagerStaticData.VFXWhenDamage, healthCom.HightTransform.position, true, Color.yellow);
                            }
                            world.SpawnTextMassageCom($"-{damageAdderCom.DamagerStaticData.Damage}", Color.red, healthCom.HightTransform.position + healthCom.GetDamageOffcet + UnityEngine.Random.Range(-2.5f, 2.5f) * Vector3.right, Vector3.up * 7f, 0.5f);
                            //Debug.Log($"Add {damageAdderCom.DamagerStaticData.Damage} damage to {healthCom.GameObject.name} from {damageAdderCom.GameObject}");
                            if (damageAdderCom.Destroyable)
                            {
                                damageAdderCom.GameObject.SetActive(false);
                            }
                            break;
                        }
                    }
                }
            }
        }
    }


    private float GetEllipsRadius(Vector3 posStart, Vector3 posFinish, float a, float b, out float dist)
    {
        var vector = posFinish - posStart;
        dist = vector.magnitude;
        var anlge = Mathf.Atan2(vector.y, vector.x);
        float sin = Mathf.Sin(anlge);
        float cos = Mathf.Cos(anlge);
        var r = a * b / Mathf.Sqrt(a * a * sin * sin + b * b * cos * cos);
        return r;
    }
}