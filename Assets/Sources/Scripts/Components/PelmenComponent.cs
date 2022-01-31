using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct PelmenComponent {
        public SpriteRenderer Face;
        public PelmenFaceType FaceType;
        public PelmenFaceType BaseFaceType;
        public bool IsSmall;
    }
}