using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class SpawnEnvironmentSystem : IEcsInitSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly GameStaticData staticData = null;

        private Transform parent;
        public void Init () {
            parent = new GameObject("Environment").transform;
            for (int i = 0; i < staticData.EnvironmentStaticData.TreeCount; i++)
            {
                var tree = staticData.EnvironmentStaticData.Tree.Instantiate(parent);
                Vector2 circlePos = UnityEngine.Random.insideUnitCircle * staticData.EnvironmentStaticData.SpawnTreeRadius;
                tree.transform.position = new Vector3(circlePos.x, circlePos.y);
            }
        }
    }
}