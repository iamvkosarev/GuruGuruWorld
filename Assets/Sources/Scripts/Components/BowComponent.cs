using Leopotam.Ecs;
using System;
using UnityEngine;

[Serializable]
public struct TensionStage
{
    public Sprite Sprite;
    public Transform PointForArrow;
}

namespace Client {
    [Serializable]
    public struct BowComponent {

        public GunView GunView;
        public int currentTensionStages;
        public int tensionStagesCount;
        public float timeForTensionStages;
        public float spandedTime;
        public SpriteRenderer SpriteRenderer;
        public ShootingStaticData ShootingStaticData;
        public TensionStage[] TensionStages;
        public EcsEntity CurrentBullet;
    }
}