using System;
using UnityEngine;


public enum VFXType
{
    Default,
    DeathPelmen,
    LoveVFX,
    HitVFX
}
namespace Client {
    [Serializable]
    public struct VFXComponent {
        public float PlayTime;
        [NonSerialized]public float PlaySpandedTime;
        public GameObject GameObject;
        public Transform Transform;
        public VFXType VFXType;
        public SpriteRenderer SpriteRenderer;
    }
}