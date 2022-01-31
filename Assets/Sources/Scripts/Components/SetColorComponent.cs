using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct SetColorComponent {
        public SpriteRenderer[] SpriteRenderers;
        public Color NeedColor;
    }
}