using Leopotam.Ecs;
using UnityEngine;
using Client;

sealed class HungrySystem : IEcsRunSystem
{
    // auto-injected fields.
    readonly EcsWorld world = null;
    readonly EcsFilter<HungryComponent> filter = null;
    void IEcsRunSystem.Run()
    {
        foreach (var i in filter)
        {
            ref var com = ref filter.Get1(i);

            #region ShowingTImer
            if (com.ShowingSliderTimer >= 0)
            {
                com.ShowingSliderTimer -= Time.deltaTime;

                if (!com.Slider.gameObject.activeSelf)
                {
                    com.Slider.gameObject.SetActive(true);
                }
            }
            else
            {
                if (com.Slider.gameObject.activeSelf)
                {
                    com.Slider.gameObject.SetActive(false);
                }
            }
            #endregion

            var colliders = Physics2D.OverlapAreaAll(com.MouthPoint.position, com.MouthSize);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent<FoodView>(out var food))
                {
                    if (Vector3.Distance(com.MouthPoint.position, food.transform.position) < com.MouthSize.x / 2f)
                    {
                        com.ShowingSliderTimer = com.ShowingSliderTimerMax;
                        com.EatenPoints = Mathf.Min(com.EatenPoints + food.AddingPoints, com.MaxEatenPoints);
                        com.Slider.value = com.EatenPoints;
                        com.EatingVFX.startColor = food.Color;
                        com.EatingVFX.Play();
                        world.SpawnSoundCom(ClipType.PickUpFood);
                        collider.enabled = false;
                        food.gameObject.SetActive(false);
                    }
                }
            }

        }
    }
}