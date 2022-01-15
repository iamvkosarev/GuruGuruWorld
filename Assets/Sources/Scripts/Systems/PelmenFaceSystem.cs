using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Client {
    sealed class PelmenFaceSystem : IEcsRunSystem, IEcsInitSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly GameStaticData staticData = null;
        readonly EcsFilter<PelmenComponent, MovingComponent> pelmenFilter = null;
        readonly EcsFilter<PelmenComponent,TargetedFaceComponent> targetedFaceFilter = null;

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

                pelmenCom.FaceType = PelmenFaceType.Base;
            }
            if(pelmenFilter.GetEntitiesCount() > 1)
            {

                foreach (var i in pelmenFilter)
                {
                    ref var moverCom = ref pelmenFilter.Get2(i);
                    world.AddTargetedFace(pelmenFilter.GetEntity(i), moverCom.Body, GetRandomPelmen(i), GetRandomFace(), 20f, moverCom.RotatingParts[0]);
                }
            }
        }

        private PelmenFaceType GetRandomFace()
        {
            int index = UnityEngine.Random.Range(1, pelmenFaceDictionary.Count);
            int count = 0;
            foreach (var item in pelmenFaceDictionary)
            {
                if(count == index)
                {
                    return item.Key;
                }
                count++;
            }
            return PelmenFaceType.Base;
        }

        private Transform GetRandomPelmen(int except)
        {
            int index = UnityEngine.Random.Range(0, pelmenFilter.GetEntitiesCount());
            if(index == except)
            {
                index += 1;
                index %= pelmenFilter.GetEntitiesCount();
            }
            return pelmenFilter.Get2(index).Body;
        }

        void IEcsRunSystem.Run () {
            foreach (var i in pelmenFilter)
            {
                ref var pelmenCom = ref pelmenFilter.Get1(i);
                ref var moverCom = ref pelmenFilter.Get2(i);

                if(moverCom.Direction.y > 0f)
                {
                    pelmenCom.Face.sprite = pelmenFaceDictionary[PelmenFaceType.Ass].Sprite;
                }
                else
                {
                    if(pelmenCom.Face.sprite != pelmenFaceDictionary[pelmenCom.FaceType].Sprite)
                    {
                        pelmenCom.Face.sprite = pelmenFaceDictionary[pelmenCom.FaceType].Sprite;
                    }
                }
            }
            foreach (var i in targetedFaceFilter)
            {

                ref var pelmenCom = ref targetedFaceFilter.Get1(i);
                ref var targetedFaceCom = ref targetedFaceFilter.Get2(i);
                if(Vector3.Distance(targetedFaceCom.Object.position, targetedFaceCom.Target.position) <= targetedFaceCom.WorkingDistance && 
                    (targetedFaceCom.RotatingPart.localScale.x == -1 && targetedFaceCom.Object.position.x - targetedFaceCom.Target.position.x >= 0f ||
                    targetedFaceCom.RotatingPart.localScale.x == 1 && targetedFaceCom.Object.position.x - targetedFaceCom.Target.position.x <= 0f))
                {
                    pelmenCom.FaceType = targetedFaceCom.ReactionType;
                }
                else
                {
                    pelmenCom.FaceType = PelmenFaceType.Base;

                }
            }
        }

    }
}