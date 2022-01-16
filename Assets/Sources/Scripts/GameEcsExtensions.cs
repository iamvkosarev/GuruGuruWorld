using Client;
using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameEcsExtensions
{

    public static T Instantiate<T>(this T item, Transform parent = null, Vector3 pos = default, Quaternion rot = default) where T : UnityEngine.Object
    {
        return UnityEngine.Object.Instantiate(item, pos, rot, parent);
    }

    public static void SpawnIceCream(this EcsWorld world, Vector3 pos, IceCreamView iceCreamView, Vector3 localScaleRotatingPart, float fallingHight =0f, float lifeTime =3f)
    {
        var entity = world.NewEntity();
        iceCreamView.transform.position = pos;
        iceCreamView.transform.localScale = localScaleRotatingPart;
        iceCreamView.SpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        ref var iceCreamCom = ref entity.Get<IceCreamComponent>();
        iceCreamCom.FallingHight = fallingHight;
        iceCreamCom.LifeTime = lifeTime;
        iceCreamCom.IceCreamView = iceCreamView;
    }
    public static void AddEaterCom(this EcsWorld world, EcsEntity entity,  Transform eaterTransform, Transform rotatingTransform)
    {
        ref var eaterCom = ref entity.Get<EaterComponent>();
        eaterCom.EaterTransform = eaterTransform;
        eaterCom.RotatingPart = rotatingTransform;
    }

    public static void AddTargetedFace(this EcsWorld world, EcsEntity entity, Transform @object,Transform target, PelmenFaceType pelmenFaceType, float workingDistance, Transform rotatingPart)
    {
        ref var targetedFaceCom = ref entity.Get<TargetedFaceComponent>();
        targetedFaceCom.Target = target;
        targetedFaceCom.Object = @object;
        targetedFaceCom.ReactionType = pelmenFaceType;
        targetedFaceCom.WorkingDistance = workingDistance;
        targetedFaceCom.RotatingPart = rotatingPart;
    }

    public static void AddPelmenCom(this EcsWorld world, EcsEntity entity, PelmenView pelmenView, bool chooseHat = false, PelmenHatType pelmenHatType = PelmenHatType.Random, bool setColor = false)
    {
        ref var pelmenCom = ref entity.Get<PelmenComponent>();
        GameEcsStartup.SelectHat(ref pelmenView, chooseHat, pelmenHatType);
        if (setColor)
        {
            Color skinColor = GameEcsStartup.GetPelmenRandomSkinColor();
            pelmenView.BodySpriteRenderer.color = skinColor;
            pelmenView.FaceSpriteRenderer.color = skinColor;
        }
        pelmenCom.ID = GameEcsStartup.GetPelmenID();
        pelmenCom.Face = pelmenView.FaceSpriteRenderer;
    }

    
}
