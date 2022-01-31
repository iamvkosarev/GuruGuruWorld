using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct TransportUserComponent {
        public bool LeaveTranspor;
        public bool UseTranspor;
        public TransportView UsingTransport;
        public MovingStaticData OwnMovingStaticData;
    }
}