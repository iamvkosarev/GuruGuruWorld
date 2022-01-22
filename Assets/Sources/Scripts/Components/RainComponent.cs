using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct RainComponent {
        public float MoveTimer;
        public float SwitchOffTimer;
        public Vector2 MoveTimerMax;
        public float SwitchOffMaxTimer;
        public Animator Animator;
        public GameObject GameObject;
    }
}