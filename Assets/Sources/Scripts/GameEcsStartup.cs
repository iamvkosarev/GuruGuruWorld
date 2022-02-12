using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

[Serializable]
public class SystemData
{
    [NonSerialized]public int Index;
    [NonSerialized]public bool WasWorking;
    public bool IsWorking = true;
    public string Name;
}

[Serializable]
public class SystemsData
{
    public List<SystemData> SystemDatas;
    public EcsSystems EcsSystems;
}

namespace Client {
    sealed class GameEcsStartup : MonoBehaviour {

        [SerializeField] private GameSceneDataView sceneData;
        [SerializeField] private GameStaticData staticData;
        [SerializeField] private GameShareDataView shareData;
        public List<SystemsData> systemsDatas = new List<SystemsData>();

        private static GameStaticData _staticData;
        private static GameShareDataView _shareData;
        EcsWorld world;
        EcsSystems updateSystem;
        EcsSystems fixedUpdateSystem;
        EcsSystems lateUpdateSystem;

        #region Static Methods
        public static Color GetPelmenRandomSkinColor()
        {
            return _staticData.PelmenStaticData.colors[UnityEngine.Random.Range(0, _staticData.PelmenStaticData.colors.Length)];
        }
        public static int GetPelmenID()
        {
            int result = _shareData.PelemnCount;
            _shareData.PelemnCount++;
            return result;
        }
        
        #endregion
        void Start ()
        {
            world = new EcsWorld();
            updateSystem = new EcsSystems(world);
            fixedUpdateSystem = new EcsSystems(world);
            lateUpdateSystem = new EcsSystems(world);


            #region Editor
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(updateSystem);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(fixedUpdateSystem);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(lateUpdateSystem);
#endif
            #endregion

            _staticData = staticData;
            _shareData = shareData;
            updateSystem
                .Add(new SpawnEnvironmentSystem())
                .Add(new SpawnPlayerSystem())
                .Add(new SpawnNPCSystem())
                .ConvertScene()
                .Add(new IceCreamSystem())
                .Add(new ShowHelpImageSystem())
                .Add(new SetCameraSystem())
                .Add(new NPCInputSystem())
                .Add(new SetHatSystem())
                .Add(new PlayerInputSystem())
                .Add(new TransportUserSystem())
                .Add(new SkateSystem())
                .Add(new TextSystem())
                .Add(new DialogSystem())
                .Add(new SetColorSystem())
                .Add(new DuplicateMovingSystem())
                .Add(new WalkerToTargetSystem())
                .Add(new DamageSystem())
                .Add(new DeathSystem())
                .Add(new TextMassageSystem())
                //.Add(new RainSystem())
                .Add(new HungrySystem())
                .Add(new PelmenFaceSystem())
                .Add(new AudioSystem())
                .Add(new PlayerAnimationSystem());

            fixedUpdateSystem
                .Add(new MovingSystem());
            lateUpdateSystem
                .Add(new PositionRendererSorterSystem())
                .Add(new RotatingSystem());


            AddSystemsDatas(updateSystem);
            AddSystemsDatas(fixedUpdateSystem);
            AddSystemsDatas(lateUpdateSystem);

            #region Inject and Init Systems

            updateSystem.Inject(sceneData);
            fixedUpdateSystem.Inject(sceneData);
            lateUpdateSystem.Inject(sceneData);

            updateSystem.Inject(shareData);
            fixedUpdateSystem.Inject(shareData);
            lateUpdateSystem.Inject(shareData);

            updateSystem.Inject(staticData);
            fixedUpdateSystem.Inject(staticData);
            lateUpdateSystem.Inject(staticData);

            updateSystem.Init();
            fixedUpdateSystem.Init();
            lateUpdateSystem.Init();

            #endregion
        }

        private void AddSystemsDatas(EcsSystems ecsSystems)
        {
            SystemsData systemsData = new SystemsData();
            systemsData.SystemDatas = new List<SystemData>();
            systemsData.EcsSystems = ecsSystems;
            var _runSystems = ecsSystems.GetRunSystems();
            for (int i = 0, iMax = _runSystems.Count; i < iMax; i++)
            {
                var runItem = _runSystems.Items[i];

                var systemData = new SystemData();
                systemData.Index = i;
                systemData.Name = runItem.System.GetType().ToString();
                systemData.IsWorking = runItem.Active;

                systemsData.SystemDatas.Add(systemData);
            }
            systemsDatas.Add(systemsData);
        }

        #region Updates And Destroy
        void Update()
        {
            updateSystem?.Run();
            foreach (var systemData in systemsDatas)
            {
                for (int i = 0; i < systemData.SystemDatas.Count; i++)
                {
                    var systemUpdateData = systemData.SystemDatas[i];
                    if (systemUpdateData.WasWorking != systemUpdateData.IsWorking)
                    {
                        Debug.Log($"Change {systemData.EcsSystems.GetRunSystems().Items[systemUpdateData.Index].System.GetType().ToString()} to {systemUpdateData.IsWorking}");
                        systemData.EcsSystems.SetRunSystemState(systemUpdateData.Index, systemUpdateData.IsWorking);
                    }
                    systemUpdateData.WasWorking = systemUpdateData.IsWorking;
                }

            }
        }

        private void FixedUpdate()
        {
            fixedUpdateSystem.Run();
        }

        private void LateUpdate()
        {
            lateUpdateSystem.Run();

        }

        void OnDestroy()
        {
            if (updateSystem != null)
            {
                updateSystem.Destroy();
                updateSystem = null;
            }
            if (fixedUpdateSystem != null)
            {
                fixedUpdateSystem.Destroy();
                fixedUpdateSystem = null;
            }
            if (lateUpdateSystem != null)
            {
                lateUpdateSystem.Destroy();
                lateUpdateSystem = null;
            }
            world.Destroy();
            world = null;
        }
        #endregion


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(sceneData.MainCamera.transform.position, staticData.OptimizationStaticData.CheckPlayerZoneSize);
        }
    }

    
}