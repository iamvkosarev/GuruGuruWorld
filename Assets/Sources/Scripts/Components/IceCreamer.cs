using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct IceCreamDispenserComponent
    {
        public Transform EaterTransform;
        public Transform RotatingPart;
        public float IceCreamTime;
    }
}