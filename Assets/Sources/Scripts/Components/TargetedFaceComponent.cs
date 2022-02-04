using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct TargetedFaceComponent {
        public Transform Target, Object, RotatingPart;
        public PelmenFaceType FaceType;
        public bool UseRandomFace;
        public float WorkingDistance;
    }
}