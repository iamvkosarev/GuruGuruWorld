using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct HealthComponent {
        public CheckerEllipse[] DamageCheckerEllipses;
        public Transform HightTransform;
        public SpriteRenderer[] ReactingOnDamageParts;
        public int StartHealth;
        public bool IsDead;
        [NonSerialized] public int Health;
        public bool IsImmortal;
        public Vector3 GetDamageOffcet;
        public GameObject GameObject;
    }

    [Serializable]
    public struct CheckerEllipse
    {
        public float A, B;
        public Vector3 LocalPos;
    }
}