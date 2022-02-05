using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Client {
    sealed class GameEcsStartup : MonoBehaviour {

        [SerializeField] private GameSceneDataView sceneData;
        [SerializeField] private GameStaticData staticData;
        [SerializeField] private GameShareDataView shareData;

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
        void Start () {
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

        #region Updates And Destroy
        void Update()
        {
            updateSystem?.Run();
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
    }
}