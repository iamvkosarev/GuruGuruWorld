using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Client {
    sealed class PelmenFaceSystem : IEcsRunSystem, IEcsInitSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly GameStaticData staticData = null;
        readonly EcsFilter<PelmenComponent, MovingComponent> pelmenFilter = null;

        private Dictionary<PelmenFaceType, PelmenFaceData> pelmenFaceDictionary = new Dictionary<PelmenFaceType, PelmenFaceData>();
        void IEcsInitSystem.Init()
        {
            foreach (var pelmenFace in staticData.PelmenStaticData.PelmenFaces)
            {
                pelmenFaceDictionary.Add(pelmenFace.PelmenFaceType, pelmenFace);
            }
            foreach (var i in pelmenFilter)
            {
                ref var pelmenCom = ref pelmenFilter.Get1(i);

                ChangeType(ref pelmenCom, PelmenFaceType.Base);
            }
        }

        void IEcsRunSystem.Run () {
            foreach (var i in pelmenFilter)
            {
                ref var pelmenCom = ref pelmenFilter.Get1(i);
                ref var moverCom = ref pelmenFilter.Get2(i);

                if(moverCom.Direction.y > 0f)
                {
                    ChangeType(ref pelmenCom, PelmenFaceType.Ass);
                }
                else
                {

                    ChangeType(ref pelmenCom, PelmenFaceType.Base);
                }
            }
        }

        private void ChangeType(ref PelmenComponent pelmenCom, PelmenFaceType newFaceType)
        {
            if(pelmenCom.CurrentFaceType == newFaceType) { return; }
            pelmenCom.PreviouseType = pelmenCom.CurrentFaceType;
            pelmenCom.CurrentFaceType = newFaceType;
            pelmenCom.Face.sprite = pelmenFaceDictionary[newFaceType].Sprite;
        }
    }
}