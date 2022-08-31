using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct SetFaceComponent {
        public PelmenFaceType PelmenFaceType;
        public bool RandomType;
        public Transform PelmenTransform;
    }
}