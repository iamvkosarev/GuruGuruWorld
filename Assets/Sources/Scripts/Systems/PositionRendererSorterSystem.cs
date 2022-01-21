using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Client {
    sealed class PositionRendererSorterSystem : IEcsInitSystem, IEcsRunSystem {
        

        readonly GameStaticData staticData = null;
        readonly EcsFilter<PositionRendererSorterComponent> filter = null;

        private float timer = 0;
        void IEcsInitSystem.Init()
        {
            SetPositions();
            DeleteRendererSorters();

        }


        void IEcsRunSystem.Run () 
        {
            SetPositions();
        }
        
        private void DeleteRendererSorters()
        {
            foreach (var i in filter)
            {
                ref var positionRendererSorterCom = ref filter.Get1(i);
                if (positionRendererSorterCom.RunOnlyOnce)
                {
                    filter.GetEntity(i).Destroy();
                }
            }
        }
        private void SetPositions()
        {
            timer -= Time.deltaTime;
            
            if (timer <= 0)
            {
                foreach (var i in filter)
                {
                    ref var com = ref filter.Get1(i);
                    com.Renderer.sortingOrder = (int)(staticData.PositionRenserSorterStaticData.SortingOrderBase - com.Transform.position.y - com.Offset);

                }
                timer = staticData.PositionRenserSorterStaticData.TimerMax;

            }
        }
    }
}