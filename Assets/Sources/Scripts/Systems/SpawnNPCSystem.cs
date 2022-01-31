using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Client {
    sealed class SpawnNPCSystem : IEcsInitSystem {
        readonly EcsWorld world = null;
        readonly GameStaticData staticData = null;

        private Transform npcParent;
        public void Init()
        {
            npcParent = new GameObject("NPC").transform;
            for (int i = 0; i < 30; i++)
            {
                SpawnNPC(staticData.CharactersStaticData.Pelmen);
                SpawnNPC(staticData.CharactersStaticData.PelmenSmall);
                SpawnNPC(staticData.CharactersStaticData.Butterfly);
                SpawnNPC(staticData.CharactersStaticData.Rabbit);
                SpawnNPC(staticData.CharactersStaticData.Frog);
            }
        }
        private void SpawnNPC(CharacterStaticData characterStaticData)
        {
            var characterView = characterStaticData.Prefab.Instantiate(npcParent, characterStaticData.SpawnRadius * new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)));
            
        }

        
    }
}