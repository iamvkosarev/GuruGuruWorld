using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Client {
    sealed class SpawnNPCSystem : IEcsInitSystem {
        readonly EcsWorld world = null;
        readonly GameStaticData staticData = null;
        readonly GameSceneDataView sceneDataView = null;

        private Transform npcParent;
        public void Init()
        {
            npcParent = new GameObject("NPC").transform;
            var terrainMap = sceneDataView.MapGenerator.TryGetMap(out int width, out int hight);
            foreach (var keyValuePair in terrainMap)
            {
                var typeOfTerrain = keyValuePair.Key;
                var avaliableCoordinates = keyValuePair.Value;


                if (typeOfTerrain == TerrainType.Grass)
                {
                    for (int i = 0; i < 30; i++)
                    {
                        SpawnNPC(staticData.CharactersStaticData.Pelmen, avaliableCoordinates);
                        SpawnNPC(staticData.CharactersStaticData.PelmenSmall, avaliableCoordinates);
                        SpawnNPC(staticData.CharactersStaticData.Butterfly, avaliableCoordinates);
                        SpawnNPC(staticData.CharactersStaticData.Rabbit, avaliableCoordinates);
                        SpawnNPC(staticData.CharactersStaticData.Frog, avaliableCoordinates);
                    }
                }
            

            }
        }
        private void SpawnNPC(CharacterStaticData characterStaticData, List<int[]> avaliableCoordinates)
        {

            var coordinate = avaliableCoordinates[UnityEngine.Random.Range(0, avaliableCoordinates.Count)];
            Vector2 spawnPos = new Vector2(coordinate[0] * 2f, coordinate[1] * 2f);
            characterStaticData.Prefab.Instantiate(npcParent, new Vector3(spawnPos.x, spawnPos.y));
            
        }

        
    }
}