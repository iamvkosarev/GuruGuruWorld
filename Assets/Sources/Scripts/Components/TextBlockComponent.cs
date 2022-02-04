using System;
using TMPro;
using UnityEngine;

namespace Client
{
    [Serializable]
    public struct TextBlockComponent
    {
        public float HeightStep,HeightStart, WidthStep, WidthStart;
        public SpriteRenderer Form;
        public TextMeshPro Text;
        public RectTransform ButtomTag;
        public GameObject GameObject;
        public Vector3 StartOffset;
        public string[] TypingWords;
        public int TypedWords;
        public float TypingLatterTime;
        public float TypeLatterSpandedTime;
        public float SwitchOffTime;
        public float SpandedSwitchOffTime;
    }
}