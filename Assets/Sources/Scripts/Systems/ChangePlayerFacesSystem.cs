using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class ChangePlayerFacesSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly GameStaticData staticData = null;
        readonly EcsFilter<PlayerComponent, MovingComponent, PelmenComponent> playerFilter;

        private int currentTypeIndex = 0;
        void IEcsRunSystem.Run () {
            if (Input.GetKeyDown(KeyCode.R))
            {
                currentTypeIndex += 1;
                currentTypeIndex %= staticData.PelmenStaticData.PlayerFaces.Length;
                SpawnSetter();
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                currentTypeIndex -= 1;
                currentTypeIndex %= staticData.PelmenStaticData.PlayerFaces.Length;
                SpawnSetter();
            }
        }

        private void SpawnSetter()
        {
            foreach (var i in playerFilter)
            {
                ref var moverCom = ref playerFilter.Get2(i);
                ref var pelmenCom = ref playerFilter.Get3(i);
                var setFaceGO = staticData.PelmenStaticData.SetFaceProvider.Instantiate().gameObject;
                setFaceGO.GetComponent<SetFaceProvider>().SetData(moverCom.Transform, staticData.PelmenStaticData.PlayerFaces[currentTypeIndex]);
            }
        }
    }
}