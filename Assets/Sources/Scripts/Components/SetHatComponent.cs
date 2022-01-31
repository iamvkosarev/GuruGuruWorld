using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct SetHatComponent {
        public PelmenView PelmenView;
        public bool ChooseHat;
        public PelmenHatType PelmenHatType;
    }
}