using Client;
using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameEcsExtensions
{

    public static T Instantiate<T>(this T item, Transform parent = null, Vector3 pos = default,
        Quaternion rot = default) where T : UnityEngine.Object
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
    public static void SpawnSoundCom(this EcsWorld world,  ClipType clipType)
    {
        var entity = world.NewEntity();
        ref var com = ref entity.Get<PlayClipComponent>();
        com.ClipType = clipType;
    }
    
    public static void SpawnTextMassageCom(this EcsWorld world,  string text, Color color, Vector3 startPos, Vector3 moveLocalPos, float moveTime = 3f)
    {
        var entity = world.NewEntity();
        ref var com = ref entity.Get<SpawnTextMassageComponent>();
        com.Text = text;
        com.Color= color;
        com.StartPos= startPos;
        com.MoveLocalPos= moveLocalPos;
        com.MoveTime= moveTime;
    }


    
}
