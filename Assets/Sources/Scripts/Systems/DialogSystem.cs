using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Client {
    sealed class DialogSystem : IEcsInitSystem, IEcsRunSystem
    {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly GameStaticData staticData;
        readonly EcsFilter<DialogComponent> filter = null;

        private bool CanSpawn;
        void IEcsInitSystem.Init()
        {
        }

        void IEcsRunSystem.Run()
        {

            foreach (var i in filter)
            {
                ref var com = ref filter.Get1(i);
                com.SpandedTime += Time.deltaTime;
                if (com.SpandedTime >= com.WaitNextLineTime)
                {
                    var speakingTextStructure = com.speakingTextStructures[com.SpawnedText];
                    int speakerIndex = FindSpeakerIndex(speakingTextStructure.text);
                    if (speakerIndex != -1)
                    {

                        var setTextProviderGO = staticData.DialogStaticData.SetTextProvider.gameObject.Instantiate();
                        var setTextProvider = setTextProviderGO.GetComponent<SetTextProvider>();
                        setTextProvider.SetStruct(speakingTextStructure.text[speakerIndex], com.speakers[speakerIndex],( com.speakingTextStructures[com.SpawnedText].WaitTime - com.WaitNextLineTime) *0.6f);
                        com.WaitNextLineTime = com.speakingTextStructures[com.SpawnedText].WaitTime;
                        com.SpawnedText++;
                    }
                }

                if (com.SpawnedText == com.speakingTextStructures.Count)
                {
                    filter.GetEntity(i).Destroy();
                }
            }
        }

        private int FindSpeakerIndex(List<string> allText)
        {
            int count = 0;
            foreach (var text in allText)
            {
                if(text.Length > 0)
                {
                    return count;
                }
                count++;
            }
            return -1;
        }
    }
}