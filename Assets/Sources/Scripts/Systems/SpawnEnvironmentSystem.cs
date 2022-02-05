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
            var terrainMap = sceneDataView.MapGenerator.TryGetMap(out int width, out int hight);
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

            SpawnWater(terrainMap, width, hight, 4f);
        }

        private void SpawnWater(Dictionary<TerrainType, List<int[]>> terrainMap, int width, int hight, float scale = 1f)
        {
            var old_width = width;
            var old_hight = hight;
            width = (int)((float)width * scale);
            hight = (int)((float)hight * scale);
            List<int[]> riversCoordinates = new List<int[]>();
            if (!terrainMap.ContainsKey(TerrainType.River)) { return; }

            foreach (var avaliableCoordinate in terrainMap[TerrainType.River])
            {
                riversCoordinates.Add(avaliableCoordinate);
            }
            foreach (var avaliableCoordinate in terrainMap[TerrainType.PreRiver])
            {
                riversCoordinates.Add(avaliableCoordinate);
            }

            Texture2D tex = new Texture2D(width, hight);
            int[,] matrix = new int[width, hight];

            for (int x = 0; x < tex.width; x++)
            {
                for (int y = 0; y < tex.height; y++)
                {
                    matrix[x, y] = 0;
                    tex.SetPixel(x, y, Color.clear);
                }
            }

            foreach (var riversCoordinate in riversCoordinates)
            {

                var x = riversCoordinate[0] + old_width /2;
                var y = riversCoordinate[1] + old_hight / 2;

                matrix[x, y] = 1;
                x = (int)((float)x * scale);
                y = (int)((float)y * scale);

                for (int i = -2; i <= 2; i++)
                {
                    for (int j = -2; j <= 2; j++)
                    {

                        tex.SetPixel(x+i, y+j, Color.blue);
                    }
                }
            }

            foreach (var riversCoordinate in riversCoordinates)
            {

                var x = riversCoordinate[0] + old_width / 2;
                var y = riversCoordinate[1] + old_hight / 2;

                
                try
                {
                    
                        if (
                  (matrix[x - 1, y + 1] == 0 && matrix[x, y + 1] == 1 && matrix[x + 1, y + 1] == 1
                && matrix[x - 1, y + 0] == 1 && matrix[x, y + 0] == 1 && matrix[x + 1, y + 0] == 1
                && matrix[x - 1, y - 1] == 1 && matrix[x, y - 1] == 1 && matrix[x + 1, y - 1] == 1)
                
                || 
                  (matrix[x - 1, y + 1] == 0 && matrix[x, y + 1] == 0 && matrix[x + 1, y + 1] == 1
                && matrix[x - 1, y + 0] == 1 && matrix[x, y + 0] == 1 && matrix[x + 1, y + 0] == 1
                && matrix[x - 1, y - 1] == 1 && matrix[x, y - 1] == 1 && matrix[x + 1, y - 1] == 1)
                ||
                  (matrix[x - 1, y + 1] == 0 && matrix[x, y + 1] ==1 && matrix[x + 1, y + 1] == 1
                && matrix[x - 1, y + 0] == 0 && matrix[x, y + 0] == 1 && matrix[x + 1, y + 0] == 1
                && matrix[x - 1, y - 1] == 1 && matrix[x, y - 1] == 1 && matrix[x + 1, y - 1] == 1)
                )
                    { 
                        x = (int)((float)(x - 1 )* scale);
                        y = (int)((float)(y + 1 )* scale);

                        for (int i = -2; i <= 2; i++)
                        {
                            for (int j = -2; j <= 2; j++)
                            {
                                if ((i + j) % 2 != 0) { continue; }
                                tex.SetPixel(x + i, y + j, Color.blue);
                            }
                        }
                    }

                    else if (
                  (matrix[x - 1, y + 1] == 1 && matrix[x, y + 1] == 1 && matrix[x + 1, y + 1] == 0
                && matrix[x - 1, y + 0] == 1 && matrix[x, y + 0] == 1 && matrix[x + 1, y + 0] == 1
                && matrix[x - 1, y - 1] == 1 && matrix[x, y - 1] == 1 && matrix[x + 1, y - 1] == 1)

                ||
                  (matrix[x - 1, y + 1] == 1 && matrix[x, y + 1] == 0 && matrix[x + 1, y + 1] == 0
                && matrix[x - 1, y + 0] == 1 && matrix[x, y + 0] == 1 && matrix[x + 1, y + 0] == 1
                && matrix[x - 1, y - 1] == 1 && matrix[x, y - 1] == 1 && matrix[x + 1, y - 1] == 1)
                ||
                  (matrix[x - 1, y + 1] == 1 && matrix[x, y + 1] == 1 && matrix[x + 1, y + 1] == 0
                && matrix[x - 1, y + 0] == 1 && matrix[x, y + 0] == 1 && matrix[x + 1, y + 0] == 0
                && matrix[x - 1, y - 1] == 1 && matrix[x, y - 1] == 1 && matrix[x + 1, y - 1] == 1)
               )
                    {
                        x = (int)((float)(x + 1) * scale);
                        y = (int)((float)(y + 1) * scale);

                        for (int i = -2; i <= 2; i++)
                        {
                            for (int j = -2; j <= 2; j++)
                            {
                                if ((i + j) % 2 != 0) { continue; }
                                tex.SetPixel(x + i, y + j, Color.blue);
                            }
                        }
                    }

                    else if (
                  (matrix[x - 1, y + 1] == 1 && matrix[x, y + 1] == 1 && matrix[x + 1, y + 1] == 1
                && matrix[x - 1, y + 0] == 1 && matrix[x, y + 0] == 1 && matrix[x + 1, y + 0] == 1
                && matrix[x - 1, y - 1] == 1 && matrix[x, y - 1] == 1 && matrix[x + 1, y - 1] == 0)

                ||
                  (matrix[x - 1, y + 1] == 1 && matrix[x, y + 1] == 1 && matrix[x + 1, y + 1] == 1
                && matrix[x - 1, y + 0] == 1 && matrix[x, y + 0] == 1 && matrix[x + 1, y + 0] == 0
                && matrix[x - 1, y - 1] == 1 && matrix[x, y - 1] == 1 && matrix[x + 1, y - 1] == 0)
                ||
                  (matrix[x - 1, y + 1] == 1 && matrix[x, y + 1] == 1 && matrix[x + 1, y + 1] == 1
                && matrix[x - 1, y + 0] == 1 && matrix[x, y + 0] == 1 && matrix[x + 1, y + 0] == 1
                && matrix[x - 1, y - 1] == 1 && matrix[x, y - 1] == 0 && matrix[x + 1, y - 1] == 0)
               )
                    {
                        x = (int)((float)(x + 1) * scale);
                        y = (int)((float)(y - 1) * scale);

                        for (int i = -2; i <= 2; i++)
                        {
                            for (int j = -2; j <= 2; j++)
                            {
                                if ((i + j) % 2 != 0) { continue; }
                                tex.SetPixel(x + i, y + j, Color.blue);
                            }
                        }
                    }
                    else if (
                  (matrix[x - 1, y + 1] == 1 && matrix[x, y + 1] == 1 && matrix[x + 1, y + 1] == 1
                && matrix[x - 1, y + 0] == 1 && matrix[x, y + 0] == 1 && matrix[x + 1, y + 0] == 1
                && matrix[x - 1, y - 1] == 0 && matrix[x, y - 1] == 1 && matrix[x + 1, y - 1] == 1)

                ||
                  (matrix[x - 1, y + 1] == 1 && matrix[x, y + 1] == 1 && matrix[x + 1, y + 1] == 1
                && matrix[x - 1, y + 0] == 0 && matrix[x, y + 0] == 1 && matrix[x + 1, y + 0] == 1
                && matrix[x - 1, y - 1] == 0 && matrix[x, y - 1] == 1 && matrix[x + 1, y - 1] == 1)
                ||
                  (matrix[x - 1, y + 1] == 1 && matrix[x, y + 1] == 1 && matrix[x + 1, y + 1] == 1
                && matrix[x - 1, y + 0] == 1 && matrix[x, y + 0] == 1 && matrix[x + 1, y + 0] == 1
                && matrix[x - 1, y - 1] == 0 && matrix[x, y - 1] == 0 && matrix[x + 1, y - 1] == 1)
               )
                    {
                        x = (int)((float)(x - 1) * scale);
                        y = (int)((float)(y - 1) * scale);

                        for (int i = -2; i <= 2; i++)
                        {
                            for (int j = -2; j <= 2; j++)
                            {
                                if ((i + j) % 2 != 0) { continue; }
                                tex.SetPixel(x + i, y + j, Color.blue);
                            }
                        }
                    }
                    else if (
              (matrix[x - 1, y + 1] == 0 && matrix[x, y + 1] == 1 && matrix[x + 1, y + 1] == 1
            && matrix[x - 1, y + 0] == 0 && matrix[x, y + 0] == 1 && matrix[x + 1, y + 0] == 1
            && matrix[x - 1, y - 1] == 0 && matrix[x, y - 1] == 1 && matrix[x + 1, y - 1] == 1)

           )
                    {
                        for (int k = -1; k <= 1; k++)
                        {
                            var x_in = (int)((float)(x - 1) * scale);
                            var y_in = (int)((float)(y + k) * scale);

                            for (int i = -2; i <= 2; i++)
                            {
                                for (int j = -2; j <= 2; j++)
                                {
                                    if ((i + j) % 2 != 0) { continue; }
                                    tex.SetPixel(x_in + i, y_in + j, Color.blue);
                                }
                            }
                        }
                    }
                    else if (
              (matrix[x - 1, y + 1] == 0 && matrix[x, y + 1] == 0 && matrix[x + 1, y + 1] == 0
            && matrix[x - 1, y + 0] == 1 && matrix[x, y + 0] == 1 && matrix[x + 1, y + 0] == 1
            && matrix[x - 1, y - 1] == 1 && matrix[x, y - 1] == 1 && matrix[x + 1, y - 1] == 1)

           )
                    {
                        for (int k = -1; k <= 1; k++)
                        {
                            var x_in = (int)((float)(x + k) * scale);
                            var y_in = (int)((float)(y + 1) * scale);

                            for (int i = -2; i <= 2; i++)
                            {
                                for (int j = -2; j <= 2; j++)
                                {
                                    if ((i + j) % 2 != 0) { continue; }
                                    tex.SetPixel(x_in + i, y_in + j, Color.blue);
                                }
                            }
                        }
                    }
                    else if (
          (matrix[x - 1, y + 1] == 1 && matrix[x, y + 1] == 1 && matrix[x + 1, y + 1] == 0
        && matrix[x - 1, y + 0] == 1 && matrix[x, y + 0] == 1 && matrix[x + 1, y + 0] == 0
        && matrix[x - 1, y - 1] == 1 && matrix[x, y - 1] == 1 && matrix[x + 1, y - 1] == 0)

       )
                    {
                        for (int k = -1; k <= 1; k++)
                        {
                            var x_in = (int)((float)(x + 1) * scale);
                            var y_in = (int)((float)(y + k) * scale);

                            for (int i = -2; i <= 2; i++)
                            {
                                for (int j = -2; j <= 2; j++)
                                {
                                    if ((i + j) % 2 != 0) { continue; }
                                    tex.SetPixel(x_in + i, y_in + j, Color.blue);
                                }
                            }
                        }
                    }
                    else if (
      (matrix[x - 1, y + 1] == 1 && matrix[x, y + 1] == 1 && matrix[x + 1, y + 1] == 1
    && matrix[x - 1, y + 0] == 1 && matrix[x, y + 0] == 1 && matrix[x + 1, y + 0] == 1
    && matrix[x - 1, y - 1] == 0 && matrix[x, y - 1] == 0 && matrix[x + 1, y - 1] == 0)

   )
                    {
                        for (int k = -1; k <= 1; k++)
                        {
                            var x_in = (int)((float)(x + k) * scale);
                            var y_in = (int)((float)(y - 1) * scale);

                            for (int i = -2; i <= 2; i++)
                            {
                                for (int j = -2; j <= 2; j++)
                                {
                                    if ((i + j) % 2 != 0) { continue; }
                                    tex.SetPixel(x_in + i, y_in + j, Color.blue);
                                }
                            }
                        }

                    }
                }
                catch(Exception e){

                }
            }
            tex.Apply();

            tex.filterMode = FilterMode.Point;
            tex.wrapMode = TextureWrapMode.Clamp;

            var riversGO = new GameObject("Rivers");
            var spriteRenderer = riversGO.AddComponent<SpriteRenderer>();
            var sprite = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f,0.5f), 0.5f);
            spriteRenderer.sprite = sprite;
            riversGO.AddComponent<PolygonCollider2D>();
            riversGO.transform.localScale /= scale;

        }

    }
}