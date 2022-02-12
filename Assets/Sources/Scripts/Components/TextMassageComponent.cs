using System;
using TMPro;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct TextMassageComponent {
        public TextMeshPro TextMeshPro;
        public Transform TextFormParent, Text;
        public Vector3 MoveLocalPos;
        public AnimationCurve ChangeAlphaCurve;
        public float MoveTime;
        public float MoveSpandedTime;
    }
}