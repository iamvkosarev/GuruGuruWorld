using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct SetCameraComponent {
        public Transform Target;
        public Vector3 Offset;
    }
}