using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct DamageAdderComponent
    {
        public DamagerStaticData DamagerStaticData;
        public CheckerEllipse[] DamageCheckerEllipses;
        public Transform HightTransform;
        public int Damage;
        public bool Destroyable;
        public GameObject GameObject;
        public float DamagePause;
    }
}