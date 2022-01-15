using UnityEngine;

namespace Client {
    struct PelmenComponent {
        public SpriteRenderer Face;
        public PelmenFaceType CurrentFaceType;
        public PelmenFaceType PreviouseType;
    }
}