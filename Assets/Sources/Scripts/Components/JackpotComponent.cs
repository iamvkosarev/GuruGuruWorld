using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct JackpotComponent {
        public JackpotCaruselPartsStruct[] jackpotCaruselPartsStructs;
        public Animator Animator;
        public float SpinSpeed;
        public float YWhenTeleport;
        public float YWhereTelepor;
    }

    [Serializable]
    public struct JackpotCaruselPartsStruct
    {
        public Transform FirstPart;
        public Transform SecondPart;
    }
}