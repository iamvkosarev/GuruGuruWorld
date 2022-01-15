using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Client {
    sealed class IceCreamSystem : IEcsRunSystem, IEcsInitSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly GameStaticData staticData = null;
        readonly EcsFilter<EaterComponent> eaterFilter = null;
        readonly EcsFilter<IceCreamComponent> iceCreamFilter = null;

        private List<IceCreamView> pool = new List<IceCreamView>();

        void IEcsInitSystem.Init()
        {
            var parent = new GameObject("Ice Cream").transform;
            for (int i = 0; i < staticData.IceCreamStaticData.PoolSize; i++)
            {
                var newIceCream = staticData.IceCreamStaticData.IceCreamPrefab.Instantiate(parent).transform;
                newIceCream.gameObject.SetActive(false);
                pool.Add(newIceCream.GetComponent<IceCreamView>());
            }
        }
        void IEcsRunSystem.Run () {
            foreach (var i in eaterFilter)
            {
                ref var eaterCom = ref eaterFilter.Get1(i);
                if(eaterCom.iceCreamTime > 0f)
                {
                    eaterCom.iceCreamTime -= Time.deltaTime;

                }
                else
                {
                    if(eaterCom.EaterTransform.localPosition.y > 0.2f)
                    {

                        if (ChooseIceCream(out IceCreamView selectedIceView))
                        {
                            world.SpawnIceCream(eaterCom.EaterTransform.position, selectedIceView, eaterCom.RotatingPart.localScale, eaterCom.EaterTransform.localPosition.y);
                            selectedIceView.Animator.SetTrigger("Fall");
                            eaterCom.iceCreamTime = UnityEngine.Random.Range(2f, 4f);
                        }
                    }
                }
            }

            foreach (var i in iceCreamFilter)
            {
                ref var iceCreamCom = ref iceCreamFilter.Get1(i);
                if(iceCreamCom.FallingHight > -0.295f)
                {
                    float moveDistamce =  staticData.IceCreamStaticData.FallingSpeed * Time.deltaTime;
                    iceCreamCom.IceCreamView.transform.position += moveDistamce * Vector3.down;
                    iceCreamCom.FallingHight -= moveDistamce;
                    if(iceCreamCom.FallingHight <= 0)
                    {
                        iceCreamCom.LifeTime = UnityEngine.Random.Range(staticData.IceCreamStaticData.LifeTime * 0.6f, staticData.IceCreamStaticData.LifeTime);
                        iceCreamCom.SwitchingTime = staticData.IceCreamStaticData.SwitchingTime;
                    }
                }
                else
                {
                    if(iceCreamCom.LifeTime > 0)
                    {
                        iceCreamCom.LifeTime -= Time.deltaTime;
                        iceCreamCom.IceCreamView.SpriteRenderer.color = new Color(1f, 1f, 1f, staticData.IceCreamStaticData.SwitchingCurve.Evaluate(iceCreamCom.LifeTime));
                    }
                    else
                    {
                        if (iceCreamCom.SwitchingTime > 0)
                        {
                            iceCreamCom.SwitchingTime -= Time.deltaTime;

                            if(iceCreamCom.SwitchingTime <= 0)
                            {
                                iceCreamCom.IceCreamView.transform.gameObject.SetActive(false);
                                iceCreamFilter.GetEntity(i).Destroy();
                            }
                        }
                    }
                }
            }
        }

        private bool ChooseIceCream(out IceCreamView selectedIceCream)
        {
            selectedIceCream = null;

            foreach (var @object in pool)
            {
                if (!@object.gameObject.activeSelf)
                {
                    selectedIceCream = @object;
                    selectedIceCream.gameObject.SetActive(true);
                    return true;
                }
            }

            return false;
        }
    }
}