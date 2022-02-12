using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct WalkerToTargetComponent
    {
        public WalkerToTargetStaticData WalkerToTargetStaticData;
        [NonSerialized] public Transform Target;
    }
}