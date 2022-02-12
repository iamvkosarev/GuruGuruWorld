using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct OrganismComponent {
        public Transform Transform;
        public OrganismStaticData OrganismStaticData;
    }
}