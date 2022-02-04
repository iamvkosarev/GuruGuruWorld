using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client {
    [Serializable]
    public struct SetTextComponent {

        [TextArea] public string Text;
        public float TypingLatterTime;
        public Transform Taraget;
        public Vector3 Offcet;
        public float SwitchOffTime;
        [Range(0.05f, 0.95f)]public float ButtomTagOffset;
    }
}