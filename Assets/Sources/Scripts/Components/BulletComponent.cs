using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct BulletComponent {
        public ShootingStaticData ShootingStaticData;
        public GameObject GameObject;
        public Transform Transform;
        public bool CanFly;
        public float Hight;
        [NonSerialized]public float FlyTime;
        [NonSerialized]public float FlyingSpandedTime;
        [NonSerialized]public float DroppingSpandedTime;

    }
}