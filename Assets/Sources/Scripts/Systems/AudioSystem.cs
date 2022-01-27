using Leopotam.Ecs;
using System.Collections.Generic;

namespace Client {
    sealed class AudioSystem : IEcsRunSystem, IEcsInitSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly GameStaticData staticData = null;
        readonly GameSceneDataView sceneDataView = null;
        readonly EcsFilter<PlayClipComponent> filter = null;

        private Dictionary<ClipType, ClipData> clipDictionary = new Dictionary<ClipType, ClipData>();

        void IEcsInitSystem.Init()
        {
            for (int i = 0; i < staticData.AudioStaticData.ClipDatas.Length; i++)
            {
                clipDictionary.Add(staticData.AudioStaticData.ClipDatas[i].ClipType, staticData.AudioStaticData.ClipDatas[i]);
            }
        }
        void IEcsRunSystem.Run () {
            foreach (var i in filter)
            {
                ref var playClipCom = ref filter.Get1(i);
                sceneDataView.AudioSource.PlayOneShot(clipDictionary[playClipCom.ClipType].Clip);
                filter.GetEntity(i).Destroy();
            }
        }
    }
}