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
            for (int i = 0; i < staticData.EnvironmentStaticData.EnvironmentDatas.Length; i++)
            {
                var environmentData = staticData.EnvironmentStaticData.EnvironmentDatas[i];
                for (int j = 0; j < environmentData.SpawnCount; j++)
                {
                    var @object = environmentData.Prefab.Instantiate(parent);
                    @object.GetComponent<SpriteRenderer>().sprite = environmentData.GressSprites[UnityEngine.Random.Range(0, environmentData.GressSprites.Length)];
                    Vector2 circlePos = UnityEngine.Random.insideUnitCircle * environmentData.SpawnRadius;
                    @object.transform.position = new Vector3(circlePos.x, circlePos.y);
                }
            }
        }
    }
}