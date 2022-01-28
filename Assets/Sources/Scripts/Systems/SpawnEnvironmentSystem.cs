using Leopotam.Ecs;
using System;
using System.Collections.Generic;
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
            var terrainMap = sceneDataView.MapGenerator.GenerateMap(out int width, out int hight);
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

            SpawnWater(terrainMap, width, hight);
        }

        private void SpawnWater(Dictionary<TerrainType, List<int[]>> terrainMap, int width, int hight)
        {
            List<int[]> riversCoordinates = new List<int[]>();
            int left = int.MaxValue, right = int.MinValue, top = int.MinValue, buttom = int.MaxValue;

            foreach (var avaliableCoordinate in terrainMap[TerrainType.River])
            {
                riversCoordinates.Add(avaliableCoordinate);
                var x = avaliableCoordinate[0];
                var y = avaliableCoordinate[1];
                if(x > right)
                {
                    right = x;
                }
                if(x < left)
                {
                    left = x;
                }
                if(y > top)
                {
                    top = y;
                }
                if(y < buttom)
                {
                    buttom = y;
                }
            }
            foreach (var avaliableCoordinate in terrainMap[TerrainType.PreRiver])
            {
                riversCoordinates.Add(avaliableCoordinate);
                var x = avaliableCoordinate[0];
                var y = avaliableCoordinate[1];
                if (x > right)
                {
                    right = x;
                }
                if (x < left)
                {
                    left = x;
                }
                if (y > top)
                {
                    top = y;
                }
                if (y < buttom)
                {
                    buttom = y;
                }
            }

            Texture2D tex = new Texture2D(width, hight);

            for (int x = 0; x < tex.width; x++)
            {
                for (int y = 0; y < tex.height; y++)
                {
                    tex.SetPixel(x, y, Color.clear);
                }
            }

            foreach (var riversCoordinate in riversCoordinates)
            {

                var x = riversCoordinate[0];
                var y = riversCoordinate[1];

                tex.SetPixel(x + width/2, y + hight / 2, Color.blue);
            }
            tex.Apply();

            tex.filterMode = FilterMode.Point;
            tex.wrapMode = TextureWrapMode.Clamp;

            var riversGO = new GameObject("Rivers");
            var spriteRenderer = riversGO.AddComponent<SpriteRenderer>();
            var sprite = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f,0.5f), 0.5f);
            spriteRenderer.sprite = sprite;

        }

    }
}