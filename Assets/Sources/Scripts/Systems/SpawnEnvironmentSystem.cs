using Leopotam.Ecs;
using System.Linq;
using UnityEngine;

namespace Client {
    sealed class SpawnEnvironmentSystem : IEcsInitSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly GameStaticData staticData = null;
        readonly GameSceneDataView sceneDataView = null;

        private Transform parent;
        public void Init () {
            parent = new GameObject("Environment").transform;
            sceneDataView.MapGenerator.Seed = UnityEngine.Random.Range(0,1000);
            var terrainMap = sceneDataView.MapGenerator.GenerateMap();
            foreach (var keyValuePair in terrainMap)
            {
                var typeOfTerrain = keyValuePair.Key;
                var avaliableCoordinates = keyValuePair.Value;


                for (int i = 0; i < staticData.EnvironmentStaticData.EnvironmentDatas.Length; i++)
                {
                    var environmentData = staticData.EnvironmentStaticData.EnvironmentDatas[i];
                    if(!environmentData.TerrainTypes.Contains(typeOfTerrain)) { continue; }

                    for (int j = 0; j < environmentData.SpawnCount; j++)
                    {
                        var @object = environmentData.Prefab.Instantiate(parent);
                        @object.GetComponent<SpriteRenderer>().sprite = environmentData.GressSprites[UnityEngine.Random.Range(0, environmentData.GressSprites.Length)];
                        Vector2 spawnPos = new Vector2();

                        var coordinate = avaliableCoordinates[UnityEngine.Random.Range(0, avaliableCoordinates.Count)];
                        spawnPos = new Vector2(coordinate[0] *2f, coordinate[1]*2f);
                        //spawnPos = UnityEngine.Random.insideUnitCircle * environmentData.SpawnRadius;
                        @object.transform.position = new Vector3(spawnPos.x, spawnPos.y);
                    }
                }

            }
        }
    }
}