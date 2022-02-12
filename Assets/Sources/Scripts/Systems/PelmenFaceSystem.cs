using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;
using Client;

sealed class PelmenFaceSystem : IEcsRunSystem, IEcsInitSystem
{
    // auto-injected fields.
    readonly EcsWorld world = null;
    readonly GameStaticData staticData = null;
    readonly EcsFilter<PelmenComponent, MovingComponent> pelmenFilter = null;
    readonly EcsFilter<PelmenComponent, TargetedFaceComponent> targetedFaceFilter = null;

    private Dictionary<PelmenFaceType, PelmenFaceData> pelmenFaceDictionary = new Dictionary<PelmenFaceType, PelmenFaceData>();
    private Dictionary<PelmenFaceType, PelmenFaceData> pelmenSmallFaceDictionary = new Dictionary<PelmenFaceType, PelmenFaceData>();
    private PelmenFaceData pelmenAss;
    private PelmenFaceData pelmenSmallAss;
    void IEcsInitSystem.Init()
    {
        foreach (var pelmenFace in staticData.PelmenStaticData.PelmenFaces)
        {
            pelmenFaceDictionary.Add(pelmenFace.PelmenFaceType, pelmenFace);
        }
        foreach (var pelmenFace in staticData.PelmenStaticData.PelmenSmallFaces)
        {
            pelmenSmallFaceDictionary.Add(pelmenFace.PelmenFaceType, pelmenFace);
        }
        pelmenAss = staticData.PelmenStaticData.PelmenAss;
        pelmenSmallAss = staticData.PelmenStaticData.PelmenSmallAss;
        foreach (var i in pelmenFilter)
        {
            ref var pelmenCom = ref pelmenFilter.Get1(i);

            pelmenCom.FaceType = pelmenCom.BaseFaceType;

            if (pelmenCom.IsSmall)
            {
                if (pelmenCom.Face.sprite != pelmenSmallFaceDictionary[pelmenCom.FaceType].Sprite)
                {
                    pelmenCom.Face.sprite = pelmenSmallFaceDictionary[pelmenCom.FaceType].Sprite;
                    pelmenCom.Face.transform.localPosition = pelmenSmallFaceDictionary[pelmenCom.FaceType].FaceLocalPos;
                }

            }
            else
            {

                if (pelmenCom.Face.sprite != pelmenFaceDictionary[pelmenCom.FaceType].Sprite)
                {
                    pelmenCom.Face.sprite = pelmenFaceDictionary[pelmenCom.FaceType].Sprite;
                    pelmenCom.Face.transform.localPosition = pelmenFaceDictionary[pelmenCom.FaceType].FaceLocalPos;
                }
            }
        }

    }

    private PelmenFaceType GetRandomFace()
    {
        int index = UnityEngine.Random.Range(1, pelmenFaceDictionary.Count);
        int count = 0;
        foreach (var item in pelmenFaceDictionary)
        {
            if (count == index)
            {
                return item.Key;
            }
            count++;
        }
        return PelmenFaceType.Base;
    }

    private Transform GetRandomPelmen(int except)
    {
        int index = UnityEngine.Random.Range(0, targetedFaceFilter.GetEntitiesCount());
        if (index == except)
        {
            index += 1;
            index %= targetedFaceFilter.GetEntitiesCount();
        }
        return pelmenFilter.Get2(index).Transform;
    }

    void IEcsRunSystem.Run()
    {
        foreach (var i in pelmenFilter)
        {
            ref var pelmenCom = ref pelmenFilter.Get1(i);
            ref var moverCom = ref pelmenFilter.Get2(i);

            if (Mathf.Abs(moverCom.Direction.y) > 0.2f)
            {
                if (moverCom.Direction.y > 0f)
                {
                    if (pelmenCom.IsSmall)
                    {
                        pelmenCom.Face.sprite = pelmenSmallAss.Sprite;
                        pelmenCom.Face.transform.localPosition = pelmenSmallAss.FaceLocalPos;

                    }
                    else
                    {

                        pelmenCom.Face.sprite = pelmenAss.Sprite;
                        pelmenCom.Face.transform.localPosition = pelmenAss.FaceLocalPos;
                    }
                }
                else
                {
                    if (pelmenCom.IsSmall)
                    {
                        if (pelmenCom.Face.sprite != pelmenSmallFaceDictionary[pelmenCom.FaceType].Sprite)
                        {
                            pelmenCom.Face.sprite = pelmenSmallFaceDictionary[pelmenCom.FaceType].Sprite;
                            pelmenCom.Face.transform.localPosition = pelmenSmallFaceDictionary[pelmenCom.FaceType].FaceLocalPos;
                        }

                    }
                    else
                    {

                        if (pelmenCom.Face.sprite != pelmenFaceDictionary[pelmenCom.FaceType].Sprite)
                        {
                            pelmenCom.Face.sprite = pelmenFaceDictionary[pelmenCom.FaceType].Sprite;
                            pelmenCom.Face.transform.localPosition = pelmenFaceDictionary[pelmenCom.FaceType].FaceLocalPos;
                        }
                    }
                }
            }

        }
        foreach (var i in targetedFaceFilter)
        {

            ref var pelmenCom = ref targetedFaceFilter.Get1(i);
            ref var targetedFaceCom = ref targetedFaceFilter.Get2(i);

            if (targetedFaceCom.Target == null)
            {
                targetedFaceCom.Target = GetRandomPelmen(i);
                if (targetedFaceCom.UseRandomFace)
                {
                    targetedFaceCom.FaceType = GetRandomFace();
                }
            }


            if (Vector3.Distance(targetedFaceCom.Object.position, targetedFaceCom.Target.position) <= targetedFaceCom.WorkingDistance &&
                (targetedFaceCom.RotatingPart.localScale.x == -1 && targetedFaceCom.Object.position.x - targetedFaceCom.Target.position.x >= 0f ||
                targetedFaceCom.RotatingPart.localScale.x == 1 && targetedFaceCom.Object.position.x - targetedFaceCom.Target.position.x <= 0f))
            {
                pelmenCom.FaceType = targetedFaceCom.FaceType;
            }
            else
            {
                pelmenCom.FaceType = pelmenCom.BaseFaceType;

            }
        }


    }

}