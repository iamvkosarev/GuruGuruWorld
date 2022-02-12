using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct SlimeRotatingComponent {
        public SpriteRenderer SpriteRenderer;
        public Transform Body;
        public Transform RotatingPart;
    }
}