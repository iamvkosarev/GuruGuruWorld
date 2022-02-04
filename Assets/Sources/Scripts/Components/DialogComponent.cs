using System;
using System.Collections.Generic;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct DialogComponent {
        public SpeakerStruct[] speakers;
        public int SpawnedText;
        public float SpandedTime;
        public float WaitNextLineTime;
        public List<SpeakingTextStructure> speakingTextStructures;
    }

    [Serializable]
    public struct SpeakingTextStructure
    {
        [TextArea] public List<string> text;
        public float WaitTime;
    }

    [Serializable]
    public struct SpeakerStruct
    {
        public Transform Transform;
        public Vector3 Offcet;
        [Range(0.05f, 0.95f)] public float ButtomTagOffset;
    }
}