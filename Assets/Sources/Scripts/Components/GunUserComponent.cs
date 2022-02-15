using System;
using UnityEngine;

namespace Client {
    [Serializable]
    public struct GunUserComponent
    {
        public bool DropGun;
        public bool UseGun;
        public GunView UsingGunView;
    }
}