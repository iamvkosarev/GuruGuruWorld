using UnityEngine;

namespace Client {
    struct TargetedFaceComponent {
        public Transform Target, Object, RotatingPart;
        public PelmenFaceType ReactionType;
        public float WorkingDistance;
    }
}