using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct ShowHelpImageComponent {
        public GameObject ShowingGameObject;
        public Transform CheckPoint;
        public Vector3 CheckPlayerSize;
        public UsingView UsingInterface;
    }
}