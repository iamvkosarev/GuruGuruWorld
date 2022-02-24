using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TensionStage
{
    public Sprite Sprite;
    public Transform[] PointsForArrowParents;
}

namespace Client {
    [Serializable]
    public struct BowComponent {

        public GunView GunView;
        public int currentTensionStages;
        public int tensionStagesCount;
        public int bulletCount;
        public float timeForTensionStages;
        public float spandedTime;
        public SpriteRenderer SpriteRenderer;
        public ShootingStaticData ShootingStaticData;
        public TensionStage[] TensionStages;
        public List<EcsEntity> CurrentBullets;
    }
}