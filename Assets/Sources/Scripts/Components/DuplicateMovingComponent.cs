using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct DuplicateMovingComponent {
        public Vector3 WhenTelportPoint;
        public Vector3 WhereTelportLocalPoint;
        public float CheckRadius;
        public Transform DuplicateTransform;
    }
}