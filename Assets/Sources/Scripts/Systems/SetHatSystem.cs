using Leopotam.Ecs;

namespace Client {
    sealed class SetHatSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly GameStaticData staticData = null;
        readonly EcsFilter<SetHatComponent> filter = null;
        void IEcsRunSystem.Run () {
            foreach (var i in filter)
            {
                ref var com = ref filter.Get1(i);
                SelectHat(com.PelmenView, com.ChooseHat, com.PelmenHatType);
                filter.GetEntity(i).Destroy();
            }
        }

        public void SelectHat(PelmenView pelmenView, bool chooseHat, PelmenHatType pelmenHatType)
        {
            PelmenHatData hatData = staticData.PelmenStaticData.PelmenHats[0];
            if (chooseHat)
            {
                if (pelmenHatType == PelmenHatType.Random)
                {
                    int indexHat = UnityEngine.Random.Range(0, staticData.PelmenStaticData.PelmenHats.Length);
                    hatData = staticData.PelmenStaticData.PelmenHats[indexHat];
                }
                else
                {
                    foreach (var pelmenHat in staticData.PelmenStaticData.PelmenHats)
                    {
                        if (pelmenHat.PelmenHatType == pelmenHatType)
                        {
                            hatData = pelmenHat;
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (var pelmenHat in staticData.PelmenStaticData.PelmenHats)
                {
                    if (pelmenHat.PelmenHatType == PelmenHatType.Base)
                    {
                        hatData = pelmenHat;
                        break;
                    }
                }

            }
            if (hatData.PelmenHatType == PelmenHatType.Base)
            {
                pelmenView.HatSpriteRenderer.enabled = false;
            }
            else
            {
                pelmenView.HatSpriteRenderer.enabled = true;
                pelmenView.HatSpriteRenderer.sprite = hatData.Sprite;
                pelmenView.HatSpriteRenderer.transform.localPosition = hatData.HatLocalPos;

            }
        }
    }
}