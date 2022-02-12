using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

sealed class SpawnPlayerSystem : IEcsInitSystem
{
    // auto-injected fields.
    readonly EcsWorld world = null;
    readonly GameSceneDataView sceneData = null;
    readonly GameStaticData staticData = null;

    public void Init()
    {

        var characterView = staticData.CharactersStaticData.PlayerPelmen.Instantiate(null);

    }
}